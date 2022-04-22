using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_EMPLOYER")]
    public partial class HcmEmployer
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(250)]
        public string AlternateNames { get; set; }
        [StringLength(250)]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        public string WebSite { get; set; }
        public long IndustryType { get; set; }
        public long? CompanyType { get; set; }
        [Column(TypeName = "ntext")]
        public string Profile { get; set; }
        [Column(TypeName = "ntext")]
        public string KeyWords { get; set; }
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Column(TypeName = "xml")]
        public string Emp4LevelFunction { get; set; }
        [Column(TypeName = "ntext")]
        public string IndustryText { get; set; }
        [Column(TypeName = "ntext")]
        public string SubIndustryText { get; set; }
        [Column(TypeName = "ntext")]
        public string FunctionText { get; set; }
        [Column(TypeName = "ntext")]
        public string SubFunctionText { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public short NoPouch { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
    }
}
