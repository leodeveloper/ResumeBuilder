using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("JobSeekerResume")]
    public partial class JobSeekerResume
    {
        public JobSeekerResume()
        {
            Education = new HashSet<Education>();
            Employer = new HashSet<Employer>();
            Reference = new HashSet<Reference>();
            Source = new HashSet<Source>();
            Training = new HashSet<Training>();
        }

        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string MiddleName { get; set; }
        [StringLength(25)]
        public string ThridName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstNameAr { get; set; }
        [Required]
        [StringLength(25)]
        public string MiddleNameAr { get; set; }
        [StringLength(25)]
        public string ThridNameAr { get; set; }
        [Required]
        [StringLength(25)]
        public string LastNameAr { get; set; }
        [StringLength(25)]
        public string FamilyNameAr { get; set; }
        [StringLength(50)]
        public string FamilyName { get; set; }
        [StringLength(30)]
        public string Salutation { get; set; }

        public string FullName { get { return $" {FirstName} {LastName} {FamilyName}"; } }
        public string FullNameAr { get { return $" {FirstNameAr} {LastNameAr} {FamilyNameAr}"; } }

        public int GenderId { get; set; }

        public string Gender { get 
            {
                string _gender = string.Empty;
                if(this.GenderId == 1)
                {
                    _gender= "Male";
                }
                else if(this.GenderId == 2)
                {
                    _gender= "Female";
                }
                return _gender;
            } 
        }

        [Column("DOB", TypeName = "datetime")]
        public DateTime? Dob { get; set; }


        public int? Age { get 
            {
                // Save today's date.
                var today = DateTime.Today;
                if (this.Dob.HasValue)
                {
                    // Calculate the age.
                    int age = today.Year - this.Dob.Value.Year;

                    // Go back to the year in which the person was born in case of a leap year
                    if (this.Dob > today.AddYears(-age)) age--;
                    return age;
                }
                else
                    return null;
                
                
            } 
        }

        public int PlaceOfBirth { get; set; }
        public int MartialStatus { get; set; }
        [Required]
        [Column("KAQNo")]
        [StringLength(50)]
        public string Kaqno { get; set; }
        [Required]
        [StringLength(50)]
        public string FamilyNo { get; set; }
        [Required]
        [StringLength(50)]
        public string TownNo { get; set; }
        [Required]
        [Column("KAQPageNo")]
        [StringLength(50)]
        public string KaqpageNo { get; set; }
        [Required]
        [StringLength(50)]
        public string EmiratesId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EmiratesIdExpiryDate { get; set; }
        [Required]
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
        [Required]
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
        public int StatusId { get; set; }
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
        public string StatusTitle { get; set; }
        public string StatusTitleAr { get; set; }

        [InverseProperty("Resume")]
        public virtual ICollection<Education> Education { get; set; }
        [InverseProperty("Resume")]
        public virtual ICollection<Employer> Employer { get; set; }
        [InverseProperty("Resume")]
        public virtual ICollection<Reference> Reference { get; set; }
        [InverseProperty("Resume")]
        public virtual ICollection<Source> Source { get; set; }
        [InverseProperty("Resume")]
        public virtual ICollection<Training> Training { get; set; }
    }
}
