using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IToolsKnowledgeRepository
    {
        Task<bool> DeletetoolsKnowledge(long rid);
        Task<WebresponsePaging<IList<ToolsKnowledge>>> GetAlltoolsKnowledge(int pageNumber, int rowCount);
        Task<Webresponse<ToolsKnowledge>> GettoolsKnowledgeById(long rid);
        Task<Webresponse<IList<ToolsKnowledge>>> GettoolsKnowledgeByResumeId(long resumeId);
        Task<bool> InserttoolsKnowledge(ToolsKnowledge toolsKnowledge);
        Task<bool> UpdatetoolsKnowledge(ToolsKnowledge toolsKnowledge);

        Task<bool> InsertDeletetoolsKnowledge(ToolsKnowledgeViewModel toolsKnowledgeVM);
        Task<Webresponse<IList<ToolsKnowledgeDto>>> GetToolsKnowledgeByResumeIdPreview(long resumeId);
    }
}