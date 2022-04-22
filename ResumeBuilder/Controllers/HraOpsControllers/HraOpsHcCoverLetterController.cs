using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Models.HraOpsModels;
using ResumeBuilder.Repository.HraOpsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Controllers.HraOpsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HraOpsHcCoverLetterController : ControllerBase
    {
        IHcResumeCoverLetterRepository _ihcCoverLetterRepository;
        public HraOpsHcCoverLetterController(IHcResumeCoverLetterRepository ihcResumeRepository)
        {
            _ihcCoverLetterRepository = ihcResumeRepository;
        }      

        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> Update([FromBody] HcResumeCoverLetterViewModel hcResumeBankViewModel, [FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcCoverLetterRepository.InsertUpdate(hcResumeBankViewModel, rid, userid));
        }      

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcCoverLetterRepository.GetById(rid));
        }

        [HttpGet("GetByResumeId/{resumeId}")]
        public async Task<IActionResult> GetByResumeId(long resumeId)
        {
            return Ok(await _ihcCoverLetterRepository.GetByResumeId(resumeId));
        }
    }
}
