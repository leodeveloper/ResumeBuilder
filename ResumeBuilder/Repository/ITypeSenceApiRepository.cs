using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface ITypeSenceApiRepository
    {
        void UpdateTypeSenceIndex(long resumeId);
    }
}