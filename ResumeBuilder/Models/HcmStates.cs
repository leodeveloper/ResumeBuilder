using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_STATES")]
    public partial class HcmStates
    {
        [Dapper.Contrib.Extensions.Key]
       
        public long Rid { get; set; }
        [StringLength(100)]
        public string StateTitle { get; set; }
        [Column("CountryID")]
        public long CountryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime? McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Required]
        [StringLength(10)]
        public string StateCode { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public int PriorityRank { get; set; }
    }
}
