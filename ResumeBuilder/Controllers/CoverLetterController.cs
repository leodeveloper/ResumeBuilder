using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoverLetterController : ControllerBase
    {
        readonly IResumeRepository _iResumeRepository;

        public CoverLetterController(IResumeRepository iResumeRepository)
        {
            _iResumeRepository = iResumeRepository;
        }

        [HttpGet("GetCoverLeter")]
        public async Task<IActionResult> GetCoverLeter(long resume_ID)
        {
            return Ok(await _iResumeRepository.GetCoverLetter(resume_ID));
        }

        [HttpPost("InsertUpdateCoverLeter")]
        public async Task<IActionResult> Post(JobSeekerCoverLetter coverLetter)
        {
            return Ok(await _iResumeRepository.AddUpdateCoverLetter(coverLetter));
        }
    }
}
