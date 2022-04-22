using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_EDUCATION_GROUP")]
    public partial class HcmEducationGroup
    {
        [Dapper.Contrib.Extensions.Key]       
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string GroupTitle { get; set; }
        [Column(TypeName = "ntext")]
        public string EducationGroupAlias { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string EducationGroupDescription { get; set; }
        [Required]
        [Column("RefID")]
        [StringLength(10)]
        public string RefId { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public short Level { get; set; }
        public short Type { get; set; }
        public int PriorityRank { get; set; }
    }
}
