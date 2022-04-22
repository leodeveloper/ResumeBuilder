using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_NATIONALITY")]
    public partial class HcmNationality
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        [Column("RegionID")]
        public long RegionId { get; set; }
        [Column(TypeName = "ntext")]
        public string IncludeList { get; set; }
        [Column(TypeName = "ntext")]
        public string ExcludeList { get; set; }
        [Required]
        [Column("RefID")]
        [StringLength(20)]
        public string RefId { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short LanguageType { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
    }
}
