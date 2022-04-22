using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_COVERLETTER")]
    public partial class HcResumeCoverLetter
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        [Column("userid")]
        public int? Userid { get; set; }
        [Column("postdate", TypeName = "datetime")]
        public DateTime Postdate { get; set; }
        [Column("notestype")]
        public short? Notestype { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedUserId { get; set; }
    }
}
