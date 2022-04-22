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
    public class NotesController : ControllerBase
    {
        readonly INotesRepository _iNotesRepository;
        public NotesController(INotesRepository iNotesRepository)
        {
            _iNotesRepository = iNotesRepository;
        }

        // GET: api/<Notes>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iNotesRepository.GetAllnotes(pageNumber, rowCount));
        }

        [HttpGet("GetAllNotes")]
        public async Task<object> GetAllNotes(DataSourceLoadOptions loadOptions)
        {
            var allNotes = await _iNotesRepository.GetAllnotes(1, 10000);
            return DataSourceLoader.Load(allNotes.data, loadOptions);
        }

        [HttpGet("GetAllNotesResume")]
        public async Task<object> GetAllNotesResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allNotes = await _iNotesRepository.GetnotesByResumeId(resumeid);
                return DataSourceLoader.Load(allNotes.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Notes>(), loadOptions);

        }

        // GET api/<Notes>/5
        [HttpGet("GetByNotesId/{id}")]
        public async Task<IActionResult> GetByNotesId(long id)
        {
            return Ok(await _iNotesRepository.GetnotesById(id));
        }

        // POST api/<Notes>
        [HttpPost("InsertNotes")]
        public async Task<IActionResult> InsertNotes(Notes notes)
        {
            return Ok(await _iNotesRepository.Insertnotes(notes));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var notes = new Notes();
            JsonConvert.PopulateObject(values, notes);

            if (!TryValidateModel(notes))
                return BadRequest();

            notes.UserName = User.Identity.Name;
            await _iNotesRepository.Insertnotes(notes);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getNotes = await _iNotesRepository.GetnotesById(key);
            JsonConvert.PopulateObject(values, getNotes.data);

            if (!TryValidateModel(getNotes.data))
                return BadRequest();

            await _iNotesRepository.Updatenotes(getNotes.data);
            return Ok();
        }

        // PUT api/<Notes>/5
        [HttpPut("UpdateNotes")]
        public async Task<IActionResult> UpdateNotes(Notes notes)
        {
            return Ok(await _iNotesRepository.Updatenotes(notes));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iNotesRepository.Deletenotes(key));
        }
    }
}
