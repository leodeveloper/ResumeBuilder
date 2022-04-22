using Microsoft.AspNetCore.Mvc;
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
    public class HraOpsLookupController : ControllerBase
    {
        IHraOpsLookupRepository _ihraopsLookupRepository;
        public HraOpsLookupController(IHraOpsLookupRepository ihraopsLookupRepository)
        {
            _ihraopsLookupRepository = ihraopsLookupRepository;
        }

        [HttpGet("GetNationalities")]
        public async Task<IActionResult> GetNationalities(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetNationalities(languageType));
        }

        [HttpGet("GetInternationalCountries")]
        public async Task<IActionResult> GetInternationalCountries(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetInternationalCountries(languageType));
        }

        [HttpGet("GetInternationalCitesByCountryId")]
        public async Task<IActionResult> GetInternationalCitesByCountryId(int countryId)
        {
            return Ok(await _ihraopsLookupRepository.GetInternationalCitesByCountryId(countryId));
        }

        [HttpGet("GetEmirates")]
        public async Task<IActionResult> GetEmirates(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetCountries(languageType));
        }

        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetStates(languageType));
        }

        [HttpGet("GetLocation")]
        public async Task<IActionResult> GetLocation(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetLocations(languageType));
        }

        [HttpGet("GetPrefferedLocation")]
        public async Task<IActionResult> GetPrefferedLocation(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetPrefferedLocation(languageType));
        }

        [HttpGet("GetDrivingLicenceType")]
        public async Task<IActionResult> GetDrivingLicenceType(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetDrivingLicenceType(languageType));
        }

        [HttpGet("GetMilitaryBatch")]
        public async Task<IActionResult> GetMilitaryBatch(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetMilitaryBatch(languageType));
        }

        [HttpGet("GetMartitalStatus")]
        public async Task<IActionResult> GetMartitalStatus(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetMartitalStatus(languageType));
        }

        [HttpGet("GetEducationGroup")]
        public async Task<IActionResult> GetEducationGroup(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetEducationGroup(languageType));
        }

        [HttpGet("GetEducationType")]
        public async Task<IActionResult> GetEducationType(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetEducationType(languageType));
        }

        [HttpGet("GetEducationMajor")]
        public async Task<IActionResult> GetEducationMajor(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetEducationMajor(languageType));
        }

        [HttpGet("GetUniversity")]
        public async Task<IActionResult> GetUniversity(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetUniversity(languageType));
        }

        [HttpGet("GetGrade")]
        public async Task<IActionResult> GetGrade(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetGrade(languageType));
        }

        [HttpGet("GetEmployer")]
        public async Task<IActionResult> GetEmployer(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetEmployer(languageType));
        }

        [HttpGet("GetJobTitle")]
        public async Task<IActionResult> GetJobTitle(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetJobTitle(languageType));
        }

        [HttpGet("GetJobIndustry")]
        public async Task<IActionResult> GetJobIndustry(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetJobIndustry(languageType));
        }

        [HttpGet("GetStaffTitle")]
        public async Task<IActionResult> GetStaffTitle(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetStaffTitle(languageType));
        }

        [HttpGet("GetSkillGroup")]
        public async Task<IActionResult> GetSkillGroup(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetSkillGroup(languageType));
        }

        [HttpGet("EuropeanStandardOccupation")]
        public async Task<IActionResult> EuropeanStandardOccupation(int languageType, long skillGroupId)
        {
            return Ok(await _ihraopsLookupRepository.EuropeanStandardOccupation(languageType, skillGroupId));
        }

        [HttpGet("GetSkillTypes")]
        public async Task<IActionResult> GetSkillTypes(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.SkillTypes(languageType));
        }

        [HttpGet("ToolsKnowledge")]
        public async Task<IActionResult> ToolsKnowledge(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.ToolsKnowledge(languageType));
        }

        [HttpGet("GetProficiencylevels")]
        public async Task<IActionResult> GetProficiencylevels(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.Proficiencylevels(languageType));
        }

        [HttpGet("GetDisablities")]
        public async Task<IActionResult> GetDisablities(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.Disablities(languageType));
        }

        [HttpGet("GetBeneficiaryName")]
        public async Task<IActionResult> GetBeneficiaryName(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.BeneficiaryName(languageType));
        }

        [HttpGet("GetCoach")]
        public async Task<IActionResult> GetCoach()
        {
            return Ok(await _ihraopsLookupRepository.GetCoach());
        }

        [HttpGet("GetLanguage")]
        public async Task<IActionResult> GetLanguage(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetLanguage(languageType));
        }

        [HttpGet("GetCertificateType")]
        public async Task<IActionResult> GetCertificateType(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetCertificateType(languageType));
        }

        [HttpGet("GetCertificateProvider")]
        public async Task<IActionResult> GetCertificateProvider()
        {
            return Ok(await _ihraopsLookupRepository.GetCertificateProvider());
        }

        [HttpGet("GetReferenceType")]
        public async Task<IActionResult> GetReferenceType(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetReferenceType(languageType));
        }

        [HttpGet("GetStatusReason")]
        public async Task<IActionResult> GetStatusReason(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetStatusReason(languageType));
        }

        [HttpGet("GetEngagementStatus")]
        public async Task<IActionResult> GetEngagementStatus(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetEngagementStatus(languageType));
        }

        [HttpGet("GetJPCStatus")]
        public async Task<IActionResult> GetJPCStatus(int languageType)
        {
            return Ok(await _ihraopsLookupRepository.GetJPCStatus(languageType));
        }

    }
}
