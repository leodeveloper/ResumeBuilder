using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using ResumeBuilder.Helper;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
   // [ApiController]
    public class ResumeController : ControllerBase
    {
        readonly ILogger<lookupController> _logger;
        readonly IResumeRepository _iResumeRepository;
        readonly IOptions<LookUpApiUrl> _appSettings;
        IHttpClientFactory _httpClientFactory;
        public ResumeController(ILogger<lookupController> logger, IResumeRepository iResumeRepository, IConfiguration configuration, IOptions<LookUpApiUrl> appSettings, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _iResumeRepository = iResumeRepository;
            _appSettings = appSettings;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("GetCourse")]
        public async Task<object> GetCourse(DataSourceLoadOptions loadOptions)
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
                    _logger.LogError(ex, "ResumeController:::GetCourse lookup GetCourse api is down");
                }
                return DataSourceLoader.Load(course, loadOptions);
            }
        }

        [HttpGet("GetDesignation")]
        public async Task<object> GetDesignation(DataSourceLoadOptions loadOptions)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Designation> designations = new List<Designation>();
                try
                {
                    designations = await _httpClient.GetFromJsonAsync<List<Designation>>($"{_appSettings.Value.DesignationApiUrl}/get");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ResumeController:::GetDesignation lookup GetDesignation api is down");
                }
                return DataSourceLoader.Load(designations, loadOptions);
            }
        }

        [HttpGet("GetInstitute")]
        public async Task<object> GetInstitute(DataSourceLoadOptions loadOptions)
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
                    _logger.LogError(ex, "ResumeController:::GetInstitute lookup GetInstitute api is down");
                }
                return DataSourceLoader.Load(institute, loadOptions);
            }
        }

        [HttpGet("GetUniversity")]
        public async Task<object> GetUniversity(DataSourceLoadOptions loadOptions)
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
                    _logger.LogError(ex, "ResumeController:::GetUniversity lookup GetUniversity api is down");
                }
                return DataSourceLoader.Load(university, loadOptions);
            }
        }

        [HttpGet("GetEmployer")]
        public async Task<object> GetEmployer(DataSourceLoadOptions loadOptions)
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
                    _logger.LogError(ex, "ResumeController:::GetEmployer lookup GetEmployer api is down");
                }
                return DataSourceLoader.Load(employer, loadOptions);
            }
        }

        /// <summary>
        /// International Cities
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountryCity")]
        public async Task<object> GetCountryCity(DataSourceLoadOptions loadOptions)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<CountryCity> countrycities = new List<CountryCity>();
                try
                {
                    countrycities = await _httpClient.GetFromJsonAsync<List<CountryCity>>($"{_appSettings.Value.CountryCityApiUrl}");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "lookupController:::GetCountryCity lookup country city api is down");
                }
                return DataSourceLoader.Load(countrycities, loadOptions);
            }
        }

        [HttpGet]
        public object GetData(DataSourceLoadOptions loadOptions)
        {
            IList<Resume> obj = new List<Resume>();
            obj.Add(new Resume());
            return DataSourceLoader.Load(obj, loadOptions);
        }

        // GET: api/<Resume>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber=1, int rowCount=10 )
        {
            return Ok(await _iResumeRepository.GetAllResume(pageNumber, rowCount));          
        }

        // GET: api/<Resume>
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(long mongoDBID)
        {
            Webresponse<Resume_Attachment> file = await _iResumeRepository.GetAttachment(mongoDBID.ToString());
            if(file.status == APIStatus.success)
            {
                byte[] bytes = System.Convert.FromBase64String(file.data.ResumeAttachmentBase64String);
                return File(bytes, "application/octet-stream", file.data.FileName);
            }

            return NotFound("file not found");         
        }

        
       [HttpGet("GetFileJobApplication")]
        public async Task<IActionResult> GetFileJobApplication(string mongoDBID, string collectionName)
        {
            Webresponse<Job_ApplicationAttachmentMongoDB> file = await _iResumeRepository.GetAttachmentJobApplication(mongoDBID, collectionName);
            if (file.status == APIStatus.success)
            {
                byte[] bytes = System.Convert.FromBase64String(file.data.DataBase64String);
                return File(bytes, "application/octet-stream", file.data.FileName);
            }

            return NotFound("file not found");
        }

        
        [HttpGet("GetAllResume")]
        public async Task<object> GetAllResume(DataSourceLoadOptions loadOptions,
            int? LocationId = null,
            int? EducationGroupId = null,
            int? EducationTypeId = null,
            int? EducationMajorId = null,
            string StatusId = null)
        {

            IList<int> statusIdList = new List<int>();
            if (StatusId != null)
            {
                statusIdList = StatusId.Split(',').Select(int.Parse).ToList();
            }

            var allresume = await _iResumeRepository.GetAllResume(1, 100000000);
            allresume.data = allresume.data
                .Where(z => (
                    ((LocationId != null) && (z.LocationId == LocationId)
                    ||
                    (LocationId == null))
                    &&
                     ((EducationGroupId != null)
                      && (z.Education.Any(t => t?.Education_Group_Id == EducationGroupId))
                    //  && (z.Education.Contains(new Education { Education_Group_Id=(int)EducationGroupId })))
                    ||
                    (EducationGroupId == null))
                    &&
                     ((EducationTypeId != null)
                     && (z.Education.Any(t => t?.Education_Type_Id == EducationTypeId))
                    ||
                    (EducationTypeId == null))
                &&
                 ((EducationMajorId != null)
                 && (z.Education.Any(t => t?.Education_Major_Id == EducationMajorId))
                ||
                (EducationMajorId == null))
                &&
                 ((StatusId != null)
                 && (statusIdList.Contains(z.StatusId))
                ||
                (StatusId == null))
                )).ToList();
            return DataSourceLoader.Load(allresume.data, loadOptions);

        }

        // GET api/<Resume>/5
        [HttpGet("GetByResumeId/{id}")]
        public async Task<IActionResult> GetByResumeId(long id)
        {
            return Ok(await _iResumeRepository.GetResumeById(id));
        }

        //[HttpGet("CheckEmiratesIdUniqueKey/{emiratesId}")]
        //public async Task<IActionResult> CheckEmiratesIdUniqueKey(string emiratesId)
        //{
        //    Webresponse<Resume> webresponseResume = await _iResumeRepository.GetResumeByEmiratesId(emiratesId);
        //    if(webresponseResume.status == APIStatus.NotFound)
        //    {
        //        return Ok(false);
        //    }
        //    else
        //    {
        //        return Ok(true);
        //    }           
        //}

        [HttpGet("GetResumePreview/{id}")]
        public async Task<IActionResult> GetResumePreview(long id)
        {
            return Ok(await _iResumeRepository.Resume_Preview(id));
        }

        // POST api/<Resume>
        [HttpPost("InsertResume")]
        public async Task<IActionResult> Post(Resume resume)
        {
            return Ok(await _iResumeRepository.InsertResume(resume));
        }

        // PUT api/<Resume>/5
        [HttpPut("UpdateResume")]
        public async Task<IActionResult> Put(Resume resume)
        {
            return Ok(await _iResumeRepository.UpdateResume(resume));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iResumeRepository.DeleteResume(key));
        }

        #region resume status

        // GET api/<Resume>/5
        [HttpGet("GetByResumeStatusById/{id}")]
        public async Task<IActionResult> GetByResumeStatusId(long id)
        {
            return Ok(await _iResumeRepository.GetResumeStatus(id));
        }

        [HttpGet("GetAllResumeStatus")]
        public async Task<object> GetAllResumeStatus(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allresmestatus = await _iResumeRepository.GetResumeStatus(resumeid);

                return DataSourceLoader.Load(allresmestatus.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<JobsSeekerStatus>(), loadOptions);

        }

        [HttpPost("InsertResumeStatus")]
        public async Task<IActionResult> InsertResumeStatus(string values)
        {
            var jobsSeekerStatus = new JobsSeekerStatus();
            JsonConvert.PopulateObject(values, jobsSeekerStatus);

            if (!TryValidateModel(jobsSeekerStatus))
                return BadRequest();

            jobsSeekerStatus.UserId = User.Identity.Name;
            await _iResumeRepository.InsertResumeStatus(jobsSeekerStatus);
            return Ok();
        }

        // PUT api/<Resume>/5
        [HttpPut("UpdateResumeStatus")]
        public async Task<IActionResult> UpdateResumeStatus(JobsSeekerStatus jobsSeekerStatus)
        {
            return Ok(await _iResumeRepository.UpdateResumeStatus(jobsSeekerStatus));
        }

        

       

        #endregion
    }
}
