using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public class EmployerRepository : IEmployerRepository
    {
        readonly ILogger<EmployerRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Employer> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;

        public EmployerRepository(IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<EmployerRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Employer> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<bool> SetOtherThisIsMycurrentRolFalse(long rid, long resume_id)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Employer set IsThisMyCurrentRole='false' where RID!={rid} and Resume_ID={resume_id}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(SetOtherThisIsMycurrentRolFalse)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Insertemployer(Employer employer)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Employer>(employer);
                if (employer.IsThisMyCurrentRole == true)
                    await SetOtherThisIsMycurrentRolFalse(i, employer.Resume_ID);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(Insertemployer)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Updateemployer(Employer employer)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    bool respose = await con.UpdateAsync<Employer>(employer);
                    if (employer.IsThisMyCurrentRole == true)
                        await SetOtherThisIsMycurrentRolFalse(employer.Rid, employer.Resume_ID);
                    return respose;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(Insertemployer)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Deleteemployer(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Employer set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(Deleteemployer)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Employer>> GetemployerById(long rid)
        {
            Webresponse<Employer> employer = new Webresponse<Employer>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Employer>(rid);
                if (result == null)
                {
                    employer.message = "No Record found";
                }
                else
                {
                    employer.data = result;
                }
                employer.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(GetemployerById)} -- " + ex.Message);
                employer.message = ex.Message;
                employer.status = APIStatus.error;
            }
            return employer;
        }

        public async Task<Webresponse<IList<Employer>>> GetemployerByResumeId(long resumeId)
        {
            Webresponse<IList<Employer>> employer = new Webresponse<IList<Employer>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Employer>($"select * from Employer where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    employer.message = "No Record found";
                }
                else
                {
                    employer.data = result.ToList();
                }
                employer.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetemployerByResumeId)} -- " + ex.Message);
                employer.message = ex.Message;
                employer.status = APIStatus.error;
            }
            return employer;
        }

        public async Task<Webresponse<IList<EmployerDto>>> GetemployerByResumeIdPreview(long resumeId)
        {
            Webresponse<IList<EmployerDto>> employerDto = new Webresponse<IList<EmployerDto>> { data = new List<EmployerDto>() };
            try
            {
                employerDto.status = APIStatus.success;
                var employers = await GetemployerByResumeId(resumeId);
                foreach (Employer employer in employers?.data)
                {
                    try
                    {
                        EmployerDto _employerDto = new EmployerDto();
                        _employerDto.Employer = employer;

                        List<Task> tasks = new List<Task>();
                        tasks.Add(_ilookupRespository.GetHttpClient<Particular>($"{_appSettingsAPIUrls.Value.ParticularApiUrl}/{employer.Particular_ID}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<EmployerName>($"{_appSettingsAPIUrls.Value.EmployerApiUrl}/{ employer.Employer_ID}"));
                        tasks.Add(_ilookupRespository.GetHttpClient<Designation>($"{_appSettingsAPIUrls.Value.DesignationApiUrl}/GetDesignationBy?Id={employer.Desigination_ID}"));
                        await Task.WhenAll(tasks.ToArray());
                        var particular = ((Task<Particular>)tasks[0]).Result;
                        var employerName = ((Task<EmployerName>)tasks[1]).Result;
                        var designation = ((Task<Designation>)tasks[2]).Result;
                        
                        _employerDto.Particular = particular?.EnTitle;
                        _employerDto.ParticularAr = particular?.ArTitle;
                        _employerDto.EmployerName = employerName?.EnTitle;
                        _employerDto.EmployerNameAr = employerName?.ArTitle;
                        _employerDto.Designation = designation?.EnTitle;
                        _employerDto.DesignationAr = designation?.ArTitle;
                        _employerDto.IsThisMyCurrentRole = employer.IsThisMyCurrentRole;
                        employerDto.data.Add(_employerDto);
                    }
                    catch (Exception ex)
                    {
                        _ilogger.LogError(ex, "lookupController:::GetEmployerGroup lookup GetEmployerGroup api is down");
                    }
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(GetemployerByResumeId)} -- " + ex.Message);
                employerDto.message = ex.Message;
                employerDto.status = APIStatus.error;
            }
            return employerDto;
        }

        public async Task<WebresponsePaging<IList<Employer>>> GetAllemployer(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Employer>> employer = new WebresponsePaging<IList<Employer>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Employer).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    employer.message = "No Record found";
                }
                else
                {
                    employer = result;
                }
                employer.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EmployerRepository)}::{nameof(GetAllemployer)} -- " + ex.Message);
                employer.message = ex.Message;
                employer.status = APIStatus.error;
            }
            return employer;
        }
    }
}
