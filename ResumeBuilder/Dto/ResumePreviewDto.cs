using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class ResumePreviewDto
    {
        public ResumePreviewDto()
        {
            this.Resume = new Resume();
            this.EducationDtos = new List<EducationDto>();
            this.EmployerDtos = new List<EmployerDto>();
            this.SkillOccupationDto = new List<OccupationDto>();
            this.CertificateDtos = new List<Certification>();
            this.ToolsKnowledgeDto = new List<ToolsKnowledgeDto>();
            this.JobSeekerCvphoto = new ChildJobSeekerCvphoto();
            this.OtherJobSeekerInfo = new OtherJobSeekerInfo();
        }

        public double TotalExperience 
        { get 
            {
                double _totalExperience = 0;
                try
                {
                    foreach (EmployerDto employerDto in EmployerDtos)
                    {
                        _totalExperience += CalculateExperience(employerDto.Employer.FromDate, employerDto.Employer.ToDate);
                    }

                    if (_totalExperience > 0)
                    {
                        _totalExperience = Math.Round((_totalExperience / 12), 1);

                    }
                }
                catch { }
               

                return _totalExperience; 
            } 
        }

        public int? Age
        {
            get
            {
                // Save today's date.
                var today = DateTime.Today;
                if (this.Resume.DOB.HasValue)
                {
                    // Calculate the age.
                    int age = today.Year - this.Resume.DOB.Value.Year;

                    // Go back to the year in which the person was born in case of a leap year
                    if (this.Resume.DOB > today.AddYears(-age)) age--;
                    return age;
                }
                else
                    return null;


            }
        }

        public Resume Resume { get; set; }
        public IList<EducationDto> EducationDtos { get; set; }
        public IList<EmployerDto> EmployerDtos { get; set; }
        public IList<Certification> CertificateDtos { get; set; }
        public IList<OccupationDto> SkillOccupationDto { get; set; }
        public IList<ToolsKnowledgeDto> ToolsKnowledgeDto { get; set; }
        public ChildJobSeekerCvphoto JobSeekerCvphoto { get; set; }
        public OtherJobSeekerInfo OtherJobSeekerInfo { get; set; }
        public string HighestEducation { get { return getHighestEducation(); } }
        public string CurrentRole { get { return getCurrentRole(); } }
        private static int CalculateExperience(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                int? monthsApart = 12 * (startDate?.Year - endDate?.Year) + startDate?.Month - endDate?.Month;
                return Math.Abs((int)monthsApart);
            }
            catch { return 0; }
            
        }

        private string getHighestEducation()
        {
            if(this.EducationDtos.Any())
            {
                EducationDto _hightEducation = this.EducationDtos.FirstOrDefault(h => h.ThisIsHighestEducation == true);
                if (_hightEducation != null)
                    return $"{_hightEducation.Education_Group}, {_hightEducation.Education_Type}, {_hightEducation.Education_Major}";
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        private string getCurrentRole()
        {
            if (this.EmployerDtos.Any())
            {
                EmployerDto _currentRole = this.EmployerDtos.FirstOrDefault(h => h.IsThisMyCurrentRole == true);
                if (_currentRole != null)
                    return $"{_currentRole.EmployerName}, {_currentRole.Designation} ";
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
    }

    public class OtherJobSeekerInfo
    {
        public string Salutation { get; set; }
        public string SalutationAr { get; set; }
        public string Emirates { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string EmiratesAr { get; set; }
        public string CityAr { get; set; }
        public string LocationAr { get; set; }
        public string MartialStatus { get; set; }
        public string MartialStatusAr { get; set; }
        public string Gender { get; set; }
        public string GenderAr { get; set; }
    }
}
