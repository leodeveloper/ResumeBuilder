using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeEducationViewModel
    {
        
        public long ResumeId { get; set; }
        public long GroupId { get; set; }
        public long EducationTypeId { get; set; }
        public long MajorId { get; set; }
        public int GradeId { get; set; }
        public string Score { get; set; }
        public long UniversityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsHighestEducation { get; set; }
        public bool IsEducationAbroad { get; set; }
        public long? EducationAbroadCountryId { get; set; }
    }

    public class HcResumeEducationViewModel_Get : HcResumeEducationViewModel
    {
        public long Rid { get; set; }

        public string GroupTitle { get; set; }
        public string EducationTypeTitle { get; set; }
        public string MajorTitle { get; set; }
        public string GradeTitle { get; set; }

        public string UniversityName { get; set; }
        public string CountryName { get; set; }

        public string GroupTitleAe { get; set; }
        public string EducationTypeTitleAe { get; set; }
        public string MajorTitleAe { get; set; }
        public string GradeTitleAe { get; set; }

        public string UniversityNameAe { get; set; }

        public string CountryNameAe { get; set; }

        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public bool Is_Deleted { get; set; }

    }
}
