using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeEmployerViewModel
    {
        public long EmployerId { get; set; }
        public long ResumeId { get; set; }
        public long JobTitleId { get; set; }
        public long JobIndustryId { get; set; }
        public bool IsJobInUae { get; set; }
        public long? JobUaeCityId { get; set; }
        public long CountryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Enddate { get; set; }
        public short IsCrurrentJob { get; set; } //Particular
        public long EmploymentTypeId { get; set; } //staff_title
        public string Achievement { get; set; }
        public long? EuropeanStandardOccupationId { get; set; } //HCM_OCCUPATION
        public short SequenceNo { get; set; }
    }

    public class HcResumeEmployerViewModel_Get : HcResumeEmployerViewModel
    {
        public long Rid { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleAe { get; set; }

        public string EmployerTitle { get; set; }

        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
