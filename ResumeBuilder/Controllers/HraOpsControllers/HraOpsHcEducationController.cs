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
    public class HraOpsHcEducationController : ControllerBase
    {
        IHcResumeEducationRepository _ihcEducationRepository;
        public HraOpsHcEducationController(IHcResumeEducationRepository ihcResumeRepository)
        {
            _ihcEducationRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] HcResumeEducationViewModel hcResumeBankViewModel, [FromQuery] long userid)
        {
            return Ok(await _ihcEducationRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HcResumeEducationViewModel hcResumeBankViewModel,[FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcEducationRepository.Update(hcResumeBankViewModel,rid, userid));
        }

        [HttpDelete("Delete/{rid}")]
        public async Task<IActionResult> Delete(long rid)
        {
            return Ok(await _ihcEducationRepository.Delete(rid));
        }

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcEducationRepository.GetById(rid));
        }

        [HttpGet("GetByResumeIdId/{resumeId}")]
        public async Task<IActionResult> GetByResumeIdId(long resumeId)
        {
            return Ok(await _ihcEducationRepository.GetByResumeId(resumeId));
        }
    }
}
