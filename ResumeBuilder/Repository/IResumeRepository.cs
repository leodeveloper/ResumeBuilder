using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IResumeRepository
    {
        Task<bool> InsertResume(Resume resume);
        Task<bool> UpdateResume(Resume resume);
        Task<bool> DeleteResume(long Rid);
        Task<WebresponsePaging<IList<JobSeekerResume>>> GetAllResume(int pageNumber, int rowCount);
        Task<Webresponse<Resume>> GetResumeById(long rid);
        Task<Webresponse<Resume>> GetResumeByEmiratesId(string emiratedId);
        Task<Webresponse<IList<Resume>>> GetManyResumeByIds(long[] rid);
        Task<WebresponsePaging<IList<JobsSeekerStatus>>> GetResumeStatus(long resumeId);
        Task<WebresponseNoData> InsertResumeStatus(JobsSeekerStatus resume);
        Task<bool> UpdateResumeStatus(JobsSeekerStatus resume);
        Task<Webresponse<JobSeekerCoverLetter>> GetCoverLetter(long resume_ID);
        Task<bool> AddUpdateCoverLetter(JobSeekerCoverLetter jobSeekerCoverLetter);
        Task<Webresponse<string>> Insert(Resume_Attachment resume_Attachment);
        Task<Webresponse<Resume_Attachment>> GetAttachment(string mongodbUniqueId);
        Task<Webresponse<Job_ApplicationAttachmentMongoDB>> GetAttachmentJobApplication(string mongodbUniqueId, string collectionName);
        Task<Webresponse<ResumePreviewDto>> Resume_Preview(long resume_Id);
        Task<Webresponse<ChildJobSeekerCvphoto>> GetJobSeekerPhoto(long resumeId);
    }
}