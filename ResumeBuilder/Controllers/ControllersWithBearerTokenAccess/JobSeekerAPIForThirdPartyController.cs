using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Dto;
using ResumeBuilder.Helper;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Controllers.ControllersWithBearerTokenAccess
{
    [CustomAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerAPIForThirdPartyController : ControllerBase
    {
        readonly IResumeRepository _iResumeRepository;
        public JobSeekerAPIForThirdPartyController(IResumeRepository iResumeRepository)
        {
            _iResumeRepository = iResumeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("InsertResume")]
        public async Task<IActionResult> Post(Resume resume)
        {
            return Ok(await _iResumeRepository.InsertResume(resume));
        }

        // PUT api/<Resume>/5
        [HttpPut("UpdateResume")]
        public async Task<IActionResult> Put(Resume resume)
        {
            return Ok(await _iResumeRepository.UpdateResume(resume));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iResumeRepository.DeleteResume(key));
        }
    }
}
