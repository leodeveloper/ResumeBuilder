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
    public class JobSeekerGrieveanceRepository : IJobSeekerGrieveanceRepository
    {
        readonly ILogger<JobSeekerGrieveanceRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<JobSeekerGrieveance> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public JobSeekerGrieveanceRepository(IOptions<AppSettings> appSettings, ILogger<JobSeekerGrieveanceRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<JobSeekerGrieveance> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> InsertjobSeekerGrieveance(JobSeekerGrieveance jobSeekerGrieveance)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<JobSeekerGrieveance>(jobSeekerGrieveance);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(JobSeekerGrieveanceRepository)}::{nameof(InsertjobSeekerGrieveance)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdatejobSeekerGrieveance(JobSeekerGrieveance jobSeekerGrieveance)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<JobSeekerGrieveance>(jobSeekerGrieveance);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(JobSeekerGrieveanceRepository)}::{nameof(InsertjobSeekerGrieveance)} -- " + ex.Message);
                return false;
            }

        }

        //public async Task<bool> DeletejobSeekerGrieveance(long Rid)
        //{
        //    try
        //    {
        //        using (var con = _iUnitOfWork.Connection)
        //        {
        //            var result = await con.QueryAsync($"update JobSeekerGrieveance set Is_Deleted='true' where RID={Rid}");
        //            return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _ilogger.LogError(ex, $"{nameof(JobSeekerGrieveanceRepository)}::{nameof(DeletejobSeekerGrieveance)} -- " + ex.Message);
        //        return false;
        //    }
        //}

        public async Task<Webresponse<JobSeekerGrieveance>> GetjobSeekerGrieveanceById(long rid)
        {
            Webresponse<JobSeekerGrieveance> jobSeekerGrieveance = new Webresponse<JobSeekerGrieveance>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<JobSeekerGrieveance>(rid);
                if (result == null)
                {
                    jobSeekerGrieveance.message = "No Record found";
                }
                else
                {
                    jobSeekerGrieveance.data = result;
                }
                jobSeekerGrieveance.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(JobSeekerGrieveanceRepository)}::{nameof(GetjobSeekerGrieveanceById)} -- " + ex.Message);
                jobSeekerGrieveance.message = ex.Message;
                jobSeekerGrieveance.status = APIStatus.error;
            }
            return jobSeekerGrieveance;
        }

        public async Task<Webresponse<IList<JobSeekerGrieveance>>> GetjobSeekerGrieveanceByResumeId(long resumeId)
        {
            Webresponse<IList<JobSeekerGrieveance>> jobSeekerGrieveance = new Webresponse<IList<JobSeekerGrieveance>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<JobSeekerGrieveance>($"select * from JobSeekerGrieveance where resumeid={resumeId} and Is_Deleted='false'");
                if (result == null)
                {
                    jobSeekerGrieveance.message = "No Record found";
                }
                else
                {
                    jobSeekerGrieveance.data = result.ToList();
                }
                jobSeekerGrieveance.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetjobSeekerGrieveanceByResumeId)} -- " + ex.Message);
                jobSeekerGrieveance.message = ex.Message;
                jobSeekerGrieveance.status = APIStatus.error;
            }
            return jobSeekerGrieveance;
        }

        public async Task<WebresponsePaging<IList<JobSeekerGrieveance>>> GetAlljobSeekerGrieveance(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<JobSeekerGrieveance>> jobSeekerGrieveance = new WebresponsePaging<IList<JobSeekerGrieveance>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(JobSeekerGrieveance).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    jobSeekerGrieveance.message = "No Record found";
                }
                else
                {
                    jobSeekerGrieveance = result;
                }
                jobSeekerGrieveance.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(JobSeekerGrieveanceRepository)}::{nameof(GetAlljobSeekerGrieveance)} -- " + ex.Message);
                jobSeekerGrieveance.message = ex.Message;
                jobSeekerGrieveance.status = APIStatus.error;
            }
            return jobSeekerGrieveance;
        }
    }
}
