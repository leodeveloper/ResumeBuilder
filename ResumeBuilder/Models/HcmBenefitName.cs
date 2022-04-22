using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("HCM_BENEFIT_NAME")]
    public partial class HcmBenefitName
    {
        [Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
    }
}
