using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_UNIVERSITY")]
    public partial class HcmUniversity
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(10)]
        public string UniverstiyCode { get; set; }
        [Column("UniversityTypeID")]
        public long UniversityTypeId { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short LanguageType { get; set; }
        public short MarkAsBlackList { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string IncludeList { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string ExcludeList { get; set; }
    }
}
