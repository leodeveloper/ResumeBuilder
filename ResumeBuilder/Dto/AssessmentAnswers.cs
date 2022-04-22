using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    
    public class AssessmentAnswers
    {
        /// <summary>
        /// question
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Answers
        /// </summary>
        public string value { get; set; }
    }

    //For updating the answer 
    public class AssesmentAnswer
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public string Answer { get; set; }
        public string AssessmentUserId { get; set; }
    }
}
