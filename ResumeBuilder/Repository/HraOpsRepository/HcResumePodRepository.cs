using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HcResumePodRepository : IHcResumePodRepository
    {
        readonly ILogger<HcResumePodRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumePodRepository(ILogger<HcResumePodRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumePodViewModel hcPodResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBenefits hcPodResumeBank = new HcResumeBenefits();
                    hcPodResumeBank.Resumeid = hcPodResumeBankVm.ResumeId;
                    hcPodResumeBank.Benefitid = hcPodResumeBankVm.Benefitid;
                    hcPodResumeBank.Createddate = DateTime.Now;
                    hcPodResumeBank.Createduserid = userid;
                    hcPodResumeBank.Isdeleted = 0;

                    await _con.InsertAsync<HcResumeBenefits>(hcPodResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePodRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumePodViewModel hcPodResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBenefits hcPodResumeBank = await _con.GetAsync<HcResumeBenefits>(rid);
                    hcPodResumeBank.Resumeid = hcPodResumeBankVm.ResumeId;
                    hcPodResumeBank.Benefitid = hcPodResumeBankVm.Benefitid;

                    hcPodResumeBank.Modifieddate = DateTime.Now;
                    hcPodResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumeBenefits>(hcPodResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePodRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumePodViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumePodViewModel_Get> webresponse = new Webresponse<HcResumePodViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_Benefits set isdeleted=1 where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePodRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumePodViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumePodViewModel_Get> webresponse = new Webresponse<HcResumePodViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBenefits hcPodResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeBenefits>($"select * from HC_RESUME_Benefits where RID={rid} and (isdeleted=0 or  isdeleted is null) ");
                    if (hcPodResumeBank != null)
                    {
                        HcResumePodViewModel_Get hcPodResumeBankVm = Convert_Model_to_ViewModel(hcPodResumeBank);
                        webresponse.data = hcPodResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumePodRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumePodViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumePodViewModel_Get>> webresponse = new Webresponse<IList<HcResumePodViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    IEnumerable<HcResumePodViewModel_Get> hcPodResumeBanks = await _con.QueryAsync<HcResumePodViewModel_Get>($"select RBenifit.*, Benifit.Title as BenefitTitle from HC_RESUME_Benefits RBenifit, HCM_BENEFIT_NAME Benifit where RBenifit.Benefitid = Benifit.EngKeyID and Benifit.LanguageType=0 and  Resumeid={resumeId} and (isdeleted=0 or  isdeleted is null) ");
                    if (hcPodResumeBanks != null)
                    {
                        webresponse.data = hcPodResumeBanks.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumePodRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumePodViewModel_Get Convert_Model_to_ViewModel(HcResumeBenefits _hc_resume_bank)
        {
            HcResumePodViewModel_Get hcPodResumeBankVm = new HcResumePodViewModel_Get();
            try
            {
                hcPodResumeBankVm.RID = _hc_resume_bank.Rid;
                hcPodResumeBankVm.ResumeId = _hc_resume_bank.Resumeid;
                hcPodResumeBankVm.Benefitid = _hc_resume_bank.Benefitid;


                hcPodResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcPodResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcPodResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcPodResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumePodRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcPodResumeBankVm;
        }
    }
}
