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
    // [ApiController]
    public class ReferenceController : ControllerBase
    {
        readonly IReferenceRepository _iReferenceRepository;
        public ReferenceController(IReferenceRepository iReferenceRepository)
        {
            _iReferenceRepository = iReferenceRepository;
        }

        // GET: api/<Reference>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iReferenceRepository.GetAllreference(pageNumber, rowCount));
        }

        [HttpGet("GetAllReference")]
        public async Task<object> GetAllReference(DataSourceLoadOptions loadOptions)
        {
            var allReference = await _iReferenceRepository.GetAllreference(1, 10000);
            return DataSourceLoader.Load(allReference.data, loadOptions);
        }

        [HttpGet("GetAllReferenceResume")]
        public async Task<object> GetAllReferenceResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allreference = await _iReferenceRepository.GetreferenceByResumeId(resumeid);
                return DataSourceLoader.Load(allreference.data, loadOptions);
            }
            return  DataSourceLoader.Load(new List<Reference>(), loadOptions);

        }

        // GET api/<Reference>/5
        [HttpGet("GetByReferenceId/{id}")]
        public async Task<IActionResult> GetByReferenceId(long id)
        {
            return Ok(await _iReferenceRepository.GetreferenceById(id));
        }

        // POST api/<Reference>
        [HttpPost("InsertReference")]
        public async Task<IActionResult> InsertReference(Reference reference)
        {
            return Ok(await _iReferenceRepository.Insertreference(reference));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var reference = new Reference();
            JsonConvert.PopulateObject(values, reference);

            if (!TryValidateModel(reference))
                return BadRequest();

            await _iReferenceRepository.Insertreference(reference);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getReference = await _iReferenceRepository.GetreferenceById(key);
            JsonConvert.PopulateObject(values, getReference.data);

            if (!TryValidateModel(getReference.data))
                return BadRequest();

            await _iReferenceRepository.Updatereference(getReference.data);
            return Ok();
        }

        // PUT api/<Reference>/5
        [HttpPut("UpdateReference")]
        public async Task<IActionResult> UpdateReference(Reference reference)
        {
            return Ok(await _iReferenceRepository.Updatereference(reference));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iReferenceRepository.Deletereference(key));
        }
    }
}
