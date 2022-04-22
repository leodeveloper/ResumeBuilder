using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly ILogger<AssessmentController> _logger;
        private readonly IAssessmentRepository _iAssessmentRepository;
        private readonly IResumeRepository _iResumeRepository;


        public AssessmentController(ILogger<AssessmentController> logger, IAssessmentRepository iAssessmentRepository, IResumeRepository iResumeRepository)
        {
            _logger = logger;
            _iAssessmentRepository = iAssessmentRepository;
            _iResumeRepository = iResumeRepository;
        }
        // GET: api/<AssessmentController>
        [HttpGet("GetTemplates")]
        public async Task<IActionResult> GetTemplates()
        {
            return Ok(await _iAssessmentRepository.GetTemplates());
        }

        [HttpGet("GetAllTemplate")]
        public async Task<object> GetAllTemplate(DataSourceLoadOptions loadOptions)
        {
            var allAssessments = await _iAssessmentRepository.GetTemplates();
            return DataSourceLoader.Load(allAssessments.data, loadOptions);
        }

        // GET: api/<AssessmentController>
        [HttpGet("GetQuestionsByTemplateId")]
        public async Task<IActionResult> GetQuestionsByTemplateId(string Id)
        {
            return Ok(await _iAssessmentRepository.GetQuestionsByTemplateId(Id));
        }

        [HttpPost("SaveAssessmentAnswers/{templateId}")]
        public async Task<IActionResult> SaveAssessmentAnswers(IList<AssessmentAnswers> assessmentAnswers, string templateId)
        {
            return Ok(await _iAssessmentRepository.SaveAnswers(assessmentAnswers, templateId));
        }

        [HttpGet("GetAnswers")]
        public async Task<IActionResult> GetAnswers(string jobSeekerID, string templateID)
        {
            return Ok(await _iAssessmentRepository.GetAnswers(jobSeekerID, templateID));
        }

        [HttpGet("GetAnswerResultsReport")]
        public async Task<IActionResult> GetAnswerResultsReport(string emiratesID)
        {
            return Ok(await _iAssessmentRepository.GetAnswersResult(emiratesID));
        }       

        [HttpGet("GetAllAnswersByTemplateId")]
        public async Task<object> GetAllAnswersByTemplateId(string templateID, DataSourceLoadOptions loadOptions)
        {
            IList<AssessmentReportModel> reportModels = new List<AssessmentReportModel>();
            var allAnswers = await _iAssessmentRepository.GetAllAnswersByTemplateId(templateID);

            if (allAnswers.status == APIStatus.success)
            {
                return DataSourceLoader.Load(reportModels, loadOptions);
            }
            return DataSourceLoader.Load(reportModels, loadOptions);
        }

       
    }      
   
}
