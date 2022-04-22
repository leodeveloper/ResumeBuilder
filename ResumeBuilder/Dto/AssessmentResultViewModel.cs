using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class AssessmentResultUserViewModel
    {
        public AssessmentResultUserViewModel()
        {
            this.AssessmentResultViewModel = new List<AssessmentResultViewModel>();
        }
        public IList<AssessmentResultViewModel> AssessmentResultViewModel { get; set; }
    }
    public class AssessmentResultViewModel
    {
        public AssessmentResultViewModel()
        {
            this.QuestionResultViewModels = new List<QuestionResultViewModel>();
        }
        public Guid? TemplateId { get; set; }
        public string TemplateName { get; set; }
        public IList<QuestionResultViewModel> QuestionResultViewModels { get; set; }
    }

    public class QuestionResultViewModel
    {
        public QuestionResultViewModel()
        {
            this.AnswerResultViewModels = new List<AnswerResultViewModel>();
        }
        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public string QuestionAr { get; set; }
        public string DefaultAnswer { get; set; }
        public string DefaultAnswerAr { get; set; }
        public int Weightage { get; set; }
        public string AnswerChoices { get; set; }
        public string AnswerChoicesAr { get; set; }
        public int DataTypeID { get; set; }
        public IList<AnswerResultViewModel> AnswerResultViewModels { get; set; }
    }

    public class AnswerResultViewModel
    {
        public Guid AnswerId { get; set; }
        public string Answer { get; set; }
        public string AssessmentUserId { get; set; }
        public DateTime LastAnswerUpdate { get; set; }
    }

    public class AnswerReport
    {
        public Guid? TemplateID { get; set; }
        public string TemplateName { get; set; }
        public Guid QuestionID { get; set; }
        public string Question { get; set; }
        public string DefaultAnswer { get; set; }
        public string DefaultAnswerAr { get; set; }
        public Guid AnswerID { get; set; }
        public string Answer { get; set; }
        public int Weightage { get; set; }
        public string JobSeekerID { get; set; }
        public int Score { get; set; } 
        public int DataTypeID { get; set; }
        public string AnswerChoices { get; set; }
        public string AnswerChoicesAr { get; set; }

    }


    public class ConvertReport {
        public static IList<AnswerReport> CopytoAnswerReport(IList<AssessmentResultViewModel> assessmentResultViewModels, string jobSeekerId)
        {
            IList<AnswerReport> answerReports = new List<AnswerReport>();
            foreach (var assessmentResultViewModel in assessmentResultViewModels)
            {
                foreach (var questionViewModel in assessmentResultViewModel.QuestionResultViewModels)
                {
                    AnswerReport answerReport = new AnswerReport();
                    answerReport.TemplateID = assessmentResultViewModel.TemplateId;
                    answerReport.TemplateName = assessmentResultViewModel.TemplateName;
                    answerReport.QuestionID = questionViewModel.QuestionId;
                    answerReport.Question = questionViewModel.Question;
                    answerReport.Weightage = questionViewModel.Weightage;
                    answerReport.DefaultAnswer = questionViewModel.DefaultAnswer;
                    answerReport.DefaultAnswerAr = questionViewModel.DefaultAnswerAr;
                    answerReport.AnswerChoices = questionViewModel.AnswerChoices;
                    answerReport.AnswerChoicesAr = questionViewModel.AnswerChoicesAr;
                    answerReport.DataTypeID = questionViewModel.DataTypeID;


                    if (questionViewModel.AnswerResultViewModels.Any())
                    {
                        var _answer = questionViewModel.AnswerResultViewModels.FirstOrDefault();
                        answerReport.AnswerID = _answer.AnswerId;
                        answerReport.Answer = _answer.Answer;
                        answerReport.JobSeekerID = _answer.AssessmentUserId;
                        
                    }
                    else
                    {
                        answerReport.JobSeekerID = jobSeekerId;
                    }
                    
                    if ((string.IsNullOrEmpty(questionViewModel.DefaultAnswer) || string.IsNullOrEmpty(questionViewModel.DefaultAnswerAr)) 
                        && string.IsNullOrEmpty(answerReport.Answer))
                    {
                        answerReport.Score = 0;
                    }
                    else if ((string.IsNullOrEmpty(questionViewModel.DefaultAnswer) || string.IsNullOrEmpty(questionViewModel.DefaultAnswerAr)) 
                        && !string.IsNullOrEmpty(answerReport.Answer))
                    {
                        answerReport.Score = answerReport.Weightage;
                    }
                    else if(questionViewModel.DefaultAnswer == answerReport.Answer || questionViewModel.DefaultAnswerAr == answerReport.Answer)
                    {
                        answerReport.Score = answerReport.Weightage;
                    }
                    else if (questionViewModel.DefaultAnswer != answerReport.Answer || questionViewModel.DefaultAnswerAr != answerReport.Answer)
                    {
                        answerReport.Score = 0;
                    }

                    answerReports.Add(answerReport);
                }
            }

            return answerReports;
        }
    }

    
}
