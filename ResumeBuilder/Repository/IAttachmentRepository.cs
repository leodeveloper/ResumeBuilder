using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IAttachmentRepository
    {
        Task<bool> DeleteAttachment(long Rid);
        Task<Webresponse<JobSeekerAttachment>> GetattachmentById(long rid);
        Task<Webresponse<IList<JobSeekerAttachment>>> GetattachmentByResumeId(long resumeId);
        Task<bool> Insertattachment(JobSeekerAttachment attachment);
        Task<bool> Updateattachment(JobSeekerAttachment attachment);
    }
}