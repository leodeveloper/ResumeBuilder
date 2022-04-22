using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeSkillGroupViewModel
    {
        public long ResumeId { get; set; }
        public long SkillGroup { get; set; }
        public long? Occupation { get; set; }
        public short? Proficiencylevel { get; set; }
    }

    public class HcResumeSkillGroupViewModel_Get : HcResumeSkillGroupViewModel
    {
        public long Rid { get; set; }
        public string SkillGroupEn { get; set; }
        public string SkillGroupAe { get; set; }
        public string OccupationEn { get; set; }
        public string OccupationAe { get; set; }
        public string ProficiencylevelEn { get; set; }
        public string ProficiencylevelAe { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
        public bool Is_Deleted { get; set; }
    }
}
