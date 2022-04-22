using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeBuilder.Models;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Route("api/[controller]")]
   // [ApiController]
    public class EducationController : ControllerBase
    {
        readonly IEducationRepository _iEducationRepository;
        public EducationController(IEducationRepository iEducationRepository)
        {
            _iEducationRepository = iEducationRepository;
        }

        // GET: api/<Education>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iEducationRepository.GetAlleducation(pageNumber, rowCount));
        }

        [HttpGet("GetAllEducation")]
        public async Task<object> GetAllEducation(DataSourceLoadOptions loadOptions)
        {
            var allEducation = await _iEducationRepository.GetAlleducation(1, 10000);
            return DataSourceLoader.Load(allEducation.data, loadOptions);
        }

        [HttpGet("GetAllEducationResume")]
        public async Task<object> GetAllEducationResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if(resumeid > 0)
            {
                var allEducation = await _iEducationRepository.GeteducationByResumeId(resumeid);
                return DataSourceLoader.Load(allEducation.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Education>(),loadOptions);

        }

        // GET api/<Education>/5
        [HttpGet("GetByEducationId/{id}")]
        public async Task<IActionResult> GetByEducationId(long id)
        {
            return Ok(await _iEducationRepository.GeteducationById(id));
        }

        // GET api/<Education>/5
        [HttpGet("GetByEducationResumeId/{resumeid}")]
        public async Task<IActionResult> GetByEducationResumeId(long resumeid)
        {
            return Ok(await _iEducationRepository.GeteducationByResumeId(resumeid));
        }

        // POST api/<Education>
        [HttpPost("InsertEducation")]
        public async Task<IActionResult> InsertEducation(Education education)
        {
            return Ok(await _iEducationRepository.Inserteducation(education));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var education = new Education();
            JsonConvert.PopulateObject(values, education);

            if (!TryValidateModel(education))
                return BadRequest();

            await _iEducationRepository.Inserteducation(education);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getEducation = await _iEducationRepository.GeteducationById(key);
            JsonConvert.PopulateObject(values, getEducation.data);          

            if (!TryValidateModel(getEducation.data))
                return BadRequest();

            await _iEducationRepository.Updateeducation(getEducation.data);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {           
            return Ok(await _iEducationRepository.DeleteEducation(key));
        }

        // PUT api/<Education>/5
        [HttpPut("UpdateEducation")]
        public async Task<IActionResult> UpdateEducation(Education education)
        {
            return Ok(await _iEducationRepository.Updateeducation(education));
        }
    }
}
