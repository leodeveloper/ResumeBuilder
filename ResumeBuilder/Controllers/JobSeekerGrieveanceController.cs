using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class JobSeekerGrieveanceController : ControllerBase
    {
        readonly IJobSeekerGrieveanceRepository _iJobSeekerGrieveanceRepository;
        public JobSeekerGrieveanceController(IJobSeekerGrieveanceRepository iJobSeekerGrieveanceRepository)
        {
            _iJobSeekerGrieveanceRepository = iJobSeekerGrieveanceRepository;
        }

        // GET: api/<JobSeekerGrieveance>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iJobSeekerGrieveanceRepository.GetAlljobSeekerGrieveance(pageNumber, rowCount));
        }

        [HttpGet("GetAllJobSeekerGrieveance")]
        public async Task<object> GetAllJobSeekerGrieveance(DataSourceLoadOptions loadOptions)
        {
            var allJobSeekerGrieveance = await _iJobSeekerGrieveanceRepository.GetAlljobSeekerGrieveance(1, 10000);
            return DataSourceLoader.Load(allJobSeekerGrieveance.data, loadOptions);
        }

        [HttpGet("GetAllJobSeekerGrieveanceResume")]
        public async Task<object> GetAllJobSeekerGrieveanceResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allJobSeekerGrieveance = await _iJobSeekerGrieveanceRepository.GetjobSeekerGrieveanceByResumeId(resumeid);
                return DataSourceLoader.Load(allJobSeekerGrieveance.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<JobSeekerGrieveance>(), loadOptions);

        }

        // GET api/<JobSeekerGrieveance>/5
        [HttpGet("GetByJobSeekerGrieveanceId/{id}")]
        public async Task<IActionResult> GetByJobSeekerGrieveanceId(long id)
        {
            return Ok(await _iJobSeekerGrieveanceRepository.GetjobSeekerGrieveanceById(id));
        }

        // POST api/<JobSeekerGrieveance>
        [HttpPost("InsertJobSeekerGrieveance")]
        public async Task<IActionResult> InsertJobSeekerGrieveance(JobSeekerGrieveance jobSeekerGrieveance)
        {
            return Ok(await _iJobSeekerGrieveanceRepository.InsertjobSeekerGrieveance(jobSeekerGrieveance));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var jobSeekerGrieveance = new JobSeekerGrieveance();
            JsonConvert.PopulateObject(values, jobSeekerGrieveance);

            if (!TryValidateModel(jobSeekerGrieveance))
                return BadRequest();
            jobSeekerGrieveance.Createdate = System.DateTime.Now;
            jobSeekerGrieveance.Ticketno = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}/{System.DateTime.Now.Year}";
            jobSeekerGrieveance.Status = 1;
            //jobSeekerGrieveance.Createduserid = User.Identity.Name;
            await _iJobSeekerGrieveanceRepository.InsertjobSeekerGrieveance(jobSeekerGrieveance);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getJobSeekerGrieveance = await _iJobSeekerGrieveanceRepository.GetjobSeekerGrieveanceById(key);
            JsonConvert.PopulateObject(values, getJobSeekerGrieveance.data);

            if (!TryValidateModel(getJobSeekerGrieveance.data))
                return BadRequest();

            await _iJobSeekerGrieveanceRepository.UpdatejobSeekerGrieveance(getJobSeekerGrieveance.data);
            return Ok();
        }

        // PUT api/<JobSeekerGrieveance>/5
        [HttpPut("UpdateJobSeekerGrieveance")]
        public async Task<IActionResult> UpdateJobSeekerGrieveance(JobSeekerGrieveance jobSeekerGrieveance)
        {
            return Ok(await _iJobSeekerGrieveanceRepository.UpdatejobSeekerGrieveance(jobSeekerGrieveance));
        }

        //[HttpDelete]
        //public async Task<IActionResult> Delete(long key)
        //{
        //    return Ok(await _iJobSeekerGrieveanceRepository.DeletejobSeekerGrieveance(key));
        //}
    }
}
