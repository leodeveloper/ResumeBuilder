using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumePodViewModel
    {
        public long? ResumeId { get; set; }
        public long? Benefitid { get; set; }
    }

    public class HcResumePodViewModel_Get : HcResumePodViewModel
    {
        public long RID { get; set; }
        public string BenefitTitle { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
