using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class JobSeekerPreviewDetailDto
    {

        //RID NUMBER
        //JS_ENG_NAME NVARCHAR2(301)
        //DOB DATE
        //MARITALSTATUS VARCHAR2(26)
        //CITY NVARCHAR2(200)
        //LOC NVARCHAR2(200)
        //JOBSEEKERID NVARCHAR2(50)
        //EMAILID VARCHAR2(55)
        //MOBILEPHONE NVARCHAR2(45)
        //TOTALEXPERIENCE NUMBER
        //EDUCATION_GROUP NVARCHAR2(200)
        //EDUCATION NVARCHAR2(400)
        //SPECIALIZATIONTITLE VARCHAR2(32767 CHAR)
        //INSTITUTE VARCHAR2(32767 CHAR)
        //YEARS VARCHAR2(32767)
        //SOURCENAME VARCHAR2(100)
        //LETTERNUMBER NVARCHAR2(50)
        //LETTERDATE DATE

        // THRIVING_INDEX_CONFIDENCE NUMBER(10)
        //THRIVING_INDEX_OPTIMISM NUMBER(10)
        //THRIVING_INDEX_GROWTH_MINDSET NUMBER(10)
        //THRIVING_INDEX_ACHIEVEMENT NUMBER(10)
        //THRIVING_INDEX_GRIT NUMBER(10)
        //THRIVING_INDEX_RESILIENCE NUMBER(10)
        //COGNITIVE_ABILITY_REASONIFY NUMBER(10)
        //COGNITIVE_ABILITY_DETECTIFY NUMBER(10)
        //COGNITIVE_ABILITY_NUMERIFY NUMBER(10)
        //COGNITIVE_ABILITY_AGILE_OVERALL NUMBER(10)
        //MICROSOFT_OFFICE_OVERALL NUMBER(5,2)
        //THRIVING_INDEX_OVERALL NUMBER(5,2)

        public JobSeekerPreviewDetailDto()
        {
            this.Notes = new List<JobSeekerPreviewNotesDto>();
            this.ProfileSummaryDto = new JobSeekerPreviewProfileSummaryDto();
            this.MatchingJourneys = new List<JobSeekerPreviewMatchingJourney>();
            this.MatchingJourneyRejected = new List<JobSeekerPreviewMatchingJourney>();
            this.MatchingJourneyNotRejected = new List<JobSeekerPreviewMatchingJourney>();
        }

        public string JobSeekerPhoto { get; set; }
        public long RID { get; set; }
        public string JS_ENG_NAME { get; set; }
        public DateTime? DOB { get; set; }
        public string MARITALSTATUS { get; set; }
        public string CITY { get; set; }
        public string LOC { get; set; }
        public string JOBSEEKERID { get; set; }
        public string EMAILID { get; set; }
        public string MOBILEPHONE { get; set; }
        public long TOTALEXPERIENCE { get; set; }
        public string EDUCATION_GROUP { get; set; }
        public string EDUCATION { get; set; }
        public string SPECIALIZATIONTITLE { get; set; }
        public string INSTITUTE { get; set; }
        public string YEARS { get; set; }
        public string SOURCENAME { get; set; }
        public string LETTERNUMBER { get; set; }
        public DateTime? LETTERDATE { get; set; }
        public int THRIVING_INDEX_CONFIDENCE { get; set; } //NUMBER(10)
        public int THRIVING_INDEX_OPTIMISM { get; set; } // NUMBER(10)
        public int THRIVING_INDEX_GROWTH_MINDSET { get; set; } // NUMBER(10)
        public int THRIVING_INDEX_ACHIEVEMENT { get; set; } // NUMBER(10)
        public int THRIVING_INDEX_GRIT { get; set; } // NUMBER(10)
        public int THRIVING_INDEX_RESILIENCE { get; set; } //NUMBER(10)
        public int COGNITIVE_ABILITY_REASONIFY { get; set; } //NUMBER(10)
        public int COGNITIVE_ABILITY_DETECTIFY { get; set; } //NUMBER(10)
        public int COGNITIVE_ABILITY_NUMERIFY { get; set; } //NUMBER(10)
        public int COGNITIVE_ABILITY_AGILE_OVERALL { get; set; } //NUMBER(10)
        public decimal MICROSOFT_OFFICE_OVERALL { get; set; } //NUMBER(5,2)
        public decimal THRIVING_INDEX_OVERALL { get; set; } //NUMBER(5,2)
        public decimal OVERALL_SCORE_PER_CANDIDATE { get; set; } //NUMBER(5,2)
        public string CAREER_TOP_01 { get; set; } //NVARCHAR2(100)
        public int CAREER_STARS_01 { get; set; } //NUMBER(10)
        public string CAREER_TOP_02 { get; set; } //NVARCHAR2(100)
        public int CAREER_STARS_02 { get; set; } //NUMBER(10)
        public string CAREER_TOP_03 { get; set; } //NVARCHAR2(100)
        public int CAREER_STARS_03 { get; set; } // NUMBER(10)
        public string CAREER_TOP_04 { get; set; } //NVARCHAR2(100)
        public int CAREER_STARS_04 { get; set; } //NUMBER(10)
        public string CAREER_TOP_05 { get; set; } //NVARCHAR2(100)
        public int CAREER_STARS_05 { get; set; } //NUMBER(10)
        public int AGILE_VERBIFY_ENGLISH_ASSESSMENT { get; set; } //NUMBER(10)
        public int MICROSOFT_OFFICE_EXCEL { get; set; } //NUMBER(10)
        public int MICROSOFT_OFFICE_OUTLOOK { get; set; } //NUMBER(10)
        public int MICROSOFT_OFFICE_POWERPOINT { get; set; } //NUMBER(10)
        public int MICROSOFT_OFFICE_WORD { get; set; } //NUMBER(10)
        public string Overall_Rating_Matrix_Grid { get; set; }
        public IList<JobSeekerPreviewNotesDto> Notes { get; set; }
        public JobSeekerPreviewProfileSummaryDto ProfileSummaryDto { get; set; }
        public IList<JobSeekerPreviewMatchingJourney> MatchingJourneys { get; set; }
        public IList<JobSeekerPreviewMatchingJourney> MatchingJourneyRejected   {   get;set;  }
        public IList<JobSeekerPreviewMatchingJourney> MatchingJourneyNotRejected { get; set; }
    }

    public class JobSeekerPreviewNotesDto
    {
        public long RID { get; set; } //NUMBER
        public string JOBSEEKERID { get; set; } //NVARCHAR2(50)
        public DateTime? LASTUPDATE { get; set; } // DATE
        public string NOTE  { get; set; } // NVARCHAR2(4000)
        public string USERNAME { get; set; } // NVARCHAR2(4000)
        public string NAMEEN { get; set; } // NVARCHAR2(4000)
    }

    public class JobSeekerPreviewProfileSummaryDto
    {
        public long RID { get; set; } //NUMBER
        public string JOBSEEKERID { get; set; } //NVARCHAR2(50)
        public int NOMINATIONS { get; set; } //NUMBER
        public int PENDING_JOURNEY { get; set; } //NUMBER
        public int INTERVIEW { get; set; } //NUMBER
        public int REJECTION { get; set; } //NUMBER
    }

    public class JobSeekerPreviewMatchingJourney
    {
        public long RID { get; set; } //NUMBER
        public string JOBSEEKERID { get; set; } //NVARCHAR2(50)
        public string NAMEEN { get; set; } //NVARCHAR2(200)
        public string JOBCODE { get; set; } //NVARCHAR2(300)
        public string VACANCYTITLE { get; set; } //NVARCHAR2(500)
        public DateTime? CREATEDDATE { get; set; } //DATE
        public DateTime? STATEDATE { get; set; } //DATE
        public string REASON_EN { get; set; } //NVARCHAR2(400)
        public string STATE { get; set; } //NVARCHAR2(500)
    }
}
