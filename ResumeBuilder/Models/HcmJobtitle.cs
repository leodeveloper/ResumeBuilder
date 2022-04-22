using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_JOBTITLE")]
    public partial class HcmJobtitle
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string JobTitleAliases { get; set; }
        [Column(TypeName = "ntext")]
        public string Includelist { get; set; }
        [Column(TypeName = "ntext")]
        public string Excludelist { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        [Required]
        [StringLength(100)]
        public string Title2 { get; set; }
        [Column("MinCTC", TypeName = "decimal(18, 2)")]
        public decimal MinCtc { get; set; }
        [Column("MaxCTC", TypeName = "decimal(18, 2)")]
        public decimal MaxCtc { get; set; }
        [Column("AvgCTC", TypeName = "decimal(18, 2)")]
        public decimal AvgCtc { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short IsActive { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
    }
}
