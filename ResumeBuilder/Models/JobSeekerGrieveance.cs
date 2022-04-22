using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("JobSeekerGrieveance")]
    public partial class JobSeekerGrieveance
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Column("resumeid")]
        public long? Resumeid { get; set; }
        [Column("status")]
        public short? Status { get; set; }
        [Column("ticketno")]
        [StringLength(100)]
        public string Ticketno { get; set; }
        [Column("comment")]
        [StringLength(300)]
        public string Comment { get; set; }
        [Column("document")]
        public string Document { get; set; }
        [Column("createdate", TypeName = "datetime")]
        public DateTime Createdate { get; set; }
        [Column("createduserid")]
        public string Createduserid { get; set; }
        [Column("modifieddate", TypeName = "datetime")]
        public DateTime? Modifieddate { get; set; }
        [Column("modifieduserid")]
        public string Modifieduserid { get; set; }
        [Column("id")]
        public int? Id { get; set; }
        public string MongoDBUniqueId { get; set; }
        [Required]
        [StringLength(150)]
        public string FileName { get; set; }
        public bool Is_Deleted { get; set; }
    }
}
