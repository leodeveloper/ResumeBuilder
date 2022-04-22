using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using Dapper.Contrib.Extensions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeBuilder.Dto;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using IntegrationApiClassLibrary.Repository;
using ResumeBuilder.Helper;

namespace ResumeBuilder.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        readonly ILogger<ResumeRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;
        readonly IOptions<MongoDBSettings> _appSettingsMongoDB;
        readonly IMongoClient _mongoClient;
        readonly IMongoDatabase _mongodb;
        readonly IGenericRepositoryPaggingDapper<JobSeekerResume> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        readonly IEducationRepository _iEducationRepository;
        readonly IEmployerRepository _iEmployerRepository;
        readonly IOccupationRepository _iOccupationRepository;
        readonly ICertificationRepository _iCertificationRepository;
        readonly IPersonalInfoRepository _iPersonalInfoRepository;
        readonly IToolsKnowledgeRepository _iToolsKnowledgeRepository;

        public ResumeRepository(IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IToolsKnowledgeRepository iToolsKnowledgeRepository, IPersonalInfoRepository iPersonalInfoRepository, ICertificationRepository iCertificationRepository, IOccupationRepository iOccupationRepository, IEmployerRepository iEmployerRepository, IEducationRepository iEducationRepository, IMongoClient mongoClient, IOptions<MongoDBSettings> appSettingsMongoDB, IOptions<AppSettings> appSettings, ILogger<ResumeRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<JobSeekerResume> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _appSettingsMongoDB = appSettingsMongoDB;
            _mongoClient = mongoClient;
            _mongodb = _mongoClient.GetDatabase(_appSettingsMongoDB.Value.dbName);
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _iEmployerRepository = iEmployerRepository;
            _iEducationRepository = iEducationRepository;
            _iOccupationRepository = iOccupationRepository;
            _iCertificationRepository = iCertificationRepository;
            _iPersonalInfoRepository = iPersonalInfoRepository;
            _iToolsKnowledgeRepository = iToolsKnowledgeRepository;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<Webresponse<string>> Insert(Resume_Attachment resume_Attachment)
        {
            Webresponse<string> _result = new Webresponse<string>
            {
                status = APIStatus.processing,
                message = "Initiaing db request",
            };

            try
            {
                var collection = _mongodb.GetCollection<Resume_Attachment>("resume_attachment");
                 await collection.InsertOneAsync(resume_Attachment);
                _result.data = resume_Attachment.Id.ToString();
                _result.status = APIStatus.success;
                _result.message = "Insert Successfuly";
            }
            catch (Exception ex)
            {
                _result.message = ex.Message;
                _result.status = APIStatus.error;
                _ilogger.LogError("ResumeRepository : Insert : " + ex.Message);
            }
            return _result;
        }

        public async Task<Webresponse<Resume_Attachment>> GetAttachment(string mongodbUniqueId)
        {
            Webresponse<Resume_Attachment> _result = new Webresponse<Resume_Attachment>
            {
                status = APIStatus.processing,
                message = "Initiaing db request",
            };

            try
            {
                var collection = _mongodb.GetCollection<Resume_Attachment>("resume_attachment");
                Resume_Attachment resume_Attachment = await collection.AsQueryable().FirstOrDefaultAsync<Resume_Attachment>(z => z.UnqiueId == mongodbUniqueId);
                if(resume_Attachment != null)
                {
                    // await collection.se
                    _result.data = resume_Attachment;//.ResumeAttachmentBase64String;
                    _result.status = APIStatus.success;
                    _result.message = "Get Successfuly";
                }
                else
                {
                    _result.status = APIStatus.error;
                    _result.message = "Failed";
                }
                
            }
            catch (Exception ex)
            {
                _result.message = ex.Message;
                _result.status = APIStatus.error;
                _ilogger.LogError("ResumeRepository : GetAttachment : " + ex.Message);
            }
            return _result;
        }

        public async Task<Webresponse<Job_ApplicationAttachmentMongoDB>> GetAttachmentJobApplication(string mongodbUniqueId, string collectionName)
        {
            Webresponse<Job_ApplicationAttachmentMongoDB> _result = new Webresponse<Job_ApplicationAttachmentMongoDB>
            {
                status = APIStatus.processing,
                message = "Initiaing db request",
            };

            try
            {
                var collection = _mongodb.GetCollection<Job_ApplicationAttachmentMongoDB>(collectionName);
                Job_ApplicationAttachmentMongoDB jobApplication_Attachment = await collection.AsQueryable().FirstOrDefaultAsync<Job_ApplicationAttachmentMongoDB>(z => z.FileId == mongodbUniqueId);
                if (jobApplication_Attachment != null)
                {
                    // await collection.se
                    _result.data = jobApplication_Attachment;//.ResumeAttachmentBase64String;
                    _result.status = APIStatus.success;
                    _result.message = "Get Successfuly";
                }
                else
                {
                    _result.status = APIStatus.error;
                    _result.message = "Failed";
                }

            }
            catch (Exception ex)
            {
                _result.message = ex.Message;
                _result.status = APIStatus.error;
                _ilogger.LogError("ResumeRepository : GetAttachmentJobApplication : " + ex.Message);
            }
            return _result;
        }

        public async Task<bool> InsertResume(Resume resume)
        {
            try
            {
                resume.resumestatus = 1;
                int i = await _iUnitOfWork.Connection.InsertAsync<Resume>(resume);
                //Insert insert default with resume
                await InsertResumeStatus(new JobsSeekerStatus { Reason_ID = 6, Resume_ID = i, Status_ID = 1, UserId = "System" });
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(InsertResume)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdateResume(Resume resume)
        {
            try
            {
                using(var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Resume>(resume);
                }
                          
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(InsertResume)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> DeleteResume(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update JobSeekerResume set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(DeleteResume)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<IList<Resume>>> GetManyResumeByIds(long[] rid)
        {
            Webresponse<IList<Resume>> resume = new Webresponse<IList<Resume>>();
            try
            {
                string sql = "SELECT * FROM[JobSeeker].[dbo].[JobSeekerResume] where IsDeleted='false' and RID in @ids";
                var result = await _iUnitOfWork.Connection.QueryAsync<Resume>(sql, new { ids = rid });
                if (result == null)
                {
                    resume.message = "No Record found";
                }
                else
                {
                    resume.data = result.ToList();
                }
                resume.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeById)} -- " + ex.Message);
                resume.message = ex.Message;
                resume.status = APIStatus.error;
            }
            return resume;
        }

        public async Task<Webresponse<Resume>> GetResumeById(long rid)
        {
            Webresponse<Resume> resume = new Webresponse<Resume>();
            try
            {
                
                var result = await _iUnitOfWork.Connection.GetAsync<Resume>(rid);
                if (result == null)
                {
                    resume.message = "No Record found";
                }
                else
                {
                    resume.data = result;
                }
                resume.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeById)} -- " + ex.Message);
                resume.message = ex.Message;
                resume.status = APIStatus.error;
            }
            return resume;
        }

        public async Task<Webresponse<Resume>> GetResumeByEmiratesId(string emiratedId)
        {
            Webresponse<Resume> resume = new Webresponse<Resume>();
            try
            {

                var result = await _iUnitOfWork.Connection.QueryAsync<Resume>($"select * FROM [JobSeeker].[dbo].[JobSeekerResume] where EmiratesId = '{emiratedId}' and IsDeleted='false'");
                if (result.Count() == 0 || result == null)
                {
                    resume.message = "No Record found";
                    resume.status = APIStatus.NotFound;
                }
                else
                {
                    resume.data = result.FirstOrDefault();
                    resume.status = APIStatus.success;
                }
                
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeById)} -- " + ex.Message);
                resume.message = ex.Message;
                resume.status = APIStatus.error;
            }
            return resume;
        }

        public async Task<WebresponsePaging<IList<JobSeekerResume>>> GetAllResume(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<JobSeekerResume>> resume = new WebresponsePaging<IList<JobSeekerResume>>();
            try
            {
                 string sqlQuery = "select JSR.*,S.RID as StatusID, S.Title as StatusTitle,s.TitleAr as StatusTitleAr,Edu.* from [dbo].[JobSeekerResume] JSR left JOIN[dbo].[Education] as Edu ON JSR.RID = Edu.Resume_ID , [dbo].[JobsSeekerStatus] as JSS, [dbo].[Status] as S where JSR.RID = JSS.Resume_ID and JSS.Status_ID = S.RID and JSS.StatusUpdateDateTime in (select max(StatusUpdateDateTime) as LastUpdate from [dbo].[JobsSeekerStatus] group by Resume_ID) and JSR.IsDeleted = 'false'";

               //  string sqlQuery = "select JSR.*,Edu.* from [dbo].[JobSeekerResume] JSR left JOIN[dbo].[Education] as Edu ON JSR.RID = Edu.Resume_ID where JSR.IsDeleted = 'false'";

              //  string sqlQuery = "SELECT * from  [dbo].[VW_JobSeekerResumeList]";

                var jobSeekerResumes = new Dictionary<long, JobSeekerResume>();
                var result = await _iUnitOfWork.Connection.QueryAsync<JobSeekerResume, Education, JobSeekerResume>
                    (sqlQuery, (pd, pp) =>
                    {
                        JobSeekerResume jobSeekerResume;
                        if (!jobSeekerResumes.TryGetValue(pd.Rid, out jobSeekerResume))
                        {
                            jobSeekerResumes.Add(pd.Rid, jobSeekerResume = pd);
                        }

                        jobSeekerResume.Education.Add(pp);
                        return jobSeekerResume;
                    }, splitOn: "Rid");

                if (result == null && !result.Any())
                {
                    resume.message = "No Record found";
                }
                else
                {
                    resume.data = jobSeekerResumes.Values.ToList();
                }
                resume.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetAllResume)} -- " + ex.Message);
                resume.message = ex.Message;
                resume.status = APIStatus.error;
            }
            return resume;
        }       

        public async Task<Webresponse<ResumePreviewDto>> Resume_Preview(long resume_Id)
        {
            Webresponse<ResumePreviewDto> resume = new Webresponse<ResumePreviewDto> { data = new ResumePreviewDto() };
            try
            {
                var result = await _iUnitOfWork.Connection.QueryFirstOrDefaultAsync<Resume>($"select * from JobSeekerResume where RID={resume_Id} and IsDeleted='false'");
                if (result == null)
                {
                    resume.message = "No Record found";
                }
                else
                {
                    resume.data.Resume = result;

                    List<Task> tasks = new List<Task>();

                    tasks.Add(GetResumeEducation(resume_Id, resume));
                    tasks.Add(GetResumeEmployer(resume_Id, resume));
                    tasks.Add(GetResumeOccupation(resume_Id, resume));
                    tasks.Add(GetResumeCertifcate(resume_Id, resume));
                    tasks.Add(GetResumeToolsAndKnowledge(resume_Id, resume));
                    tasks.Add(GetJobSeekerPhoto(resume_Id)); //Do change the seq of task array 
                    tasks.Add(GetJobSeekerOtherInfo(resume));
                    await Task.WhenAll(tasks.ToArray());

                    Webresponse<ChildJobSeekerCvphoto> jobSeekerPhoto = ((Task<Webresponse<ChildJobSeekerCvphoto>>)tasks[5]).Result;

                   // Webresponse<JobSeekerCvphoto> jobSeekerPhoto = await GetJobSeekerPhoto(resume_Id);
                    if(jobSeekerPhoto.status == APIStatus.success)
                    {
                        resume.data.JobSeekerCvphoto = jobSeekerPhoto.data;
                    }
                    

                }
                resume.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeById)} -- " + ex.Message);
                resume.message = ex.Message;
                resume.status = APIStatus.error;
            }
            return resume;
        }

        private async Task GetJobSeekerOtherInfo(Webresponse<ResumePreviewDto> resume)
        {            
            try
            {
                List<Task> tasks = new List<Task>();
                tasks.Add(_ilookupRespository.GetHttpClient<City>($"{_appSettingsAPIUrls.Value.CityApiUrl}/GetCityBy/GetCityById?Id={resume.data.Resume.CityId}"));
                tasks.Add(_ilookupRespository.GetHttpClient<Location>($"{_appSettingsAPIUrls.Value.LocationApiUrl}/GetLocationBy?Id={resume.data.Resume.LocationId}"));
                tasks.Add(_ilookupRespository.GetHttpClient<Emirates>($"{_appSettingsAPIUrls.Value.EmiratesApiUrl}/GetEmiratesBy?Id={resume.data.Resume.Emirates}"));
                
                await Task.WhenAll(tasks.ToArray());

                var city = ((Task<City>)tasks[0]).Result;
                var location = ((Task<Location>)tasks[1]).Result;
                var emirates = ((Task<Emirates>)tasks[2]).Result;

                var martialStatus = _ilookupRespository.GetMartialStatus().FirstOrDefault(m=>m.Id == resume.data.Resume.MartialStatus);
                var gender = _ilookupRespository.GetGender().FirstOrDefault(m => m.Id == resume.data.Resume.GenderId);
                if(resume.data.Resume.Salutation > 0)
                {
                    Salutation salutations = _ilookupRespository.GetSalutation(resume.data.Resume.Salutation).FirstOrDefault();
                    resume.data.OtherJobSeekerInfo.Salutation = salutations.EnName;
                    resume.data.OtherJobSeekerInfo.SalutationAr = salutations.ArName;
                }
                

                resume.data.OtherJobSeekerInfo.City = city?.EnTitle;
                resume.data.OtherJobSeekerInfo.CityAr = city?.ArTitle;
                resume.data.OtherJobSeekerInfo.Emirates = emirates?.EnTitle;
                resume.data.OtherJobSeekerInfo.EmiratesAr = emirates?.ArTitle;
                resume.data.OtherJobSeekerInfo.Location = location?.EnTitle;
                resume.data.OtherJobSeekerInfo.LocationAr = location?.ArTitle;

                resume.data.OtherJobSeekerInfo.MartialStatus = martialStatus?.EnName;
                resume.data.OtherJobSeekerInfo.MartialStatusAr = martialStatus?.ArName;

                resume.data.OtherJobSeekerInfo.Gender = gender?.EnName;
                resume.data.OtherJobSeekerInfo.GenderAr = gender?.ArName;

            }
            catch(Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetJobSeekerOtherInfo)} -- " + ex.Message);
            }
        }

        public async Task<Webresponse<ChildJobSeekerCvphoto>> GetJobSeekerPhoto(long resumeId)
        {
            Webresponse<ChildJobSeekerCvphoto> resumeStatus = new Webresponse<ChildJobSeekerCvphoto>();
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryFirstOrDefaultAsync<ChildJobSeekerCvphoto>($"select [RID],[PhotoContent],[Filename],[Resume_ID],[CreatedDateTime],[id] from JobSeekerCVPhoto where Resume_ID={resumeId} order by CreatedDateTime desc");
                  
                    if (result == null)
                    {
                        resumeStatus.status = APIStatus.NotFound;
                        resumeStatus.message = "No Record found";
                    }
                    else
                    {
                        resumeStatus.status = APIStatus.success;
                        resumeStatus.data = result;

                        string fileExtension = HelperMethod.GetFileExtension(resumeStatus.data.PhotoContent);
                        if (fileExtension != string.Empty)
                            resumeStatus.data.Base64fileContentWithContentType = $"data:image/{fileExtension};base64, ";
            
                    }
                }
                
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeStatus)} -- " + ex.Message);
                resumeStatus.message = ex.Message;
                resumeStatus.status = APIStatus.error;
            }
            return resumeStatus;
        }

        #region resume status
        /// <summary>
        /// resume status get
        /// </summary>
        /// <param name="resumeId"></param>
        /// <returns></returns>
        public async Task<WebresponsePaging<IList<JobsSeekerStatus>>> GetResumeStatus(long resumeId)
        {
            WebresponsePaging<IList<JobsSeekerStatus>> resumeStatus = new WebresponsePaging<IList<JobsSeekerStatus>>();
            try
            {
                using(var con  = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync<JobsSeekerStatus>($"select * from JobsSeekerStatus where Resume_ID={resumeId}");
                    if (result == null)
                    {
                        resumeStatus.message = "No Record found";
                    }
                    else
                    {
                        resumeStatus.data = result.ToList();
                    }
                }               
                resumeStatus.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeStatus)} -- " + ex.Message);
                resumeStatus.message = ex.Message;
                resumeStatus.status = APIStatus.error;
            }
            return resumeStatus;
        }

        public async Task<WebresponseNoData> InsertResumeStatus(JobsSeekerStatus resume)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            try
            {
                using(var con = _iUnitOfWork.Connection)
                {
                    if(con.Query<Resume>($"select [RID] from [dbo].[JobSeekerResume] where RID={resume.Resume_ID}").Any())
                    {
                        int i = await con.InsertAsync<JobsSeekerStatus>(resume);
                        Webresponse<Resume> resumeupdate = await GetResumeById(resume.Resume_ID);
                        resumeupdate.data.resumestatus = resume.Status_ID;
                        await UpdateResume(resumeupdate.data);                        
                        webresponseNoData.status = APIStatus.success;                       
                    }
                    else
                    {
                        webresponseNoData.status = APIStatus.error;
                        webresponseNoData.message = $"Invalid rId = {resume.Resume_ID}";
                    }
                    con.Close();
                    con.Dispose();
                    return webresponseNoData;
                }              
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(InsertResumeStatus)} -- For rid {resume.Resume_ID} -- " + ex.Message);
                webresponseNoData.status = APIStatus.error;
                webresponseNoData.message = ex.Message;
                return webresponseNoData;
            }

        }

        public async Task<bool> UpdateResumeStatus(JobsSeekerStatus resume)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<JobsSeekerStatus>(resume);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(UpdateResumeStatus)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<Webresponse<JobSeekerCoverLetter>> GetCoverLetter(long resume_ID)
        {
            Webresponse<JobSeekerCoverLetter> jobSeekerResult = new Webresponse<JobSeekerCoverLetter>() { data = new JobSeekerCoverLetter() };
            try
            {                
                using (var con = _iUnitOfWork.Connection)
                {

                    var result = await con.QueryFirstOrDefaultAsync<JobSeekerCoverLetter>("select * from JobSeekerCoverLetter where Resume_ID=" + resume_ID);
                    if(result != null)
                    {
                        jobSeekerResult.data = result;
                    }
                    jobSeekerResult.status = APIStatus.success;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(UpdateResumeStatus)} -- " + ex.Message);
                jobSeekerResult.message = ex.Message;
                jobSeekerResult.status = APIStatus.error;

            }
            return jobSeekerResult;
        }

        public async Task<bool> AddUpdateCoverLetter(JobSeekerCoverLetter jobSeekerCoverLetter)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryFirstOrDefaultAsync<JobSeekerCoverLetter>("select * from JobSeekerCoverLetter where Resume_ID="+ jobSeekerCoverLetter.Resume_ID);
                    
                    if(result != null)
                    {
                        await con.ExecuteAsync($"update JobSeekerCoverLetter  SET [CoverLetter] = '{jobSeekerCoverLetter.CoverLetter}' ,[Accomplishments] = '{jobSeekerCoverLetter.Accomplishments}' WHERE  Resume_ID={jobSeekerCoverLetter.Resume_ID}");
                    }
                    else
                    {
                        await con.ExecuteAsync($"insert into JobSeekerCoverLetter  ([Resume_ID],[CoverLetter],[Accomplishments])  VALUES({jobSeekerCoverLetter.Resume_ID},'{jobSeekerCoverLetter.CoverLetter}', '{jobSeekerCoverLetter.Accomplishments}')");
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(UpdateResumeStatus)} -- " + ex.Message);
                return false;
            }
        }

        #endregion

        #region Private
        private async Task GetResumeEducation(long resume_Id, Webresponse<ResumePreviewDto> resume)
        {
            try
            {
                var resumeEducation = await _iEducationRepository.GeteducationByResumeIdPreview(resume_Id);
                if (resumeEducation.status == APIStatus.success)
                {
                    foreach (var education in resumeEducation.data)
                    {
                        resume.data.EducationDtos.Add(education);
                    }
                }
            }
            catch(Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeEducation)} -- " + ex.Message);
            }
            
        }

        private async Task GetResumeEmployer(long resume_Id, Webresponse<ResumePreviewDto> resume)
        {
            try
            {
                var resumeEmployer = await _iEmployerRepository.GetemployerByResumeIdPreview(resume_Id);
                if (resumeEmployer.status == APIStatus.success)
                {
                    foreach (var employer in resumeEmployer.data)
                    {
                        resume.data.EmployerDtos.Add(employer);
                    }
                }
            }
            catch(Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeEmployer)} -- " + ex.Message);
            }            
        }

        private async Task GetResumeOccupation(long resume_Id, Webresponse<ResumePreviewDto> resume)
        {
            try
            {
                var resumeOccupation = await _iOccupationRepository.GetoccupationByResumeIdPreview(resume_Id);
                if (resumeOccupation.status == APIStatus.success)
                {
                    foreach (var occupation in resumeOccupation.data)
                    {
                        resume.data.SkillOccupationDto.Add(occupation);
                    }
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeOccupation)} -- " + ex.Message);
            }
        }

        private async Task GetResumeCertifcate(long resume_Id, Webresponse<ResumePreviewDto> resume)
        {
            try
            {
                var resumeCertifcate = await _iCertificationRepository.GetcertificationByResumeId(resume_Id);
                if (resumeCertifcate.status == APIStatus.success)
                {
                    foreach (var certifcate in resumeCertifcate.data)
                    {
                        resume.data.CertificateDtos.Add(certifcate);
                    }
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeCertifcate)} -- " + ex.Message);
            }
        }

        private async Task GetResumeToolsAndKnowledge(long resume_Id, Webresponse<ResumePreviewDto> resume)
        {
            try
            {
                var resumeToolsKnowledge = await _iToolsKnowledgeRepository.GetToolsKnowledgeByResumeIdPreview(resume_Id);
                if (resumeToolsKnowledge.status == APIStatus.success)
                {
                    foreach (var toolsKnowledge in resumeToolsKnowledge.data)
                    {
                        resume.data.ToolsKnowledgeDto.Add(toolsKnowledge);
                    }
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ResumeRepository)}::{nameof(GetResumeToolsAndKnowledge)} -- " + ex.Message);
            }
        }

        #endregion

    }
}
