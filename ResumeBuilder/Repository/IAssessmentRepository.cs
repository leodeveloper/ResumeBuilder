using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IAssessmentRepository
    {
        Task<Webresponse<AnswerResults>> GetAnswersResult(string emiratesID);
        Task<Webresponse<IList<AssessmentQuestion>>> GetQuestionsByTemplateId(string Id);
        Task<Webresponse<IList<AssessmentTemplate>>> GetTemplates();
        Task<Webresponse<bool>> SaveAnswers(IList<AssessmentAnswers> assessmentAnswers, string templateId);
        Task<Webresponse<bool>> UpdateAnswer(AssesmentAnswer assessmentAnswer);
        Task<Webresponse<IList<AssessmentResultViewModel>>> GetAnswers(string jobseekerID, string templateID);
        Task<Webresponse<AssessmentQuestionAnswerReportViewModel>> GetAllAnswersByTemplateId(string templateID);
    }
}