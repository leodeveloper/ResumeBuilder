using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public abstract class AbstractBase
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public int? Order { get; set; }
    }

    public class EmployerLookup: AbstractBase
    {

    }

    public class Location : AbstractBase
    {

    }

    public class Designation : AbstractBase
    {

    }

    public class City : AbstractBase
    {

    }

    public class SpecialNeedLookup : AbstractBase
    {

    }

    public class Country : AbstractBase
    {
        public Country()
        {
            this.CountryCity = new List<City>();
        }

        public IList<City> CountryCity { get; set; }
    }
    /// <summary>
    /// International Cities
    /// </summary>
    public class CountryCity : AbstractBase
    {
        public CountryCity()
        {
            this.Country = new Country();
        }

        public Country Country { get; set; }
    }

    public class CertificateType : AbstractBase
    {

    }

    public class JobIndustry : AbstractBase
    {

    }
    public class JobCategory : AbstractBase
    {

    }
    public class Institute : AbstractBase
    {

    }

    public class Language : AbstractBase
    {

    }

    public class Emirates : AbstractBase
    {

    }

    public class Course : AbstractBase
    {

    }

    public class EmployerName : AbstractBase
    {

    }

    public class Particular : AbstractBase
    {

    }

    public class SourceType : AbstractBase
    {

    }

    public class University : AbstractBase
    {

    }

    public class SkillGroup : AbstractBase
    {

    }


    public class SkillGroupOccupation : AbstractBase { }
    public class ToolsKnowledgeLookup : AbstractBase { }

    public class TrainingCenter : AbstractBase
    {
        public bool MarkAsBlackList { get; set; }
    }

    public class UniversityType : AbstractBase
    {

    }
    public class EmploymentType : AbstractBase
    {

    }

    public class JobSeekerGrieveanceStatus : AbstractBase
    {

    }



    public class Gender
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }

   
    public class ProficiencyType
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }

    public class EducationCategory
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }

    public class GradeCategory
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }

    public class Salutation
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }

    public class MartialStatus
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }

    /// <summary>
    /// This used in the _ResumeListFilterPartial
    /// </summary>
    public class JobSeekerEmiratesIdsViewModel
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
    /// <summary>
    /// This used in the _ResumeListFilterPartial
    /// </summary>
    public class JobSeekerIdsViewModel
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class MilitaryServiceStatus
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }
    public class NoteType
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
    }
    


    public abstract class AbstractEducationBase
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }

        public string FullName { get { return this.EnName +" -- "+ this.ArName; } }

        public byte Type { get; set; }
        public string Code { get; set; }
    }

    public partial class Group : AbstractEducationBase
    {       
      
    }

    public partial class EducationType : AbstractEducationBase
    {

    }

    public partial class Major : AbstractEducationBase
    {

    }

    public partial class EducationCombine
    {         
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TypeId { get; set; }
        public int MajorId { get; set; }
    }
}
