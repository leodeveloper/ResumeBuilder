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
    public class ToolsKnowledgeController : ControllerBase
    {
        readonly IToolsKnowledgeRepository _iToolsKnowledgeRepository;
        public ToolsKnowledgeController(IToolsKnowledgeRepository iToolsKnowledgeRepository)
        {
            _iToolsKnowledgeRepository = iToolsKnowledgeRepository;
        }

        // GET: api/<ToolsKnowledge>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iToolsKnowledgeRepository.GetAlltoolsKnowledge(pageNumber, rowCount));
        }

        [HttpGet("GetAllToolsKnowledge")]
        public async Task<object> GetAllToolsKnowledge(DataSourceLoadOptions loadOptions)
        {
            var allToolsKnowledge = await _iToolsKnowledgeRepository.GetAlltoolsKnowledge(1, 10000);
            return DataSourceLoader.Load(allToolsKnowledge.data, loadOptions);
        }

        [HttpGet("GetAllToolsKnowledgeResume")]
        public async Task<object> GetAllToolsKnowledgeResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            var allToolsKnowledge = await _iToolsKnowledgeRepository.GettoolsKnowledgeByResumeId(resumeid);
            return DataSourceLoader.Load(allToolsKnowledge.data, loadOptions);
        }

        // GET api/<ToolsKnowledge>/5
        [HttpGet("GetByToolsKnowledgeId/{id}")]
        public async Task<IActionResult> GetByToolsKnowledgeId(long id)
        {
            return Ok(await _iToolsKnowledgeRepository.GettoolsKnowledgeById(id));
        }

        // POST api/<ToolsKnowledge>
        [HttpPost("InsertDeleteToolsKnowledge")]
        public async Task<IActionResult> InsertDeleteToolsKnowledge(ToolsKnowledgeViewModel toolsKnowledgeVM)
        { 
            return Ok(await _iToolsKnowledgeRepository.InsertDeletetoolsKnowledge(toolsKnowledgeVM));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var toolsKnowledge = new ToolsKnowledge();
            JsonConvert.PopulateObject(values, toolsKnowledge);

            if (!TryValidateModel(toolsKnowledge))
                return BadRequest();

            await _iToolsKnowledgeRepository.InserttoolsKnowledge(toolsKnowledge);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getToolsKnowledge = await _iToolsKnowledgeRepository.GettoolsKnowledgeById(key);
            JsonConvert.PopulateObject(values, getToolsKnowledge.data);

            if (!TryValidateModel(getToolsKnowledge.data))
                return BadRequest();

            await _iToolsKnowledgeRepository.UpdatetoolsKnowledge(getToolsKnowledge.data);
            return Ok();
        }

        // PUT api/<ToolsKnowledge>/5
        [HttpPut("UpdateToolsKnowledge")]
        public async Task<IActionResult> UpdateToolsKnowledge(ToolsKnowledge toolsKnowledge)
        {
            return Ok(await _iToolsKnowledgeRepository.UpdatetoolsKnowledge(toolsKnowledge));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iToolsKnowledgeRepository.DeletetoolsKnowledge(key));
        }
    }
}
