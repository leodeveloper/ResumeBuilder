using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Dto.HraOpsDto;
using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HcResumeEmployerRepository : IHcResumeEmployerRepository
    {
        readonly ILogger<HcResumeEmployerRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly ITypeSenceApiRepository _iTypeSenceApiRepository;
        public HcResumeEmployerRepository(ITypeSenceApiRepository iTypeSenceApiRepository,ILogger<HcResumeEmployerRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            _iTypeSenceApiRepository = iTypeSenceApiRepository;
        }

        public async Task<WebresponseNoData> Insert(HcResumeEmployerViewModel hcEmployerResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeEmployer hcEmployerResumeBank = new HcResumeEmployer();
                    hcEmployerResumeBank.EuropeanStandardOccupationId = hcEmployerResumeBankVm.EuropeanStandardOccupationId;
                    hcEmployerResumeBank.Achievement = hcEmployerResumeBankVm.Achievement;
                    hcEmployerResumeBank.IsJobInUae = hcEmployerResumeBankVm.IsJobInUae;

                    hcEmployerResumeBank.FromDate = hcEmployerResumeBankVm.StartDate;
                    hcEmployerResumeBank.ToDate = hcEmployerResumeBankVm.Enddate;

                    if(hcEmployerResumeBankVm.StartDate.HasValue)
                    {
                        hcEmployerResumeBank.FromMonth = (short)hcEmployerResumeBankVm.StartDate.Value.Month;
                        hcEmployerResumeBank.FromYear = hcEmployerResumeBankVm.StartDate.Value.Year;
                    }
                    if (hcEmployerResumeBankVm.Enddate.HasValue)
                    {
                        hcEmployerResumeBank.ToMonth = (short)hcEmployerResumeBankVm.Enddate.Value.Month;
                        hcEmployerResumeBank.ToYear = hcEmployerResumeBankVm.Enddate.Value.Year;
                    }
                   

                   

                    hcEmployerResumeBank.EmployerId = hcEmployerResumeBankVm.EmployerId;
                    hcEmployerResumeBank.ResumeId = hcEmployerResumeBankVm.ResumeId;
                    hcEmployerResumeBank.DesignationId = hcEmployerResumeBankVm.JobTitleId;

                    hcEmployerResumeBank.Particular = hcEmployerResumeBankVm.IsCrurrentJob;
                    hcEmployerResumeBank.CompanyType = hcEmployerResumeBankVm.EmploymentTypeId;
                    hcEmployerResumeBank.SequenceNo = hcEmployerResumeBankVm.SequenceNo;
                    hcEmployerResumeBank.EmployerName = "";
                    hcEmployerResumeBank.EmployerAddress = "";
                    hcEmployerResumeBank.PhoneNo = "";
                    hcEmployerResumeBank.EmployeeCode = "";
                    hcEmployerResumeBank.Department = "";
                    hcEmployerResumeBank.ManagerName = "";
                    hcEmployerResumeBank.ManagerEmailId = "";
                    hcEmployerResumeBank.ManagerContactNumber = "";

                    hcEmployerResumeBank.DutyResponsibilities = "";
                    hcEmployerResumeBank.LeavingReason = "";
                    hcEmployerResumeBank.IndustryType = "";
                    hcEmployerResumeBank.CountryId = hcEmployerResumeBankVm.CountryId;
                    hcEmployerResumeBank.City = "";
                    hcEmployerResumeBank.Cityid = hcEmployerResumeBankVm.JobUaeCityId;
                    hcEmployerResumeBank.DeviationReason = "";
                    hcEmployerResumeBank.IndustryId = hcEmployerResumeBankVm.JobIndustryId;
                    hcEmployerResumeBank.Createddate = DateTime.Now;
                    hcEmployerResumeBank.Createduserid = userid;

                    await _con.InsertAsync<HcResumeEmployer>(hcEmployerResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";

                    if (hcEmployerResumeBankVm.IsCrurrentJob == 1)
                        getLastInsertedId(hcEmployerResumeBankVm.ResumeId);

                    UpdateTotalExperience(hcEmployerResumeBankVm.ResumeId);
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEmployerRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeEmployerViewModel hcEmployerResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeEmployer hcEmployerResumeBank = await _con.GetAsync<HcResumeEmployer>(rid);
                    hcEmployerResumeBank.EuropeanStandardOccupationId = hcEmployerResumeBankVm.EuropeanStandardOccupationId;
                    hcEmployerResumeBank.Achievement = hcEmployerResumeBankVm.Achievement;
                    hcEmployerResumeBank.IsJobInUae = hcEmployerResumeBankVm.IsJobInUae;
                    hcEmployerResumeBank.FromDate = hcEmployerResumeBankVm.StartDate;
                    hcEmployerResumeBank.ToDate = hcEmployerResumeBankVm.Enddate;

                    if (hcEmployerResumeBankVm.StartDate.HasValue)
                    {
                        hcEmployerResumeBank.FromMonth = (short)hcEmployerResumeBankVm.StartDate.Value.Month;
                        hcEmployerResumeBank.FromYear = hcEmployerResumeBankVm.StartDate.Value.Year;
                    }
                    else
                    {
                        hcEmployerResumeBank.FromMonth = null;
                        hcEmployerResumeBank.FromYear = null;
                    }
                    if (hcEmployerResumeBankVm.Enddate.HasValue)
                    {
                        hcEmployerResumeBank.ToMonth = (short)hcEmployerResumeBankVm.Enddate.Value.Month;
                        hcEmployerResumeBank.ToYear = hcEmployerResumeBankVm.Enddate.Value.Year;
                    }
                    else
                    {
                        hcEmployerResumeBank.ToMonth = null;
                        hcEmployerResumeBank.ToYear = null;
                    }

                    hcEmployerResumeBank.EmployerId = hcEmployerResumeBankVm.EmployerId;
                    hcEmployerResumeBank.ResumeId = hcEmployerResumeBankVm.ResumeId;
                    hcEmployerResumeBank.DesignationId = hcEmployerResumeBankVm.JobTitleId;
                    hcEmployerResumeBank.Particular = hcEmployerResumeBankVm.IsCrurrentJob;
                    hcEmployerResumeBank.CompanyType = hcEmployerResumeBankVm.EmploymentTypeId;
                    hcEmployerResumeBank.SequenceNo = hcEmployerResumeBankVm.SequenceNo;
                    hcEmployerResumeBank.CountryId = hcEmployerResumeBankVm.CountryId;
                    hcEmployerResumeBank.Cityid = hcEmployerResumeBankVm.JobUaeCityId;
                    hcEmployerResumeBank.IndustryId = hcEmployerResumeBankVm.JobIndustryId;

                    hcEmployerResumeBank.ModifiedDate = DateTime.Now;
                    hcEmployerResumeBank.Modifieduserid = userid;

                    await _con.UpdateAsync<HcResumeEmployer>(hcEmployerResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";

                    if(hcEmployerResumeBankVm.IsCrurrentJob == 1)
                        SetOtherThisIsMycurrentRolFalse(rid, hcEmployerResumeBankVm.ResumeId);

                    UpdateTotalExperience(hcEmployerResumeBankVm.ResumeId);
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEmployerRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeEmployerViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeEmployerViewModel_Get> webresponse = new Webresponse<HcResumeEmployerViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_EMPLOYER set Is_Deleted='true' where RID={rid}");

                    HcResumeEmployer hC_RESUME_BANK = await _con.GetAsync<HcResumeEmployer>(rid);
                    UpdateTotalExperience(hC_RESUME_BANK.ResumeId);

                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEmployerRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeEmployerViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeEmployerViewModel_Get> webresponse = new Webresponse<HcResumeEmployerViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeEmployer hcEmployerResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeEmployer>($"select * from HC_RESUME_EMPLOYER where RID={rid} and Is_Deleted='false'");
                    if (hcEmployerResumeBank != null)
                    {
                        HcResumeEmployerViewModel_Get hcEmployerResumeBankVm = Convert_Model_to_ViewModel(hcEmployerResumeBank);
                        webresponse.data = hcEmployerResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeEmployerRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeEmployerViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeEmployerViewModel_Get>> webresponse = new Webresponse<IList<HcResumeEmployerViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    IEnumerable<HcResumeEmployerViewModel_Get> hcEmployerResumeBanks = await _con.QueryAsync<HcResumeEmployerViewModel_Get>($"select Employer.*,Employer.Particular as IsCrurrentJob,Employer.CompanyType as EmploymentTypeId,Employer.IndustryId as JobIndustryId,Employer.Cityid as JobUaeCityId,Employer.FromDate as StartDate,Employer.ToDate as Enddate,jobtitle.JobTitle,jobtitle.JobTitle as JobTitleAe , HEmployer.Title as EmployerTitle from HC_RESUME_EMPLOYER Employer left outer join HCM_JOBTITLE jobtitle on Employer.DesignationId = jobtitle.EngKeyID and jobtitle.LanguageType=0 left outer join Hcm_Employer HEmployer on Employer.EmployerID = HEmployer.RID  where ResumeID={resumeId} and Is_Deleted='false'");
                    if (hcEmployerResumeBanks != null)
                    {
                        //webresponse.data = new List<HcResumeEmployerViewModel_Get>();
                        //foreach (HcResumeEmployer hcEmployerResumeBank in hcEmployerResumeBanks)
                        //{
                        //    HcResumeEmployerViewModel_Get hcEmployerResumeBankVm = Convert_Model_to_ViewModel(hcEmployerResumeBank);
                        //    webresponse.data.Add(hcEmployerResumeBankVm);
                        //}
                        webresponse.data = hcEmployerResumeBanks.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumeEmployerRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeEmployerViewModel_Get Convert_Model_to_ViewModel(HcResumeEmployer _hc_resume_bank)
        {
            HcResumeEmployerViewModel_Get hcEmployerResumeBankVm = new HcResumeEmployerViewModel_Get();
            try
            {
                hcEmployerResumeBankVm.Rid = _hc_resume_bank.Rid;

                hcEmployerResumeBankVm.EuropeanStandardOccupationId = _hc_resume_bank.EuropeanStandardOccupationId;
                hcEmployerResumeBankVm.Achievement = _hc_resume_bank.Achievement;
                hcEmployerResumeBankVm.IsJobInUae = _hc_resume_bank.IsJobInUae;
                hcEmployerResumeBankVm.StartDate = _hc_resume_bank.FromDate;
                hcEmployerResumeBankVm.Enddate = _hc_resume_bank.ToDate;
                hcEmployerResumeBankVm.EmployerId = _hc_resume_bank.EmployerId;
                hcEmployerResumeBankVm.ResumeId = _hc_resume_bank.ResumeId;
                hcEmployerResumeBankVm.JobTitleId = _hc_resume_bank.DesignationId;
                hcEmployerResumeBankVm.IsCrurrentJob = _hc_resume_bank.Particular;
                hcEmployerResumeBankVm.EmploymentTypeId = _hc_resume_bank.CompanyType;
                hcEmployerResumeBankVm.SequenceNo = _hc_resume_bank.SequenceNo;
                hcEmployerResumeBankVm.CountryId = _hc_resume_bank.CountryId;
                hcEmployerResumeBankVm.JobUaeCityId = _hc_resume_bank.Cityid;
                hcEmployerResumeBankVm.JobIndustryId = _hc_resume_bank.IndustryId;

                hcEmployerResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcEmployerResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcEmployerResumeBankVm.ModifiedDate = _hc_resume_bank.ModifiedDate;
                hcEmployerResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;
                
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeEmployerRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcEmployerResumeBankVm;
        }

        private async void UpdateTotalExperience(long resumeId)
        {
            try
            {
                using (var _con = _iUnitOfWork.Connection)
                {
                    string strQry = "select  sum(temp.totalexp) as totalexperience from ( ";
                    strQry += " select DATEDIFF(MONTH, DATETIME2FROMPARTS(FromYear, FromMonth,1,0,0,0,0,0),";
                    strQry += " DATETIME2FROMPARTS(case when isnull(ToYear, 0) = 0 then YEAR(GETUTCDATE())  else isnull(ToYear, 0) end ,";
                    strQry += " case when isnull(ToMonth,0) = 0 then Month(GETUTCDATE())  else isnull(ToMonth, 0) end ,1,0,0,0,0,0)) as totalexp,isnull(resumeid, 0) as resumeid";
                    strQry += " from HC_RESUME_EMPLOYER with(nolock)";
                    strQry += $" where Is_Deleted='false' and isnull(resumeid, 0) = {resumeId}";
                    strQry += " and isnull(FromYear,0) <> 0 and isnull(FromMonth,0) <> 0 and isnull(ToMonth,0) <> 0 and isnull(ToMonth,0) <> 0)Temp";
                    strQry += " Group by temp.resumeid";

                    var totalExp = await _con.QueryAsync<int>(strQry);

                    HC_RESUME_BANK hC_RESUME_BANK = await _con.GetAsync<HC_RESUME_BANK>(resumeId);
                    hC_RESUME_BANK.TotalExp = totalExp.FirstOrDefault();
                    await _con.UpdateAsync<HC_RESUME_BANK>(hC_RESUME_BANK);
                    //Update type sense index
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(resumeId);

                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(HcResumeEducationRepository)}::{nameof(UpdateTotalExperience)} -- " + ex.Message);
            }

        }

        private async void getLastInsertedId(long resumeId)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync<long>($"select top 1 rid from HC_RESUME_EMPLOYER where ResumeID ={resumeId}  order by Createddate desc ");
                    SetOtherThisIsMycurrentRolFalse(result.FirstOrDefault(), resumeId);
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(HcResumeEmployerRepository)}::{nameof(SetOtherThisIsMycurrentRolFalse)} -- " + ex.Message);
            }
        }

        public async void SetOtherThisIsMycurrentRolFalse(long rid, long resume_id)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update HC_RESUME_EMPLOYER set Particular=0 where RID!={rid} and ResumeId={resume_id}");                   
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(HcResumeEmployerRepository)}::{nameof(SetOtherThisIsMycurrentRolFalse)} -- " + ex.Message);               
            }
        }
    }
}
