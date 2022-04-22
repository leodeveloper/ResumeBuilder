using IntegrationApiClassLibrary.Model;
using IntegrationApiClassLibrary.Repository;
using Microsoft.Extensions.Logging;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ResumeBuilder.Helper.EnumHelper;

namespace ResumeBuilder.Repository
{
    public class IntegrationRepository : IIntegrationRepository
    {
        readonly IPersonalInfoRepository _iPersonalInfoRepository;
        readonly ILogger<IntegrationRepository> _ilogger;
        readonly IResumeRepository _iResumeRepository;
        readonly IPensionfundRepository _iPensionfundRepository;
        readonly IIntegrationLogRepository _iIntegrationLogRepository;
        public IntegrationRepository(IIntegrationLogRepository iIntegrationLogRepository,IPensionfundRepository iPensionfundRepository,IResumeRepository iResumeRepository, IPersonalInfoRepository iPersonalInfoRepository, ILogger<IntegrationRepository> ilogger)
        {
            _ilogger = ilogger;
            _iPersonalInfoRepository = iPersonalInfoRepository;
            _iResumeRepository = iResumeRepository;
            _iPensionfundRepository = iPensionfundRepository;
            _iIntegrationLogRepository = iIntegrationLogRepository;
        }

        public async Task<WebresponseNoData> UpdateJobSeekerPersonalInfo(long resumeId)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            try
            {
                Webresponse<Resume> webresponse = await _iResumeRepository.GetResumeById(resumeId);
                if (webresponse.status == APIStatus.success)
                {
                    PersonalInfo personalInfo = await _iPersonalInfoRepository.GetPersonalInfoByEmirateID(webresponse.data?.EmiratesId);
                    if (personalInfo == null)
                    {
                        throw new NullReferenceException($"No resume found for this emiratesid {webresponse.data.EmiratesId}");
                    }
                    else
                    {
                        CopyToResume(personalInfo.PersonProfile, webresponse.data);
                        bool isUpdate = await _iResumeRepository.UpdateResume(webresponse.data);
                        if (isUpdate)
                        {
                            webresponseNoData.message = "Update Successfully";
                            webresponseNoData.status = APIStatus.success;
                        }
                        else
                        {
                            webresponseNoData.message = "Failed";
                            webresponseNoData.status = APIStatus.error;
                        }

                        await _iIntegrationLogRepository.InsertintegrationLogs(IntegrationEnum.PersonalInfo, isUpdate, webresponseNoData.message, webresponse.data.Rid).ConfigureAwait(false);
                    }
                }
                else
                {
                    throw new NullReferenceException($"No resume found for this resumeId {resumeId}");
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message, ex);
                webresponseNoData.message = ex.Message;
                webresponseNoData.status = APIStatus.error;
            }
            return webresponseNoData;            
        }        

        public async Task<WebresponseNoData> InsertUpdatePensionfund(long resumeId)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            bool IsUpdateInsert = false;
            try
            {
                Webresponse<Resume> webresponse = await _iResumeRepository.GetResumeById(resumeId);
                if(webresponse.status == APIStatus.success)
                {
                    PensionfundPost pensionfundPost = new PensionfundPost { NationalId = webresponse.data?.EmiratesId };
                    PensionfundDto pensionfundDto = await _iPersonalInfoRepository.GetPensionfund(pensionfundPost);
                    if (pensionfundDto == null)
                    {
                        throw new NullReferenceException($"No pension fund found");
                    }
                    else
                    {
                        Webresponse<Pensionfund> pensionfund = await _iPensionfundRepository.GetpensionFundByNationId(pensionfundPost.NationalId);
                        //Is pension is in the database than update
                        if (pensionfund.status == APIStatus.success)
                        {
                            CopyToPensionfund(pensionfund.data, pensionfundDto);
                            IsUpdateInsert = await _iPensionfundRepository.UpdatepensionFund(pensionfund.data);

                        }
                        else if (pensionfund.status == APIStatus.NotFound)
                        {
                            pensionfund.data = new Pensionfund();
                            CopyToPensionfund(pensionfund.data, pensionfundDto);
                            IsUpdateInsert = await _iPensionfundRepository.InsertpensionFund(pensionfund.data);
                        }

                        if (IsUpdateInsert)
                        {
                            webresponseNoData.message = "Update Successfully";
                            webresponseNoData.status = APIStatus.success;
                        }
                        else
                        {
                            webresponseNoData.message = "Failed";
                            webresponseNoData.status = APIStatus.error;
                        }

                        await _iIntegrationLogRepository.InsertintegrationLogs(IntegrationEnum.PensionFund, IsUpdateInsert, webresponseNoData.message, webresponse.data.Rid).ConfigureAwait(false);
                    }
                }
                else
                {
                    _ilogger.LogError($"IntegrationRepository :::: UpdatePensionfund No record found agaist this resume id{resumeId}");
                    webresponseNoData.message = $"No record found agaist this resume id{resumeId}";
                    webresponseNoData.status = APIStatus.error;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message, ex);
                webresponseNoData.message = ex.Message;
                webresponseNoData.status = APIStatus.error;
            }
            return webresponseNoData;
        }

        #region prviate
        private void CopyToResume(PersonProfile personProfile, Resume resume)
        {

            resume.CityId = personProfile.addresses.address.localAddress.city.id;
            resume.DOB = personProfile.dateOfBirth;
            //resume.EmailId
            resume.Emirates = personProfile.addresses.address.localAddress.emirate.id;
            //resume.EmiratesId = personProfile.identityCard.number
            resume.EmiratesIdExpiryDate = personProfile.identityCard.expiryDate;
            resume.FamilyName = personProfile.personName.familyNameEnglish;
            resume.FamilyNameAr = personProfile.personName.familyNameArabic;
            resume.FamilyNo = personProfile.familyBookNumber == null ? "" : personProfile.familyBookNumber;
            resume.FirstName = personProfile.personName.firstNameEnglish;
            resume.FirstNameAr = personProfile.personName.firstNameArabic;
            resume.GenderId = personProfile.gender.id;
            //resume.JobSeekerId
            resume.KAQNo = personProfile.khulasitQaidNumber == null ? "" : personProfile.khulasitQaidNumber;
            //resume.KaqpageNo = 
            resume.LandLine = personProfile.addresses.address.localAddress.homePhone;
            resume.LastName = personProfile.personName.fourthNameEnglish == null ? "" : personProfile.personName.fourthNameEnglish;
            resume.LastNameAr = personProfile.personName.fourthNameArabic == null ? "" : personProfile.personName.fourthNameArabic;
            //resume.LocationId = 
            resume.MartialStatus = personProfile.maritalStatus.id;
            resume.MiddleName = personProfile.personName.secondNameEnglish;
            resume.MiddleNameAr = personProfile.personName.secondNameArabic;
            resume.MobilePhone = personProfile.addresses.address.localAddress.mobileNumber;
            resume.PassportNumber = personProfile.passport.number;
            resume.PassportPlaceOfIssue = personProfile.passport.issueCountry.id;
            // resume.PlaceOfBirth = personProfile.
            //resume.PoboxCityId
            resume.PoboxNo = personProfile.addresses.address.localAddress.pobox;
            //resume.PrimaryContact
            //resume.Rid
            //resume.Salutation
            resume.ThridName = personProfile.personName.thirdNameEnglish;
            resume.ThridNameAr = personProfile.personName.thirdNameArabic;
            //resume.TownNo
            resume.UnifiedNumber = personProfile.unifiedNumber.ToString();
        }
        private void CopyToPensionfund(Pensionfund pensionfund, PensionfundDto pensionfundDto)
        {
            pensionfund.DataAccuracy = pensionfundDto.dataAccuracy;
            pensionfund.EmployerName = pensionfundDto.employerName;
            pensionfund.EmploymentStatus = pensionfundDto.employmentStatus;
            pensionfund.NationId = pensionfundDto.nationalId;
            pensionfund.PersonRecordAvailability = pensionfundDto.personRecordAvailability;
            pensionfund.Reason = pensionfundDto.reason;
            pensionfund.StringDateOfbirth = pensionfundDto.stringDateOFBirth;
            pensionfund.UnifiedNumber = pensionfund.UnifiedNumber;

        }
        #endregion


    }
}
