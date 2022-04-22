using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeBeneficiaryViewModel
    {
        public long? ResumeId { get; set; }
        public long? BeneficiaryNameId { get; set; }
    }

    public class HcResumeBeneficiaryViewModel_Get : HcResumeBeneficiaryViewModel
    {
        public long RID { get; set; }
        public string BeneficiaryNameTitle { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
