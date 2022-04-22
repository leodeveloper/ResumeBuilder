using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("ReasonStatus")]
    public partial class ReasonStatus
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Column("ReasonID")]
        public long ReasonId { get; set; }
        [Column("StatusID")]
        public long StatusId { get; set; }
    }
}
