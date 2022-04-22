using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Controllers
{
    //This is not api controller, this is normal controller, for updating the assessment and getting the assessment results
    [Route("api/[controller]")]
    public class AssessmentNonApiController : Controller
    {
        private readonly ILogger<AssessmentController> _logger;
        private readonly IAssessmentRepository _iAssessmentRepository;
        private readonly IResumeRepository _iResumeRepository;


        public AssessmentNonApiController(ILogger<AssessmentController> logger, IAssessmentRepository iAssessmentRepository, IResumeRepository iResumeRepository)
        {
            _logger = logger;
            _iAssessmentRepository = iAssessmentRepository;
            _iResumeRepository = iResumeRepository;
        }

        [HttpGet("GetAllAnswers")]
        public async Task<object> GetAllAnswers(string jobSeekerID, string templateID, DataSourceLoadOptions loadOptions)
        {
            var allAnswers = await _iAssessmentRepository.GetAnswers(jobSeekerID, templateID);
            IList<AnswerReport> answerReports = new List<AnswerReport>();
            if (allAnswers.status == APIStatus.success)
            {
                answerReports = ConvertReport.CopytoAnswerReport(allAnswers.data,jobSeekerID);
                if(answerReports.Where(z=>z.AnswerID == new Guid("00000000-0000-0000-0000-000000000000")).Count() == answerReports.Count())
                {
                    answerReports = new List<AnswerReport>();
                }
            }
            return DataSourceLoader.Load(answerReports, loadOptions);
        }

        [HttpPut("UpdateAnswer")]
        public async Task<IActionResult> UpdateAnswer(string key, string values)
        {
            if (Guid.TryParse(key, out Guid answerGuid))
            {
                var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
                AssesmentAnswer assesmentAnswer = new AssesmentAnswer();
                PopulateViewModel(assesmentAnswer, valuesDict);
               // assesmentAnswer.AnswerId = answerGuid;
                return Ok(await _iAssessmentRepository.UpdateAnswer(assesmentAnswer));
            }
            else
            {
                return BadRequest();
            }
        }

        private void PopulateViewModel(AssesmentAnswer viewModel, IDictionary values)
        {

            if (values.Contains("Answer"))
            {
                viewModel.Answer = Convert.ToString(values["Answer"]);
            }

            if (values.Contains("AnswerID"))
            {
                viewModel.AnswerId = Guid.Parse(values["AnswerID"].ToString());
            }

            if (values.Contains("QuestionID"))
            {
                viewModel.QuestionId = Guid.Parse(values["QuestionID"].ToString());
            }

            if (values.Contains("JobSeekerID"))
            {
                viewModel.AssessmentUserId = Convert.ToString(values["JobSeekerID"]);
            }

        }
    }
}
