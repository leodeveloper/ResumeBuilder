using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_PREFFERED_LOCATION")]
    public partial class HcResumePrefferedLocation
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("LocationID")]
        public long LocationId { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [Column("PType")]
        public short Ptype { get; set; }
        [Column("createduserid")]
        public long? Createduserid { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("modifieduserid")]
        public long? Modifieduserid { get; set; }
        [Column("modifieddate", TypeName = "datetime")]
        public DateTime? Modifieddate { get; set; }
    }
}
