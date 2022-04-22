using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeBuilder.Dto;
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
    [ApiController] //do not remove this
    // [ApiController]
    public class TrainingController : ControllerBase
    {
        readonly ITrainingRepository _iTrainingRepository;
        public TrainingController(ITrainingRepository iTrainingRepository)
        {
            _iTrainingRepository = iTrainingRepository;
        }

        [HttpGet("GetTrainingBatchs")]
        public async Task<IActionResult> GetTrainingBatchs(long resume_ID)
        {
            string userName = User.Identity.Name.Split('\\').Last();
            return Ok(await _iTrainingRepository.GetTrainingBatchs(resume_ID, userName));
        }

        [HttpGet("GetTrainingBatchsByIsEnrol")]
        public async Task<object> GetTrainingBatchsByIsEnrol(long resume_ID, bool IsAlreadyEnroll, DataSourceLoadOptions loadOptions)
        {
            string userName = User.Identity.Name.Split('\\').Last();
            var batchTraining = await _iTrainingRepository.GetTrainingBatchs(resume_ID, userName);
            return DataSourceLoader.Load(batchTraining.data.Where(z => z.Is_AlReadyEnrol == IsAlreadyEnroll), loadOptions);           
        }

        [HttpPost("PostTrainingBatchs")]
        public async Task<IActionResult> PostTrainingBatchs(TrainingApply trainingApply)
        {

           // TrainingApply trainingApply = new TrainingApply { Resume_ID=Resume_ID, Batch_ID = Batch_ID.ToList() };
            string userName = User.Identity.Name.Split('\\').Last();
            return Ok(await _iTrainingRepository.PostTrainingBatchs(trainingApply, userName));
        }

        // GET: api/<Training>
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageNumber = 1, int rowCount = 10)
        {
            return Ok(await _iTrainingRepository.GetAlltraining(pageNumber, rowCount));
        }

        [HttpGet("GetAllTraining")]
        public async Task<object> GetAllTraining(DataSourceLoadOptions loadOptions)
        {
            var allTraining = await _iTrainingRepository.GetAlltraining(1, 10000);
            return DataSourceLoader.Load(allTraining.data, loadOptions);
        }

        [HttpGet("GetAllTrainingResume")]
        public async Task<object> GetAllTrainingResume(int resumeid, DataSourceLoadOptions loadOptions)
        {
            if (resumeid > 0)
            {
                var allTraining = await _iTrainingRepository.GettrainingByResumeId(resumeid);
                return DataSourceLoader.Load(allTraining.data, loadOptions);
            }
            return DataSourceLoader.Load(new List<Training>(), loadOptions);

        }

        // GET api/<Training>/5
        [HttpGet("GetByTrainingId/{id}")]
        public async Task<IActionResult> GetByTrainingId(long id)
        {
            return Ok(await _iTrainingRepository.GettrainingById(id));
        }

        // POST api/<Training>
        [HttpPost("InsertTraining")]
        public async Task<IActionResult> InsertTraining(Training training)
        {
            return Ok(await _iTrainingRepository.Inserttraining(training));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var training = new Training();
            JsonConvert.PopulateObject(values, training);

            if (!TryValidateModel(training))
                return BadRequest();

            await _iTrainingRepository.Inserttraining(training);

            //_data.Employees.Add(newEmployee);
            //_data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {

            var getTraining = await _iTrainingRepository.GettrainingById(key);
            JsonConvert.PopulateObject(values, getTraining.data);

            if (!TryValidateModel(getTraining.data))
                return BadRequest();

            await _iTrainingRepository.Updatetraining(getTraining.data);
            return Ok();
        }

        // PUT api/<Training>/5
        [HttpPut("UpdateTraining")]
        public async Task<IActionResult> UpdateTraining(Training training)
        {
            return Ok(await _iTrainingRepository.Updatetraining(training));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return Ok(await _iTrainingRepository.Deletetraining(key));
        }
    }
}
