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
    public class HraOpsHcPreferenceJobTitleController : ControllerBase
    {
        IHcResumePreferenceEscoJobTitleRepository _ihcPreferenceJobTitleRepository;
        public HraOpsHcPreferenceJobTitleController(IHcResumePreferenceEscoJobTitleRepository ihcResumeRepository)
        {
            _ihcPreferenceJobTitleRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] HcResumePreferenceJobTitleViewModel hcResumeBankViewModel, [FromQuery] long userid)
        {
            return Ok(await _ihcPreferenceJobTitleRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HcResumePreferenceJobTitleViewModel hcResumeBankViewModel, [FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcPreferenceJobTitleRepository.Update(hcResumeBankViewModel, rid, userid));
        }

        [HttpDelete("Delete/{rid}")]
        public async Task<IActionResult> Delete(long rid)
        {
            return Ok(await _ihcPreferenceJobTitleRepository.Delete(rid));
        }

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcPreferenceJobTitleRepository.GetById(rid));
        }

        [HttpGet("GetByResumeId/{resumeId}")]
        public async Task<IActionResult> GetByResumeId(long resumeId)
        {
            return Ok(await _ihcPreferenceJobTitleRepository.GetByResumeId(resumeId));
        }
    }
}
