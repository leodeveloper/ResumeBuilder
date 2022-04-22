using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Controllers.ControllersWithBearerTokenAccess
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeApiController : ControllerBase
    {
        readonly IResumeRepository _iResumeRepository;
        public ResumeApiController(IResumeRepository iResumeRepository)
        {
            _iResumeRepository = iResumeRepository;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("GetResumePreview/{id}")]
        public async Task<IActionResult> GetResumePreview(long id)
        {
            return Ok(await _iResumeRepository.Resume_Preview(id));
        }

        [HttpPost("GetMultipleResumeByIds")]
        public async Task<IActionResult> GetMultipleResumeByIds(long[] Rid)
        {
            return Ok(await _iResumeRepository.GetManyResumeByIds(Rid));
        }

        [HttpPost("UpdateJobSeekerStatustoHired")]
        public async Task<IActionResult> UpdateJobSeekerStatustoHired(long rId)
        {
            try
            {
                Webresponse<Resume> resume = await _iResumeRepository.GetResumeById(rId);
                resume.data.resumestatus = 4;
                await _iResumeRepository.UpdateResume(resume.data);
                //4 = Hired, 3 = Reason from the [JobSeeker].[dbo].[Reason_Status] table
                return Ok(await _iResumeRepository.InsertResumeStatus(new Models.JobsSeekerStatus { Resume_ID = rId, Status_ID = 4, Reason_ID = 3, UserId = User.Identity.Name }));

            }
            catch (Exception ex)
            {
                return Ok(new WebresponseNoData { message=ex.Message, status = APIStatus.error });            
            }
        }

    }
}
