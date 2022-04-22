using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController] //do not remove this
    public class VacancyController : ControllerBase
    {
        private readonly ILogger<VacancyController> _logger;
        private readonly IOptions<LookUpApiUrl> _appSettings;
        private IHttpClientFactory _httpClientFactory;
        private IUserRepository _iUserRespository;
        private IHttpContextAccessor _iHttpContextAccessor;

        public VacancyController(IHttpContextAccessor iHttpContextAccessor,IUserRepository iUserRespository, ILogger<VacancyController> logger, IOptions<LookUpApiUrl> appSettings, IHttpClientFactory httpClientFactory)
        {
            _iUserRespository = iUserRespository;
            _logger = logger;
            _appSettings = appSettings;
            _httpClientFactory = httpClientFactory;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        [HttpGet("GetActiveVacancies")]
        public async Task<IActionResult> GetActiveVacancies(int jobSeekerId)
        {
            //get all not applied vancaies
            return Ok( await getAllVacancies(jobSeekerId, false));
        }

        [HttpGet("GetAppliedVacancies")]
        public async Task<IActionResult> GetAppliedVacancies(int jobSeekerId)
        {
            // get all applied vancncies
            return Ok( await getAllVacancies(jobSeekerId, true));
        }

        

        [HttpGet("GetJobApplicationAttachments")]
        public async Task<IActionResult> GetJobApplicationAttachments(int jobSeekerId)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Job_ApplicationAttachment> attachments = new List<Job_ApplicationAttachment>();
                try
                {
                    List<Task> tasks = new List<Task>();
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    var responseattachments = await _httpClient.GetFromJsonAsync<Webresponse<IList<Job_ApplicationAttachment>>>($"{_appSettings.Value.JobApplicationUrl}/APIs/GetJobSeekerDocuments?JobSeekerId={jobSeekerId}");
                    if (responseattachments.status == APIStatus.success)
                    {
                        attachments = responseattachments.data;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "VacancyController:::GetActiveVacancies api is down");
                }
                return Ok(attachments);
            }
        }

        [HttpPost("PostVacancies")]
        public async Task<IActionResult> PostVacancies(PostVacancy postVacancy) 
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {               
                try
                {

                    string userName = User.Identity.Name.Split('\\').Last();
                    string token = await _iUserRespository.GetToken(userName);

                 //   var jwt = "token";
                    var handler = new JwtSecurityTokenHandler();
                    var tokenDecode = handler.ReadJwtToken(token);
                  //  var tokenDecode = new System.IdentityModel.Tokens.JwtSecurityToken(token);
                    Claim claimModel = tokenDecode.Claims.FirstOrDefault(z => z.Type == "UserId");

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    postVacancy.UserId = claimModel.Value;
                    var json = JsonConvert.SerializeObject(postVacancy);
                    //for posting the vanacy url shouldbe like that
                    //"http://172.19.21.127/JobApplication/APIs/ApplyForVacancies?UserId=1231&JobSeekerId=7&listVacancy=1&listVacancy=3"

                    string url = $"{_appSettings.Value.JobApplicationUrl}/APIs/ApplyForVacancies?UserId={postVacancy.UserId}&JobSeekerId={postVacancy.JobSeekerId}";
                    foreach(int vacancyId in postVacancy.listVacancy)
                    {
                        url += $"&listVacancy={vacancyId}";
                    }

                    var response = await _httpClient.PostAsJsonAsync<PostVacancy>(url, postVacancy);                   
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "VacancyController:::PostVacancies api is down");
                }
                return Ok(postVacancy);
            }
        }


        #region private
        private async Task<IList<Vacancy>> getAllVacancies(int jobSeekerId, bool IsApplied)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                IList<Vacancy> vacancies = new List<Vacancy>();
                try
                {
                    List<Task> tasks = new List<Task>();
                    string userName = User.Identity.Name.Split('\\').Last();
                    string token = await _iUserRespository.GetToken(userName);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    tasks.Add(_httpClient.GetFromJsonAsync<Webresponse<IList<Vacancy>>>($"{_appSettings.Value.VacancyApiUrl}/apis/getVacancyLookup"));
                    //get applied vancies for the jobseekerID
                    tasks.Add(_httpClient.GetFromJsonAsync<Webresponse<IList<int>>>($"{_appSettings.Value.JobApplicationUrl}/APIs/GetAppliedVacancies?JobSeekerId={jobSeekerId}"));

                    await Task.WhenAll(tasks.ToArray());

                    var response = ((Task<Webresponse<IList<Vacancy>>>)tasks[0]).Result;
                    var appliedVacancies = ((Task<Webresponse<IList<int>>>)tasks[1]).Result;

                    if (response.status == APIStatus.success)
                    {
                        if (appliedVacancies.status == APIStatus.success)
                        {
                            foreach (int vacancyId in appliedVacancies.data)
                            {
                                if (response.data.Any(z => z.Id == vacancyId))
                                {
                                    //set status is ture if jobseeker is already allpied vancaies
                                    response.data.SingleOrDefault(t => t.Id == vacancyId).AppliedVacancyStatus = true;
                                }
                            }
                        }                      
                        vacancies = response.data.Where(z => z.AppliedVacancyStatus == IsApplied).ToList();
                    }
                    else
                    {
                        _logger.LogError("VacancyController:::GetAppliedVacancies no data found");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "VacancyController:::GetAppliedVacancies api is down");
                }
                return vacancies;
            }
        }
        #endregion

    }
}
