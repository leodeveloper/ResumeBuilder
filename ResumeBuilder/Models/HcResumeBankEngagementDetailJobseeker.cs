using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_BANK_ENGAGEMENT_DETAIL_JOBSEEKER")]
    public partial class HcResumeBankEngagementDetailJobseeker
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long ResumeId { get; set; }
        public long EngagementId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public int Status { get; set; }
        public string JobSeekerNotes { get; set; }
    }
}
