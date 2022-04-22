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
    public class HcResumeBeneficiaryRepository : IHcResumeBeneficiaryRepository
    {
        readonly ILogger<HcResumeBeneficiaryRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumeBeneficiaryRepository(ILogger<HcResumeBeneficiaryRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumeBeneficiaryViewModel hcBeneficiaryResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBeneficiary hcBeneficiaryResumeBank = new HcResumeBeneficiary();
                    hcBeneficiaryResumeBank.Resumeid = hcBeneficiaryResumeBankVm.ResumeId;
                    hcBeneficiaryResumeBank.BeneficiaryNameId = hcBeneficiaryResumeBankVm.BeneficiaryNameId;
                    hcBeneficiaryResumeBank.Createddate = DateTime.Now;
                    hcBeneficiaryResumeBank.Createduserid = userid;
                    hcBeneficiaryResumeBank.Isdeleted = 0;

                    await _con.InsertAsync<HcResumeBeneficiary>(hcBeneficiaryResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeBeneficiaryRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeBeneficiaryViewModel hcBeneficiaryResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBeneficiary hcBeneficiaryResumeBank = await _con.GetAsync<HcResumeBeneficiary>(rid);
                    hcBeneficiaryResumeBank.Resumeid = hcBeneficiaryResumeBankVm.ResumeId;
                    hcBeneficiaryResumeBank.BeneficiaryNameId = hcBeneficiaryResumeBankVm.BeneficiaryNameId;

                    hcBeneficiaryResumeBank.Modifieddate = DateTime.Now;
                    hcBeneficiaryResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumeBeneficiary>(hcBeneficiaryResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeBeneficiaryRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeBeneficiaryViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeBeneficiaryViewModel_Get> webresponse = new Webresponse<HcResumeBeneficiaryViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_Beneficiary set isdeleted=1 where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeBeneficiaryRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeBeneficiaryViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeBeneficiaryViewModel_Get> webresponse = new Webresponse<HcResumeBeneficiaryViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeBeneficiary hcBeneficiaryResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeBeneficiary>($"select * from HC_RESUME_Beneficiary where RID={rid} and (isdeleted=0 or  isdeleted is null) ");
                    if (hcBeneficiaryResumeBank != null)
                    {
                        HcResumeBeneficiaryViewModel_Get hcBeneficiaryResumeBankVm = Convert_Model_to_ViewModel(hcBeneficiaryResumeBank);
                        webresponse.data = hcBeneficiaryResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeBeneficiaryRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeBeneficiaryViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeBeneficiaryViewModel_Get>> webresponse = new Webresponse<IList<HcResumeBeneficiaryViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    IEnumerable<HcResumeBeneficiaryViewModel_Get> hcBeneficiaryResumeBanks = await _con.QueryAsync<HcResumeBeneficiaryViewModel_Get>($"select RBeneficiary.*, Beneficiary.Title as BeneficiaryNameTitle from HC_RESUME_Beneficiary RBeneficiary, HCM_BENEFICIARY_NAME Beneficiary where RBeneficiary.BeneficiaryNameId = Beneficiary.EngKeyID and Beneficiary.LanguageType = 0 and  Resumeid = {resumeId} and (isdeleted = 0 or  isdeleted is null) ");
                    if (hcBeneficiaryResumeBanks != null)
                    {
                        webresponse.data = hcBeneficiaryResumeBanks.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumeBeneficiaryRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeBeneficiaryViewModel_Get Convert_Model_to_ViewModel(HcResumeBeneficiary _hc_resume_bank)
        {
            HcResumeBeneficiaryViewModel_Get hcBeneficiaryResumeBankVm = new HcResumeBeneficiaryViewModel_Get();
            try
            {
                hcBeneficiaryResumeBankVm.RID = _hc_resume_bank.Rid;
                hcBeneficiaryResumeBankVm.ResumeId = _hc_resume_bank.Resumeid;
                hcBeneficiaryResumeBankVm.BeneficiaryNameId = _hc_resume_bank.BeneficiaryNameId;


                hcBeneficiaryResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcBeneficiaryResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcBeneficiaryResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcBeneficiaryResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeBeneficiaryRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcBeneficiaryResumeBankVm;
        }
    }
}
