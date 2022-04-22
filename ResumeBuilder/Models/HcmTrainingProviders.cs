using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("HCM_TRAINING_PROVIDERS")]
    public partial class HcmTrainingProviders
    {
        [Key]
        [Column("RID")]
        public long Rid { get; set; }
        [StringLength(100)]
        public string ProviderName { get; set; }
        [Column(TypeName = "ntext")]
        public string Profile { get; set; }
        public long Location { get; set; }
        [StringLength(100)]
        public string ContactPerson { get; set; }
        [StringLength(50)]
        public string PhoneNo { get; set; }
        [StringLength(50)]
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string FaxNo { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        public short Status { get; set; }
        [Column("languagetype")]
        public short Languagetype { get; set; }
        [Column("EngkeyID")]
        public long EngkeyId { get; set; }
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Column(TypeName = "image")]
        public byte[] Document { get; set; }
        [Column("ActivatedUserID")]
        public long ActivatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivatedDate { get; set; }
        [Column("CreatedUserID")]
        public long CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("ModifiedUserID")]
        public long ModifiedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string PassWord { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(100)]
        public string Branch { get; set; }
        [Column("EmeritesID")]
        public long EmeritesId { get; set; }
        [Column(TypeName = "ntext")]
        public string Address { get; set; }
        [Column(TypeName = "ntext")]
        public string Address2 { get; set; }
        [StringLength(100)]
        public string LicenseNo { get; set; }
        public long LicenseIssuedAuthority { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfIssue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [StringLength(100)]
        public string AddedValue { get; set; }
        [Column(TypeName = "ntext")]
        public string ClientList { get; set; }
        [Column("UserID")]
        public long UserId { get; set; }
        public short AllowToPost { get; set; }
        public short ModifiedUserType { get; set; }
        [StringLength(1000)]
        public string SecurityQuestion { get; set; }
        [StringLength(1000)]
        public string SecurityAnswer { get; set; }
        public bool? IsProfileCompleted { get; set; }
        public string ExtProviderNameAr { get; set; }
        [Column("ExtISselfManaged")]
        public string ExtIsselfManaged { get; set; }
        [Column("ManagerID")]
        public long ManagerId { get; set; }
        public short IsPasswordEncrpted { get; set; }
    }
}
