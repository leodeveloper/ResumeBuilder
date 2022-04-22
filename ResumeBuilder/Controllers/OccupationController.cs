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
    public class OccupationController : ControllerBase
    {
        readonly IOccupationRepository _iOccupationRepository;
        public OccupationController(IOccupationRepository iOccupationRepository)
        {
            _iOccupationRepository = iOccupationRepository;
        }

        // GET: api/<Occupation>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iOccupationRepository.GetAlloccupation(pageNumber, rowCount));
        }

        [HttpGet("GetAllOccupation")]
        public async Task<object> GetAllOccupation(DataSourceLoadOptions loadOptions)
        {
            var allOccupation = await _iOccupationRepository.GetAlloccupation(1, 10000);
            return DataSourceLoader.Load(allOccupation.data, loadOptions);
        }

        [HttpGet("GetAllOccupationResume")]
        public async Task<object> GetAllOccupationResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allOccupation = await _iOccupationRepository.GetoccupationByResumeId(resumeid);
                return DataSourceLoader.Load(allOccupation.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Occupation>(), loadOptions);

        }

        // GET api/<Occupation>/5
        [HttpGet("GetByOccupationId/{id}")]
        public async Task<IActionResult> GetByOccupationId(long id)
        {
            return Ok(await _iOccupationRepository.GetoccupationById(id));
        }

        // POST api/<Occupation>
        [HttpPost("InsertOccupation")]
        public async Task<IActionResult> InsertOccupation(Occupation occupation)
        {
            return Ok(await _iOccupationRepository.Insertoccupation(occupation));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var occupation = new Occupation();
            JsonConvert.PopulateObject(values, occupation);

            if (!TryValidateModel(occupation))
                return BadRequest();

            await _iOccupationRepository.Insertoccupation(occupation);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getOccupation = await _iOccupationRepository.GetoccupationById(key);
            JsonConvert.PopulateObject(values, getOccupation.data);

            if (!TryValidateModel(getOccupation.data))
                return BadRequest();

            await _iOccupationRepository.Updateoccupation(getOccupation.data);
            return Ok();
        }

        // PUT api/<Occupation>/5
        [HttpPut("UpdateOccupation")]
        public async Task<IActionResult> UpdateOccupation(Occupation occupation)
        {
            return Ok(await _iOccupationRepository.Updateoccupation(occupation));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iOccupationRepository.Deleteoccupation(key));
        }
    }
}
