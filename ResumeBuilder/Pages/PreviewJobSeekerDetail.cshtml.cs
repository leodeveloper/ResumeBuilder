using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;

namespace ResumeBuilder.Pages
{
    public class PreviewJobSeekerDetailModel : PageModel
    {

        private readonly ILogger<PreviewJobSeekerDetailModel> _logger;
        private readonly IOptions<OracleAppSettings> _orcaleAppSettings;
        private readonly IResumeRepository _iResumeRepository;
        public PreviewJobSeekerDetailModel(ILogger<PreviewJobSeekerDetailModel> logger, IResumeRepository iResumeRepository, IOptions<OracleAppSettings> orcaleAppSettings)
        {
            _logger = logger;
            _orcaleAppSettings = orcaleAppSettings;
            _iResumeRepository = iResumeRepository;
            resume = new Webresponse<JobSeekerPreviewDetailDto>();
            
        }

        [BindProperty]
        public Webresponse<JobSeekerPreviewDetailDto> resume { get; set; }

        public async Task OnGet(long resumeId)
        {
            try
            {
                IList<Task> tasks = new List<Task>();
                tasks.Add(PersonalInfo(resumeId));
                tasks.Add(Notes(resumeId));
                tasks.Add(ProfileSummary(resumeId));
                tasks.Add(MatchingJourney(resumeId));
                tasks.Add(MatchingJourneyNotRejected(resumeId));
                tasks.Add(MatchingJourneyRejected(resumeId));
                

               await Task.WhenAll(tasks);
               await GetJobSeekerPhoto(resumeId);
            }
            catch (Exception ex)
            {
                resume.status = APIStatus.error;
                _logger.LogError($"{nameof(PreviewJobSeekerDetailModel)} -- {nameof(OnGet)} :::: {ex.Message}");
            }
           
        }

        private async Task GetJobSeekerPhoto(long resumeId)
        {
            try
            {
                var response = await _iResumeRepository.GetJobSeekerPhoto(resumeId);
                if (!string.IsNullOrEmpty(response.data?.Base64fileContentWithContentType))
                    resume.data.JobSeekerPhoto = response.data.Base64fileContentWithContentType + response.data.PhotoContent;
                else
                    resume.data.JobSeekerPhoto = "images/user.png";

            }
            catch(Exception ex) 
            {
                _logger.LogError($"{nameof(PreviewJobSeekerDetailModel)} -- {nameof(GetJobSeekerPhoto)}:::: {ex.Message}");
            }
            
        }

        private async Task PersonalInfo(long resumeId)
        {
            if (OracleConfiguration.OracleDataSources.Count == 0)
            {
                OracleConfiguration.OracleDataSources.Add(_orcaleAppSettings.Value.dbDBName, _orcaleAppSettings.Value.dbTNSEntry);
            }

            using (OracleConnection con = new OracleConnection(_orcaleAppSettings.Value.dbConnectionString))
            {
                con.Open();

                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandText = string.Format("select * from js_main WHERE rid = :rid");

                    OracleParameter rid = new OracleParameter("rid", resumeId);
                    cmd.Parameters.Add(rid);
                    
                    using (OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync())
                    {
                        var parser = reader.GetRowParser<JobSeekerPreviewDetailDto>(typeof(JobSeekerPreviewDetailDto));
                        while (reader.Read())
                        {
                            try
                            {
                                resume.data = parser(reader);
                                resume.status = APIStatus.success;
                            }
                            catch (Exception ex)
                            {
                                resume.status = APIStatus.error;
                                _logger.LogError($"{nameof(PreviewJobSeekerDetailModel)} :::: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        private async Task Notes(long resumeId)
        {
            if (OracleConfiguration.OracleDataSources.Count == 0)
            {
                OracleConfiguration.OracleDataSources.Add(_orcaleAppSettings.Value.dbDBName, _orcaleAppSettings.Value.dbTNSEntry);
            }

            using (OracleConnection con = new OracleConnection(_orcaleAppSettings.Value.dbConnectionString))
            {
                con.Open();

                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandText = string.Format("select * from js_notes WHERE rid = :rid");

                    OracleParameter rid = new OracleParameter("rid", resumeId);
                    cmd.Parameters.Add(rid);
                    
                    using (OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync())
                    {
                        var parser = reader.GetRowParser<JobSeekerPreviewNotesDto>(typeof(JobSeekerPreviewNotesDto));
                        while (reader.Read())
                        {
                            try
                            {
                                resume.data.Notes.Add(parser(reader));
                                resume.status = APIStatus.success;
                            }
                            catch (Exception ex)
                            {
                                resume.status = APIStatus.error;
                                _logger.LogError($"{nameof(JobSeekerPreviewNotesDto)} :::: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        private async Task ProfileSummary(long resumeId)
        {
            if (OracleConfiguration.OracleDataSources.Count == 0)
            {
                OracleConfiguration.OracleDataSources.Add(_orcaleAppSettings.Value.dbDBName, _orcaleAppSettings.Value.dbTNSEntry);
            }

            using (OracleConnection con = new OracleConnection(_orcaleAppSettings.Value.dbConnectionString))
            {
                con.Open();

                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandText = string.Format("select * from js_journey WHERE rid = :rid");

                    OracleParameter rid = new OracleParameter("rid", resumeId);
                    cmd.Parameters.Add(rid);
                    
                    using (OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync())
                    {
                        var parser = reader.GetRowParser<JobSeekerPreviewProfileSummaryDto>(typeof(JobSeekerPreviewProfileSummaryDto));
                        while (reader.Read())
                        {
                            try
                            {
                                resume.data.ProfileSummaryDto = parser(reader);
                                resume.status = APIStatus.success;
                            }
                            catch (Exception ex)
                            {
                                resume.status = APIStatus.error;
                                _logger.LogError($"{nameof(JobSeekerPreviewProfileSummaryDto)} :::: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        private async Task MatchingJourney(long resumeId)
        {
            if (OracleConfiguration.OracleDataSources.Count == 0)
            {
                OracleConfiguration.OracleDataSources.Add(_orcaleAppSettings.Value.dbDBName, _orcaleAppSettings.Value.dbTNSEntry);
            }

            using (OracleConnection con = new OracleConnection(_orcaleAppSettings.Value.dbConnectionString))
            {
                con.Open();

                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandText = string.Format("select * from js_matching_journey WHERE rid = :rid order by CREATEDDATE desc");

                    OracleParameter rid = new OracleParameter("rid", resumeId);
                    cmd.Parameters.Add(rid);

                    using (OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync())
                    {
                        var parser = reader.GetRowParser<JobSeekerPreviewMatchingJourney>(typeof(JobSeekerPreviewMatchingJourney));
                        while (reader.Read())
                        {
                            try
                            {
                                resume.data.MatchingJourneys.Add(parser(reader));
                                resume.status = APIStatus.success;
                            }
                            catch (Exception ex)
                            {
                                resume.status = APIStatus.error;
                                _logger.LogError($"{nameof(JobSeekerPreviewProfileSummaryDto)} :::: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        private async Task MatchingJourneyNotRejected(long resumeId)
        {
            if (OracleConfiguration.OracleDataSources.Count == 0)
            {
                OracleConfiguration.OracleDataSources.Add(_orcaleAppSettings.Value.dbDBName, _orcaleAppSettings.Value.dbTNSEntry);
            }

            using (OracleConnection con = new OracleConnection(_orcaleAppSettings.Value.dbConnectionString))
            {
                con.Open();

                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandText = string.Format("select * from js_active_nominations WHERE rid = :rid order by CREATEDDATE desc");

                    OracleParameter rid = new OracleParameter("rid", resumeId);
                    cmd.Parameters.Add(rid);

                    using (OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync())
                    {
                        var parser = reader.GetRowParser<JobSeekerPreviewMatchingJourney>(typeof(JobSeekerPreviewMatchingJourney));
                        while (reader.Read())
                        {
                            try
                            {
                                resume.data.MatchingJourneyNotRejected.Add(parser(reader));
                                resume.status = APIStatus.success;
                            }
                            catch (Exception ex)
                            {
                                resume.status = APIStatus.error;
                                _logger.LogError($"{nameof(JobSeekerPreviewProfileSummaryDto)} :::: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        private async Task MatchingJourneyRejected(long resumeId)
        {
            if (OracleConfiguration.OracleDataSources.Count == 0)
            {
                OracleConfiguration.OracleDataSources.Add(_orcaleAppSettings.Value.dbDBName, _orcaleAppSettings.Value.dbTNSEntry);
            }

            using (OracleConnection con = new OracleConnection(_orcaleAppSettings.Value.dbConnectionString))
            {
                con.Open();

                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandText = string.Format("select * from js_top5_rejection WHERE rid = :rid order by CREATEDDATE desc");

                    OracleParameter rid = new OracleParameter("rid", resumeId);
                    cmd.Parameters.Add(rid);

                    using (OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync())
                    {
                        var parser = reader.GetRowParser<JobSeekerPreviewMatchingJourney>(typeof(JobSeekerPreviewMatchingJourney));
                        while (reader.Read())
                        {
                            try
                            {
                                resume.data.MatchingJourneyRejected.Add(parser(reader));
                                resume.status = APIStatus.success;
                            }
                            catch (Exception ex)
                            {
                                resume.status = APIStatus.error;
                                _logger.LogError($"{nameof(JobSeekerPreviewProfileSummaryDto)} :::: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
    }
}
