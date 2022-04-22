using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("HCM_RESUME_REFERENCE_TYPE")]
    public partial class HcmResumeReferenceType
    {
        [Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Required]
        [StringLength(1000)]
        public string Title { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short LanguageType { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        public int PriorityRank { get; set; }
    }
}
