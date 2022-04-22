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
    public class HraOpsHcSkillController : ControllerBase
    {
        IHcResumeSkillRepository _ihcSkillRepository;
        public HraOpsHcSkillController(IHcResumeSkillRepository ihcResumeRepository)
        {
            _ihcSkillRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] HcResumeSkillViewModel hcResumeBankViewModel, [FromQuery] long userid)
        {
            return Ok(await _ihcSkillRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HcResumeSkillViewModel hcResumeBankViewModel, [FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcSkillRepository.Update(hcResumeBankViewModel, rid, userid));
        }

        [HttpDelete("Delete/{rid}")]
        public async Task<IActionResult> Delete(long rid)
        {
            return Ok(await _ihcSkillRepository.Delete(rid));
        }

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcSkillRepository.GetById(rid));
        }

        [HttpGet("GetByResumeIdId/{resumeId}")]
        public async Task<IActionResult> GetByResumeIdId(long resumeId)
        {
            return Ok(await _ihcSkillRepository.GetByResumeId(resumeId));
        }
    }
}
