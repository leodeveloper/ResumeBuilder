using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_EMPLOYER")]
    public partial class HcResumeEmployer
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long? EuropeanStandardOccupationId { get; set; }
        public string Achievement { get; set; }
        public bool IsJobInUae { get; set; }
        public bool Is_Deleted {get;set;}
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [Column("EmployerID")]
        public long EmployerId { get; set; }
        [StringLength(200)]
        public string Employer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column("DesignationID")]
        public long DesignationId { get; set; }
        public short Particular { get; set; }
        public short? FromMonth { get; set; }
        public int? FromYear { get; set; }
        public short? ToMonth { get; set; }
        public int? ToYear { get; set; }
        public long CompanyType { get; set; }
        [StringLength(100)]
        public string DesignationText { get; set; }
        public short SequenceNo { get; set; }
        [Required]
        [StringLength(100)]
        public string EmployerName { get; set; }
        [Required]
        [StringLength(500)]
        public string EmployerAddress { get; set; }
        [Required]
        [StringLength(100)]
        public string PhoneNo { get; set; }
        [Required]
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
        [Column(TypeName = "ntext")]
        public string NoofEmployees { get; set; }
        [Required]
        [StringLength(100)]
        public string ManagerName { get; set; }
        [Required]
        [Column("ManagerEmailID")]
        [StringLength(100)]
        public string ManagerEmailId { get; set; }
        [Required]
        [StringLength(100)]
        public string ManagerContactNumber { get; set; }
        public int ReferenceTaken { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string DutyResponsibilities { get; set; }
        [Required]
        [StringLength(500)]
        public string LeavingReason { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? FirstSalary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? LastSalary { get; set; }
        [Column(TypeName = "ntext")]
        public string AgencyDetails { get; set; }
        public short PositionDetails { get; set; }
        [Required]
        [StringLength(100)]
        public string IndustryType { get; set; }
        [StringLength(100)]
        public string Experience { get; set; }
        [Column(TypeName = "ntext")]
        public string JobDescription { get; set; }
        [Column("CountryID")]
        public long CountryId { get; set; }
        [Required]
        [StringLength(25)]
        public string City { get; set; }
        public long JobType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FixedPay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VariablePay { get; set; }
        public short LifeCycle { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VariableFirstSalary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VariableLastSalary { get; set; }
        [Column("CurrencyID")]
        public long CurrencyId { get; set; }
        [Column("FrequencyID")]
        public long FrequencyId { get; set; }
        [Required]
        [StringLength(200)]
        public string DeviationReason { get; set; }
        public long EmploymentStatus { get; set; }
        [Column("IndustryID")]
        public long IndustryId { get; set; }
        public long ExpInMonths { get; set; }
        [Column("IndustryTypeID")]
        public long IndustryTypeId { get; set; }
        [Column("OtherCountryID")]
        public long OtherCountryId { get; set; }
        public short? PushedToTanmia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PushedToTanmiaOn { get; set; }
        public string TanmiaIntgRespSts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("createduserid")]
        public long? Createduserid { get; set; }
        [Column("cityid")]
        public long? Cityid { get; set; }
        public long? Modifieduserid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }
}
