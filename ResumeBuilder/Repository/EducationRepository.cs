using Dapper.Contrib.Extensions;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeBuilder.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;

namespace ResumeBuilder.Repository
{
    public class EducationRepository : IEducationRepository
    {
        readonly ILogger<EducationRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Education> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;

        public EducationRepository(IlookupRespository ilookupRespository, IHttpClientFactory httpClientFactory, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<EducationRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Education> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<bool> SetOtherIsHigestQualificationFalse(long rid, long resume_id)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Education set IsThisHigestQualication='false' where RID!={rid} and Resume_ID={resume_id}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(SetOtherIsHigestQualificationFalse)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Inserteducation(Education education)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Education>(education);
                if (education.IsThisHigestQualication == true)
                    await SetOtherIsHigestQualificationFalse(i, education.Resume_ID);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(Inserteducation)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Updateeducation(Education education)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    bool response = await con.UpdateAsync<Education>(education);
                    if (education.IsThisHigestQualication == true)
                        await SetOtherIsHigestQualificationFalse(education.Rid, education.Resume_ID);
                    return response;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(Updateeducation)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> DeleteEducation(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Education set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(DeleteEducation)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Education>> GeteducationById(long rid)
        {
            Webresponse<Education> education = new Webresponse<Education>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Education>(rid);
                if (result == null)
                {
                    education.message = "No Record found";
                }
                else
                {
                     education.data = result;
                }
                education.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GeteducationById)} -- " + ex.Message);
                education.message = ex.Message;
                education.status = APIStatus.error;
            }
            return education;
        }

        public async Task<Webresponse<IList<Education>>> GeteducationByResumeId(long resumeId)
        {
            Webresponse<IList<Education>> education = new Webresponse<IList<Education>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Education>($"select * from Education where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    education.message = "No Record found";
                }
                else
                {
                    education.data = result.ToList();
                }
                education.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GeteducationByResumeId)} -- " + ex.Message);
                education.message = ex.Message;
                education.status = APIStatus.error;
            }
            return education;
        }

        public async Task<Webresponse<IList<EducationDto>>> GeteducationByResumeIdPreview(long resumeId)
        {
            Webresponse<IList<EducationDto>> educationDto = new Webresponse<IList<EducationDto>> { data = new List<EducationDto>() };
            try
            {
                educationDto.status = APIStatus.success;
                var educations = await GeteducationByResumeId(resumeId);                
                foreach (Education education in educations?.data)
                {
                    try
                    {
                        EducationDto _educationDto = new EducationDto();
                        _educationDto.Education = education;

                        List<Task> tasks = new List<Task>();
                        tasks.Add(_ilookupRespository.GetHttpClient<Group>($"{_appSettingsAPIUrls.Value.EducationGroupApiUrl}/{education.Education_Group_Id}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<EducationType>($"{_appSettingsAPIUrls.Value.EducationTypeApiUrl}/GetType/{education.Education_Type_Id}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<Major>($"{_appSettingsAPIUrls.Value.EducationMajorApiUrl}/GetMajor/{education.Education_Major_Id}"));
                        
                        tasks.Add(_ilookupRespository.GetHttpClient<UniversityType>($"{_appSettingsAPIUrls.Value.UniversityTypeApiUrl}/{education.University_Type_Id}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<University>($"{_appSettingsAPIUrls.Value.UniversityApiUrl}/{education.University_Id}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<Emirates>($"{_appSettingsAPIUrls.Value.EmiratesApiUrl}/GetEmiratesBy?Id={education.Emirate_Id}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<Course>($"{_appSettingsAPIUrls.Value.CourseApiUrl}/{education.Course_Id}"));

                        await Task.WhenAll(tasks.ToArray());

                        var group = ((Task<Group>)tasks[0]).Result;
                        var educationtype = ((Task<EducationType>)tasks[1]).Result;
                        var major = ((Task<Major>)tasks[2]).Result;
                        var universityType = ((Task<UniversityType>)tasks[3]).Result;
                        var university = ((Task<University>)tasks[4]).Result;
                        var emirates = ((Task<Emirates>)tasks[5]).Result;
                        var course = ((Task<Course>)tasks[6]).Result;

                        _educationDto.ThisIsHighestEducation = education.IsThisHigestQualication;
                        _educationDto.Education_Group = group?.EnName;
                        _educationDto.Education_GroupAr = group?.ArName;
                        _educationDto.Education_Type = educationtype?.EnName;
                        _educationDto.Education_TypeAr = educationtype?.ArName;
                        _educationDto.Education_Major = major?.EnName;
                        _educationDto.Education_MajorAr = major?.ArName;
                        _educationDto.University_Type = universityType?.EnTitle;
                        _educationDto.University_TypeAr = universityType?.ArTitle;
                        _educationDto.University = university?.EnTitle;
                        _educationDto.UniversityAr = university?.ArTitle;
                        _educationDto.Education_Emirates = emirates?.EnTitle;
                        _educationDto.Education_EmiratesAr = emirates?.ArTitle;
                        _educationDto.Course = course?.EnTitle;
                        _educationDto.CourseAr = course?.ArTitle;
                        educationDto.data.Add(_educationDto);
                    }
                    catch (Exception ex)
                    {
                        _ilogger.LogError(ex, "lookupController:::GetEducationGroup lookup GetEducationGroup api is down");
                    }
                }                
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GeteducationByResumeId)} -- " + ex.Message);
                educationDto.message = ex.Message;
                educationDto.status = APIStatus.error;
            }
            return educationDto;
        }

        public async Task<WebresponsePaging<IList<Education>>> GetAlleducation(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Education>> education = new WebresponsePaging<IList<Education>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Education).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    education.message = "No Record found";
                }
                else
                {
                    education = result;
                }
                education.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetAlleducation)} -- " + ex.Message);
                education.message = ex.Message;
                education.status = APIStatus.error;
            }
            return education;
        }     
    }
}
