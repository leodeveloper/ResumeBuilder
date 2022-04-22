using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_BANK_ENGAGEMENT_NOTES")]
    public partial class HcResumeBankEngagementNotes
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long EngagementDetailId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? Status { get; set; }
        [StringLength(2000)]
        public string JobSeekerNotes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
    }
}
