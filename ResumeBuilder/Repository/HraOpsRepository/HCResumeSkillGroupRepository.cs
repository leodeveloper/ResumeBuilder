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
    public class HCResumeSkillGroupRepository : IHCResumeSkillGroupRepository
    {
        readonly ILogger<HCResumeSkillGroupRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly IList<LookupViewModel> proficiencyLevelEn, proficiencyLevelAe;

        public HCResumeSkillGroupRepository(ILogger<HCResumeSkillGroupRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            proficiencyLevelEn = HraOpsLookupRepository.getProficiencylevels(0);
            proficiencyLevelAe = HraOpsLookupRepository.getProficiencylevels(1);
        }

        public async Task<WebresponseNoData> Insert(HcResumeSkillGroupViewModel hcSkillGroupResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeSkillGroup hcSkillGroupResumeBank = new HcResumeSkillGroup();
                    hcSkillGroupResumeBank.ResumeId = hcSkillGroupResumeBankVm.ResumeId;
                    hcSkillGroupResumeBank.SkillGroupId = hcSkillGroupResumeBankVm.SkillGroup;
                    hcSkillGroupResumeBank.OccupationId = hcSkillGroupResumeBankVm.Occupation;
                    hcSkillGroupResumeBank.LevelId = hcSkillGroupResumeBankVm.Proficiencylevel;



                    hcSkillGroupResumeBank.Createddate = DateTime.Now;
                    hcSkillGroupResumeBank.Createduserid = userid;

                    await _con.InsertAsync<HcResumeSkillGroup>(hcSkillGroupResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HCResumeSkillGroupRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeSkillGroupViewModel hcSkillGroupResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeSkillGroup hcSkillGroupResumeBank = await _con.GetAsync<HcResumeSkillGroup>(rid);
                    hcSkillGroupResumeBank.ResumeId = hcSkillGroupResumeBankVm.ResumeId;
                    hcSkillGroupResumeBank.SkillGroupId = hcSkillGroupResumeBankVm.SkillGroup;
                    hcSkillGroupResumeBank.OccupationId = hcSkillGroupResumeBankVm.Occupation;
                    hcSkillGroupResumeBank.LevelId = hcSkillGroupResumeBankVm.Proficiencylevel;

                    hcSkillGroupResumeBank.Modifieddate = DateTime.Now;
                    hcSkillGroupResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumeSkillGroup>(hcSkillGroupResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HCResumeSkillGroupRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeSkillGroupViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeSkillGroupViewModel_Get> webresponse = new Webresponse<HcResumeSkillGroupViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_SKILL_GROUP set Is_Deleted='true' where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HCResumeSkillGroupRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeSkillGroupViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeSkillGroupViewModel_Get> webresponse = new Webresponse<HcResumeSkillGroupViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeSkillGroup hcSkillGroupResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumeSkillGroup>($"select * from HC_RESUME_SKILL_GROUP where RID={rid} and Is_Deleted='false'");
                    if (hcSkillGroupResumeBank != null)
                    {
                        HcResumeSkillGroupViewModel_Get hcSkillGroupResumeBankVm = Convert_Model_to_ViewModel(hcSkillGroupResumeBank);
                        webresponse.data = hcSkillGroupResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HCResumeSkillGroupRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeSkillGroupViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeSkillGroupViewModel_Get>> webresponse = new Webresponse<IList<HcResumeSkillGroupViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    string strQry = "select SkillGroups.*,SkillGroups.levelID as Proficiencylevel,skill.SkillGroupTitle as SkillGroupEn, skillAe.SkillGroupTitle as SkillGroupAe, ";
                    strQry += " Occupation.Title as OccupationEn, OccupationAe.Title as OccupationAe";
                    strQry += " from HC_RESUME_SKILL_GROUP SkillGroups";
                    strQry += " left outer join HCM_SKILL_GROUP skill on SkillGroups.SkillGroupID = skill.EngKeyID and skill.LanguageType = 0";
                    strQry += " left outer join HCM_SKILL_GROUP skillAe on SkillGroups.SkillGroupID = skillAe.EngKeyID and skillAe.LanguageType = 1";


                    strQry += " left outer join HCM_OCCUPATION Occupation on SkillGroups.OccupationID = Occupation.EngKeyID and Occupation.LanguageType = 0";
                    strQry += " left outer join HCM_OCCUPATION OccupationAe on SkillGroups.OccupationID = OccupationAe.EngKeyID and OccupationAe.LanguageType = 1";
                    strQry += $" where ResumeID={resumeId} and SkillGroups.Is_Deleted='false'";

                    IEnumerable<HcResumeSkillGroupViewModel_Get> hcSkillGroupResumeBanks = await _con.QueryAsync<HcResumeSkillGroupViewModel_Get>(strQry);
                    if (hcSkillGroupResumeBanks != null)
                    {
                        webresponse.data = hcSkillGroupResumeBanks.Select(z =>
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
                    _ilogger.LogError($"{nameof(HCResumeSkillGroupRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeSkillGroupViewModel_Get Convert_Model_to_ViewModel(HcResumeSkillGroup _hc_resume_bank)
        {
            HcResumeSkillGroupViewModel_Get hcSkillGroupResumeBankVm = new HcResumeSkillGroupViewModel_Get();
            try
            {
                hcSkillGroupResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcSkillGroupResumeBankVm.ResumeId = _hc_resume_bank.ResumeId;
                hcSkillGroupResumeBankVm.SkillGroup = _hc_resume_bank.SkillGroupId;
                hcSkillGroupResumeBankVm.Occupation = _hc_resume_bank.OccupationId;
                hcSkillGroupResumeBankVm.Proficiencylevel = _hc_resume_bank.LevelId;

                hcSkillGroupResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcSkillGroupResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcSkillGroupResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcSkillGroupResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HCResumeSkillGroupRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcSkillGroupResumeBankVm;
        }
    }
}
