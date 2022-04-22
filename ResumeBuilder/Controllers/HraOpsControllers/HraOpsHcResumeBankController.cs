using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Models.HraOpsModels;
using ResumeBuilder.Repository.HraOpsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers.HraOpsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HraOpsHcResumeBankController : ControllerBase
    {
        IHcResumeRepository _ihcResumeRepository;
        public HraOpsHcResumeBankController(IHcResumeRepository ihcResumeRepository)
        {
            _ihcResumeRepository = ihcResumeRepository;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Hc_ResumeBankViewModel hcResumeBankViewModel, [FromQuery] long userid)
        {
            return Ok(await _ihcResumeRepository.Insert(hcResumeBankViewModel, userid));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Hc_ResumeBankViewModel hcResumeBankViewModel,[FromQuery] long userid)
        {
            return Ok(await _ihcResumeRepository.Update(hcResumeBankViewModel, userid));
        }

        [HttpPost("GetById/{rid}")]
        public async Task<IActionResult> GetById(long rid)
        {
            return Ok(await _ihcResumeRepository.GetById(rid));
        }

        [HttpPost("GetByEmiratesId/{emiratesId}")]
        public async Task<IActionResult> GetByEmiratesId(string emiratesId)
        {
            return Ok(await _ihcResumeRepository.GetByEmiratesId(emiratesId));
        }

        [HttpPost("GetByJobSeekerId/{jobSeekerId}")]
        public async Task<IActionResult> GetByJobSeekerId(string jobSeekerId)
        {
            return Ok(await _ihcResumeRepository.GetByJobSeekerId(jobSeekerId));
        }

        #region ConatctInfo
        [HttpPost("UpdateContactInfo")]
        public async Task<IActionResult> UpdateContactInfo([FromBody] Hc_ContactInfoViewModel hcContactVm, [FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcResumeRepository.Update(hcContactVm, rid, userid));
        }

        [HttpPost("GetConatctInfoById/{rid}")]
        public async Task<IActionResult> GetConatctInfoById(long rid)
        {
            return Ok(await _ihcResumeRepository.GetContactInfoById(rid));
        }
        #endregion

        #region AdditionalInfo
        [HttpPost("UpdateAdditionalInfo")]
        public async Task<IActionResult> UpdateAdditionalInfo([FromBody] Hc_AdditionalInfoViewModel hcResumeBankViewModel,[FromQuery] long rid, [FromQuery] long userid)
        {
            return Ok(await _ihcResumeRepository.Update(hcResumeBankViewModel,rid, userid));
        }

        [HttpPost("GetAdditionalInfoById/{rid}")]
        public async Task<IActionResult> GetAdditionalInfoById(long rid)
        {
            return Ok(await _ihcResumeRepository.GetAdditionalInfoById(rid));
        }
        #endregion

        #region Assessment

        [HttpGet("UpdateJPCAssessment/{rid}/{JPCAssessment}")]
        public async Task<IActionResult> UpdateJPCAssessment(long rid, int JPCAssessment)
        {
            return Ok(await _ihcResumeRepository.UpdateJPCAssessment(rid, JPCAssessment));
        }

        [HttpPost("UpdateJPCAssessmentstatus/{rid}/{JPCAssessmentStatus}")]
        public async Task<IActionResult> UpdateJPCAssessmentstatus(long rid, string JPCAssessmentStatus)
        {
            return Ok(await _ihcResumeRepository.UpdateJPCAssessmentstatus(rid, JPCAssessmentStatus));
        }
        #endregion

        #region Challanges
        [HttpPost("InsertChallanges/{userId}")]
        public async Task<IActionResult> InsertChallanges(HcResumeChallangesViewModel hcResumeChallangesViewModel , int userId)
        {
            return Ok(await _ihcResumeRepository.InsertChallanges(hcResumeChallangesViewModel, userId));
        }

        [HttpGet("GetChallanges/{resumeId}")]
        public async Task<IActionResult> GetChallanges(long resumeId)
        {
            return Ok(await _ihcResumeRepository.GetChallanges(resumeId));
        }
        #endregion
    }
}
