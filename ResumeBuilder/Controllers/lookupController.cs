using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class lookupController : ControllerBase
    {
        private readonly ILogger<lookupController> _logger;
        private readonly IOptions<LookUpApiUrl> _appSettings;
        private IHttpClientFactory _httpClientFactory;
        private readonly IlookupRespository _ilookupRespository;
        readonly IResumeRepository _iResumeRepository;
       

        public lookupController(IResumeRepository iResumeRepository,ILogger<lookupController> logger, IOptions<LookUpApiUrl> appSettings, IHttpClientFactory httpClientFactory, IlookupRespository ilookupRespository)
        {
            _logger = logger;
            _appSettings = appSettings;
            _httpClientFactory = httpClientFactory;
            _ilookupRespository = ilookupRespository;
            _iResumeRepository = iResumeRepository;
        }

        [HttpGet("CheckEmiratesIdUniqueKey/{emiratesId}")]
        public async Task<IActionResult> CheckEmiratesIdUniqueKey(string emiratesId)
        {
            Webresponse<Resume> webresponseResume = await _iResumeRepository.GetResumeByEmiratesId(emiratesId);
            if (webresponseResume.status == APIStatus.NotFound)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        // GET: api/<lookupController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "api is running", "" };
        }

        [HttpGet("GetSpecialNeed")]
        public async Task<IActionResult> GetSpecialNeed()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<SpecialNeedLookup> countrys = new List<SpecialNeedLookup>();
                try
                {
                    countrys = await _httpClient.GetFromJsonAsync<List<SpecialNeedLookup>>($"{_appSettings.Value.SpecialNeedApiUrl}");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetSpecialNeed lookup city api is down");
                }
                return Ok(countrys);
            }
        }

        [HttpGet("GetCountry")]
        public async Task<IActionResult> GetCountry()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Country> countrys = new List<Country>();
                try
                {
                    countrys = await _httpClient.GetFromJsonAsync<List<Country>>($"{_appSettings.Value.CountryApiUrl}");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetCountry lookup city api is down");
                }
                return Ok(countrys);
            }
        }

        [HttpGet("GetCity")]
        public async Task<IActionResult> GetCity()
        {
            using(var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<City> locations = new List<City>();
                try
                {
                    locations = await _httpClient.GetFromJsonAsync<List<City>>($"{_appSettings.Value.CityApiUrl}/get");
                    
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetCity lookup city api is down");
                }
                return Ok(locations);
            }           
        }

        [HttpGet("GetLanguage")]
        public async Task<IActionResult> GetLanguage()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Language> locations = new List<Language>();
                try
                {
                    locations = await _httpClient.GetFromJsonAsync<List<Language>>($"{_appSettings.Value.LanguageApiUrl}");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetLanguage lookup city api is down");
                }
                return Ok(locations);
            }
        }

        [HttpGet("GetEmirates")]
        public async Task<IActionResult> GetEmirates()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Emirates> locations = new List<Emirates>();
                try
                {
                    locations = await _httpClient.GetFromJsonAsync<List<Emirates>>($"{_appSettings.Value.EmiratesApiUrl}/get");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetCity lookup city api is down");
                }
                return Ok(locations);
            }
        }

        

        [HttpGet("Getlocation")]
        public async Task<IActionResult> Getlocation()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Location> locations = new List<Location>();
                try
                {
                    locations = await _httpClient.GetFromJsonAsync<List<Location>>($"{_appSettings.Value.LocationApiUrl}/get");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::Getlocation lookup Getlocation api is down");
                }
                return Ok(locations);
            }
        }

        [HttpGet("GetDesignation")]
        public async Task<IActionResult> GetDesignation()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Designation> locations = new List<Designation>();
                try
                {
                    locations = await _httpClient.GetFromJsonAsync<List<Designation>>($"{_appSettings.Value.DesignationApiUrl}/get");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetDesignation lookup GetDesignation api is down");
                }
                return Ok(locations);
            }
        }

        [HttpGet("GetEducationType")]
        public async Task<IActionResult> GetEducationType()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<EducationType> eductionType = new List<EducationType>();
                try
                {
                    eductionType = await _httpClient.GetFromJsonAsync<List<EducationType>>($"{_appSettings.Value.EducationTypeApiUrl}/GetType");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducationType lookup GetEducationType api is down");
                }
                return Ok(eductionType);
            }
        }

        [HttpGet("GetEducationTypeByGroupID")]
        public async Task<IActionResult> GetEducationTypeByGroupID(int groupID)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<EducationType> eductionType = new List<EducationType>();
                try
                {
                    eductionType = await _httpClient.GetFromJsonAsync<List<EducationType>>($"{_appSettings.Value.EducationTypeApiUrl}/GetEmp_EducationTypes/{groupID}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducationType lookup GetEducationType api is down");
                }
                return Ok(eductionType);
            }
        }



        [HttpGet("GetEducationGroup")]
        public async Task<IActionResult> GetEducationGroup()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Group> group = new List<Group>();
                try
                {
                    group = await _httpClient.GetFromJsonAsync<List<Group>>($"{_appSettings.Value.EducationGroupApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducationGroup lookup GetEducationGroup api is down");
                }
                return Ok(group);
            }
        }

        [HttpGet("GetEducationGroup/{Id}")]
        public async Task<IActionResult> GetEducationGroup(int Id)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                Group group = new Group();
                try
                {
                    group = await _httpClient.GetFromJsonAsync<Group>($"{_appSettings.Value.EducationGroupApiUrl}/{Id}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducationGroup lookup GetEducationGroup api is down");
                }
                return Ok(group);
            }
        }

        [HttpGet("GetEducationMajor")]
        public async Task<IActionResult> GetEducationMajor()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Major> major = new List<Major>();
                try
                {
                    major = await _httpClient.GetFromJsonAsync<List<Major>>($"{_appSettings.Value.EducationMajorApiUrl}/GetMajor");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducationMajor lookup GetEducationMajor api is down");
                }
                return Ok(major);
            }
        }

        [HttpGet("GetEducationMajorByGroupIDandTypeId")]
        public async Task<IActionResult> GetEducationMajorByGroupIDandTypeId(int groupID, int typeID)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Major> major = new List<Major>();
                try
                {
                    major = await _httpClient.GetFromJsonAsync<List<Major>>($"{_appSettings.Value.EducationMajorApiUrl}/GetEmp_EducationMajors/{groupID}/{typeID}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducationMajor lookup GetEducationMajor api is down");
                }
                return Ok(major);
            }
        }

        [HttpGet("GetEducation")]
        public async Task<IActionResult> GetEducation()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<EducationCombine> educationCombine = new List<EducationCombine>();
                try
                {
                    educationCombine = await _httpClient.GetFromJsonAsync<List<EducationCombine>>($"{_appSettings.Value.EducationApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEducation lookup GetEducation api is down");
                }
                return Ok(educationCombine);
            }
        }

        [HttpGet("GetSkillGroup/{id?}")]
        public async Task<IActionResult> GetSkillGroup(int? id = null)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<SkillGroup> skillGroup = new List<SkillGroup>();
                try
                {
                    skillGroup = await _httpClient.GetFromJsonAsync<List<SkillGroup>>($"{_appSettings.Value.GetSkillGroupsApiUrl}{(id.HasValue ? "/" + id : "")}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetSkillGroup lookup GetSkillGroup api is down");
                }
                return Ok(skillGroup);
            }
        }


        [HttpGet("GetSkillGroupOccupation/{skillGroupId?}")]
        public async Task<IActionResult> GetSkillGroupOccupation(int? skillGroupId = null)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<SkillGroupOccupation> skillGroup = new List<SkillGroupOccupation>();
                try
                {
                    skillGroup = await _httpClient.GetFromJsonAsync<List<SkillGroupOccupation>>($"{_appSettings.Value.GetSkillGroupOccupationsUrl}{(skillGroupId.HasValue? "/" + skillGroupId : "")}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetSkillGroupOccupation lookup GetSkillGroupOccupation api is down");
                }
                return Ok(skillGroup);
            }
        }

        [HttpGet("GetToolsKnowledge/{Id?}")]
        public async Task<IActionResult> GetToolsKnowledge(int? Id = null)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<ToolsKnowledgeLookup> toolsKnowledgeLookups = new List<ToolsKnowledgeLookup>();
                try
                {
                    toolsKnowledgeLookups = await _httpClient.GetFromJsonAsync<List<ToolsKnowledgeLookup>>($"{_appSettings.Value.GetToolsKnowledgeLookupUrl}{(Id.HasValue ? "/" + Id : "")}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetToolsKnowledge lookup GetToolsKnowledge api is down");
                }
                return Ok(toolsKnowledgeLookups);
            }
        }

        [HttpGet("GetCertificateType")]
        public async Task<IActionResult> GetCertificateType()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<CertificateType> group = new List<CertificateType>();
                try
                {
                    group = await _httpClient.GetFromJsonAsync<List<CertificateType>>($"{_appSettings.Value.CertificateTypeApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetCertificateType lookup GetCertificateType api is down");
                }
                return Ok(group);
            }        
        }
        [HttpGet("GetJobIndustry")]
        public async Task<IActionResult> GetJobIndustry()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<JobIndustry> jobIndustry = new List<JobIndustry>();
                try
                {
                    jobIndustry = await _httpClient.GetFromJsonAsync<List<JobIndustry>>($"{_appSettings.Value.JobIndustryApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::JobIndustry lookup JobIndustry api is down");
                }
                return Ok(jobIndustry);
            }
        }
        [HttpGet("GetJobCategory")]
        public async Task<IActionResult> GetJobCategory()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<JobCategory> jobCategory = new List<JobCategory>();
                try
                {
                    jobCategory = await _httpClient.GetFromJsonAsync<List<JobCategory>>($"{_appSettings.Value.JobCategoryApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetJobCategory lookup GetJobCategory api is down");
                }
                return Ok(jobCategory);
            }        
        }

        [HttpGet("GetJobSeekerGrieveanceStatus")]
        public IActionResult GetJobSeekerGrieveanceStatus()
        {
            IList<JobSeekerGrieveanceStatus> jobSeekerGrieveanceStatus = new List<JobSeekerGrieveanceStatus>();
            jobSeekerGrieveanceStatus.Add(new JobSeekerGrieveanceStatus { Id = 1, ArTitle = "ريثما", EnTitle = "Pending" });
            jobSeekerGrieveanceStatus.Add(new JobSeekerGrieveanceStatus { Id = 2, ArTitle = "تم الحل", EnTitle = "Resolved" });          
            return Ok(jobSeekerGrieveanceStatus);
        }

        [HttpGet("GetEmploymentType")]
        public IActionResult GetEmploymentType()
        {
            IList<EmploymentType> employmentType = new List<EmploymentType>();
            employmentType.Add(new EmploymentType { Id = 2, ArTitle = "عقد", EnTitle = "Contract" });
            employmentType.Add(new EmploymentType { Id = 9, ArTitle = "موظف دائم", EnTitle = "Permanent Employee" });
            employmentType.Add(new EmploymentType { Id = 10, ArTitle = "متدرب", EnTitle = "Internship" });
            employmentType.Add(new EmploymentType { Id = 11, ArTitle = "موظف مؤقت", EnTitle = "Temporary Employee" });
            return Ok(employmentType);
        }

        [HttpGet("GetInstitute")]
        public async Task<IActionResult> GetInstitute()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Institute> institute = new List<Institute>();
                try
                {
                    institute = await _httpClient.GetFromJsonAsync<List<Institute>>($"{_appSettings.Value.InstituteApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetInstitute lookup GetInstitute api is down");
                }
                return Ok(institute);
            }
        }

        
        [HttpGet("GetEducationCategory")]
        public IActionResult GetEducationCategory()
        {
            IList<EducationCategory> educationCategory = new List<EducationCategory>();
            educationCategory.Add(new EducationCategory { Id = 1, ArName = "عادي", EnName = "Regular" });
            educationCategory.Add(new EducationCategory { Id = 2, ArName = "مهني", EnName = "Vocational" });
            
            return Ok(educationCategory);
        }

        [HttpGet("GetGradeCategory")]
        public IActionResult GetGradeCategory()
        {
            IList<GradeCategory> gradeCategory = new List<GradeCategory>();
            gradeCategory.Add(new GradeCategory { Id = 1, ArName = "صنف", EnName = "Class" });
            gradeCategory.Add(new GradeCategory { Id = 2, ArName = "النسبة المئوية", EnName = "Percentage" });
            gradeCategory.Add(new GradeCategory { Id = 3, ArName = "رتبة", EnName = "Grade" });
            gradeCategory.Add(new GradeCategory { Id = 4, ArName = "CGPI", EnName = "CGPI" });
            gradeCategory.Add(new GradeCategory { Id = 5, ArName = "رتبة", EnName = "Rank" });
            return Ok(gradeCategory);
        }

        [HttpGet("GetProficiencyType")]
        public IActionResult GetProficiencyType()
        {
            IList<ProficiencyType> proficiencyType = new List<ProficiencyType>();
            proficiencyType.Add(new ProficiencyType { Id = 1, ArName = "مبتدئ", EnName= "Beginner" });
            proficiencyType.Add(new ProficiencyType { Id = 2, ArName = "متوسط", EnName = "Intermediate" });
            proficiencyType.Add(new ProficiencyType { Id = 3, ArName = "خبير", EnName = "Expert" });
            return Ok(proficiencyType);
        }

        [HttpGet("GetLanguageProficiencyType")]
        public IActionResult GetLanguageProficiencyType()
        {
            IList<ProficiencyType> proficiencyType = new List<ProficiencyType>();
            proficiencyType.Add(new ProficiencyType { Id = 1, ArName = "اقرأ", EnName = "Read" });
            proficiencyType.Add(new ProficiencyType { Id = 2, ArName = "وضع الألحان", EnName = "Write" });
            proficiencyType.Add(new ProficiencyType { Id = 3, ArName = "تكلم", EnName = "Speak" });
            proficiencyType.Add(new ProficiencyType { Id = 3, ArName = "فهم", EnName = "Comprehend" });
            return Ok(proficiencyType);
        }

        //'Mr.', 'Mrs.', 'Dr.', 'Miss', 'Ms.'
        [HttpGet("GetSalutation")]
        public IActionResult GetSalutation()
        {
            return Ok(_ilookupRespository.GetSalutation());
        }

        [HttpGet("GetGender")]
        public IActionResult GetGender()
        {          
            return Ok(_ilookupRespository.GetGender());
        }

        [HttpGet("GetMilitaryServiceStatus")]
        public IActionResult GetMilitaryServiceStatus()
        {           
            return Ok(_ilookupRespository.GetMilitaryServiceStatus());
        }

        [HttpGet("GetNoteType")]
        public IActionResult GetNoteType()
        {
            IList<NoteType> noteType = new List<NoteType>();
            noteType.Add(new NoteType { Id = 1, ArName = "تعليق دعم JS", EnName = "JS Support comment" });           
            return Ok(noteType);
        }

        [HttpGet("GetMartialStatus")]
        public IActionResult GetMartialStatus()
        {
            //Married, single, divorced, and widowed            
            return Ok(_ilookupRespository.GetMartialStatus());
        }

        [HttpGet("GetResumeStatus")]
        public async Task<IActionResult> GetResumeStatus()
        {
            var response = await _ilookupRespository.GetAllStatus();
            return Ok(response.data);
        }

        //[HttpGet("GetResumeStatusreason")]
        //public async Task<IActionResult> GetResumeStatusReason()
        //{
        //    var response = await _ilookupRespository.GetReasonWithStatusId();
        //    return Ok(response.data);
        //}

        [HttpGet("GetReasonByStatus")]
        public async Task<object> GetReasonByStatus(DataSourceLoadOptions loadOptions)
        {
            var response = await _ilookupRespository.GetReasonWithStatusId();
            var filteredData = DataSourceLoader.Load(response.data, loadOptions);
            IList<LookupReason> _lookup = filteredData.data.Cast<LookupReason>().ToList();
            if (_lookup.Count == 0)
            {
                int statusID = Convert.ToInt32(loadOptions.Filter[2].ToString());
                LookupReason _lookupReason = new LookupReason { Rid = 0, Title = "None", Status_ID = statusID };
                _lookup.Add(_lookupReason);
                return DataSourceLoader.Load(_lookup, loadOptions);
            }
            else
            {
                return filteredData;
            }           
        }

        [HttpGet("GetCourse")]
        public async Task<IActionResult> GetCourse()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Course> course = new List<Course>();
                try
                {
                    course = await _httpClient.GetFromJsonAsync<List<Course>>($"{_appSettings.Value.CourseApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetCourse lookup GetCourse api is down");
                }
                return Ok(course);
            }
        }

        [HttpGet("GetEmployer")]
        public async Task<IActionResult> GetEmployer()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<EmployerName> employer = new List<EmployerName>();
                try
                {
                    employer = await _httpClient.GetFromJsonAsync<List<EmployerName>>($"{_appSettings.Value.EmployerApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetEmployer lookup GetEmployer api is down");
                }
                return Ok(employer);
            }
        }
        // 2 Current, 1 Previous
        [HttpGet("GetParticular")]
        public async Task<IActionResult> GetParticular()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Particular> particular = new List<Particular>();
                try
                {
                    particular = await _httpClient.GetFromJsonAsync<List<Particular>>($"{_appSettings.Value.ParticularApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetParticular lookup GetParticular api is down");
                }
                return Ok(particular);
            }
        }

        [HttpGet("GetSourceType")]
        public async Task<IActionResult> GetSourceType()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<SourceType> sourceType = new List<SourceType>();
                try
                {
                    sourceType = await _httpClient.GetFromJsonAsync<List<SourceType>>($"{_appSettings.Value.SourceTypeApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetSourceType lookup GetSourceType api is down");
                }
                return Ok(sourceType);
            }
        }

        [HttpGet("GetUniversity")]
        public async Task<IActionResult> GetUniversity()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<University> university = new List<University>();
                try
                {
                    university = await _httpClient.GetFromJsonAsync<List<University>>($"{_appSettings.Value.UniversityApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetUniversity lookup GetUniversity api is down");
                }
                return Ok(university);
            }
        }

        [HttpGet("GetTrainingCenter")]
        public async Task<IActionResult> GetTrainingCenter()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<TrainingCenter> trainingCenter = new List<TrainingCenter>();
                try
                {
                    trainingCenter = await _httpClient.GetFromJsonAsync<List<TrainingCenter>>($"{_appSettings.Value.TrainingCenterApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetUniversity lookup GetUniversity api is down");
                }
                return Ok(trainingCenter);
            }
        }

        [HttpGet("GetUniversityType")]
        public async Task<IActionResult> GetUniversityType()
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<UniversityType> universityType = new List<UniversityType>();
                try
                {
                    universityType = await _httpClient.GetFromJsonAsync<List<UniversityType>>($"{_appSettings.Value.UniversityTypeApiUrl}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetUniversityType lookup GetUniversityType api is down");
                }
                return Ok(universityType);
            }
        }

        [HttpGet("GetActiveCompaniesLookup")]
        public async Task<IActionResult> GetActiveCompaniesLookup(bool isActive = true)
        {
            IList<EmployerLookup> result = new List<EmployerLookup>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetFromJsonAsync<Webresponse<IList<EmployerLookup>>>($"{_appSettings.Value.CompanyApiUrl}/company/GetActiveCompaniesLookup/{isActive.ToString()}");

                    if (response.status == APIStatus.success)
                        result = response.data;
                    else
                        result = new List<EmployerLookup>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Ok(result);
        }

        [HttpGet("GetDocumentType")]
        public async Task<IActionResult> GetDocumentType()
        {
            var response = await _ilookupRespository.GetAllDocumentType();
            return Ok(response.data);
        }

    } 
}
