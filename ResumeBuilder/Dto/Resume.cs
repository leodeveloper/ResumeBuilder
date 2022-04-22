using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ResumeBuilder.Dto
{
    [Dapper.Contrib.Extensions.Table("JobSeekerResume")]
    public class Resume
    {
        [Dapper.Contrib.Extensions.Key]
        //[Column("RID")]
        public long Rid { get; set; } = 0;
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(25)]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(25)]
        public string MiddleName { get; set; }
        [StringLength(25)]
        public string ThridName { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(25)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(25)]
        public string FirstNameAr { get; set; }
        //[Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(25)]
        public string MiddleNameAr { get; set; }
        [StringLength(25)]
        public string ThridNameAr { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(25)]
        public string LastNameAr { get; set; }
        [StringLength(25)]
        public string FamilyNameAr { get; set; }
        [StringLength(50)]
        public string FamilyName { get; set; }

        public int? Salutation { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        public int GenderId { get; set; }
        [Column("DOB", TypeName = "datetime")]
        public DateTime? DOB { get; set; }
        public int PlaceOfBirth { get; set; }
        public int MartialStatus { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [Column("KAQNo")]
        [StringLength(50)]
        public string KAQNo { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(50)]
        public string FamilyNo { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(50)]
        public string TownNo { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [Column("KAQPageNo")]
        [StringLength(50)]
        public string KAQPageNo { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(50)]
        public string EmiratesId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EmiratesIdExpiryDate { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(50)]
        public string PassportNumber { get; set; }
        public long PassportPlaceOfIssue { get; set; }

        /// <summary>
        /// this job seeker id is automated generated from sql server trigger
        /// </summary>
        [Column("JobSeekerID")]
        [StringLength(50)]
        public string JobSeekerId { get; set; }
        public int? Emirates { get; set; }
        public int? CityId { get; set; }
        public int LocationId { get; set; }
        [Column("POBoxNo")]
        [StringLength(10)]
        public string PoboxNo { get; set; }
        [Column("POBoxCityId")]
        public int? PoboxCityId { get; set; }
        [Required(ErrorMessage = "RequiredValidationMessage")]
        [StringLength(45)]
        public string MobilePhone { get; set; }
        [StringLength(45)]
        public string LandLine { get; set; }
        [StringLength(55)]
        public string EmailId { get; set; }
        public string PrimaryContact { get; set; }
        [StringLength(50)]
        public string UnifiedNumber { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }

        public string MilitaryServiceBatch { get; set; }
        public int? MilitaryServiceStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MilitaryServiceFromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MilitaryServiceToDate { get; set; }

        [StringLength(75)]
        public string Twitter { get; set; }
        [StringLength(75)]
        public string Linkedin { get; set; }
        public string Address { get; set; }
        public int resumestatus { get; set; }

        public string CreatedUserID { get; set; }
        public string LastModifiedUserID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
