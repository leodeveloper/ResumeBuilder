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
    public class HraOpsHcPodController : ControllerBase
    {
        IHcResumePodRepository _ihcPodRepository;
        public HraOpsHcPodController(IHcResumePodRepository ihcResumeRepository)
        {
            _ihcPodRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] HcResumePodViewModel hcResumeBankViewModel, [FromQuery] long userid)
        {
            return Ok(await _ihcPodRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HcResumePodViewModel hcResumeBankViewModel, [FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcPodRepository.Update(hcResumeBankViewModel, rid, userid));
        }

        [HttpDelete("Delete/{rid}")]
        public async Task<IActionResult> Delete(long rid)
        {
            return Ok(await _ihcPodRepository.Delete(rid));
        }

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcPodRepository.GetById(rid));
        }

        [HttpGet("GetByResumeIdId/{resumeId}")]
        public async Task<IActionResult> GetByResumeIdId(long resumeId)
        {
            return Ok(await _ihcPodRepository.GetByResumeId(resumeId));
        }
    }
}
