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
    public class HraOpsHcResumeEngagementController : ControllerBase
    {
        IHcResumeEngagementRepository _ihcEngagementRepository;
        public HraOpsHcResumeEngagementController(IHcResumeEngagementRepository ihcResumeRepository)
        {
            _ihcEngagementRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] HcResumeEngagementViewModel hcResumeBankViewModel, [FromQuery] int userid)
        {
            return Ok(await _ihcEngagementRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HcResumeEngagementViewModel hcResumeBankViewModel, [FromQuery] long rid, [FromQuery] int userid)
        {
            return Ok(await _ihcEngagementRepository.Update(hcResumeBankViewModel, rid, userid));
        }

        [HttpPost("UpdateJobSeekerStatus")]
        public async Task<IActionResult> UpdateJobSeekerStatus([FromBody] HcResumeEngagementStatusNotes hcResumeEngagementStatusNotes)
        {
            return Ok(await _ihcEngagementRepository.UpdateJobSeekerStatus(hcResumeEngagementStatusNotes));
        }

        [HttpDelete("Delete/{rid}")]
        public async Task<IActionResult> Delete(long rid)
        {
            return Ok(await _ihcEngagementRepository.Delete(rid));
        }

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcEngagementRepository.GetById(rid));
        }

        [HttpPost("GetAllOrByAdvisorId")]
        public async Task<IActionResult> GetAllOrByAdvisorId([FromBody] HcResumeEngagementViewModel_GetByAdvisor hcResumeEngagementViewModel_GetByAdvisor)
        {
            return Ok(await _ihcEngagementRepository.GetAllOrByAdvisorId(hcResumeEngagementViewModel_GetByAdvisor));
        }

        [HttpGet("GetByResumeIdId/{resumeId}")]
        public async Task<IActionResult> GetByResumeIdId(long resumeId)
        {
            return Ok(await _ihcEngagementRepository.GetByResumeId(resumeId));
        }
    }
}
