using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("HCM_COMPANY_LIST")]
    public partial class HcmCompanyList
    {
        [Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(200)]
        public string EstCode { get; set; }
        [Required]
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [Column("BUCode")]
        [StringLength(100)]
        public string Bucode { get; set; }
        [Required]
        [StringLength(10)]
        public string RegCode { get; set; }
        [Column(TypeName = "ntext")]
        public string Address { get; set; }
        [Column("CompanyManagerID")]
        public long CompanyManagerId { get; set; }
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
