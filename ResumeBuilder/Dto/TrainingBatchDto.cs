using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class TrainingBatchDto
    {
        public Guid ID { get; set; }
        public Guid Training_Program_ID { get; set; }
        public string Title { get; set; }
        public int Training_Type_ID { get; set; }
        public int Employer_ID { get; set; }
        public int Vacancy_ID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool DaysInSunday { get; set; }
        public bool DaysInMonday { get; set; }
        public bool DaysInTuesday { get; set; }
        public bool DaysInWednesday { get; set; }
        public bool DaysInThursday { get; set; }
        public bool DaysInFriday { get; set; }
        public bool DaysInSaturday { get; set; }
        public int Total_Training_Hours { get; set; }
        public int Emirates_ID { get; set; }
        public int Location_ID { get; set; }
        public bool Is_Deleted { get; set; }
        public bool Is_AlReadyEnrol { get; set; }
    }
}
