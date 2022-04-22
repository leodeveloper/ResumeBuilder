using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [ApiController]
      public class PeopleofDeterminationController : ControllerBase
    {
        readonly IPeopleofDeterminationRepository _iPeopleofDeterminationRepository;
        public PeopleofDeterminationController(IPeopleofDeterminationRepository iPeopleofDeterminationRepository)
        {
            _iPeopleofDeterminationRepository = iPeopleofDeterminationRepository;
        }

        // GET: api/<PeopleofDetermination>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iPeopleofDeterminationRepository.GetAlljobSeekerPod(pageNumber, rowCount));
        }

        [HttpGet("GetAllPeopleofDetermination")]
        public async Task<object> GetAllPeopleofDetermination(DataSourceLoadOptions loadOptions)
        {
            var allPeopleofDetermination = await _iPeopleofDeterminationRepository.GetAlljobSeekerPod(1, 10000);
            return DataSourceLoader.Load(allPeopleofDetermination.data, loadOptions);
        }

        [HttpGet("GetAllPeopleofDeterminationResume")]
        public async Task<object> GetAllPeopleofDeterminationResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            var allPeopleofDetermination = await _iPeopleofDeterminationRepository.GetjobSeekerPodByResumeId(resumeid);
            return DataSourceLoader.Load(allPeopleofDetermination.data, loadOptions);
        }

        // GET api/<PeopleofDetermination>/5
        [HttpGet("GetByPeopleofDeterminationId/{id}")]
        public async Task<IActionResult> GetByPeopleofDeterminationId(long id)
        {
            return Ok(await _iPeopleofDeterminationRepository.GetjobSeekerPodById(id));
        }

        // POST api/<PeopleofDetermination>
        [HttpPost("InsertDeletePeopleofDetermination")]
        public async Task<IActionResult> InsertDeletePeopleofDetermination(JobSeekerPodViewModel jobSeekerPodVM)
        {
            return Ok(await _iPeopleofDeterminationRepository.InsertDeletejobSeekerPod(jobSeekerPodVM));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var jobSeekerPod = new JobSeekerPod();
            JsonConvert.PopulateObject(values, jobSeekerPod);

            if (!TryValidateModel(jobSeekerPod))
                return BadRequest();

            await _iPeopleofDeterminationRepository.InsertjobSeekerPod(jobSeekerPod);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getPeopleofDetermination = await _iPeopleofDeterminationRepository.GetjobSeekerPodById(key);
            JsonConvert.PopulateObject(values, getPeopleofDetermination.data);

            if (!TryValidateModel(getPeopleofDetermination.data))
                return BadRequest();

            await _iPeopleofDeterminationRepository.UpdatejobSeekerPod(getPeopleofDetermination.data);
            return Ok();
        }

        // PUT api/<PeopleofDetermination>/5
        [HttpPut("UpdatePeopleofDetermination")]
        public async Task<IActionResult> UpdatePeopleofDetermination(JobSeekerPod jobSeekerPod)
        {
            return Ok(await _iPeopleofDeterminationRepository.UpdatejobSeekerPod(jobSeekerPod));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iPeopleofDeterminationRepository.DeletejobSeekerPod(key));
        }
    }
}
