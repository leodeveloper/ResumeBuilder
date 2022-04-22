using IntegrationApiClassLibrary.Model;
using IntegrationApiClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Dto;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IPersonalInfoRepository _iPersonalInfoRepository;
        private readonly IIntegrationRepository _iIntegrationRespository;
        private readonly IIntegrationLogRepository _iIntegrationLogRepository;
        public IntegrationController(IPersonalInfoRepository iPersonalInfoRepository, IIntegrationRepository iIntegrationRespository, IIntegrationLogRepository iIntegrationLogRepository)
        {
            _iPersonalInfoRepository = iPersonalInfoRepository;
            _iIntegrationRespository = iIntegrationRespository;
            _iIntegrationLogRepository = iIntegrationLogRepository;
        }

        //GET: api/<IntegrationController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Api is running");
        }

        [HttpPut("UpdatePersonalInfo")]
        public async Task<IActionResult> UpdatePersonalInfo(long resumeId)
        {            
            return Ok(await _iIntegrationRespository.UpdateJobSeekerPersonalInfo(resumeId));
        }

        [HttpPut("InsertUpdatePensionfund")]
        public async Task<IActionResult> InsertUpdatePensionfund(long resumeId)
        {
            return Ok(await _iIntegrationRespository.InsertUpdatePensionfund(resumeId));
        }

        [HttpGet("GetPersonalInfo")]
        public async Task<IActionResult> GetPersonalInfo(long resumeId)
        {
            return Ok(await _iIntegrationRespository.InsertUpdatePensionfund(resumeId));
        }

        [HttpGet("GetLastUpdatePensionfund")]
        public async Task<IActionResult> GetLastUpdatePensionfund(long resumeId)
        {
            return Ok(await _iIntegrationRespository.InsertUpdatePensionfund(resumeId));
        }

        [HttpGet("GetIntegrationPersonalInfoLog")]
        public async Task<IActionResult> GetIntegrationPersonalInfoLog(long resumeId)
        {
            return Ok(await _iIntegrationLogRepository.GetintegrationLogsByIntegrationName(Helper.EnumHelper.IntegrationEnum.PersonalInfo, resumeId));
        }

        [HttpGet("GetIntegrationPensionInfoLog")]
        public async Task<IActionResult> GetIntegrationPensionInfoLog(long resumeId)
        {
            return Ok(await _iIntegrationLogRepository.GetintegrationLogsByIntegrationName(Helper.EnumHelper.IntegrationEnum.PensionFund, resumeId));
        }

    }
}
