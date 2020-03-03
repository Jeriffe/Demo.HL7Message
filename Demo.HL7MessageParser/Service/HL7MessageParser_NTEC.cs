﻿using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.DTOs;
using Demo.HL7MessageParser.Model;
using Demo.HL7MessageParser.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Demo.HL7MessageParser
{
    public class HL7MessageParser_NTEC : IHL7MessageParser
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string RestUrl;

        private string SoapUrl;

        private string UserName;
        private string Password;

        private string ClientSecret;
        private string ClientId;

        private string HospitalCode;
        private string AccessCode;

        private string DrugMasterSoapUrl;
        private string MDSCheckRestUrl;
        private IPatientVisitParser patientVisitParser;
        private IProfileRestService profileService;
        private IDrugMasterSoapService drugMasterSoapService;
        private IMDSCheckRestService mdsCheckRestService;

        public HL7MessageParser_NTEC()
        {
            Initialize();

            patientVisitParser = new SoapPatientVisitParser(SoapUrl, UserName, Password, HospitalCode);

            profileService = new ProfileRestService(RestUrl, ClientSecret, ClientId, HospitalCode);

            drugMasterSoapService = new DrugMasterSoapService(DrugMasterSoapUrl);

            mdsCheckRestService = new MDSCheckRestService(MDSCheckRestUrl);

        }

        private void Initialize()
        {
            //TODO Initialize parameters from DB

            SoapUrl = "http://localhost:8096/PatientService.asmx";
            UserName = "pas-appt-ws-user";
            Password = "pas-appt-ws-user-pwd";


            RestUrl = "http://localhost:8290/pms-asa/1/";
            ClientSecret = "CLIENT_SECRET";
            ClientId = "AccessCenter";

            AccessCode = "AccessCode";
            HospitalCode = "VH";

            DrugMasterSoapUrl = "http://localhost:44368/DrugMasterService.asmx";

            MDSCheckRestUrl = "http://localhost:3181/pms-asa/1/";
        }

        public HL7MessageParser_NTEC(IPatientVisitParser patientVisitParser,
                                     IProfileRestService profileService,
                                     IDrugMasterSoapService drugMasterSoapService,
                                     IMDSCheckRestService mdsCheckRestService)
        {
            this.patientVisitParser = patientVisitParser;
            this.profileService = profileService;
            this.drugMasterSoapService = drugMasterSoapService;
            this.mdsCheckRestService = mdsCheckRestService;
        }

        public PatientDemoEnquiry GetPatientEnquiry(string caseno)
        {
            try
            {
                return GetFuncWithRetry<PatientDemoEnquiry>(() =>
                {
                    var pr = patientVisitParser.GetPatientResult(caseno);


                    if (pr != null && pr.Patient != null && pr.CaseList != null)
                    {
                        Cache_HK.PataientCache.Register(pr.Patient.HKID, new Patient_AlertProfile { PatientDemoEnquiry = pr });
                    }

                    logger.Info(XmlHelper.XmlSerializeToString(pr));


                    //TODO: storage the response Postponse

                    //var patientVisit = pr.ToConvert();

                    //TODO: storage accesscenter business object to db            


                    return pr;
                });
            }
            catch (AMException amex)
            {
                logger.Error(amex);

                throw amex;
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                throw ex;
            }
        }

        public MedicationProfileResult GetMedicationProfiles(string caseno)
        {
            return GetFuncWithRetry<MedicationProfileResult>(() =>
            {
                var medicationProfile = profileService.GetMedicationProfile(caseno);

                logger.Info(JsonHelper.ToJson(medicationProfile));

                return medicationProfile;
            });
        }

        public AlertProfileResult GetAlertProfiles(AlertInputParm alertinput)
        {
            return GetFuncWithRetry<AlertProfileResult>(() =>
            {
                alertinput.Credentials.AccessCode = AccessCode;

                var apr = profileService.GetAlertProfile(alertinput);

                if (Cache_HK.PataientCache[alertinput.PatientInfo.Hkid] != null)
                {
                    Cache_HK.PataientCache[alertinput.PatientInfo.Hkid].AlertProfileRes = apr;
                }


                logger.Info(JsonHelper.ToJson(apr));
                //TODO:storage the response

                // var result = apr.ToConvert();

                return apr;
            });
        }

        public GetDrugMdsPropertyHqResponse GetDrugMdsPropertyHq(GetDrugMdsPropertyHqRequest request)
        {
            return drugMasterSoapService.getDrugMdsPropertyHq(request);
        }

        public GetPreparationResponse GetPreparation(GetPreparationRequest request)
        {
            return drugMasterSoapService.getPreparation(request);
        }

        public MDSCheckResult CheckMDS(MDSCheckInputParm inputParam)
        {
            return mdsCheckRestService.CheckMDS(inputParam);
        }

        public string SearchRemotePatient(string caseNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            var patient = patientVisitParser.GetPatientResult(caseNumber);

            if (patient == null)
            {
                return string.Empty;
            }
            var orders = profileService.GetMedicationProfile(caseNumber);


            var alertInputParm = new Models.AlertInputParm
            {
                PatientInfo = new PatientInfo
                {
                    Hkid = patient.Patient.HKID,
                    Name = patient.Patient.Name,
                    Sex = patient.Patient.Sex,
                    Dob = patient.Patient.DOB,
                    Cccode1 = patient.Patient.CCCode1,
                    Cccode2 = patient.Patient.CCCode2,
                    Cccode3 = patient.Patient.CCCode3,
                    Cccode4 = patient.Patient.CCCode4,
                    Cccode5 = patient.Patient.CCCode5,
                    Cccode6 = patient.Patient.CCCode6
                },
                UserInfo = new UserInfo
                {
                    HospCode = HospitalCode,
                    LoginId = "LoginId"
                },
                SysInfo = new SysInfo
                {
                    WsId = UtilityExtensions.GetLoalIPAddress(),
                    SourceSystem = "PMS"
                },
                Credentials = new Credentials
                {
                    AccessCode = AccessCode
                }
            };
            var allergys = profileService.GetAlertProfile(alertInputParm);

            var itemCodes = orders.MedProfileMoItems
                .SelectMany(s => s.MedProfilePoItems)
                .Select(o => o.ItemCode)
                .ToList();

            var getDrugMdsPropertyHq = drugMasterSoapService.getDrugMdsPropertyHq(
                new GetDrugMdsPropertyHqRequest
                {
                    Arg0 = new Arg { ItemCode = itemCodes }
                });

            foreach (var ICode in itemCodes)
            {
                var getpreparation = drugMasterSoapService.getPreparation(new GetPreparationRequest
                {
                    Arg0 = new Arg0
                    {
                        DispHospCode = string.Empty,
                        DispWorkstore = string.Empty,
                        ItemCode = ICode,
                        DrugScope = "I",
                        SpecialtyType = "I",
                        PasSpecialty = string.Empty,
                        PasSubSpecialty = string.Empty,
                        CostIncluded = true,
                        HqFlag = true
                    }
                });
            }



            return patient.Patient.HKID;
        }

        public ComplexMDSResult CheckRemoteMasterDrug(string HKID, string drugItemCode, out string errorMessage)
        {
            errorMessage = string.Empty;

            var patientInfo = Cache_HK.PataientCache[HKID];

            if (patientInfo == null)
            {
                return null;
            }
            var patient = patientInfo.PatientDemoEnquiry;

            var getDrugMdsPropertyHq = drugMasterSoapService.getDrugMdsPropertyHq(new GetDrugMdsPropertyHqRequest
            {
                Arg0 = new Arg { ItemCode = new List<string> { drugItemCode } }
            });

            var getpreparation = drugMasterSoapService.getPreparation(new GetPreparationRequest
            {
                Arg0 = new Arg0
                {
                    DispHospCode = string.Empty,
                    DispWorkstore = string.Empty,
                    ItemCode = drugItemCode,
                    DrugScope = "I",
                    SpecialtyType = "I",
                    PasSpecialty = string.Empty,
                    PasSubSpecialty = string.Empty,
                    CostIncluded = true,
                    HqFlag = true
                }
            });

            return CheckDrugClass(patient, patientInfo.AlertProfileRes, getDrugMdsPropertyHq, getpreparation);
        }

        private static bool IsInvalidAccessCode(AlertProfileResult actualProfile)
        {
            return actualProfile.ErrorMessage.Count > 0 && "20083".Equals(actualProfile.ErrorMessage[0].MsgCode);
        }

        static int Max_Retry_Count = 3;
        private static T GetFuncWithRetry<T>(Func<T> func) where T : class
        {
            int retryCount = 1;

            while (true)
            {
                try
                {
                    return func();
                }
                catch
                {
                    retryCount++;

                    if (retryCount > Max_Retry_Count)
                    {
                        throw;
                    }

                    Thread.Sleep(100);
                }
            }
        }

        public ComplexMDSResult CheckDrugClass(PatientDemoEnquiry patientEnquiry,
            AlertProfileResult alertProfileRes,
            GetDrugMdsPropertyHqResponse getDrugMdsPropertyHqRes,
            GetPreparationResponse getPreparationRes)
        {

            if (alertProfileRes == null
                || alertProfileRes.AdrProfile.Count == 0
                || alertProfileRes.AlertProfile.Count == 0
                || alertProfileRes.AllergyProfile.Count == 0
                )
            {
                /*ErrorMessage = "System cannot perform Allergy, ADR and G6PD Deficiency Contraindication checking. 
                Please exercise your professional judgement during the downtime period and contact [vendor contact information].*/

                return new ComplexMDSResult
                {
                    IsPerformMDSCheck = false,
                    ErrorMessage = "System cannot perform Allergy, AlertProfile is empty."
                };
            }

            if (getDrugMdsPropertyHqRes == null
              || getDrugMdsPropertyHqRes.Return.Count == 0
              )
            {
                return new ComplexMDSResult
                {
                    IsPerformMDSCheck = false,
                    ErrorMessage = "System cannot perform Allergy, getDrugMdsPropertyHq Response is empty."
                };
            }

            if (getPreparationRes == null)
            {
                return new ComplexMDSResult
                {
                    IsPerformMDSCheck = false,
                    ErrorMessage = "System cannot perform Allergy, getPreparation Response is empty."
                };
            }

            var mdsCheckObj = new MDSCheckInputParm { };

            mdsCheckObj.HasG6pdDeficiency = true;

            mdsCheckObj.PatientInfo = new MDSCheck_PatientInfo
            {
                HKID = patientEnquiry.Patient.HKID,
                PatientKey = patientEnquiry.Patient.Key,
                Sex = patientEnquiry.Patient.Sex,
                //Number of days between Current Date and Patient DOB  \\TODO-JERIFFE: CONFIRM THE AgeInDats Type
                AgeInDays = (DateTime.Now - patientEnquiry.Patient.DOB.ToDateTime()).TotalDays.ToString(),
                HeightInCM = "0",
                BSAInM2 = "0",
                WeightInKG = "0",
                DataUpdated = "N",
                DataWithinValidPeriod = "N"
            };

            mdsCheckObj.UserInfo = new MDSCheck_UserInfo
            {
                HospCode = HospitalCode,
                PharSpec = patientEnquiry.CaseList[0].Specialty,
                WrkStnID = UtilityExtensions.GetLoalIPAddress(),
                WrkStnType = "I"
            };

            foreach (var profile in alertProfileRes.AllergyProfile)
            {
                var patientAllergyProfile = new PatientAllergyProfile
                {
                    AllergySeqNo = profile.AllergySeqNo,
                    AllergenCode = "",
                    Displayname = profile.DisplayName,
                    Aliasname = profile.AliasName,
                    Salt = profile.Salt,
                    NameType = profile.NameType,
                    Allergen = profile.Allergen,
                    AllergenType = profile.AllergenType,
                    Certainty = profile.Certainty,
                    Remark = profile.Remark,
                    SourceSystem = "PMS",
                    CreateBy = "",
                    CreateUserName = "",
                    CreateHosp = "",
                    CreateRank = "",
                    CreateRankDesc = "",
                    CreateDtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.0"),
                    UpdateBy = profile.UpdateBy,
                    UpdateUserName = profile.UpdateUser,
                    UpdateHosp = profile.UpdateHospital,
                    UpdateRank = profile.UpdateUserRank,
                    UpdateRankDesc = profile.UpdateRankDesc,
                    //TODO: MAYE NEED TO CHECK profile.UpdateDtm IS NULL
                    UpdateDtm = profile.UpdateDtm,
                    Manifestations = null,
                    EhrLocalDesc = string.Empty,
                    HiclSeqNo = profile.HiclSeqno,
                    HicSeqNos = new HiclSeqNos { HicSeqNo = profile.HicSeqno }
                };

                /* 
                 * <updateDtm>2019-08-16 17:27:01.0</updateDtm>
                    <manifestations>
                        <seqNo>0</seqNo>
                        <mDesc>Allergic rhinitis</mDesc>
                    </manifestations>
                    <manifestations>
                        <seqNo>0</seqNo>
                        <mDesc>Asthma</mDesc>
                    </manifestations>
                    <hiclSeqNo>12057</hiclSeqNo>
                    <hicSeqNos>
                        <hicSeqNo>1117</hicSeqNo>
                        <hicSeqNo>1121</hicSeqNo>
                    </hicSeqNos>
                  */
                patientAllergyProfile.Manifestations = new List<Manifestations>();
                foreach (var item in profile.Manifestation.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    patientAllergyProfile.Manifestations.Add(
                    new Manifestations
                    {
                        SeqNo = "0",
                        MDesc = item,
                    });
                }

                mdsCheckObj.PatientAllergyProfile.Add(patientAllergyProfile);
            }

            foreach (var adrProfile in alertProfileRes.AdrProfile)
            {
                var patientAdrProfile = new PatientAdrProfile
                {
                    AdrSeqNo = adrProfile.AdrSeqNo,
                    AllergenCode = "",
                    Displayname = adrProfile.DisplayName,
                    Aliasname = adrProfile.AliasName,
                    Salt = adrProfile.Salt,
                    NameType = adrProfile.NameType,
                    Adr = adrProfile.Drug,
                    AdrType = adrProfile.DrugType,
                    Severity = adrProfile.Severity,
                    Remark = adrProfile.Remark,
                    SourceSystem = "PMS",
                    CreateBy = "",
                    CreateUserName = "",
                    CreateHosp = "",
                    CreateRank = "",
                    CreateRankDesc = "",
                    CreateDtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.0"),
                    UpdateBy = adrProfile.UpdateBy,
                    UpdateUserName = adrProfile.UpdateUser,
                    UpdateHosp = adrProfile.UpdateHospital,
                    UpdateRank = adrProfile.UpdateUserRank,
                    UpdateRankDesc = adrProfile.UpdateRankDesc,
                    //TODO: MAYE NEED TO CHECK profile.UpdateDtm IS NULL
                    UpdateDtm = adrProfile.UpdateDtm,
                    Reactions = null,
                    HiclSeqNo = adrProfile.HiclSeqno,
                    HicSeqNos = new HiclSeqNos { HicSeqNo = adrProfile.HicSeqno }
                };

                /*<updateDtm>2020-02-24 10:02:15.0</updateDtm>
                  <reactions>
                      <seqNo></seqNo>
                      <rDesc>Bronchospasm</rDesc>
                      <severCode>0</severCode>
                      <freqCode>0</freqCode>
                  </reactions>
                  <hiclSeqNo>13446</hiclSeqNo>
                  <hicSeqNos/>
                */
                patientAdrProfile.Reactions = new List<Reactions>();
                foreach (var item in adrProfile.Reaction.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    patientAdrProfile.Reactions.Add(new Reactions
                    {
                        SeqNo = "",
                        RDesc = item,
                        SeverCode = "0",
                        FreqCode = "0",
                    });
                }

                mdsCheckObj.PatientAdrProfile.Add(patientAdrProfile);

            }

            var currentRxDrugProfile = new CurrentRxDrugProfile
            {
                IsCapdItem = "false",
                //Initialize in the comming 
                GcnSeqNo = 0,
                RDfGenId = 0,
                RGenId = 0,
                Type = "",

                TrueDisplayName = getDrugMdsPropertyHqRes.Return[0].DrugProperty.Displayname,
                //Initialize in the comming 
                DrugDisplayName = "",
                DrugDdimDisplayName = "",
                DrugErrorDisplayName = "",

                ArrayPos = "0",
                IndRow = "0",
                OrdNo = "0",
                HospCode = HospitalCode,
                Delete = "false",

                Salt = getDrugMdsPropertyHqRes.Return[0].DrugProperty.SaltProperty,
                Strength = getPreparationRes.Return.Strength,
                FormCode = getDrugMdsPropertyHqRes.Return[0].DrugProperty.FormCode,
                ItemCode = getDrugMdsPropertyHqRes.Return[0].ItemCode,

                SpecRestrict = "",
                SpecInstruct = "",
                PharSpec = "",
                DataUpdate = "N",
                DdimDosRelatedCheck = "N",
            };

            /*GcnSeqNo
             * if  (moeCheckFlag from 2.4.1.2) = “Y”
                 if (gcnSeqno from 2.4.1.2) > 0, then gcnSeqno from 2.4.1.2
                 else “0”
              else if (GroupMoeCheckFlag from 2.4.1.2) = “Y”
                 if (groupGcnSeqno from2.4.1.2) > 0, then groupGcnSeqno from2.4.1.2
                 else “0”
             else skip MDS checking


            *RDfGenId
            *if  (moeCheckFlag from 2.4.1.2) = “Y”
                if (routeformGeneric from 2.4.1.2) > 0, then routeformGeneric from 2.4.1.2 
                else “0”
            else if (GroupMoeCheckFlag from 2.4.1.2) = “Y”
                if (groupRouteformGeneric from2.4.1.2) > 0, then groupRouteformGeneric from2.4.1.2
                else “0”
            else skip MDS checking

            *RGenId
            *if  (moeCheckFlag from 2.4.1.2) = “Y”
                  if (routedGeneric from 2.4.1.2) > 0, then routedGeneric from 2.4.1.2
                  else “0”
              else if (GroupMoeCheckFlag from 2.4.1.2) = “Y”
                  if (groupRoutedGeneric from2.4.1.2) > 0, then groupRoutedGeneric from2.4.1.2
                  else “0”
              else skip MDS checking
            */
            var drugMds = getDrugMdsPropertyHqRes.Return[0].DrugMds;
            var drugProperty = getDrugMdsPropertyHqRes.Return[0].DrugProperty;
            if (drugMds.MoeCheckFlag == "Y")
            {
                currentRxDrugProfile.GcnSeqNo = drugMds.GcnSeqno > 0 ? drugMds.GcnSeqno : 0;
                currentRxDrugProfile.RGenId = drugMds.RoutedGeneric > 0 ? drugMds.RoutedGeneric : 0;
                currentRxDrugProfile.RDfGenId = drugMds.RouteformGeneric > 0 ? drugMds.RouteformGeneric : 0;
            }
            else if (drugMds.GroupMoeCheckFlag == "Y")
            {
                currentRxDrugProfile.GcnSeqNo = drugMds.GroupGcnSeqno > 0 ? drugMds.GroupGcnSeqno : 0;
                currentRxDrugProfile.RGenId = drugMds.GroupRoutedGeneric > 0 ? drugMds.GroupRoutedGeneric : 0;
                currentRxDrugProfile.RDfGenId = drugMds.GroupRouteformGeneric > 0 ? drugMds.GroupRouteformGeneric : 0;
            }
            else
            {
                // skip MDS checking
            }

            /*Type
             if (gcnSeqNum > 0), then "S"
             else if (gcnSeqNum <= 0 && (rdfgenId > 0 || rgenId > 0)), then "R"
             else "X"
            */
            if (currentRxDrugProfile.GcnSeqNo > 0)
            {
                currentRxDrugProfile.Type = "S";
            }
            else if (currentRxDrugProfile.RDfGenId > 0 || currentRxDrugProfile.RGenId > 0)
            {
                currentRxDrugProfile.Type = "R";
            }
            else
            {
                currentRxDrugProfile.Type = "X";

            }

            /*DrugDdimDisplayName
             drugDisplayName +
             if (Drug moeDesc from 2.4.2.2) is BLANK, then BLANK
             else “ “ + lowercase(Drug moeDesc from 2.4.2.2)
            */
            currentRxDrugProfile.DrugDdimDisplayName = drugProperty.Displayname;
            if (!string.IsNullOrEmpty(getPreparationRes.Return.MoeDesc))
            {
                currentRxDrugProfile.DrugDdimDisplayName += " " + getPreparationRes.Return.MoeDesc.ToLower();
            }

            /*DrugErrorDisplayName
            drugDdimDisplayName +
            1.	if(Drug strength from 2.4.2.2) is BLANK, then BLANK
            else “ “ + lowercase(Drug strength from 2.4.2.2)
            2.	if(Drug volumeValue from 2.4.2.2) not > 0, then BLANK
            else “ “ + (Drug volumeValue from 2.4.2.2) in format "#######.####" + lowercase(Drug  volumeUnit from 2.4.2.2)
            */
            currentRxDrugProfile.DrugErrorDisplayName += currentRxDrugProfile.DrugDdimDisplayName;
            if (!string.IsNullOrEmpty(getPreparationRes.Return.Strength))
            {
                currentRxDrugProfile.DrugErrorDisplayName += " " + getPreparationRes.Return.Strength.ToLower();
            }

            if (getPreparationRes.Return.VolumeValue > 0)
            {
                var formatValue = FormatVolumeValue(getPreparationRes.Return.VolumeValue);

                currentRxDrugProfile.DrugErrorDisplayName += " " + formatValue + getPreparationRes.Return.VolumeUnit.ToLower();
            }

            mdsCheckObj.CurrentRxDrugProfile = currentRxDrugProfile;

            /*if alertProfile from 1.4.2 = G6PD, then “true”, 
              else “false”
            */
            mdsCheckObj.HasG6pdDeficiency = false;
            mdsCheckObj.HasPregnancy = false;

            mdsCheckObj.CheckDdim = false;
            mdsCheckObj.CheckDdcm = false;
            mdsCheckObj.CheckDam = false;
            mdsCheckObj.CheckAdr = false;
            mdsCheckObj.CheckDscm = false;
            mdsCheckObj.CheckDrcm = false;
            mdsCheckObj.CheckDlcm = false;
            mdsCheckObj.CheckSteroid = false;
            mdsCheckObj.CheckDiscon = false;

            mdsCheckObj.CallerSourceSystem = "PMS";


            var medCache = new MDSCheckCacheResult { Req = mdsCheckObj, };
            Cache_HK.MDS_CheckCache.Register(mdsCheckObj.PatientInfo.HKID, medCache);

            var result = mdsCheckRestService.CheckMDS(mdsCheckObj);

            var wrapperResult = WrapperMDSResponse(result);

            medCache.Res = wrapperResult;

            return medCache.Res;
        }

        private ComplexMDSResult WrapperMDSResponse(MDSCheckResult result)
        {
            //Mapper result

            return new ComplexMDSResult
            {
                ErrorMessage = "",
                IsPerformMDSCheck = true,
                Result = result
            };
        }

        private string FormatVolumeValue(float volumeValue)
        {
            return string.Format("{0:#######.####}", volumeValue);
        }
    }

    public class ComplexMDSResult
    {
        public bool IsPerformMDSCheck { get; set; }
        public string ErrorMessage { get; set; }

        public MDSCheckResult Result { get; set; }
    }
}
/*MDS Check Service 
<inputParm>
    <patientInfo>        
        <!-- HN170002520 -->
        <HKID>I0013638</HKID>
        <patientKey>92621100</patientKey>
        <sex>M</sex>
        <ageInDays>7805</ageInDays>
        <heightInCM>0</heightInCM>
        <BSAInM2>0</BSAInM2>
        <weightInKG>0</weightInKG>
        <dataUpdated>N</dataUpdated>
        <dataWithinValidPeriod>N</dataWithinValidPeriod>
    </patientInfo>
    <userInfo>
        <hospCode>VH</hospCode>
        <pharSpec>MED</pharSpec>
        <wrkStnID>160.68.34.60</wrkStnID>
        <wrkStnType>O</wrkStnType>
    </userInfo>
    <patientAllergyProfile>
        <allergySeqNo>0000091639</allergySeqNo>
        <allergenCode></allergenCode>
        <displayname>CIPROFLOXACIN</displayname>
        <ehrLocalDesc></ehrLocalDesc>
        <aliasname></aliasname>
        <salt></salt>
        <nameType>D</nameType>
        <allergen>CIPROFLOXACIN</allergen>
        <allergenType>D</allergenType>
        <certainty>Certain</certainty>
        <remark></remark>
        <sourceSystem>CMS</sourceSystem>
        <createBy></createBy>
        <createUserName></createUserName>
        <createHosp></createHosp>
        <createRank></createRank>
        <createRankDesc></createRankDesc>
        <createDtm>2020-02-24 10:03:30.0</createDtm>
        <updateBy>Y2KMED</updateBy>
        <updateUserName>AA, FORMAT in IP</updateUserName>
        <updateHosp>VH</updateHosp>
        <updateRank>Department Manager (Pharmacy)</updateRank>
        <updateRankDesc>Department Manager (Pharmacy)</updateRankDesc>
        <updateDtm>2020-02-24 10:01:47.0</updateDtm>
        <manifestations>
            <seqNo>0</seqNo>
            <mDesc>Allergic rhinitis</mDesc>
        </manifestations>
        <manifestations>
            <seqNo>0</seqNo>
            <mDesc>Asthma</mDesc>
        </manifestations>
        <manifestations>
            <seqNo>0</seqNo>
            <mDesc>Rash</mDesc>
        </manifestations>
        <hiclSeqNo>13446</hiclSeqNo>
        <hicSeqNos/>
    </patientAllergyProfile>
    <patientAllergyProfile>
        <allergySeqNo>0000090491</allergySeqNo>
        <allergenCode></allergenCode>
        <displayname>GAVISCON</displayname>
        <ehrLocalDesc></ehrLocalDesc>
        <aliasname></aliasname>
        <salt></salt>
        <nameType>D</nameType>
        <allergen>GAVISCON</allergen>
        <allergenType>D</allergenType>
        <certainty>Certain</certainty>
        <remark></remark>
        <sourceSystem>CMS</sourceSystem>
        <createBy></createBy>
        <createUserName></createUserName>
        <createHosp></createHosp>
        <createRank></createRank>
        <createRankDesc></createRankDesc>
        <createDtm>2020-02-24 10:03:30.0</createDtm>
        <updateBy>Y2KMED</updateBy>
        <updateUserName>AA, FORMAT in IP</updateUserName>
        <updateHosp>VH</updateHosp>
        <updateRank>Department Manager (Pharmacy)</updateRank>
        <updateRankDesc>Department Manager (Pharmacy)</updateRankDesc>
        <updateDtm>2019-09-09 17:39:30.0</updateDtm>
        <manifestations>
            <seqNo>0</seqNo>
            <mDesc>Allergic contact dermatitis</mDesc>
        </manifestations>
        <hiclSeqNo xsi:nil="true" 
            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"/>
        <hicSeqNos>
            <hicSeqNo>1121</hicSeqNo>
            <hicSeqNo>1123</hicSeqNo>
            <hicSeqNo>2434</hicSeqNo>
            <hicSeqNo>2523</hicSeqNo>
        </hicSeqNos>
    </patientAllergyProfile>
    <patientAdrProfile>
        <adrSeqNo>0000005400</adrSeqNo>
        <allergenCode></allergenCode>
        <displayname>CIPROFLOXACIN</displayname>
        <aliasname></aliasname>
        <salt></salt>
        <nameType>D</nameType>
        <adr>CIPROFLOXACIN</adr>
        <adrType>D</adrType>
        <severity>S</severity>
        <remark></remark>
        <sourceSystem>CMS</sourceSystem>
        <createBy></createBy>
        <createUserName></createUserName>
        <createHosp></createHosp>
        <createRank></createRank>
        <createRankDesc></createRankDesc>
        <createDtm>2020-02-24 10:03:30.0</createDtm>
        <updateBy>Y2KMED</updateBy>
        <updateUserName>AA, FORMAT in IP</updateUserName>
        <updateHosp>VH</updateHosp>
        <updateRank>Department Manager (Pharmacy)</updateRank>
        <updateRankDesc>Department Manager (Pharmacy)</updateRankDesc>
        <updateDtm>2020-02-24 10:02:15.0</updateDtm>
        <reactions>
            <seqNo></seqNo>
            <rDesc>Abdominal Pain with Cramps</rDesc>
            <severCode>0</severCode>
            <freqCode>0</freqCode>
        </reactions>
        <reactions>
            <seqNo></seqNo>
            <rDesc>Others</rDesc>
            <severCode>0</severCode>
            <freqCode>0</freqCode>
        </reactions>
        <hiclSeqNo>13446</hiclSeqNo>
        <hicSeqNos/>
    </patientAdrProfile>
    <patientAdrProfile>
        <adrSeqNo>0000004921</adrSeqNo>
        <allergenCode></allergenCode>
        <displayname>ASPIRIN</displayname>
        <aliasname></aliasname>
        <salt></salt>
        <nameType>D</nameType>
        <adr>ASPIRIN</adr>
        <adrType>D</adrType>
        <severity>S</severity>
        <remark>TEST 2</remark>
        <sourceSystem>CMS</sourceSystem>
        <createBy></createBy>
        <createUserName></createUserName>
        <createHosp></createHosp>
        <createRank></createRank>
        <createRankDesc></createRankDesc>
        <createDtm>2020-02-24 10:03:30.0</createDtm>
        <updateBy>Y2KMED</updateBy>
        <updateUserName>AA, FORMAT in IP</updateUserName>
        <updateHosp>VH</updateHosp>
        <updateRank>Department Manager (Pharmacy)</updateRank>
        <updateRankDesc>Department Manager (Pharmacy)</updateRankDesc>
        <updateDtm>2019-08-29 16:43:06.0</updateDtm>
        <reactions>
            <seqNo></seqNo>
            <rDesc>Abdominal Pain with Cramps</rDesc>
            <severCode>0</severCode>
            <freqCode>0</freqCode>
        </reactions>
        <reactions>
            <seqNo></seqNo>
            <rDesc>Heartburn</rDesc>
            <severCode>0</severCode>
            <freqCode>0</freqCode>
        </reactions>
        <hiclSeqNo>1820</hiclSeqNo>
        <hicSeqNos/>
    </patientAdrProfile>
    <currentRxDrugProfile>
        <isCapdItem>false</isCapdItem>
        <type>S</type>
        <rGenId>1052700</rGenId>
        <rDfGenId>9927</rDfGenId>
        <gcnSeqNo>9509</gcnSeqNo>
        <trueDisplayName>CIPROFLOXACIN</trueDisplayName>
        <drugDisplayName>Ciprofloxacin Hcl</drugDisplayName>
        <drugErrorDisplayName>Ciprofloxacin Hcl tablet</drugErrorDisplayName>
        <arrayPos>0</arrayPos>
        <indRow>1</indRow>
        <ordNo>0</ordNo>
        <hospCode>VH</hospCode>
        <delete>false</delete>
        <salt>HCL</salt>
        <strength xsi:nil="true" 
            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"/>
        <drugDdimDisplayName>Ciprofloxacin Hcl tablet</drugDdimDisplayName>
        <formCode>TAB</formCode>
        <itemCode>CIPR01</itemCode>
        <specRestrict></specRestrict>
        <specInstruct></specInstruct>
        <pharSpec></pharSpec>
        <dataUpdate>N</dataUpdate>
        <ddimDosRelatedCheck>N</ddimDosRelatedCheck>
    </currentRxDrugProfile>
    <hasG6pdDeficiency>true</hasG6pdDeficiency>
    <hasPregnancy>false</hasPregnancy>
    <checkDdim>false</checkDdim>
    <checkDdcm>true</checkDdcm>
    <checkDam>true</checkDam>
    <checkAdr>true</checkAdr>
    <checkDscm>false</checkDscm>
    <checkDrcm>false</checkDrcm>
    <checkDlcm>false</checkDlcm>
    <checkSteroid>false</checkSteroid>
    <callerSourceSystem>PMS</callerSourceSystem>
    <checkDiscon>false</checkDiscon>
    <checkHepaB>false</checkHepaB>
</inputParm>
*/
