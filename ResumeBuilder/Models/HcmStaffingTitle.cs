using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_STAFFING_TITLE")]
    public partial class HcmStaffingTitle
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(100)]
        public string StaffingTitle { get; set; }
        public short StaffingType { get; set; }
        [Required]
        [StringLength(1)]
        public string EmplType { get; set; }
        [Column("languagetype")]
        public short Languagetype { get; set; }
        [Column("EngkeyID")]
        public long EngkeyId { get; set; }
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
