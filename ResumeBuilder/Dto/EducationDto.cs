using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
   
    public class EducationDto
    {
        public EducationDto()
        {
            this.Education = new Education();
        }

        public Education Education { get; set; }
        public string Education_Group { get; set; }
        public string Education_GroupAr { get; set; }
        public string University_Type { get; set; }
        public string University_TypeAr { get; set; }
        public string University { get; set; }
        public string UniversityAr { get; set; }
        public string Course { get; set; }
        public string CourseAr { get; set; }
        public string Education_Major { get; set; }
        public string Education_MajorAr { get; set; }
        public string Education_Type { get; set; }
        public string Education_TypeAr { get; set; }
        public string Education_Emirates { get; set; }
        public string Education_EmiratesAr { get; set; }
        public bool ThisIsHighestEducation { get; set; }
    }
}
