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
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{

    public class PeopleofDeterminationRepository : IPeopleofDeterminationRepository
    {
        readonly ILogger<PeopleofDeterminationRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<JobSeekerPod> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;

        public PeopleofDeterminationRepository(IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<PeopleofDeterminationRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<JobSeekerPod> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<bool> InsertDeletejobSeekerPod(JobSeekerPodViewModel jobSeekerPodVM)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync<JobSeekerPod>($"select * from JobSeekerPod where Resume_ID={jobSeekerPodVM.Resume_ID}");
                    if (result.Any())
                    {
                        var InserttoolKnowledge = result.Where(z => jobSeekerPodVM.SpeicalNeeds_ID.Contains(z.SpeicalNeeds_ID)); //jobSeekerPodVM.SpeicalNeeds_ID.Where(z => result.Select(i => i.SpeicalNeeds_ID).Contains(z));
                        foreach (JobSeekerPod jobSeekerPod in InserttoolKnowledge)
                        {
                           // int i = await con.InsertAsync<JobSeekerPod>(new JobSeekerPod { Resume_ID = jobSeekerPodVM.Resume_ID, SpeicalNeeds_ID = jobSeekerPod });
                            JobSeekerPod _jobSeekerPodInsert = await con.QuerySingleAsync<JobSeekerPod>($"select * from JobSeekerPod where RId={jobSeekerPod.Rid}");
                            _jobSeekerPodInsert.Is_Deleted = false;
                            await con.UpdateAsync<JobSeekerPod>(_jobSeekerPodInsert);
                        }

                        var deletetoolKnowledge = result.Where(z => !jobSeekerPodVM.SpeicalNeeds_ID.Contains(z.SpeicalNeeds_ID));
                        foreach (JobSeekerPod jobSeekerPod in deletetoolKnowledge)
                        {
                            JobSeekerPod _jobSeekerPodDelete = await con.QuerySingleAsync<JobSeekerPod>($"select * from JobSeekerPod where RId={jobSeekerPod.Rid}");
                            _jobSeekerPodDelete.Is_Deleted = true;
                            await con.UpdateAsync<JobSeekerPod>(_jobSeekerPodDelete);
                            //bool i = await con.DeleteAsync<JobSeekerPod>(jobSeekerPod);
                        }
                    }
                    else
                    {
                        foreach (int jobSeekerPod in jobSeekerPodVM.SpeicalNeeds_ID)
                        {
                            int i = await con.InsertAsync<JobSeekerPod>(new JobSeekerPod { Resume_ID = jobSeekerPodVM.Resume_ID, SpeicalNeeds_ID = jobSeekerPod });
                        }

                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(InsertDeletejobSeekerPod)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> InsertjobSeekerPod(JobSeekerPod jobSeekerPod)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<JobSeekerPod>(jobSeekerPod);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(InsertjobSeekerPod)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdatejobSeekerPod(JobSeekerPod jobSeekerPod)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<JobSeekerPod>(jobSeekerPod);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(UpdatejobSeekerPod)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> DeletejobSeekerPod(long rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update JobSeekerPod set Is_Deleted='true' where RID={rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(DeletejobSeekerPod)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<JobSeekerPod>> GetjobSeekerPodById(long rid)
        {
            Webresponse<JobSeekerPod> jobSeekerPod = new Webresponse<JobSeekerPod>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<JobSeekerPod>(rid);
                if (result == null)
                {
                    jobSeekerPod.message = "No Record found";
                }
                else
                {
                    jobSeekerPod.data = result;
                }
                jobSeekerPod.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(GetjobSeekerPodById)} -- " + ex.Message);
                jobSeekerPod.message = ex.Message;
                jobSeekerPod.status = APIStatus.error;
            }
            return jobSeekerPod;
        }

        public async Task<Webresponse<IList<JobSeekerPod>>> GetjobSeekerPodByResumeId(long resumeId)
        {
            Webresponse<IList<JobSeekerPod>> jobSeekerPod = new Webresponse<IList<JobSeekerPod>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<JobSeekerPod>($"select * from JobSeekerPod where Resume_ID={resumeId} and Is_Deleted='false'");
                if (result == null)
                {
                    jobSeekerPod.message = "No Record found";
                }
                else
                {
                    jobSeekerPod.data = result.ToList();
                }
                jobSeekerPod.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetjobSeekerPodByResumeId)} -- " + ex.Message);
                jobSeekerPod.message = ex.Message;
                jobSeekerPod.status = APIStatus.error;
            }
            return jobSeekerPod;
        }

        public async Task<Webresponse<IList<SpecialNeedDto>>> GetJobSeekerPodByResumeIdPreview(long resumeId)
        {
            Webresponse<IList<SpecialNeedDto>> specialNeedDto = new Webresponse<IList<SpecialNeedDto>> { data = new List<SpecialNeedDto>() };
            try
            {
                var jobSeekerPods = await GetjobSeekerPodByResumeId(resumeId);
                int[] jobSeekerPodIds = jobSeekerPods.data.Select(z => z.SpeicalNeeds_ID).ToArray();
                SpecialNeedDto _specialNeedDto = new SpecialNeedDto();
                IList<SpecialNeedLookup> jobSeekerPodLookups = await _ilookupRespository.GetHttpClient<IList<SpecialNeedLookup>>($"{_appSettingsAPIUrls.Value.SpecialNeedApiUrl}");
                jobSeekerPodLookups = jobSeekerPodLookups.Where(z => jobSeekerPodIds.Contains(z.Id)).ToList();
                specialNeedDto.data = jobSeekerPodLookups.Select(i => new SpecialNeedDto { Id = i.Id, ArTitle = i.ArTitle, EnTitle = i.EnTitle }).ToList();
                specialNeedDto.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(GetjobSeekerPodByResumeId)} -- " + ex.Message);
                specialNeedDto.message = ex.Message;
                specialNeedDto.status = APIStatus.error;
            }
            return specialNeedDto;
        }

        public async Task<WebresponsePaging<IList<JobSeekerPod>>> GetAlljobSeekerPod(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<JobSeekerPod>> jobSeekerPod = new WebresponsePaging<IList<JobSeekerPod>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(JobSeekerPod).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    jobSeekerPod.message = "No Record found";
                }
                else
                {
                    jobSeekerPod = result;
                }
                jobSeekerPod.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PeopleofDeterminationRepository)}::{nameof(GetAlljobSeekerPod)} -- " + ex.Message);
                jobSeekerPod.message = ex.Message;
                jobSeekerPod.status = APIStatus.error;
            }
            return jobSeekerPod;
        }
    }
}
