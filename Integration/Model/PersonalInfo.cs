using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Model
{
   

    public class PersonalInfo
    {
        public int unifiedNumber { get; set; }
        public IdentityCard identityCard { get; set; }
        public Nationality nationality { get; set; }
        public PersonName personName { get; set; }
        public string khulasitQaidNumber { get; set; }
        public string familyBookNumber { get; set; }
        public int edbarahNumber { get; set; }
        public Gender gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public CountryOfBirth countryOfBirth { get; set; }
        public EmirateOfBirth emirateOfBirth { get; set; }
        public CityOfBirth cityOfBirth { get; set; }
        public string placeOfBirthAr { get; set; }
        public string placeOfBirthEn { get; set; }
        public MaritalStatus maritalStatus { get; set; }
        public Religion religion { get; set; }
        public Passport passport { get; set; }
        public Occupation occupation { get; set; }
        public Addresses addresses { get; set; }
        public Qualification qualification { get; set; }
        public int familyCount { get; set; }
        public int familyMaleCount { get; set; }
        public int familyFemaleCount { get; set; }
    }

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

    public class Tribe
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class PersonName
    {
        public string fullArabicName { get; set; }
        public string firstNameArabic { get; set; }
        public string secondNameArabic { get; set; }
        public string thirdNameArabic { get; set; }
        public string fourthNameArabic { get; set; }
        public string familyNameArabic { get; set; }
        public string clanNameArabic { get; set; }
        public string fullEnglishName { get; set; }
        public string firstNameEnglish { get; set; }
        public string secondNameEnglish { get; set; }
        public string thirdNameEnglish { get; set; }
        public string fourthNameEnglish { get; set; }
        public string familyNameEnglish { get; set; }
        public Tribe tribe { get; set; }
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

    public class EmirateOfBirth
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class CityOfBirth
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
    //PassportType
    public class Type
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Occupation
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
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

    public class LocalAddress
    {
        public Emirate emirate { get; set; }
        public City city { get; set; }
        public string building { get; set; }
    }

    public class Country
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class AbroadAddress
    {
        public Country country { get; set; }
        public string phoneNumber { get; set; }
        public string details { get; set; }
    }

    public class Address
    {
        public LocalAddress localAddress { get; set; }
        public AbroadAddress abroadAddress { get; set; }
    }

    public class Addresses
    {
        public List<Address> address { get; set; }
    }

    public class Specialization
    {
        public int id { get; set; }
        public string arDesc { get; set; }
        public string enDesc { get; set; }
    }

    public class Qualification
    {
        public Specialization specialization { get; set; }
    }

   

   


}
