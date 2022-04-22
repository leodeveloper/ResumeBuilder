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
    public class HcResumeLanguagesRepository : IHcResumeLanguagesRepository
    {
        readonly ILogger<HcResumeLanguagesRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly IList<LookupViewModel> proficiencyLevelEn, proficiencyLevelAe;

        public HcResumeLanguagesRepository(ILogger<HcResumeLanguagesRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            proficiencyLevelEn = HraOpsLookupRepository.getProficiencylevels(0);
            proficiencyLevelAe = HraOpsLookupRepository.getProficiencylevels(1);
        }

        public async Task<WebresponseNoData> Insert(HcResumeLanguagesViewModel hcLanguageResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeLanguages hcLanguageResumeBank = new HcResumeLanguages();
                    hcLanguageResumeBank.ResumeId = hcLanguageResumeBankVm.ResumeId;
                    hcLanguageResumeBank.LanguageId = hcLanguageResumeBankVm.LanguageId;
                    hcLanguageResumeBank.Language = "";
                    hcLanguageResumeBank.ProficiencyLevel = hcLanguageResumeBankVm.Proficiencylevel;

                    hcLanguageResumeBank.ReadLevel = hcLanguageResumeBankVm.ReadLevel;
                    hcLanguageResumeBank.WriteLevel = hcLanguageResumeBankVm.WriteLevel;
                    hcLanguageResumeBank.SpeakLevel = hcLanguageResumeBankVm.SpeakLevel;


                    hcLanguageResumeBank.CreatedDate = DateTime.Now;
                    hcLanguageResumeBank.CreatedUserId = userid;

                    await _con.InsertAsync<HcResumeLanguages>(hcLanguageResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeLanguagesRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeLanguagesViewModel hcLanguageResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeLanguages hcLanguageResumeBank = await _con.GetAsync<HcResumeLanguages>(rid);
                    hcLanguageResumeBank.ResumeId = hcLanguageResumeBankVm.ResumeId;
                    hcLanguageResumeBank.LanguageId = hcLanguageResumeBankVm.LanguageId;
                    hcLanguageResumeBank.Language = "";
                    hcLanguageResumeBank.ProficiencyLevel = hcLanguageResumeBankVm.Proficiencylevel;

                    hcLanguageResumeBank.ReadLevel = hcLanguageResumeBankVm.ReadLevel;
                    hcLanguageResumeBank.WriteLevel = hcLanguageResumeBankVm.WriteLevel;
                    hcLanguageResumeBank.SpeakLevel = hcLanguageResumeBankVm.SpeakLevel;

                    hcLanguageResumeBank.ModifiedDate = DateTime.Now;
                    hcLanguageResumeBank.ModifiedUserId = userid;
                    await _con.UpdateAsync<HcResumeLanguages>(hcLanguageResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeLanguagesRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeLanguagesViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeLanguagesViewModel_Get> webresponse = new Webresponse<HcResumeLanguagesViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_LANGUAGES set Is_Deleted='true' where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeLanguagesRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeLanguagesViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeLanguagesViewModel_Get> webresponse = new Webresponse<HcResumeLanguagesViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeLanguages hcLanguageResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeLanguages>($"select * from HC_RESUME_LANGUAGES where RID={rid} and Is_Deleted='false'");
                    if (hcLanguageResumeBank != null)
                    {
                        HcResumeLanguagesViewModel_Get hcLanguageResumeBankVm = Convert_Model_to_ViewModel(hcLanguageResumeBank);
                        webresponse.data = hcLanguageResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeLanguagesRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeLanguagesViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeLanguagesViewModel_Get>> webresponse = new Webresponse<IList<HcResumeLanguagesViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlQry = "select Languages.*,Languages.ProficiencyLevel as Proficiencylevel,lan.Title as LanguageEn, lanae.Title as LanguageAe, ReadLevel, WriteLevel,SpeakLevel ";
                    sqlQry += " from HC_RESUME_LANGUAGES Languages";
                    sqlQry += " left outer join HCM_LANGUAGE lan on Languages.LanguageID = lan.EngkeyID and lan.LanguageType = 0";
                    sqlQry += " left outer join HCM_LANGUAGE lanae on Languages.LanguageID = lanae.EngkeyID and lanae.LanguageType = 1";
                    sqlQry += $" where ResumeID = {resumeId} and Is_Deleted = 'false'";
                    IEnumerable<HcResumeLanguagesViewModel_Get> hcLanguageResumeBanks = await _con.QueryAsync<HcResumeLanguagesViewModel_Get>(sqlQry);
                    if (hcLanguageResumeBanks != null)
                    {
                        webresponse.data = hcLanguageResumeBanks.Select(z =>
                        {
                            z.ProficiencylevelEn = proficiencyLevelEn.FirstOrDefault(t => t.EngKeyId == z.Proficiencylevel).Title;
                            z.ProficiencylevelAe = proficiencyLevelAe.FirstOrDefault(t => t.EngKeyId == z.Proficiencylevel).Title;
                            return z;
                        }).ToList();
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
                    _ilogger.LogError($"{nameof(HcResumeLanguagesRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeLanguagesViewModel_Get Convert_Model_to_ViewModel(HcResumeLanguages _hc_resume_bank)
        {
            HcResumeLanguagesViewModel_Get hcLanguageResumeBankVm = new HcResumeLanguagesViewModel_Get();
            try
            {
                hcLanguageResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcLanguageResumeBankVm.ResumeId = _hc_resume_bank.ResumeId;
                hcLanguageResumeBankVm.LanguageId = _hc_resume_bank.LanguageId;
                hcLanguageResumeBankVm.Proficiencylevel = _hc_resume_bank.ProficiencyLevel;
                hcLanguageResumeBankVm.ReadLevel = _hc_resume_bank.ReadLevel;
                hcLanguageResumeBankVm.WriteLevel = _hc_resume_bank.WriteLevel;
                hcLanguageResumeBankVm.SpeakLevel = _hc_resume_bank.SpeakLevel;
                hcLanguageResumeBankVm.CreatedDate = _hc_resume_bank.CreatedDate;
                hcLanguageResumeBankVm.CreatedUserId = _hc_resume_bank.CreatedUserId;
                hcLanguageResumeBankVm.ModifiedDate = _hc_resume_bank.ModifiedDate;
                hcLanguageResumeBankVm.ModifiedUserId = _hc_resume_bank.ModifiedUserId;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeLanguagesRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcLanguageResumeBankVm;
        }
    }
}
