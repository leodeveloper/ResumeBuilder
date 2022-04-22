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
    public class HcResumeSkillRepository : IHcResumeSkillRepository
    {
        readonly ILogger<HcResumeSkillRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly IList<LookupViewModel> proficiencyLevelEn, proficiencyLevelAe;

        public HcResumeSkillRepository(ILogger<HcResumeSkillRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            proficiencyLevelEn = HraOpsLookupRepository.getProficiencylevels(0);
            proficiencyLevelAe = HraOpsLookupRepository.getProficiencylevels(1);
        }

        public async Task<WebresponseNoData> Insert(HcResumeSkillViewModel hcSkillResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeSkillType hcSkillResumeBank = new HcResumeSkillType();
                    hcSkillResumeBank.ResumeId = hcSkillResumeBankVm.ResumeId;
                    hcSkillResumeBank.SkillId = hcSkillResumeBankVm.SkillId;
                    hcSkillResumeBank.Status = hcSkillResumeBankVm.Proficiencylevel;

                    hcSkillResumeBank.SkillType = 0;
                    hcSkillResumeBank.SkillCategoryId = 0;
                    hcSkillResumeBank.SkillOccurance = 0;
                    hcSkillResumeBank.SkillText = "";

                    hcSkillResumeBank.Createddate = DateTime.Now;
                    hcSkillResumeBank.Createduserid = userid;

                    await _con.InsertAsync<HcResumeSkillType>(hcSkillResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeSkillRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeSkillViewModel hcSkillResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeSkillType hcSkillResumeBank = await _con.GetAsync<HcResumeSkillType>(rid);
                    hcSkillResumeBank.ResumeId = hcSkillResumeBankVm.ResumeId;
                    hcSkillResumeBank.SkillId = hcSkillResumeBankVm.SkillId;
                    hcSkillResumeBank.Status = hcSkillResumeBankVm.Proficiencylevel;

                    hcSkillResumeBank.Modifieddate = DateTime.Now;
                    hcSkillResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumeSkillType>(hcSkillResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeSkillRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeSkillViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeSkillViewModel_Get> webresponse = new Webresponse<HcResumeSkillViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_SKILL_TYPE set Is_Deleted='true' where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeSkillRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeSkillViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeSkillViewModel_Get> webresponse = new Webresponse<HcResumeSkillViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeSkillType hcSkillResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeSkillType>($"select * from HC_RESUME_SKILL_TYPE where RID={rid} and Is_Deleted='false'");
                    if (hcSkillResumeBank != null)
                    {
                        HcResumeSkillViewModel_Get hcSkillResumeBankVm = Convert_Model_to_ViewModel(hcSkillResumeBank);
                        webresponse.data = hcSkillResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeSkillRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeSkillViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeSkillViewModel_Get>> webresponse = new Webresponse<IList<HcResumeSkillViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    IEnumerable<HcResumeSkillViewModel_Get> hcSkillResumeBanks = await _con.QueryAsync<HcResumeSkillViewModel_Get>($"select Skills.*, Skills.Status as Proficiencylevel, skill.Title as SkillsTypeEn, skillAe.Title as SkillsTypeAe from HC_RESUME_SKILL_TYPE Skills left outer join HCM_AIRLINES skill on Skills.SkillID = skill.EngKeyID and skill.LanguageType = 0 left outer join HCM_AIRLINES skillAe on skills.SkillID = skillAe.EngKeyID and skillAe.LanguageType = 1 where ResumeID ={ resumeId} and Is_Deleted = 'false'");
                    if (hcSkillResumeBanks != null)
                    {
                        webresponse.data = hcSkillResumeBanks.Select(z =>
                        { z.ProficiencylevelEn = proficiencyLevelEn.FirstOrDefault(t => t.EngKeyId == z.Proficiencylevel).Title;
                            z.ProficiencylevelAe = proficiencyLevelAe.FirstOrDefault(t => t.EngKeyId == z.Proficiencylevel).Title;
                            return z; }).ToList();
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
                    _ilogger.LogError($"{nameof(HcResumeSkillRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeSkillViewModel_Get Convert_Model_to_ViewModel(HcResumeSkillType _hc_resume_bank)
        {
            HcResumeSkillViewModel_Get hcSkillResumeBankVm = new HcResumeSkillViewModel_Get();
            try
            {
                hcSkillResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcSkillResumeBankVm.ResumeId = _hc_resume_bank.ResumeId;
                hcSkillResumeBankVm.SkillId = _hc_resume_bank.SkillId;
                hcSkillResumeBankVm.Proficiencylevel = _hc_resume_bank.Status;

                hcSkillResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcSkillResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcSkillResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcSkillResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeSkillRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcSkillResumeBankVm;
        }
    }
}
