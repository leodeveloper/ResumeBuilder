using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApiClassLibrary.Model
{
    public class PersonalInfo
    {
        public PersonProfile PersonProfile { get; set; }
    }

    public class PersonProfile
    {
        public string unifiedNumber { get; set; }
        public IdentityCard identityCard { get; set; }
        public Nationality nationality { get; set; }
        public PersonName personName { get; set; }
        public string khulasitQaidNumber { get; set; }
        public string familyBookNumber { get; set; }
        public string edbarahNumber { get; set; }
        public Gender gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public CountryOfBirth countryOfBirth { get; set; }
        public string emirateOfBirth { get; set; }
        public string cityOfBirth { get; set; }
        public string placeOfBirthAr { get; set; }
        public string placeOfBirthEn { get; set; }
        public MaritalStatus maritalStatus { get; set; }
        public Religion religion { get; set; }
        public Passport passport { get; set; }
        public Addresses addresses { get; set; }
        public int familyCount { get; set; }
        public int familyMaleCount { get; set; }
        public int familyFemaleCount { get; set; }
        public PersonType personType { get; set; }
    }


    /// <summary>
    /// /////////////////////////////////////////////////////////////////////new string
    /// </summary>

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class IdentityCard
    {
        public long number { get; set; }
        public DateTime issuDate { get; set; }
        public DateTime expiryDate { get; set; }
    }

    public class Nationality
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class PersonName
    {
        public string fullArabicName { get; set; }
        public string fullEnglishName { get; set; }
        public string firstNameArabic { get; set; }
        public string secondNameArabic { get; set; }
        public string thirdNameArabic { get; set; }
        public string fourthNameArabic { get; set; }
        public string firstNameEnglish { get; set; }
        public string secondNameEnglish { get; set; }
        public string thirdNameEnglish { get; set; }
        public string fourthNameEnglish { get; set; }
        public string familyNameEnglish { get; set; }
        public string familyNameArabic { get; set; }
        public Tribe tribe { get; set; }
    }

    public class Tribe
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Gender
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class CountryOfBirth
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class MaritalStatus
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Religion
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Type
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class IssueCountry
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Passport
    {
        public string number { get; set; }
        public Type type { get; set; }
        public DateTime issuDate { get; set; }
        public DateTime expiryDate { get; set; }
        public IssueCountry issueCountry { get; set; }
    }

    public class Emirate
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Street
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class LocalAddress
    {
        public Emirate emirate { get; set; }
        public City city { get; set; }
        public Area area { get; set; }
        public Street street { get; set; }
        public string building { get; set; }
        public string pobox { get; set; }
        public string mobileNumber { get; set; }
        public string homePhone { get; set; }
        public string workPhone { get; set; }
    }

    public class Address
    {
        public LocalAddress localAddress { get; set; }
    }

    public class Addresses
    {
        public Address address { get; set; }
    }

    public class PersonType
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    

   



    //For Post Class
    public class PersonalInfoPost
    {
        public string emiratesID { get; set; }
    }


}
