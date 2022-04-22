using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumePreferenceCityViewModel
    {
        public long ResumeId { get; set; }
        public long CityId { get; set; }
    }

    public class HcResumePreferenceCityViewModel_Get : HcResumePreferenceCityViewModel
    {
        public long Rid { get; set; }       
        public string City { get; set; }
        public string CityAe { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
    }
}
