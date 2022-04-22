using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeSkillViewModel
    {
        public long ResumeId { get; set; }
        public long SkillId { get; set; }
        public short? Proficiencylevel { get; set; }
    }

    public class HcResumeSkillViewModel_Get : HcResumeSkillViewModel
    {
        public long Rid { get; set; }
        public string SkillsTypeEn { get; set; }
        public string SkillsTypeAe { get; set; }

        public string ProficiencylevelEn { get; set; }
        public string ProficiencylevelAe { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }

}
