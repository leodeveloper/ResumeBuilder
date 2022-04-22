using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumePreferenceIndustryViewModel
    {
        public long? ResumeId { get; set; }
        public long? IndustryId { get; set; }
    }

    public class HcResumePreferenceIndustryViewModel_Get : HcResumePreferenceIndustryViewModel
    {
        public long Rid { get; set; }
        public string Industry { get; set; }
        public string IndustryAe { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
