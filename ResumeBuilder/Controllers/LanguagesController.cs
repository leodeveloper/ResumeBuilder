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
    public class LanguagesController : ControllerBase
    {
        readonly ILanguagesRepository _iLanguagesRepository;
        public LanguagesController(ILanguagesRepository iJobSeekerLanguageRepository)
        {
            _iLanguagesRepository = iJobSeekerLanguageRepository;
        }

        // GET: api/<JobSeekerLanguage>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iLanguagesRepository.GetAlljobSeekerLanguage(pageNumber, rowCount));
        }

        [HttpGet("GetAllJobSeekerLanguage")]
        public async Task<object> GetAllJobSeekerLanguage(DataSourceLoadOptions loadOptions)
        {
            var allJobSeekerLanguage = await _iLanguagesRepository.GetAlljobSeekerLanguage(1, 10000);
            return DataSourceLoader.Load(allJobSeekerLanguage.data, loadOptions);
        }

        [HttpGet("GetAllJobSeekerLanguageResume")]
        public async Task<object> GetAllJobSeekerLanguageResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allJobSeekerLanguage = await _iLanguagesRepository.GetjobSeekerLanguageByResumeId(resumeid);
                return DataSourceLoader.Load(allJobSeekerLanguage.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<JobSeekerLanguages>(), loadOptions);

        }

        // GET api/<JobSeekerLanguage>/5
        [HttpGet("GetByJobSeekerLanguageId/{id}")]
        public async Task<IActionResult> GetByJobSeekerLanguageId(long id)
        {
            return Ok(await _iLanguagesRepository.GetjobSeekerLanguageById(id));
        }

        // POST api/<JobSeekerLanguage>
        [HttpPost("InsertJobSeekerLanguage")]
        public async Task<IActionResult> InsertJobSeekerLanguage(JobSeekerLanguages jobSeekerLanguage)
        {
            return Ok(await _iLanguagesRepository.InsertjobSeekerLanguage(jobSeekerLanguage));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var jobSeekerLanguage = new JobSeekerLanguages();
            JsonConvert.PopulateObject(values, jobSeekerLanguage);

            if (!TryValidateModel(jobSeekerLanguage))
                return BadRequest();

            await _iLanguagesRepository.InsertjobSeekerLanguage(jobSeekerLanguage);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getJobSeekerLanguage = await _iLanguagesRepository.GetjobSeekerLanguageById(key);
            JsonConvert.PopulateObject(values, getJobSeekerLanguage.data);

            if (!TryValidateModel(getJobSeekerLanguage.data))
                return BadRequest();

            await _iLanguagesRepository.UpdatejobSeekerLanguage(getJobSeekerLanguage.data);
            return Ok();
        }

        // PUT api/<JobSeekerLanguage>/5
        [HttpPut("UpdateJobSeekerLanguage")]
        public async Task<IActionResult> UpdateJobSeekerLanguage(JobSeekerLanguages jobSeekerLanguage)
        {
            return Ok(await _iLanguagesRepository.UpdatejobSeekerLanguage(jobSeekerLanguage));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iLanguagesRepository.DeletejobSeekerLanguage(key));
        }
    }
}
