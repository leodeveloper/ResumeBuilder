using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public class LanguagesRepository : ILanguagesRepository
    {
        readonly ILogger<LanguagesRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<JobSeekerLanguages> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public LanguagesRepository(IOptions<AppSettings> appSettings, ILogger<LanguagesRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<JobSeekerLanguages> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> InsertjobSeekerLanguage(JobSeekerLanguages jobSeekerLanguage)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<JobSeekerLanguages>(jobSeekerLanguage);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(LanguagesRepository)}::{nameof(InsertjobSeekerLanguage)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdatejobSeekerLanguage(JobSeekerLanguages jobSeekerLanguage)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<JobSeekerLanguages>(jobSeekerLanguage);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(LanguagesRepository)}::{nameof(InsertjobSeekerLanguage)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> DeletejobSeekerLanguage(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update JobSeekerLanguages set Is_Deleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(LanguagesRepository)}::{nameof(DeletejobSeekerLanguage)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<JobSeekerLanguages>> GetjobSeekerLanguageById(long rid)
        {
            Webresponse<JobSeekerLanguages> jobSeekerLanguage = new Webresponse<JobSeekerLanguages>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<JobSeekerLanguages>(rid);
                if (result == null)
                {
                    jobSeekerLanguage.message = "No Record found";
                }
                else
                {
                    jobSeekerLanguage.data = result;
                }
                jobSeekerLanguage.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(LanguagesRepository)}::{nameof(GetjobSeekerLanguageById)} -- " + ex.Message);
                jobSeekerLanguage.message = ex.Message;
                jobSeekerLanguage.status = APIStatus.error;
            }
            return jobSeekerLanguage;
        }

        public async Task<Webresponse<IList<JobSeekerLanguages>>> GetjobSeekerLanguageByResumeId(long resumeId)
        {
            Webresponse<IList<JobSeekerLanguages>> jobSeekerLanguage = new Webresponse<IList<JobSeekerLanguages>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<JobSeekerLanguages>($"select * from JobSeekerLanguages where Resume_ID={resumeId} and Is_Deleted='false'");
                if (result == null)
                {
                    jobSeekerLanguage.message = "No Record found";
                }
                else
                {
                    jobSeekerLanguage.data = result.ToList();
                }
                jobSeekerLanguage.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetjobSeekerLanguageByResumeId)} -- " + ex.Message);
                jobSeekerLanguage.message = ex.Message;
                jobSeekerLanguage.status = APIStatus.error;
            }
            return jobSeekerLanguage;
        }

        public async Task<WebresponsePaging<IList<JobSeekerLanguages>>> GetAlljobSeekerLanguage(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<JobSeekerLanguages>> jobSeekerLanguage = new WebresponsePaging<IList<JobSeekerLanguages>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(JobSeekerLanguages).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    jobSeekerLanguage.message = "No Record found";
                }
                else
                {
                    jobSeekerLanguage = result;
                }
                jobSeekerLanguage.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(LanguagesRepository)}::{nameof(GetAlljobSeekerLanguage)} -- " + ex.Message);
                jobSeekerLanguage.message = ex.Message;
                jobSeekerLanguage.status = APIStatus.error;
            }
            return jobSeekerLanguage;
        }
    }
}
