using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("JobsSeekerStatus")]
    public partial class JobsSeekerStatus
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public int Rid { get; set; }
        [Column("Status_ID")]
        public int Status_ID { get; set; }
        [Column("Reason_ID")]
        public int Reason_ID { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        public DateTime StatusUpdateDateTime { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
