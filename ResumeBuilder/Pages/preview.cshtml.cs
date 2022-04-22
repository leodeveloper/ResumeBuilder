using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;

namespace ResumeBuilder.Pages
{
    public class previewModel : PageModel
    {
        private readonly ILogger<previewModel> _logger;
        private readonly IResumeRepository _iResumeRepository;
        public previewModel(ILogger<previewModel> logger, IResumeRepository iResumeRepository)
        {
            _logger = logger;
            _iResumeRepository = iResumeRepository;
        }

        [BindProperty]
        public Webresponse<ResumePreviewDto> resume { get; set; }

        public async Task OnGet(long resumeId)
        {
            this.resume = await _iResumeRepository.Resume_Preview(resumeId);
        }
    }
}
