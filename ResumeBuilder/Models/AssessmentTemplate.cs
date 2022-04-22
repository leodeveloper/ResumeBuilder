using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    /// <summary>
    /// This model is used for the Assessments
    /// </summary>
    public class AssessmentTemplate
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public bool EnableScoreCard { get; set; }
        public int? TemplateTypeID { get; set; }
        public bool Is_Deleted { get; set; }
    }

    public class AssessmentQuestion
    {
        public Guid ID { get; set; }
        public Guid? TemplateID { get; set; }       
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public int DataTypeID { get; set; }
        public string AnswerChoices { get; set; }
        public string AnswerChoicesAr { get; set; }
        public int Weightage { get; set; }
        public int TimeInSec { get; set; }
        public bool IsMandatory { get; set; }        
        public string AnswerTag { get; set; }       
        public int QuestionStageID { get; set; }
        public bool Is_Deleted { get; set; }
        public string DefaultAnswer { get; set; }
        public string DefaultAnswerAr { get; set; }
        public int Type { get; set; }
    }
}
