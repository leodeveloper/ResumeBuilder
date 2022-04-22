using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumePreferenceJobTitleViewModel
    {
        public long? ResumeId { get; set; }
        public long? JobTitleId { get; set; }
    }

    public class HcResumePreferenceJobTitleViewModel_Get : HcResumePreferenceJobTitleViewModel
    {
        public long Rid { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleAe { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
