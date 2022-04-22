using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_GRADE")]
    public partial class HcmGrade
    {
        [Dapper.Contrib.Extensions.Key]      
        public int Rid { get; set; }
        [Required]
        [StringLength(75)]
        public string Title { get; set; }
        [Column("EngKeyID")]
        public int EngKeyId { get; set; }
        public int LangugaeType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        public long? Createduserid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [StringLength(300)]
        public string ModifiedUser { get; set; }
    }
}
