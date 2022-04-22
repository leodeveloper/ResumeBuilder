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
    public class CertificationController : ControllerBase
    {
        readonly ICertificationRepository _iCertificationRepository;
        public CertificationController(ICertificationRepository iCertificationRepository)
        {
            _iCertificationRepository = iCertificationRepository;
        }

        // GET: api/<Certification>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iCertificationRepository.GetAllcertification(pageNumber, rowCount));
        }

        [HttpGet("GetAllCertification")]
        public async Task<object> GetAllCertification(DataSourceLoadOptions loadOptions)
        {
            var allCertification = await _iCertificationRepository.GetAllcertification(1, 10000);
            return DataSourceLoader.Load(allCertification.data, loadOptions);
        }

        [HttpGet("GetAllCertificationResume")]
        public async Task<object> GetAllCertificationResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allCertification = await _iCertificationRepository.GetcertificationByResumeId(resumeid);
                return DataSourceLoader.Load(allCertification.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Certification>(), loadOptions);

        }

        // GET api/<Certification>/5
        [HttpGet("GetByCertificationId/{id}")]
        public async Task<IActionResult> GetByCertificationId(long id)
        {
            return Ok(await _iCertificationRepository.GetcertificationById(id));
        }

        // POST api/<Certification>
        [HttpPost("InsertCertification")]
        public async Task<IActionResult> InsertCertification(Certification certification)
        {
            return Ok(await _iCertificationRepository.Insertcertification(certification));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var certification = new Certification();
            JsonConvert.PopulateObject(values, certification);

            if (!TryValidateModel(certification))
                return BadRequest();

            await _iCertificationRepository.Insertcertification(certification);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getCertification = await _iCertificationRepository.GetcertificationById(key);
            JsonConvert.PopulateObject(values, getCertification.data);

            if (!TryValidateModel(getCertification.data))
                return BadRequest();

            await _iCertificationRepository.Updatecertification(getCertification.data);
            return Ok();
        }

        // PUT api/<Certification>/5
        [HttpPut("UpdateCertification")]
        public async Task<IActionResult> UpdateCertification(Certification certification)
        {
            return Ok(await _iCertificationRepository.Updatecertification(certification));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int key)
        {
            return Ok(await _iCertificationRepository.Deletecertification(key));
        }
    }
}
