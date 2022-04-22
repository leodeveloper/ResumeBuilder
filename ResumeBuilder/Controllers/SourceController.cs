using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    // [ApiController]
    public class SourceController : ControllerBase
    {
        readonly ISourceRepository _iSourceRepository;
        public SourceController(ISourceRepository iSourceRepository)
        {
            _iSourceRepository = iSourceRepository;
        }

        // GET: api/<Source>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 1000)
        {
            return Ok(await _iSourceRepository.GetAllsource(pageNumber, rowCount));
        }

        [HttpGet("GetAllSource")]
        public async Task<object> GetAllSource(DataSourceLoadOptions loadOptions)
        {
            var allSource = await _iSourceRepository.GetAllsource(1, 10000);
            return DataSourceLoader.Load(allSource.data, loadOptions);
        }

        [HttpGet("GetAllSourceResume")]
        public async Task<object> GetAllSourceResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allSource = await _iSourceRepository.GetsourceByResumeId(resumeid);
                return DataSourceLoader.Load(allSource.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Source>(), loadOptions);

        }

        // GET api/<Source>/5
        [HttpGet("GetBySourceId/{id}")]
        public async Task<IActionResult> GetBySourceId(long id)
        {
            return Ok(await _iSourceRepository.GetsourceById(id));
        }

        // POST api/<Source>
        [HttpPost("InsertSource")]
        public async Task<IActionResult> InsertSource(Source source)
        {
            source.CreatedUserName = User.Identity.Name;
            return Ok(await _iSourceRepository.Insertsource(source));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var source = new Source();
            JsonConvert.PopulateObject(values, source);
            
            if (!TryValidateModel(source))
                return BadRequest();
            source.CreatedUserName = User.Identity.Name;
            await _iSourceRepository.Insertsource(source);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getSource = await _iSourceRepository.GetsourceById(key);
            JsonConvert.PopulateObject(values, getSource.data);
          

            if (!TryValidateModel(getSource.data))
                return BadRequest();

            getSource.data.LastUpdatedDate = DateTime.Now;
            getSource.data.UpdateUserName = User.Identity.Name;
            await _iSourceRepository.Updatesource(getSource.data);
            return Ok();
        }

        // PUT api/<Source>/5
        [HttpPut("UpdateSource")]
        public async Task<IActionResult> UpdateSource(Source source)
        {
            source.LastUpdatedDate = DateTime.Now;
            source.UpdateUserName = User.Identity.Name;
            return Ok(await _iSourceRepository.Updatesource(source));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iSourceRepository.Deletesource(key, User.Identity.Name));
        }
    }
}
