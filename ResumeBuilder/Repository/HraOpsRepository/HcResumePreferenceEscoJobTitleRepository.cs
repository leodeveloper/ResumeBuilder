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
    public class HcResumePreferenceEscoJobTitleRepository : IHcResumePreferenceEscoJobTitleRepository
    {
        readonly ILogger<HcResumePreferenceEscoJobTitleRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumePreferenceEscoJobTitleRepository(ILogger<HcResumePreferenceEscoJobTitleRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumePreferenceJobTitleViewModel hcPreferenceJobTitleResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedJobtitle hcPreferenceJobTitleResumeBank = new HcResumePrefferedJobtitle();
                    hcPreferenceJobTitleResumeBank.Resumeid = hcPreferenceJobTitleResumeBankVm.ResumeId;
                    hcPreferenceJobTitleResumeBank.Jobtitleid = hcPreferenceJobTitleResumeBankVm.JobTitleId;
                    hcPreferenceJobTitleResumeBank.Createddate = DateTime.Now;
                    hcPreferenceJobTitleResumeBank.Createduserid = userid;
                    hcPreferenceJobTitleResumeBank.Isdeleted = 0;

                    await _con.InsertAsync<HcResumePrefferedJobtitle>(hcPreferenceJobTitleResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceEscoJobTitleRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumePreferenceJobTitleViewModel hcPreferenceJobTitleResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedJobtitle hcPreferenceJobTitleResumeBank = await _con.GetAsync<HcResumePrefferedJobtitle>(rid);
                    hcPreferenceJobTitleResumeBank.Resumeid = hcPreferenceJobTitleResumeBankVm.ResumeId;
                    hcPreferenceJobTitleResumeBank.Jobtitleid = hcPreferenceJobTitleResumeBankVm.JobTitleId;

                    hcPreferenceJobTitleResumeBank.Modifieddate = DateTime.Now;
                    hcPreferenceJobTitleResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumePrefferedJobtitle>(hcPreferenceJobTitleResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceEscoJobTitleRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumePreferenceJobTitleViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumePreferenceJobTitleViewModel_Get> webresponse = new Webresponse<HcResumePreferenceJobTitleViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_PREFFERED_JOBTITLE set isdeleted=1 where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceEscoJobTitleRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumePreferenceJobTitleViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumePreferenceJobTitleViewModel_Get> webresponse = new Webresponse<HcResumePreferenceJobTitleViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedJobtitle hcPreferenceJobTitleResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumePrefferedJobtitle>($"select * from HC_RESUME_PREFFERED_JOBTITLE where RID={rid} and (isdeleted=0 or  isdeleted is null) ");
                    if (hcPreferenceJobTitleResumeBank != null)
                    {
                        HcResumePreferenceJobTitleViewModel_Get hcPreferenceJobTitleResumeBankVm = Convert_Model_to_ViewModel(hcPreferenceJobTitleResumeBank);
                        webresponse.data = hcPreferenceJobTitleResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumePreferenceEscoJobTitleRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumePreferenceJobTitleViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumePreferenceJobTitleViewModel_Get>> webresponse = new Webresponse<IList<HcResumePreferenceJobTitleViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {


                    string sqlQry = "  select jobtitle.RID,jobtitle.Resumeid,jobtitle.jobtitleid,RJobTitle.JobTitle as JobTitle, RJobTitleAe.JobTitle as JobTitleAe,";
                    sqlQry += " jobtitle.createddate,jobtitle.createduserid,jobtitle.modifieddate,jobtitle.modifieduserid,jobtitle.isdeleted";
                    sqlQry += " from HC_RESUME_PREFFERED_JOBTITLE jobtitle";
                    sqlQry += "  left outer join HCM_JOBTITLE RJobTitle on RJobTitle.EngKeyID = jobtitle.jobtitleid and RJobTitle.LanguageType = 0";
                    sqlQry += "  left outer join HCM_JOBTITLE RJobTitleAe on RJobTitleAe.EngKeyID = jobtitle.jobtitleid and RJobTitleAe.LanguageType = 1";
                    sqlQry += $" where Resumeid = {resumeId} and(isdeleted = 0 or  isdeleted is null)";

                    IEnumerable<HcResumePreferenceJobTitleViewModel_Get> hcPreferenceJobTitleResumeBanks = await _con.QueryAsync<HcResumePreferenceJobTitleViewModel_Get>(sqlQry);
                    if (hcPreferenceJobTitleResumeBanks != null)
                    {
                        webresponse.data = hcPreferenceJobTitleResumeBanks.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumePreferenceEscoJobTitleRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumePreferenceJobTitleViewModel_Get Convert_Model_to_ViewModel(HcResumePrefferedJobtitle _hc_resume_bank)
        {
            HcResumePreferenceJobTitleViewModel_Get hcPreferenceJobTitleResumeBankVm = new HcResumePreferenceJobTitleViewModel_Get();
            try
            {
                hcPreferenceJobTitleResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcPreferenceJobTitleResumeBankVm.ResumeId = _hc_resume_bank.Resumeid;
                hcPreferenceJobTitleResumeBankVm.JobTitleId = _hc_resume_bank.Jobtitleid;


                hcPreferenceJobTitleResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcPreferenceJobTitleResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcPreferenceJobTitleResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcPreferenceJobTitleResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumePreferenceEscoJobTitleRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcPreferenceJobTitleResumeBankVm;
        }
    }
}
