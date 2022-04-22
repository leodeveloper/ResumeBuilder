using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FormFactory.UnobtrusiveValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ResumeBuilder.Dto;
using ResumeBuilder.Helper;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;

namespace ResumeBuilder.Pages
{
    public class ShowAssessmentModel : BasePageModel
    {
        private readonly ILogger<ShowAssessmentModel> _logger;
        private readonly IAssessmentRepository _iAssessmentRepository;

        public ShowAssessmentModel(ILogger<ShowAssessmentModel> logger, IAssessmentRepository iAssessmentRepository)
        {
            _logger = logger;
            _iAssessmentRepository = iAssessmentRepository;
        }

        [BindProperty]
        public Webresponse<IList<AssessmentResultViewModel>> QuestionAnswers { get; set; }

        [BindProperty]
        public string JobSeekerId { get; set; }
        [BindProperty]
        public string TemplateId { get; set; }

        public async Task OnGet(string templateId, string Rid)
        {
            JobSeekerId = Rid;
            TemplateId = templateId;
         //   Questions = await _iAssessmentRepository.GetQuestionsByTemplateId(templateId);

            QuestionAnswers = await _iAssessmentRepository.GetAnswers(JobSeekerId, templateId);
        }
    }   
}
