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
    public class ShowAssessmentReportModel : BasePageModel
    {
        private readonly ILogger<ShowAssessmentReportModel> _logger;
        private readonly IAssessmentRepository _iAssessmentRepository;
        private readonly IResumeRepository _iResumeRepository;

        public ShowAssessmentReportModel(ILogger<ShowAssessmentReportModel> logger, IAssessmentRepository iAssessmentRepository, IResumeRepository iResumeRepository)
        {
            _logger = logger;
            _iAssessmentRepository = iAssessmentRepository;
            _iResumeRepository = iResumeRepository;
        }

        [BindProperty]
        public Webresponse<AssessmentQuestionAnswerReportViewModel> Questions { get; set; }
        public Webresponse<IList<Resume>> Resume { get; set; }

        public async Task OnGet(string templateId)
        {
            try
            {
                Questions = await _iAssessmentRepository.GetAllAnswersByTemplateId(templateId);
                Resume = await _iResumeRepository.GetManyResumeByIds(Questions.data.UserAnswers.Select(z => z.AssessmentUserID).Select(long.Parse).ToArray());
            }
            catch(Exception ex)
            {
                _logger.LogError($"ShowAssessmentReportModel :::::: {ex.Message}",ex);
            }
        }
    }   
}
