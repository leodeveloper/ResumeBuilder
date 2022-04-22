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
    //[ApiController]
    public class AttachmentController : ControllerBase
    {
        readonly IAttachmentRepository _iAttachmentRepository;
        public AttachmentController(IAttachmentRepository iAttachmentRepository)
        {
            _iAttachmentRepository = iAttachmentRepository;
        }
      

        [HttpGet("GetAllAttachmentResume")]
        public async Task<object> GetAllAttachmentResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allAttachment = await _iAttachmentRepository.GetattachmentByResumeId(resumeid);
                return DataSourceLoader.Load(allAttachment.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<JobSeekerAttachment>(), loadOptions);

        }

        // GET api/<Attachment>/5
        [HttpGet("GetByAttachmentId/{id}")]
        public async Task<IActionResult> GetByAttachmentId(long id)
        {
            return Ok(await _iAttachmentRepository.GetattachmentById(id));
        }

        // POST api/<Attachment>
        [HttpPost("InsertAttachment")]
        public async Task<IActionResult> InsertAttachment(JobSeekerAttachment attachment)
        {
            return Ok(await _iAttachmentRepository.Insertattachment(attachment));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var attachment = new JobSeekerAttachment();
            JsonConvert.PopulateObject(values, attachment);
            if (!TryValidateModel(attachment))
                return BadRequest();
            await _iAttachmentRepository.Insertattachment(attachment);     

            return Ok();
        }   

       

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getAttachment = await _iAttachmentRepository.GetattachmentById(key);
            JsonConvert.PopulateObject(values, getAttachment.data);

            if (!TryValidateModel(getAttachment.data))
                return BadRequest();

            await _iAttachmentRepository.Updateattachment(getAttachment.data);
            return Ok();
        }

        // PUT api/<Attachment>/5
        [HttpPut("UpdateAttachment")]
        public async Task<IActionResult> UpdateAttachment(JobSeekerAttachment attachment)
        {
            return Ok(await _iAttachmentRepository.Updateattachment(attachment));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iAttachmentRepository.DeleteAttachment(key));
        }

     
    }
}

