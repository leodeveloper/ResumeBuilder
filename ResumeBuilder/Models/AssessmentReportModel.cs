using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
  

    public class AssessmentReportModel
    {
        public string JobSeekerId { get; set; }
        public string EmiratesId { get; set; }
        public IList<Answer> Answers { get; set; }
    }

    public class Questions
    {
        public string Question { get; set; }
    }

    public class Answer : Questions
    {
        public string UserAnswers { get; set; }
    }

    //Report
    public class AssessmentQuestionAnswerReportViewModel
    {
        public AssessmentQuestionAnswerReportViewModel()
        {
            this.Questions = new List<QuestionAnswerResultViewModel>();
            this.UserAnswers = new List<AssessmentReportViewModel>();
        }
        public IList<QuestionAnswerResultViewModel> Questions { get; set; }
        public IList<AssessmentReportViewModel> UserAnswers { get; set; }
    }
    public class QuestionAnswerResultViewModel
    {

        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public string QuestionAr { get; set; }
        public string QuestionSmallName { get; set; }
    }

    public class AssessmentReportViewModel
    {
        public AssessmentReportViewModel()
        {
            this.AssessmentQuestionAnswers = new List<AssessmentQuestionAnswers>();
        }
        public string EmployerName { get; set; }
        public string EmployerNumber { get; set; }
        public string AssessmentUserID { get; set; }
        public IList<AssessmentQuestionAnswers> AssessmentQuestionAnswers { get; set; }
    }

    public class AssessmentQuestionAnswers
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
