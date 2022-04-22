using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_INDUSTRY_TYPE")]
    public partial class HcmIndustryType
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string IndustryTypeTitle { get; set; }
        [Column(TypeName = "ntext")]
        public string IndustryAlias { get; set; }
        [Column(TypeName = "ntext")]
        public string IndustryExcludeAlias { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        [Required]
        [Column("RefID")]
        [StringLength(15)]
        public string RefId { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public long CreatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public long ModifiedUser { get; set; }
    }
}
