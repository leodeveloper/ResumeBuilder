using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_CHALLANGES")]
    public partial class HcResumeChallanges
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long ResumeId { get; set; }
        public int ChallangeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
    }
}
