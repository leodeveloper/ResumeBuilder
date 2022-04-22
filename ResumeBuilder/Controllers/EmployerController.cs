using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeBuilder.Controllers
{
    [Route("api/[controller]")]
   // [ApiController]
    public class EmployerController : ControllerBase
    {
        readonly IEmployerRepository _iEmployerRepository;
        public EmployerController(IEmployerRepository iEmployerRepository)
        {
            _iEmployerRepository = iEmployerRepository;
        }

        // GET: api/<Employer>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iEmployerRepository.GetAllemployer(pageNumber, rowCount));
        }

        [HttpGet("GetAllEmployer")]
        public async Task<object> GetAllEmployer(DataSourceLoadOptions loadOptions)
        {
            var allEmployer = await _iEmployerRepository.GetAllemployer(1, 10000);
            return DataSourceLoader.Load(allEmployer.data, loadOptions);
        }

        [HttpGet("GetAllEmployerResume")]
        public async Task<object> GetAllEmployerResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allEmployer = await _iEmployerRepository.GetemployerByResumeId(resumeid);
                return DataSourceLoader.Load(allEmployer.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Employer>(), loadOptions);

        }

        // GET api/<Employer>/5
        [HttpGet("GetByEmployerId/{id}")]
        public async Task<IActionResult> GetByEmployerId(long id)
        {
            return Ok(await _iEmployerRepository.GetemployerById(id));
        }

        // POST api/<Employer>
        [HttpPost("InsertEmployer")]
        public async Task<IActionResult> InsertEmployer(Employer employer)
        {
            return Ok(await _iEmployerRepository.Insertemployer(employer));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var employer = new Employer();
            JsonConvert.PopulateObject(values, employer);

            if (!TryValidateModel(employer))
                return BadRequest();

            await _iEmployerRepository.Insertemployer(employer);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getEmployer = await _iEmployerRepository.GetemployerById(key);
            if (getEmployer.data == null)
                return BadRequest();
            JsonConvert.PopulateObject(values, getEmployer.data);          

            if (!TryValidateModel(getEmployer.data))
                return BadRequest();

            await _iEmployerRepository.Updateemployer(getEmployer.data);
            return Ok();
        }

        // PUT api/<Employer>/5
        [HttpPut("UpdateEmployer")]
        public async Task<IActionResult> UpdateEmployer(Employer employer)
        {
            return Ok(await _iEmployerRepository.Updateemployer(employer));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iEmployerRepository.Deleteemployer(key));
        }
    }
}
