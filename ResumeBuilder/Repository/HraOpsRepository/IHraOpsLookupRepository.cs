using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHraOpsLookupRepository
    {
        Task<Webresponse<IList<LookupViewModel>>> GetCountries(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetInternationalCountries(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetInternationalCitesByCountryId(int countryId);
        Task<Webresponse<IList<LookupViewModel>>> GetDrivingLicenceType(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetLocations(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetPrefferedLocation(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetNationalities(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetStates(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetMilitaryBatch(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetMartitalStatus(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetEducationGroup(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetEducationType(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetEducationMajor(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetUniversity(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetGrade(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetEmployer(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetJobTitle(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetJobIndustry(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetStaffTitle(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetSkillGroup(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> EuropeanStandardOccupation(int languageType,  long skillGroupId);
        Task<Webresponse<IList<LookupViewModel>>> SkillTypes(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> ToolsKnowledge(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> Disablities(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> BeneficiaryName(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> Proficiencylevels(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetCoach();
        Task<Webresponse<IList<LookupViewModel>>> GetLanguage(int languageType);

        Task<Webresponse<IList<LookupViewModel>>> GetCertificateType(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetCertificateProvider();
        Task<Webresponse<IList<LookupViewModel>>> GetReferenceType(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetStatusReason(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetEngagementStatus(int languageType);
        Task<Webresponse<IList<LookupViewModel>>> GetJPCStatus(int languageType);


    }
}