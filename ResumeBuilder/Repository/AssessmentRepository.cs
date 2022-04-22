using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.Controllers;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly ILogger<AssessmentRepository> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _iHttpContextAccessor;

        public AssessmentRepository(IHttpContextAccessor iHttpContextAccessor, ILogger<AssessmentRepository> logger, IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _appSettings = appSettings;
            _httpClientFactory = httpClientFactory;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        public async Task<Webresponse<AnswerResults>> GetAnswersResult(string emiratesID)
        {
            Webresponse<AnswerResults> answerResults = new Webresponse<AnswerResults>();

            if(string.IsNullOrEmpty(emiratesID))
            {
                answerResults.message = "No result found";
                answerResults.status = APIStatus.NotFound;
                return answerResults;
            }

            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    var response = await _httpClient.GetFromJsonAsync<IList<AnswerResults>>($"{_appSettings.Value.AssessmentAPI}/AnswerResultsAPI/GetByEmiratesId?EmiratesId={emiratesID}");
                    if(response != null && response.Count>0)
                    {
                        answerResults.data = response.FirstOrDefault();
                        answerResults.status = APIStatus.success;
                    }
                    else
                    {
                        answerResults.message = "No result found";
                        answerResults.status = APIStatus.NotFound;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::GetAnswersResult api is down");
                answerResults.status = APIStatus.error;
                answerResults.message = ex.Message;
            }
            return answerResults;
        }

        public async Task<Webresponse<IList<AssessmentTemplate>>> GetTemplates()
        {

            Webresponse<IList<AssessmentTemplate>> templates = new Webresponse<IList<AssessmentTemplate>>();
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    templates = await _httpClient.GetFromJsonAsync<Webresponse<IList<AssessmentTemplate>>>($"{_appSettings.Value.AssessmentAPI}/TemplateAPI/get");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::GetTemplates api is down");
                templates.status = APIStatus.error;
                templates.message = ex.Message;
            }
            return templates;
        }
        


        public async Task<Webresponse<IList<AssessmentQuestion>>> GetQuestionsByTemplateId(string Id)
        {
            Webresponse<IList<AssessmentQuestion>> questions = new Webresponse<IList<AssessmentQuestion>>();
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    questions = await _httpClient.GetFromJsonAsync<Webresponse<IList<AssessmentQuestion>>>($"{_appSettings.Value.AssessmentAPI}/questionAPI/GetQuestionByTemplate/{Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::GetQuestionsByTemplateId api is down");
                questions.status = APIStatus.error;
                questions.message = ex.Message;
            }
            return questions;
        }


        public async Task<Webresponse<bool>> UpdateAnswer(AssesmentAnswer assessmentAnswer)
        {
            Webresponse<bool> response = new Webresponse<bool>();
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    var httpResponse = await _httpClient.PostAsJsonAsync<AssesmentAnswer>($"{_appSettings.Value.AssessmentAPI}/AnswersAPI/UpdateAnswer", assessmentAnswer);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        response = await httpResponse.Content.ReadFromJsonAsync<Webresponse<bool>>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::SaveAnswers api is down");
                response.status = APIStatus.error;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Webresponse<bool>> SaveAnswers(IList<AssessmentAnswers> assessmentAnswers,string templateId)
        {
            Webresponse<bool> response = new Webresponse<bool>();
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    var httpResponse = await _httpClient.PostAsJsonAsync<IList<AssessmentAnswers>>($"{_appSettings.Value.AssessmentAPI}/AnswersAPI/SaveAnswers/{templateId}", assessmentAnswers);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        response = await httpResponse.Content.ReadFromJsonAsync<Webresponse<bool>>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::SaveAnswers api is down");
                response.status = APIStatus.error;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Webresponse<IList<AssessmentResultViewModel>>> GetAnswers(string jobseekerID, string templateID)
        {
            Webresponse<IList<AssessmentResultViewModel>> answers = new Webresponse<IList<AssessmentResultViewModel>>();
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    answers = await _httpClient.GetFromJsonAsync<Webresponse<IList<AssessmentResultViewModel>>>($"{_appSettings.Value.AssessmentAPI}/AnswersAPI/GetAnswers?assessmentUserId={jobseekerID}&templateID={templateID}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::GetAnswers api is down");
                answers.status = APIStatus.error;
                answers.message = ex.Message;
            }
            return answers;
        }

        public async Task<Webresponse<AssessmentQuestionAnswerReportViewModel>> GetAllAnswersByTemplateId(string templateID)
        {
            Webresponse<AssessmentQuestionAnswerReportViewModel> answers = new Webresponse<AssessmentQuestionAnswerReportViewModel>();
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
                {
                    var tokenclaim = _iHttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(z => z.Type == "token");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenclaim.Value);
                    answers = await _httpClient.GetFromJsonAsync<Webresponse<AssessmentQuestionAnswerReportViewModel>>($"{_appSettings.Value.AssessmentAPI}/AnswersAPI/GetAnswersByTemplateId?templateID={templateID}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AssessmentRepository:::GetAllAnswersByTemplateId api is down");
                answers.status = APIStatus.error;
                answers.message = ex.Message;
            }
            return answers;
        }
    }
}
