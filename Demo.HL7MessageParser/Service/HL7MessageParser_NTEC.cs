using Demo.HL7MessageParser.Common;
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

        public MedicationProfileResult GetMedicationProfiles(string caseno)
        {
            return GetFuncWithRetry<MedicationProfileResult>(() =>
            {
                var medicationProfile = profileService.GetMedicationProfile(caseno);

                logger.Info(JsonHelper.ToJson(medicationProfile));

                return medicationProfile;
            });
        }

        public PatientDemoEnquiry GetPatientEnquiry(string caseno)
        {
            try
            {
                return GetFuncWithRetry<PatientDemoEnquiry>(() =>
                {
                    var pr = patientVisitParser.GetPatientResult(caseno);

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

        public AlertProfileResult GetAlertProfiles(AlertInputParm alertinput)
        {
            return GetFuncWithRetry<AlertProfileResult>(() =>
            {
                alertinput.Credentials.AccessCode = AccessCode;

                var apr = profileService.GetAlertProfile(alertinput);

                logger.Info(JsonHelper.ToJson(apr));
                //TODO:storage the response

                // var result = apr.ToConvert();

                return apr;
            });
        }

        public GetDrugMdsPropertyHqResponse getDrugMdsPropertyHq(GetDrugMdsPropertyHqRequest request)
        {
            return drugMasterSoapService.getDrugMdsPropertyHq(request);
        }

        public GetPreparationResponse getPreparation(GetPreparationRequest request)
        {
            return drugMasterSoapService.getPreparation(request);
        }

        public MDSCheckResult CheckMDS(MDSCheckInputParm inputParam)
        {
            return mdsCheckRestService.CheckMDS(inputParam);
        }

        public string SaveRemoteHL7PatientToLocal(string caseNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            var actualProfile = profileService.GetAlertProfile(new Models.AlertInputParm
            {
                PatientInfo = new Models.PatientInfo { Hkid = caseNumber },
                Credentials = new Models.Credentials { AccessCode = AccessCode }
            });

            if (IsInvalidAccessCode(actualProfile))
            {
            }
            return null;
        }

        private static bool IsInvalidAccessCode(AlertProfileResult actualProfile)
        {
            return actualProfile.ErrorMessage.Count > 0 && "20083".Equals(actualProfile.ErrorMessage[0].MsgCode);
        }


        public void CheckDrugClass(PatientDemoEnquiry patientEnquiry, 
            AlertProfileResult alertProfileRes, 
            GetDrugMdsPropertyHqResponse getDrugMdsPropertyHqRes, 
            GetPreparationResponse getPreparationRes)
        {
            var mdsCheckObj = new MDSCheckInputParm
            {
                HasPregnancy = "false",
                CallerSourceSystem = "PMS",
                CheckDiscon = "false",
               
            };
            mdsCheckObj.HasG6pdDeficiency = true;

            var patientInfo = new MDSCheck_PatientInfo
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

            var userInfo = new MDSCheck_UserInfo
            {
                HospCode = HospitalCode,
                PharSpec = patientEnquiry.CaseList[0].Specialty,
                WrkStnID = "IP Address",
                WrkStnType = "I"
            };

            foreach (var profile in alertProfileRes.AllergyProfile)
            {
                var allergyProfile = new PatientAllergyProfile
                {
                    AllergySeqNo = profile.AllergySeqNo,
                    AllergenCode = "",
                    Displayname = profile.DisplayName,
                    EhrLocalDesc = string.Empty,
                    HiclSeqNo = profile.HiclSeqno,
                    HicSeqNos = new HiclSeqNos { HicSeqNo = profile.HicSeqno }
                };
            }


            foreach (var profile in alertProfileRes.AdrProfile)
            {
                var allergyProfile = new PatientAdrProfile
                {
                    AdrSeqNo = profile.AdrSeqNo,
                    AllergenCode = "",
                    Displayname = profile.DisplayName,
                    Aliasname = profile.AliasName,
                    HiclSeqNo = profile.HiclSeqno,
                    HicSeqNos = new HiclSeqNos { HicSeqNo = profile.HicSeqno }
                };
            }

            var currentRxDrugProfile = new CurrentRxDrugProfile
            {
                IsCapdItem = "false",
                TrueDisplayName = getDrugMdsPropertyHqRes.Return[0].DrugProperty.Displayname,
                ArrayPos = "0",
                IndRow = "0",

                OrdNo = "0",
                HospCode = HospitalCode,
                Delete = "false",
                Salt = getDrugMdsPropertyHqRes.Return[0].DrugProperty.SaltProperty,
                FormCode = getDrugMdsPropertyHqRes.Return[0].DrugProperty.FormCode,
                ItemCode = getDrugMdsPropertyHqRes.Return[0].ItemCode,
                Strength = getPreparationRes.Return.Strength,
                SpecRestrict = "",
                SpecInstruct = "",
                PharSpec = "",
                DataUpdate = "N",
                DdimDosRelatedCheck = "N"
            };

           /*if  (moeCheckFlag from 2.4.1.2) = “Y”
                if (gcnSeqno from 2.4.1.2) > 0, then gcnSeqno from 2.4.1.2
                else “0”
             else if (GroupMoeCheckFlag from 2.4.1.2) = “Y”
                if (groupGcnSeqno from2.4.1.2) > 0, then groupGcnSeqno from2.4.1.2
                else “0”
            else skip MDS checking
           */
            currentRxDrugProfile.GcnSeqNo = "";

            /*if  (moeCheckFlag from 2.4.1.2) = “Y”
                if (routeformGeneric from 2.4.1.2) > 0, then routeformGeneric from 2.4.1.2
                else “0”
             else if (GroupMoeCheckFlag from 2.4.1.2) = “Y”
                if (groupRouteformGeneric from2.4.1.2) > 0, then groupRouteformGeneric from2.4.1.2
                else “0”
            else skip MDS checking
             */
            currentRxDrugProfile.RDfGenId = "";

            /*if  (moeCheckFlag from 2.4.1.2) = “Y”
                  if (routedGeneric from 2.4.1.2) > 0, then routedGeneric from 2.4.1.2
                  else “0”
              else if (GroupMoeCheckFlag from 2.4.1.2) = “Y”
                  if (groupRoutedGeneric from2.4.1.2) > 0, then groupRoutedGeneric from2.4.1.2
                  else “0”
              else skip MDS checking
            */
            currentRxDrugProfile.RGenId = "";
            /*
             if (gcnSeqNum > 0), then "S"
             else if (gcnSeqNum <= 0 && (rdfgenId > 0 || rgenId > 0)), then "R"
             else "X"
            */
            currentRxDrugProfile.Type = "";


            /*drugDisplayName +
             if (Drug moeDesc from 2.4.2.2) is BLANK, then BLANK
             else “ “ + lowercase(Drug moeDesc from 2.4.2.2)
            */
            currentRxDrugProfile.DrugDdimDisplayName = "";

            /*drugDdimDisplayName +
            1.	if(Drug strength from 2.4.2.2) is BLANK, then BLANK
            else “ “ + lowercase(Drug strength from 2.4.2.2)
            2.	if(Drug volumeValue from 2.4.2.2) not > 0, then BLANK
            else “ “ + (Drug volumeValue from 2.4.2.2) in format "#######.####" + lowercase(Drug  volumeUnit from 2.4.2.2)
            */
            currentRxDrugProfile.DrugErrorDisplayName = "";
        }
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
            <mDesc>Angioedema</mDesc>
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
            <hicSeqNo>743</hicSeqNo>
            <hicSeqNo>1117</hicSeqNo>
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
            <rDesc>Bronchospasm</rDesc>
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
