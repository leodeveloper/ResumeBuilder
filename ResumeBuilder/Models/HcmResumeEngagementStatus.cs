using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_RESUME_ENGAGEMENT_STATUS")]
    public partial class HcmResumeEngagementStatus
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(25)]
        public string Title { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
    }
}
