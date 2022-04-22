using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using System;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeBuilder.Models.HraOpsModels;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HraOpsLookupRepository : IHraOpsLookupRepository
    {
        readonly ILogger<HraOpsLookupRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HraOpsLookupRepository(ILogger<HraOpsLookupRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetNationalities(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var nationalities = await _con.QueryAsync<HcmNationality>($"select EngKeyID,Title from  HCM_NATIONALITY where LanguageType={languageType}");
                    webresponse.data = nationalities.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetInternationalCountries(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var nationalities = await _con.QueryAsync<HcmCompanyType>($"select EngKeyID,CompanyTitle from  HCM_COMPANY_TYPE where LanguageType={languageType}");
                    webresponse.data = nationalities.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.CompanyTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetInternationalCitesByCountryId(int countryId)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var nationalities = await _con.QueryAsync<HcmCompanyList>($"select Rid,Title from  HCM_COMPANY_LIST  where CompanyManagerID={countryId}");
                    webresponse.data = nationalities.Select(i => new LookupViewModel { EngKeyId = i.Rid, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetCountries(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var countries = await _con.QueryAsync<HcmCountries>($"  select EngKeyID,CountryTitle  from HCM_COUNTRIES where LanguageType={languageType}");
                    webresponse.data = countries.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.CountryTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetCountries)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetStates(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var states = await _con.QueryAsync<HcmStates>($"select EngKeyID,StateTitle  from HCM_STATES where LanguageType={languageType}");
                    webresponse.data = states.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.StateTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetStates)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetLocations(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var locations = await _con.QueryAsync<HcmResumeLocations>($"select EngKeyID,LocationTitle  from HCM_RESUME_LOCATIONS where LanguageType={languageType}");
                    webresponse.data = locations.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.LocationTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetLocations)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }



        public async Task<Webresponse<IList<LookupViewModel>>> GetPrefferedLocation(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var locations = await _con.QueryAsync<HcmResumeLocations>($"select EngKeyID,LocationTitle  from HCM_RESUME_LOCATIONS where LanguageType={languageType} and  ReqLocationStatus=1");
                    webresponse.data = locations.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.LocationTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetLocations)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetDrivingLicenceType(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmDrivingLicense>($"select EngKeyID,Title from  HCM_DRIVING_LICENSE where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngkeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetMilitaryBatch(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmMilitarybatchno>($"select EngKeyID,Title from  HCM_MILITARYBATCHNO where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetMartitalStatus(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmMaritalStatus>($"select EngKeyID,Title from  HCM_MARITAL_STATUS where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetEducationGroup(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmEducationGroup>($"select EngKeyID,GroupTitle from  HCM_EDUCATION_GROUP where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.GroupTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetEducationType(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmEducationTypes>($"select EngKeyID,EducationType from  HCM_EDUCATION_TYPES where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.EducationType }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetEducationMajor(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmEducationSpecializationTypes>($"select EngKeyID,SpecializationTitle from  HCM_EDUCATION_SPECIALIZATION_TYPES where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.SpecializationTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetNationalities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetUniversity(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmUniversity>($"select EngKeyID,Title from HCM_UNIVERSITY where LanguageType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetUniversity)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetGrade(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var drivingLicensesType = await _con.QueryAsync<HcmGrade>($"select EngKeyID,Title from HCM_GRADE where LangugaeType={languageType}");
                    webresponse.data = drivingLicensesType.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetUniversity)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetEmployer(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmEmployers = await _con.QueryAsync<HcmEmployer>($"select Rid,Title from Hcm_Employer ");
                    webresponse.data = hcmEmployers.Select(i => new LookupViewModel { EngKeyId = i.Rid, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetEmployer)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetJobTitle(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmJobtitles = await _con.QueryAsync<HcmJobtitle>($"select EngKeyId,JobTitle from HCM_JOBTITLE  where LanguageType={languageType}");
                    webresponse.data = hcmJobtitles.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.JobTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetJobTitle)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetJobIndustry(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmIndustryTypes = await _con.QueryAsync<HcmIndustryType>($"select EngKeyId,IndustryTypeTitle from HCM_INDUSTRY_TYPE  where LanguageType={languageType}");
                    webresponse.data = hcmIndustryTypes.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.IndustryTypeTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetJobIndustry)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetStaffTitle(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmStaffingTitles = await _con.QueryAsync<HcmStaffingTitle>($"select EngkeyId,StaffingTitle from HCM_STAFFING_TITLE  where LanguageType={languageType}");
                    webresponse.data = hcmStaffingTitles.Select(i => new LookupViewModel { EngKeyId = i.EngkeyId, Title = i.StaffingTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetJobIndustry)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetSkillGroup(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmOccupations = await _con.QueryAsync<HcmSkillGroup>($"select EngkeyId,SkillGroupTitle from HCM_SKILL_GROUP  where LanguageType={languageType}");
                    webresponse.data = hcmOccupations.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.SkillGroupTitle }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetJobIndustry)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }


        //public async Task<Webresponse<IList<LookupViewModel>>> GetSkillGroup(int languageType)
        //{
        //    Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
        //    using (var _con = _iUnitOfWork.Connection)
        //    {
        //        try
        //        {
        //            var hcmOccupations = await _con.QueryAsync<HcmSkillGroup>($"select EngkeyId,SkillGroupTitle from HCM_SKILL_GROUP  where LanguageType={languageType}");
        //            webresponse.data = hcmOccupations.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.SkillGroupTitle }).ToList();
        //            webresponse.status = APIStatus.success;
        //        }
        //        catch (Exception ex)
        //        {
        //            _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetJobIndustry)} ::: {ex.Message}");
        //            webresponse.status = APIStatus.error;
        //            webresponse.message = ex.Message;
        //        }
        //    }
        //    return webresponse;
        //}


        public async Task<Webresponse<IList<LookupViewModel>>> EuropeanStandardOccupation(int languageType, long skillGroupId)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmOccupations = await _con.QueryAsync<HcmOccupation>($" select O.EngkeyId,O.Title from HCM_OCCUPATION O, [HCM_SKILL_OCCUPATION] SO  where O.LanguageType={languageType} and O.EngKeyID = SO.OccupationsID and SO.SkillGroupID={skillGroupId}");
                    webresponse.data = hcmOccupations.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetJobIndustry)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> SkillTypes(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmSkillTypes = await _con.QueryAsync<HcmSkillTypes>($"select Rid,SkillType from Hcm_Skill_Types");
                    webresponse.data = hcmSkillTypes.Select(i => new LookupViewModel { EngKeyId = i.Rid, Title = i.SkillType }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(SkillTypes)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> ToolsKnowledge(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    var hcmSkillTypes = await _con.QueryAsync<HcmAirlines>($"select Rid,Title from HCM_AIRLINES where LanguageType={languageType}");
                    webresponse.data = hcmSkillTypes.Select(i => new LookupViewModel { EngKeyId = i.Rid, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(SkillTypes)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> Proficiencylevels(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    webresponse.data = getProficiencylevels(languageType);
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(SkillTypes)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public static IList<LookupViewModel> getProficiencylevels(int languageType)
        {
            var hcmProficiencylevels = new List<LookupViewModel>();
            if(languageType == 0)
            {
                hcmProficiencylevels.Add(new LookupViewModel { EngKeyId = 1, Title = "Beginner" });
                hcmProficiencylevels.Add(new LookupViewModel { EngKeyId = 2, Title = "Intermediate" });
                hcmProficiencylevels.Add(new LookupViewModel { EngKeyId = 3, Title = "Expert" });
            }
            else
            {
                hcmProficiencylevels.Add(new LookupViewModel { EngKeyId = 1, Title = "مبتدئ" });
                hcmProficiencylevels.Add(new LookupViewModel { EngKeyId = 2, Title = "متوسط" });
                hcmProficiencylevels.Add(new LookupViewModel { EngKeyId = 3, Title = "خبير" });
            }
            
            return hcmProficiencylevels;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> Disablities(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmSkillTypes = await _con.QueryAsync<HcmBenefitName>($"select EngKeyID,Title from HCM_BENEFIT_NAME where LanguageType={languageType}");
                    webresponse.data = hcmSkillTypes.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(Disablities)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> BeneficiaryName(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmBeneficiaryNames = await _con.QueryAsync<HcmBeneficiaryName>($"select EngKeyID,Title from HCM_BENEFICIARY_NAME where LanguageType={languageType}");
                    webresponse.data = hcmBeneficiaryNames.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(BeneficiaryName)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetCoach()
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcUsers = await _con.QueryAsync<HcUsers>($"select [RID],[UserName],[FirstName],[LastName],[Is_Coach] from HC_USERS where Is_Coach='true' order by FirstName");
                    webresponse.data = hcUsers.Select(i => new LookupViewModel { EngKeyId = i.Rid, Title = $"{i.UserName} -- {i.FirstName}" }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetCoach)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetLanguage(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmLanguageTypes = await _con.QueryAsync<HcmLanguage>($"select EngKeyID,Title from HCM_LANGUAGE where LanguageType={languageType}");
                    webresponse.data = hcmLanguageTypes.Select(i => new LookupViewModel { EngKeyId = i.EngkeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(SkillTypes)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetCertificateType(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmCertifications = await _con.QueryAsync<HcmCertification>($"select EngKeyID,Title from HCM_CERTIFICATION where LanguageType={languageType}");
                    webresponse.data = hcmCertifications.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetCertificateType)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetCertificateProvider()
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmTrainingProviders = await _con.QueryAsync<HcmTrainingProviders>($"select RID,ProviderName from HCM_TRAINING_PROVIDERS");
                    webresponse.data = hcmTrainingProviders.Select(i => new LookupViewModel { EngKeyId = i.Rid, Title = i.ProviderName }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetCertificateProvider)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetReferenceType(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmCertifications = await _con.QueryAsync<HcmResumeReferenceType>($"select EngKeyID,Title from HCM_RESUME_REFERENCE_TYPE where LanguageType={languageType}");
                    webresponse.data = hcmCertifications.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetCertificateType)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetStatusReason(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmCertifications = await _con.QueryAsync<HcmResumeStatusReason>($"select EngKeyId,Title from HCM_RESUME_STATUS_REASON where LanguageType={languageType}");
                    webresponse.data = hcmCertifications.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetStatusReason)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetEngagementStatus(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmCertifications = await _con.QueryAsync<HcmResumeEngagementStatus>($"select EngKeyId,Title from HCM_RESUME_ENGAGEMENT_STATUS where LanguageType={languageType}");
                    webresponse.data = hcmCertifications.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetStatusReason)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<LookupViewModel>>> GetJPCStatus(int languageType)
        {
            Webresponse<IList<LookupViewModel>> webresponse = new Webresponse<IList<LookupViewModel>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {

                    var hcmCertifications = await _con.QueryAsync<HcmResumeJpcStatus>($"select EngKeyId,Title from HCM_RESUME_JPC_STATUS where LanguageType={languageType}");
                    webresponse.data = hcmCertifications.Select(i => new LookupViewModel { EngKeyId = i.EngKeyId, Title = i.Title }).ToList();
                    webresponse.status = APIStatus.success;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HraOpsLookupRepository)} :::: {nameof(GetStatusReason)} ::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }


    }
}
