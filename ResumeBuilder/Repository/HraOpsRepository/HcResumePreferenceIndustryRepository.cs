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
    public class HcResumePreferenceIndustryRepository : IHcResumePreferenceIndustryRepository
    {
        readonly ILogger<HcResumePreferenceIndustryRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumePreferenceIndustryRepository(ILogger<HcResumePreferenceIndustryRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumePreferenceIndustryViewModel hcPreferenceIndustryResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedIndustry hcPreferenceIndustryResumeBank = new HcResumePrefferedIndustry();
                    hcPreferenceIndustryResumeBank.Resumeid = hcPreferenceIndustryResumeBankVm.ResumeId;
                    hcPreferenceIndustryResumeBank.Industryid = hcPreferenceIndustryResumeBankVm.IndustryId;
                    hcPreferenceIndustryResumeBank.Createddate = DateTime.Now;
                    hcPreferenceIndustryResumeBank.Createduserid = userid;
                    hcPreferenceIndustryResumeBank.Isdeleted = 0;

                    await _con.InsertAsync<HcResumePrefferedIndustry>(hcPreferenceIndustryResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceIndustryRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumePreferenceIndustryViewModel hcPreferenceIndustryResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedIndustry hcPreferenceIndustryResumeBank = await _con.GetAsync<HcResumePrefferedIndustry>(rid);
                    hcPreferenceIndustryResumeBank.Resumeid = hcPreferenceIndustryResumeBankVm.ResumeId;
                    hcPreferenceIndustryResumeBank.Industryid = hcPreferenceIndustryResumeBankVm.IndustryId;

                    hcPreferenceIndustryResumeBank.Modifieddate = DateTime.Now;
                    hcPreferenceIndustryResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumePrefferedIndustry>(hcPreferenceIndustryResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceIndustryRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumePreferenceIndustryViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumePreferenceIndustryViewModel_Get> webresponse = new Webresponse<HcResumePreferenceIndustryViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_PREFFERED_INDUSTRY set isdeleted=1 where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceIndustryRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumePreferenceIndustryViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumePreferenceIndustryViewModel_Get> webresponse = new Webresponse<HcResumePreferenceIndustryViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedIndustry hcPreferenceIndustryResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumePrefferedIndustry>($"select * from HC_RESUME_PREFFERED_INDUSTRY where RID={rid} and (isdeleted=0 or  isdeleted is null) ");
                    if (hcPreferenceIndustryResumeBank != null)
                    {
                        HcResumePreferenceIndustryViewModel_Get hcPreferenceIndustryResumeBankVm = Convert_Model_to_ViewModel(hcPreferenceIndustryResumeBank);
                        webresponse.data = hcPreferenceIndustryResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumePreferenceIndustryRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumePreferenceIndustryViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumePreferenceIndustryViewModel_Get>> webresponse = new Webresponse<IList<HcResumePreferenceIndustryViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlQry = "  select industry.RID,industry.Resumeid,industry.industryid,RIndustry.IndustryTypeTitle as Industry, RIndustryAe.IndustryTypeTitle as IndustryAe, ";
                    sqlQry += " industry.createddate,industry.createduserid,industry.modifieddate,industry.modifieduserid,industry.isdeleted ";
                    sqlQry += " from HC_RESUME_PREFFERED_INDUSTRY industry";
                    sqlQry += " left outer join HCM_INDUSTRY_TYPE RIndustry on RIndustry.EngKeyID = industry.industryid and RIndustry.LanguageType = 0";
                    sqlQry += " left outer join HCM_INDUSTRY_TYPE RIndustryAe on RIndustryAe.EngKeyID = industry.industryid and RIndustryAe.LanguageType = 1";
                    sqlQry += $" where Resumeid = {resumeId} and(isdeleted = 0 or  isdeleted is null)";
                    IEnumerable<HcResumePreferenceIndustryViewModel_Get> hcPreferenceIndustryResumeBanks = await _con.QueryAsync<HcResumePreferenceIndustryViewModel_Get>(sqlQry);
                    if (hcPreferenceIndustryResumeBanks != null)
                    {
                        webresponse.data = hcPreferenceIndustryResumeBanks.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumePreferenceIndustryRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumePreferenceIndustryViewModel_Get Convert_Model_to_ViewModel(HcResumePrefferedIndustry _hc_resume_bank)
        {
            HcResumePreferenceIndustryViewModel_Get hcPreferenceIndustryResumeBankVm = new HcResumePreferenceIndustryViewModel_Get();
            try
            {
                hcPreferenceIndustryResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcPreferenceIndustryResumeBankVm.ResumeId = _hc_resume_bank.Resumeid;
                hcPreferenceIndustryResumeBankVm.IndustryId = _hc_resume_bank.Industryid;


                hcPreferenceIndustryResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcPreferenceIndustryResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcPreferenceIndustryResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcPreferenceIndustryResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumePreferenceIndustryRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcPreferenceIndustryResumeBankVm;
        }
    }
}
