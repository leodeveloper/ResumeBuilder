using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class AppSettings
    {
        public string AuthenticationAPI { get; set; }
        public string LogoutUrl { get; set; }
        public string samlLogURl { get; set; }
        public string EntitiesAPI { get; set; }
        public string AssessmentAPI { get; set; }
        public string TrainingAPI { get; set; }
        public string TypeSenceApi { get; set; }
    }

    public class OracleAppSettings
    {
        public string dbConnectionString { get; set; }
        public string dbTNSEntry { get; set; }
        public string dbDBName { get; set; }
        public string accessToken { get; set; }
        public Int64 createduserid { get; set; }

        public string dbHRMSConnectionString { get; set; }
        public string dbHRMSTNSEntry { get; set; }
        public string dbHRMSDBName { get; set; }

    }

    public class JobSeekerRoles
    {
        public string AdminRole { get; set; }
        public string UserRole { get; set; }
    }

    public class DBConnectionStrings
    {
        public string DatabaseConnection { get; set; }
    }

    public class MongoDBSettings
    {     
        public string connectionstring { get; set; }
        public string dbName { get; set; }
    }

    public class LookUpApiUrl
    {
        public string JobIndustryApiUrl { get; set; }
        public string JobCategoryApiUrl { get; set; }
        public string SpecialNeedApiUrl { get; set; }
        public string CityApiUrl { get; set; }
        public string LanguageApiUrl { get; set; }
        public string LocationApiUrl { get; set; }
        public string EmiratesApiUrl { get; set; }
        public string DesignationApiUrl { get; set; }
        public string EducationGroupApiUrl { get; set; }
        public string EducationTypeApiUrl { get; set; }
        public string EducationMajorApiUrl { get; set; }
        public string EducationApiUrl { get; set; }
        public string CourseApiUrl { get; set; }
        public string EmployerApiUrl { get; set; }
        public string ParticularApiUrl { get; set; }
        public string SourceTypeApiUrl { get; set; }
        public string UniversityApiUrl { get; set; }
        public string UniversityTypeApiUrl { get; set; }
        public string TrainingCenterApiUrl { get; set; }
        public string PdfApiUrl { get; set; }
        public string VacancyApiUrl { get; set; }
        public string CompanyApiUrl { get; set; }
        public string JobApplicationUrl { get; set; }
        public string GetSkillGroupsApiUrl { get; set; }
        public string GetSkillGroupOccupationsUrl { get; set; }
        public string GetOccupationsUrl { get; set; }
        public string GetToolsKnowledgeLookupUrl { get; set; }
        public string CertificateTypeApiUrl { get; set; }

        public string CountryApiUrl { get; set; }
        //International cities
        public string CountryCityApiUrl { get; set; }
        public string InstituteApiUrl { get; set; }
    }
}
