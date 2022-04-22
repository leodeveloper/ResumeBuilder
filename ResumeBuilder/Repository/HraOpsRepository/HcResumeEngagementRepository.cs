using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HcResumeEngagementRepository : IHcResumeEngagementRepository
    {
        readonly ILogger<HcResumeEngagementRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumeEngagementRepository(ILogger<HcResumeEngagementRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumeEngagementViewModel hcEngagementResumeBankVm, int userid)
        {
           
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    if (hcEngagementResumeBankVm.ResumeId.Count() < 1)
                        throw new Exception("Please provide the jobseeker...");

                    HcResumeBankEngagement hcEngagementResumeBank = new HcResumeBankEngagement();
                    hcEngagementResumeBank.StartAppointmentDate = hcEngagementResumeBankVm.StartAppointmentDate;
                    hcEngagementResumeBank.StartAppointmentTime = hcEngagementResumeBankVm.StartAppointmentTime;
                    hcEngagementResumeBank.AppointmentDate = hcEngagementResumeBankVm.AppointmentDate;
                    hcEngagementResumeBank.AppointmentTime = hcEngagementResumeBankVm.AppointmentTime;
                    hcEngagementResumeBank.AppointmentType = hcEngagementResumeBankVm.AppointmentType;
                    hcEngagementResumeBank.AppointmentMethod = hcEngagementResumeBankVm.AppointmentMethod;
                    hcEngagementResumeBank.Advisor = hcEngagementResumeBankVm.Advisor;
                    hcEngagementResumeBank.Address = hcEngagementResumeBankVm.Address;
                    hcEngagementResumeBank.Notes = hcEngagementResumeBankVm.Notes;
                    hcEngagementResumeBank.Send_Reminder = hcEngagementResumeBankVm.Send_Reminder;
                    hcEngagementResumeBank.Status = hcEngagementResumeBankVm.Status;
                    hcEngagementResumeBank.CreatedDate = DateTime.Now;
                    hcEngagementResumeBank.CreatedUserId = userid;

                    await _con.InsertAsync<HcResumeBankEngagement>(hcEngagementResumeBank);
                    HcResumeBankEngagement lastRecordhcEngagementResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeBankEngagement>($"select top 1 Rid from HC_RESUME_BANK_ENGAGEMENT where CreatedDate='{hcEngagementResumeBank.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and CreatedUserId={hcEngagementResumeBank.CreatedUserId}");
                    if(lastRecordhcEngagementResumeBank != null)
                        await InsertDetail(hcEngagementResumeBankVm.ResumeId, lastRecordhcEngagementResumeBank.Rid, userid, _con);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        private async Task InsertDetail(long[] resumeIds, long engagementId,int userId , IDbConnection _con)
        {
            foreach(long resumeId in resumeIds)
            {
                await _con.InsertAsync<HcResumeBankEngagementDetailJobseeker>(new HcResumeBankEngagementDetailJobseeker { EngagementId = engagementId, ResumeId = resumeId, CreatedUserId = userId, CreatedDate = DateTime.Now });
            }            
        }

        public async Task<WebresponseNoData> Update(HcResumeEngagementViewModel hcEngagementResumeBankVm, long rid, int userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBankEngagement hcEngagementResumeBank = await _con.GetAsync<HcResumeBankEngagement>(rid);
                    hcEngagementResumeBank.StartAppointmentDate = hcEngagementResumeBankVm.StartAppointmentDate;
                    hcEngagementResumeBank.StartAppointmentTime = hcEngagementResumeBankVm.StartAppointmentTime;
                    hcEngagementResumeBank.AppointmentDate = hcEngagementResumeBankVm.AppointmentDate;
                    hcEngagementResumeBank.AppointmentTime = hcEngagementResumeBankVm.AppointmentTime;
                    hcEngagementResumeBank.AppointmentType = hcEngagementResumeBankVm.AppointmentType;
                    hcEngagementResumeBank.AppointmentMethod = hcEngagementResumeBankVm.AppointmentMethod;
                    hcEngagementResumeBank.Advisor = hcEngagementResumeBankVm.Advisor;
                    hcEngagementResumeBank.Address = hcEngagementResumeBankVm.Address;
                    hcEngagementResumeBank.Notes = hcEngagementResumeBankVm.Notes;
                    hcEngagementResumeBank.Send_Reminder = hcEngagementResumeBankVm.Send_Reminder;
                    hcEngagementResumeBank.Status = hcEngagementResumeBankVm.Status;
                    hcEngagementResumeBank.UpdatedDate = DateTime.Now;
                    hcEngagementResumeBank.UpdatedUserId = userid;
                    await _con.UpdateAsync<HcResumeBankEngagement>(hcEngagementResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<WebresponseNoData> UpdateJobSeekerStatus(HcResumeEngagementStatusNotes hcResumeEngagementStatusNotes)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBankEngagementDetailJobseeker hcEngagementResumeBankDetail = await _con.GetAsync<HcResumeBankEngagementDetailJobseeker>(hcResumeEngagementStatusNotes.rid);
                    hcEngagementResumeBankDetail.Status = hcResumeEngagementStatusNotes.statusId;
                    hcEngagementResumeBankDetail.UpdatedDate = DateTime.Now;
                    hcEngagementResumeBankDetail.UpdatedUserId = hcResumeEngagementStatusNotes.userid;
                    hcEngagementResumeBankDetail.JobSeekerNotes = hcResumeEngagementStatusNotes.jobseekernotes;
                    await _con.UpdateAsync<HcResumeBankEngagementDetailJobseeker>(hcEngagementResumeBankDetail);
                    HcResumeBankEngagementNotes hcResumeBankEngagementNotes = new HcResumeBankEngagementNotes();
                    hcResumeBankEngagementNotes.EngagementDetailId = hcResumeEngagementStatusNotes.rid;
                    hcResumeBankEngagementNotes.Status = hcResumeEngagementStatusNotes.statusId;
                    hcResumeBankEngagementNotes.JobSeekerNotes = hcResumeEngagementStatusNotes.jobseekernotes;
                    hcResumeBankEngagementNotes.CreatedDate = DateTime.Now;
                    hcResumeBankEngagementNotes.CreatedUserId = hcResumeEngagementStatusNotes.userid;
                    await _con.InsertAsync<HcResumeBankEngagementNotes>(hcResumeBankEngagementNotes);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(UpdateJobSeekerStatus)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeEngagementViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeEngagementViewModel_Get> webresponse = new Webresponse<HcResumeEngagementViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_BANK_ENGAGEMENT set Is_Deleted='true' where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeEngagementViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeEngagementViewModel_Get> webresponse = new Webresponse<HcResumeEngagementViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    string sqlStr = $@"SELECT E.[Rid],[StartAppointmentDate],[StartAppointmentTime],[AppointmentDate],[AppointmentTime],[AppointmentType],[AppointmentMethod],[Advisor]
                                      , U.FirstName as AdvisorName,[Address],E.[Notes],E.[CreatedDate],E.[CreatedUserId],E.[UpdatedDate],E.[UpdatedUserId],E.[Is_Deleted],E.[Send_Reminder],
                                      E.[Status],SEN.Title as StatusEn, SAE.Title as StatusAe, J.JobSeekerNotes
                                      FROM[HC_RESUME_BANK_ENGAGEMENT_DETAIL_JOBSEEKER] J, [HC_RESUME_BANK_ENGAGEMENT] E
                                     left outer join[HC_USERS] U on E.Advisor = u.RID
                                      left outer join HCM_RESUME_ENGAGEMENT_STATUS SEN on SEN.EngKeyId = E.Status and SEN.LanguageType = 0
                                      left outer join HCM_RESUME_ENGAGEMENT_STATUS SAE on SAE.EngKeyId = E.Status and SAE.LanguageType = 1
                                      where E.Rid = {rid} and J.EngagementId = E.Rid  and Is_Deleted = 'false'";

                    HcResumeEngagementViewModel_Get hcEngagementResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeEngagementViewModel_Get>(sqlStr);
                    if (hcEngagementResumeBank != null)
                    {
                        //  HcResumeEngagementViewModel_Get hcEngagementResumeBankVm = Convert_Model_to_ViewModel(hcEngagementResumeBank, _con);
                        getEngagementdetail(_con, hcEngagementResumeBank);
                        webresponse.data = hcEngagementResumeBank;
                        webresponse.status = APIStatus.success;
                        webresponse.message = "Record found";
                    }
                    else
                    {
                        webresponse.status = APIStatus.NotFound;
                        webresponse.message = "No Record found";
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeEngagementViewModel_Get>>> GetAllOrByAdvisorId(HcResumeEngagementViewModel_GetByAdvisor hcResumeEngagementViewModel_GetByAdvisor)
        {
            Webresponse<IList<HcResumeEngagementViewModel_Get>> webresponse = new Webresponse<IList<HcResumeEngagementViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string strSql = "SELECT  E.[Rid],[StartAppointmentDate],[StartAppointmentTime],[AppointmentDate],[AppointmentTime],[AppointmentType],[AppointmentMethod],[Advisor],U.FirstName as AdvisorName,[Address],E.[Notes],E.[CreatedDate],E.[CreatedUserId],E.[UpdatedDate],E.[UpdatedUserId],E.[Is_Deleted],E.[Send_Reminder],E.[Status],SEN.Title as StatusEn, SAE.Title as  StatusAe  FROM [HC_RESUME_BANK_ENGAGEMENT] E left outer join [HC_USERS] U on E.Advisor = u.RID left outer join HCM_RESUME_ENGAGEMENT_STATUS SEN on SEN.EngKeyId = E.Status and SEN.LanguageType=0 left outer join HCM_RESUME_ENGAGEMENT_STATUS SAE on SAE.EngKeyId = E.Status and SAE.LanguageType=1  where  Is_Deleted = 'false'";
                    if (hcResumeEngagementViewModel_GetByAdvisor.advisor > 0)
                        strSql += $" and Advisor={hcResumeEngagementViewModel_GetByAdvisor.advisor}";
                    if(hcResumeEngagementViewModel_GetByAdvisor.startDateTime != null && hcResumeEngagementViewModel_GetByAdvisor.endDateTime != null)
                    {
                        strSql += $" and (StartAppointmentDate BETWEEN  '{hcResumeEngagementViewModel_GetByAdvisor.startDateTime}' and '{hcResumeEngagementViewModel_GetByAdvisor.endDateTime}' or AppointmentDate BETWEEN  '{hcResumeEngagementViewModel_GetByAdvisor.startDateTime}' and '{hcResumeEngagementViewModel_GetByAdvisor.endDateTime}')";
                    }
                    IEnumerable<HcResumeEngagementViewModel_Get> hcEngagementResumeBanks = await _con.QueryAsync<HcResumeEngagementViewModel_Get>(strSql);
                    if (hcEngagementResumeBanks != null)
                    {
                        getEngagementdetail(_con, hcEngagementResumeBanks.ToList());
                        webresponse.data = hcEngagementResumeBanks.ToList();
                        webresponse.status = APIStatus.success;
                        webresponse.message = "Record found";
                    }
                    else
                    {
                        webresponse.status = APIStatus.NotFound;
                        webresponse.message = "No Record found";
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(GetAllOrByAdvisorId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeEngagementViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeEngagementViewModel_Get>> webresponse = new Webresponse<IList<HcResumeEngagementViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlStr = $@"SELECT E.[Rid],[StartAppointmentDate],[StartAppointmentTime],[AppointmentDate],[AppointmentTime],[AppointmentType],[AppointmentMethod],[Advisor]
                                      , U.FirstName as AdvisorName,[Address],E.[Notes],E.[CreatedDate],E.[CreatedUserId],E.[UpdatedDate],E.[UpdatedUserId],E.[Is_Deleted],E.[Send_Reminder],
                                      E.[Status],SEN.Title as StatusEn, SAE.Title as StatusAe, J.JobSeekerNotes
                                      FROM[HC_RESUME_BANK_ENGAGEMENT_DETAIL_JOBSEEKER] J, [HC_RESUME_BANK_ENGAGEMENT] E
                                     left outer join[HC_USERS] U on E.Advisor = u.RID
                                      left outer join HCM_RESUME_ENGAGEMENT_STATUS SEN on SEN.EngKeyId = E.Status and SEN.LanguageType = 0
                                      left outer join HCM_RESUME_ENGAGEMENT_STATUS SAE on SAE.EngKeyId = E.Status and SAE.LanguageType = 1
                                      where J.ResumeId = {resumeId} and J.EngagementId = E.Rid  and Is_Deleted = 'false'";
                    IEnumerable<HcResumeEngagementViewModel_Get> hcEngagementResumeBanks = await _con.QueryAsync<HcResumeEngagementViewModel_Get>(sqlStr);
                    
                    if (hcEngagementResumeBanks != null)
                    {
                        getEngagementdetail(_con, hcEngagementResumeBanks.ToList(), resumeId);                        

                        webresponse.data = hcEngagementResumeBanks.ToList();

                        webresponse.status = APIStatus.success;
                        webresponse.message = "Record found";
                    }
                    else
                    {
                        webresponse.status = APIStatus.NotFound;
                        webresponse.message = "No Record found";
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeEngagementStatusNotes_Get>>> GetJobSeekerNotesByEngagementDetailId(IDbConnection _con, long engagementDetailId)
        {
            Webresponse<IList<HcResumeEngagementStatusNotes_Get>> webresponse = new Webresponse<IList<HcResumeEngagementStatusNotes_Get>>();
            
                try
                {
                    string sqlStr = $@"SELECT N.Rid,N.EngagementDetailId,N.CreatedDate,N.CreatedUserId,N.Status,N.JobSeekerNotes,
                                        SEN.Title as StatusEn, SAE.Title as StatusAe, Con.nameen as FullName, Con.namear as FullNameAr
                                        FROM HC_RESUME_BANK_ENGAGEMENT_NOTES N
                                        left outer join HCM_RESUME_ENGAGEMENT_STATUS SEN on SEN.EngKeyId = N.Status and SEN.LanguageType = 0
                                        left outer join HCM_RESUME_ENGAGEMENT_STATUS SAE on SAE.EngKeyId = N.Status and SAE.LanguageType = 1
                                        left outer join [Entities].[dbo].[Contact] Con on Con.id = N.CreatedUserId
                                      where N.EngagementDetailId = {engagementDetailId} order by N.CreatedDate desc ";
                    IEnumerable<HcResumeEngagementStatusNotes_Get> hcEngagementResumeBanks = await _con.QueryAsync<HcResumeEngagementStatusNotes_Get>(sqlStr);

                    if (hcEngagementResumeBanks != null)
                    {
                        webresponse.data = hcEngagementResumeBanks.ToList();
                        webresponse.status = APIStatus.success;
                        webresponse.message = "Record found";
                    }
                    else
                    {
                        webresponse.status = APIStatus.NotFound;
                        webresponse.message = "No Record found";
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(GetJobSeekerNotesByEngagementDetailId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
           
            return webresponse;
        }

        private HcResumeEngagementViewModel_Get Convert_Model_to_ViewModel(HcResumeBankEngagement _hc_resume_bank, IDbConnection _con)
        {
            HcResumeEngagementViewModel_Get hcEngagementResumeBankVm = new HcResumeEngagementViewModel_Get();
            try
            {
                hcEngagementResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcEngagementResumeBankVm.StartAppointmentDate = _hc_resume_bank.StartAppointmentDate;
                hcEngagementResumeBankVm.StartAppointmentTime = _hc_resume_bank.StartAppointmentTime;
                hcEngagementResumeBankVm.AppointmentDate = _hc_resume_bank.AppointmentDate;
                hcEngagementResumeBankVm.AppointmentTime = _hc_resume_bank.AppointmentTime;
                hcEngagementResumeBankVm.AppointmentType = _hc_resume_bank.AppointmentType;
                hcEngagementResumeBankVm.AppointmentMethod = _hc_resume_bank.AppointmentMethod;
                hcEngagementResumeBankVm.Advisor = _hc_resume_bank.Advisor;
                hcEngagementResumeBankVm.Address = _hc_resume_bank.Address;
                hcEngagementResumeBankVm.Notes = _hc_resume_bank.Notes;
                hcEngagementResumeBankVm.Send_Reminder = _hc_resume_bank.Send_Reminder;
                hcEngagementResumeBankVm.Status = _hc_resume_bank.Status;
                hcEngagementResumeBankVm.CreatedDate = _hc_resume_bank.CreatedDate;
                hcEngagementResumeBankVm.CreatedUserId = _hc_resume_bank.CreatedUserId;
                hcEngagementResumeBankVm.UpdatedDate = _hc_resume_bank.UpdatedDate;
                hcEngagementResumeBankVm.UpdatedUserId = _hc_resume_bank.UpdatedUserId;
                getEngagementdetail(_con, hcEngagementResumeBankVm);

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcEngagementResumeBankVm;
        }

        private void getEngagementdetail(IDbConnection _con, HcResumeEngagementViewModel_Get hcEngagementResumeBankVm)
        {
            try
            {
              
                string strSql = $@"SELECT  ED.Rid ,ED.EngagementId ,ED.CreatedDate ,ED.CreatedUserId ,ED.Status,ED.UpdatedDate,ED.UpdatedUserId,
                                    R.Rid as ResumeId,R.UniqueNo as JobSeekerId, R.ExFullNameEng as FullName, R.ExFullNameAr as FullNameAr,
                                    SEN.Title as StatusEn, SAE.Title as StatusAe,ED.JobSeekerNotes

                                      FROM HC_RESUME_BANK_ENGAGEMENT_DETAIL_JOBSEEKER ED
                                      left outer join HC_RESUME_BANK R on R.rid = ED.ResumeId
                                        left outer join HCM_RESUME_ENGAGEMENT_STATUS SEN on SEN.EngKeyId = ED.Status and SEN.LanguageType = 0
                                        left outer join HCM_RESUME_ENGAGEMENT_STATUS SAE on SAE.EngKeyId = ED.Status and SAE.LanguageType = 1
                                      where ED.EngagementId={hcEngagementResumeBankVm.Rid}";

                hcEngagementResumeBankVm.EngagementJobSeekersInfo = _con.Query<EngagementJobSeeker>(strSql).ToList();
                if(hcEngagementResumeBankVm.EngagementJobSeekersInfo.Count > 0)
                {
                    hcEngagementResumeBankVm.ResumeId = hcEngagementResumeBankVm.EngagementJobSeekersInfo.Select(t => t.ResumeId).ToArray();
                    foreach(var _engagementJobSeekersInfo in hcEngagementResumeBankVm.EngagementJobSeekersInfo)
                    {
                        _engagementJobSeekersInfo.JobSeekerNote = GetJobSeekerNotesByEngagementDetailId(_con,_engagementJobSeekersInfo.Rid).Result.data;
                    }
                }
                   
            }
            catch(Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(getEngagementdetail)} :::: {ex.Message}");
            }            
        }

        private void getEngagementdetailByResumeId(IDbConnection _con, HcResumeEngagementViewModel_Get hcEngagementResumeBankVm, long resumeId)
        {
            try
            {

                string strSql = $@"SELECT  ED.Rid ,ED.EngagementId ,ED.CreatedDate ,ED.CreatedUserId ,ED.Status,ED.UpdatedDate,ED.UpdatedUserId,
                                    R.Rid as ResumeId,R.UniqueNo as JobSeekerId, R.ExFullNameEng as FullName, R.ExFullNameAr as FullNameAr,
                                    SEN.Title as StatusEn, SAE.Title as StatusAe, ED.JobSeekerNotes

                                      FROM HC_RESUME_BANK R,HC_RESUME_BANK_ENGAGEMENT_DETAIL_JOBSEEKER ED
                                     
                                        left outer join HCM_RESUME_ENGAGEMENT_STATUS SEN on SEN.EngKeyId = ED.Status and SEN.LanguageType = 0
                                        left outer join HCM_RESUME_ENGAGEMENT_STATUS SAE on SAE.EngKeyId = ED.Status and SAE.LanguageType = 1
                                      where ED.EngagementId={hcEngagementResumeBankVm.Rid} and R.rid = ED.ResumeId and R.rid = {resumeId}";

                hcEngagementResumeBankVm.EngagementJobSeekersInfo = _con.Query<EngagementJobSeeker>(strSql).ToList();
                if (hcEngagementResumeBankVm.EngagementJobSeekersInfo.Count > 0)
                {
                    hcEngagementResumeBankVm.ResumeId = hcEngagementResumeBankVm.EngagementJobSeekersInfo.Select(t => t.ResumeId).ToArray();
                    foreach (var _engagementJobSeekersInfo in hcEngagementResumeBankVm.EngagementJobSeekersInfo)
                    {
                        _engagementJobSeekersInfo.JobSeekerNote = GetJobSeekerNotesByEngagementDetailId(_con, _engagementJobSeekersInfo.Rid).Result.data;
                    }
                }
                    
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(getEngagementdetail)} :::: {ex.Message}");
            }
        }

        private void getEngagementdetail(IDbConnection _con, IList<HcResumeEngagementViewModel_Get> hcEngagementResumeBankVmds, long resumeId = 0)
        {
            try
            {
                foreach (HcResumeEngagementViewModel_Get hcEngagementResumeBankVmd in hcEngagementResumeBankVmds)
                {
                    if (resumeId > 0)
                        getEngagementdetailByResumeId(_con, hcEngagementResumeBankVmd, resumeId);
                    else
                        getEngagementdetail(_con, hcEngagementResumeBankVmd);
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeEngagementRepository)} :::::: {nameof(getEngagementdetail)} :::: {ex.Message}");
            }
        }
    }
}
