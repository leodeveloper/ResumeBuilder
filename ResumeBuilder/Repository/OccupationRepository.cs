using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeBuilder.Dto;

namespace ResumeBuilder.Repository
{
    public class OccupationRepository : IOccupationRepository
    {
        readonly ILogger<OccupationRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Occupation> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;

        public OccupationRepository(IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<OccupationRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Occupation> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<bool> Insertoccupation(Occupation occupation)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Occupation>(occupation);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(OccupationRepository)}::{nameof(Insertoccupation)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Updateoccupation(Occupation occupation)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Occupation>(occupation);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(OccupationRepository)}::{nameof(Insertoccupation)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Deleteoccupation(long rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Occupation set IsDeleted='true', DeletedDate='{DateTime.Now}' where RID={rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(OccupationRepository)}::{nameof(Deleteoccupation)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Occupation>> GetoccupationById(long rid)
        {
            Webresponse<Occupation> occupation = new Webresponse<Occupation>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Occupation>(rid);
                if (result == null)
                {
                    occupation.message = "No Record found";
                }
                else
                {
                    occupation.data = result;
                }
                occupation.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(OccupationRepository)}::{nameof(GetoccupationById)} -- " + ex.Message);
                occupation.message = ex.Message;
                occupation.status = APIStatus.error;
            }
            return occupation;
        }

        public async Task<Webresponse<IList<Occupation>>> GetoccupationByResumeId(long resumeId)
        {
            Webresponse<IList<Occupation>> occupation = new Webresponse<IList<Occupation>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Occupation>($"select * from Occupation where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    occupation.message = "No Record found";
                }
                else
                {
                    occupation.data = result.ToList();
                }
                occupation.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetoccupationByResumeId)} -- " + ex.Message);
                occupation.message = ex.Message;
                occupation.status = APIStatus.error;
            }
            return occupation;
        }

        public async Task<Webresponse<IList<OccupationDto>>> GetoccupationByResumeIdPreview(long resumeId)
        {
            Webresponse<IList<OccupationDto>> occupationDto = new Webresponse<IList<OccupationDto>> { data = new List<OccupationDto>() };
            try
            {
                occupationDto.status = APIStatus.success;
                var occupations = await GetoccupationByResumeId(resumeId);
                foreach (Occupation occupation in occupations?.data)
                {
                    try
                    {
                        OccupationDto _occupationDto = new OccupationDto();
                        _occupationDto.OccupationId = occupation.OccupationId;
                        _occupationDto.SkillGroupId = occupation.SkillGroupId;

                        List<Task> tasks = new List<Task>();
                        tasks.Add(_ilookupRespository.GetHttpClient<IList<SkillGroup>>($"{_appSettingsAPIUrls.Value.GetSkillGroupsApiUrl}/{occupation.SkillGroupId}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<IList<SkillGroupOccupation>>($"{_appSettingsAPIUrls.Value.GetOccupationsUrl}/{occupation.OccupationId}"));
                       

                        await Task.WhenAll(tasks.ToArray());

                        var skills = ((Task<IList<SkillGroup>>)tasks[0]).Result;
                        var skillsoccupations = ((Task<IList<SkillGroupOccupation>>)tasks[1]).Result;


                        _occupationDto.SkillGroup = skills?.FirstOrDefault()?.EnTitle;
                        _occupationDto.SkillGroupAr = skills?.FirstOrDefault()?.ArTitle;
                        _occupationDto.Occupation = skillsoccupations?.FirstOrDefault()?.EnTitle;
                        _occupationDto.OccupationAr = skillsoccupations?.FirstOrDefault()?.ArTitle;
                      
                       
                        occupationDto.data.Add(_occupationDto);
                    }
                    catch (Exception ex)
                    {
                        _ilogger.LogError(ex, "lookupController:::GetOccupationGroup lookup GetOccupationGroup api is down");
                    }
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(OccupationRepository)}::{nameof(GetoccupationByResumeId)} -- " + ex.Message);
                occupationDto.message = ex.Message;
                occupationDto.status = APIStatus.error;
            }
            return occupationDto;
        }

        public async Task<WebresponsePaging<IList<Occupation>>> GetAlloccupation(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Occupation>> occupation = new WebresponsePaging<IList<Occupation>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Occupation).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    occupation.message = "No Record found";
                }
                else
                {
                    occupation = result;
                }
                occupation.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(OccupationRepository)}::{nameof(GetAlloccupation)} -- " + ex.Message);
                occupation.message = ex.Message;
                occupation.status = APIStatus.error;
            }
            return occupation;
        }
    }
}
