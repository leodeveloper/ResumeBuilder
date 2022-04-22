using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeLanguagesViewModel
    {
        public long ResumeId { get; set; }
        public long LanguageId { get; set; }
        public short Proficiencylevel { get; set; }
        public short ReadLevel { get; set; }
        public short WriteLevel { get; set; }
        public short SpeakLevel { get; set; }

    }

    public class HcResumeLanguagesViewModel_Get : HcResumeLanguagesViewModel
    {
        public long Rid { get; set; }
        public string LanguageEn { get; set; }
        public string LanguageAe { get; set; }

        public string ProficiencylevelEn { get; set; }
        public string ProficiencylevelAe { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
