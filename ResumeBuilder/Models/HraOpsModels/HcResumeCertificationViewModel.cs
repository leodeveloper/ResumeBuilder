using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeCertificationsViewModel
    {
        public long? ResumeId { get; set; }
        public int? CertificationTypeId { get; set; }
        public string Title { get; set; }
        public int? FromMonth { get; set; }
        public int? FromYear { get; set; }

        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }

        public int? GradeCategoryId { get; set; }
        public string Score { get; set; }
        public long? TrainingProviderId { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        /// <summary>
        /// On campus or Off Campus
        /// </summary>
        public int? CampusTypeId { get; set; }        
    }

    public class HcResumeCertificationsViewModel_Get : HcResumeCertificationsViewModel
    {
        public long Rid { get; set; }
        public string CertificateType { get; set; }
        public string TrainingProvider { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public long? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserid { get; set; }
    }
}
