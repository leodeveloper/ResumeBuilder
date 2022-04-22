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
    public class HraOpsHcBeneficiaryController : ControllerBase
    {
        IHcResumeBeneficiaryRepository _ihcBeneficiaryRepository;
        public HraOpsHcBeneficiaryController(IHcResumeBeneficiaryRepository ihcResumeRepository)
        {
            _ihcBeneficiaryRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] HcResumeBeneficiaryViewModel hcResumeBankViewModel, [FromQuery] long userid)
        {
            return Ok(await _ihcBeneficiaryRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] HcResumeBeneficiaryViewModel hcResumeBankViewModel, [FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcBeneficiaryRepository.Update(hcResumeBankViewModel, rid, userid));
        }

        [HttpDelete("Delete/{rid}")]
        public async Task<IActionResult> Delete(long rid)
        {
            return Ok(await _ihcBeneficiaryRepository.Delete(rid));
        }

        [HttpGet("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcBeneficiaryRepository.GetById(rid));
        }

        [HttpGet("GetByResumeId/{resumeId}")]
        public async Task<IActionResult> GetByResumeId(long resumeId)
        {
            return Ok(await _ihcBeneficiaryRepository.GetByResumeId(resumeId));
        }
    }
}
