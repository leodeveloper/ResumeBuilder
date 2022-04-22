using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface ITrainingRepository
    {
        Task<WebresponsePaging<IList<Training>>> GetAlltraining(int pageNumber, int rowCount);
        Task<Webresponse<IList<Training>>> GettrainingByResumeId(long resumeId);
        Task<Webresponse<Training>> GettrainingById(long rid);
        Task<bool> Inserttraining(Training training);
        Task<bool> Updatetraining(Training training);
        Task<bool> Deletetraining(long Rid);

        Task<Webresponse<IList<TrainingBatchDto>>> GetTrainingBatchs(long resumeId, string userName);
        Task<WebresponseNoData> PostTrainingBatchs(TrainingApply trainingApply, string userName);
    }
}