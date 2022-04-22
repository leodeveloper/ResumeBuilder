using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    /// <summary>
    /// this for insert and update
    /// </summary>
    public class Hc_ResumeBankViewModel
    {
        public long rid { get; set; }
        public string emirateid { get; set; }
        public string fullnameen { get; set; }
        public string fullnameae { get; set; }
        public DateTime? dob { get; set; }
        public short genderid { get; set; }
        public string firstnameen { get; set; }
        public string secondnameen { get; set; }
        public string thirdnameen { get; set; }
        public string fourthnameen { get; set; }
        public string tribename { get; set; }

        public string firstnameae { get; set; }
        public string secondnameae { get; set; }
        public string thirdnameae { get; set; }
        public string fourthnameae { get; set; }
        public string tribenameae { get; set; }

        public string familybookno { get; set; }
        public string unifiedno { get; set; }
        public string kalasathno { get; set; }
        public string edbarano { get; set; }
        public string placeofbirthen { get; set; }
        public string placeofbirthae { get; set; }
        public int placeofbirthid { get; set; }
        public int? emirateofbirthid { get; set; }
        public int? cityofbirthid { get; set; }
        public long? countryofbirthid { get; set; }
        public DateTime? eidissuedate { get; set; }
        public DateTime? eidexpirydate { get; set; }
        public short maritalstatusid { get; set; }
        public string passportno { get; set; }
        public DateTime? passportissuedate { get; set; }
        public DateTime? passportexpirydate { get; set; }
    }

    /// <summary>
    /// for select
    /// </summary>
    public class Hc_ResumeBankViewModel_Get : Hc_ResumeBankViewModel
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public short resumestatus { get; internal set; }

        public int JPCAssessment { get; set; }
        public string JPCAssessmentStatus { get; set; }
        public string JPCAssessmentStatusAe { get; set; }
        public int JPCAssessmentStatusID { get; set; }
        public string ChallangesNotes { get; set; }
    }

    public class Hc_ContactInfoViewModel
    {
        public long emirate { get; set; }
        public long cityid { get; set; }
        public long locationid { get; set; }
        public string pobox { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string phoneno { get; set; }
        public string alterphoneno { get; set; }
    }

    public class Hc_AdditionalInfoViewModel
    {
        public bool ispod { get; set; }
        public bool IsSSABeneficiary { get; set; }
        public long ismilitarycompleted { get; set; }
        public int militarybatchnoid { get; set; }
        public string isdrivinglicense { get; set; }
        public int typeofdrivinglicenseid { get; set; }
        public string linkedinurl { get; set; }
        public string twitterurl { get; set; }
        public string llpprofile { get; set; }
        public string takafoprofile { get; set; }
        public string hrmsprofile { get; set; }
        public string nafisprofile { get; set; }
        public string studentidcard { get; set; }
    }
}
