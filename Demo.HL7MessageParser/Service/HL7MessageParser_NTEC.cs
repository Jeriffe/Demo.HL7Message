using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.DTOs;
using Demo.HL7MessageParser.Model;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.Service;
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
        private string DrugMasterSoapUrl;
        private string SoapUrl;

        private string UserName;
        private string Password;

        private string ClientSecret;
        private string ClientId;
        private string AccessCode;

        private string HospitalCode;

        private ISoapParserSvc soapSvc;
        private ISoapWSEService soapWSESvc;
        private IRestParserSvc restSvc;
        private CheckBeforeMDSChecker mdsChecker = new CheckBeforeMDSChecker();
        public HL7MessageParser_NTEC()
        {
            Initialize();
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

            DrugMasterSoapUrl = "http://localhost:8096/DrugMasterService.asmx";

            soapWSESvc = new SoapWSEParserSvc(SoapUrl, UserName, Password, HospitalCode);

            soapSvc = new SoapParserSvc(DrugMasterSoapUrl, HospitalCode);

            restSvc = new RestParserSvc(RestUrl, ClientSecret, ClientId, HospitalCode);
        }

        public HL7MessageParser_NTEC(ISoapParserSvc soapSvc, ISoapWSEService soapWSESvc, IRestParserSvc restSvc)
        {
            this.soapSvc = soapSvc;
            this.soapWSESvc = soapWSESvc;
            this.restSvc = restSvc;
        }

        public string SearchRemotePatient(string caseNumber, ref string errorMessage)
        {
            errorMessage = string.Empty;
            var CASE_NUMBER = caseNumber.Trim().ToUpper();

            var patient = CallFuncWithRetry<PatientDemoEnquiry>(() =>
            {
                var pr = soapWSESvc.GetPatientResult(caseNumber);


                logger.Info(XmlHelper.XmlSerializeToString(pr));


                return pr;
            });

            if (patient == null || patient.Patient == null)
            {
                return string.Empty;
            }

            /*TODO:Storage Patient */

            var orders = CallFuncWithRetry<MedicationProfileResult>(() =>
          {
              var medicationProfile = restSvc.GetMedicationProfile(caseNumber);


              logger.Info(JsonHelper.ToJson(medicationProfile));

              return medicationProfile;
          });

            CheckItemCodeofMedicationProfile(orders, ref errorMessage);

            var alertRequest = new Models.AlertInputParm
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
                    WsId = UtilityHelper.GetLoalIPAddress(),
                    SourceSystem = "PMS"
                },
                Credentials = new Credentials
                {
                    AccessCode = AccessCode
                }
            };

            var allergys = CallFuncWithRetry<AlertProfileResult>(() =>
            {
                var apr = restSvc.GetAlertProfile(alertRequest);


                logger.Info(JsonHelper.ToJson(apr));

                return apr;
            });


            if (patient != null && patient.Patient != null && patient.CaseList != null)
            {
                FullCacheHK.PataientCache.Register(CASE_NUMBER, new Patient_AlertProfile { PatientDemoEnquiry = patient });
            }
            if (FullCacheHK.PataientCache[CASE_NUMBER] != null)
            {
                //FullCacheHK.PataientCache[CASE_NUMBER].MedicationProfileRes = orders;
                FullCacheHK.PataientCache[CASE_NUMBER].AlertProfileRes = allergys;
            }


            return patient.Patient.HKID;
        }

        public MdsCheckFinalResult MDSCheck(InventoryObj drugItem, string caseNumber, PatientDemoEnquiry patientEnquiry, AlertProfileResult alertProfileRes)
        {
            MDSCheckResult resultBeforeMDS = new MDSCheckResult() { hasMdsAlert = false };

            if (alertProfileRes == null
                || alertProfileRes.AdrProfile.Count == 0
                || alertProfileRes.AlertProfile.Count == 0
                || alertProfileRes.AllergyProfile.Count == 0
                )
            {
                /*ErrorMessage = "System cannot perform Allergy, ADR and G6PD Deficiency Contraindication checking. 
                Please exercise your professional judgement during the downtime period and contact [vendor contact information].*/
                resultBeforeMDS.errorCode = "8520001001";
                //system error, hasMdsAlert = false, only medication alert, hasMdsAlert = true

                resultBeforeMDS.errorDesc = "System cannot perform Allergy, AlertProfile is empty.";
                //here should use drug name, not drugItemCode
                return resultBeforeMDS.ToConvert(drugItem.CommonName);
            }

            #region check if need MDS check
            /****2.5.3 3:System should not perform MDS checking on a drug item if its itemCode starts with “PDF”, 
             * e.g. “PDF 2Q “, “PDF 48”. no prompt message *****/
            if (mdsChecker.CheckDrugCodeIfNoNeedMDSCheck(drugItem.Billnum))
            {
                //here should use drug name, not drugItemCode
                return resultBeforeMDS.ToConvert(drugItem.CommonName);
            }
            /*****2.5.3 2: ADR record (1.4.2) if its severity is “Mild”, not perform MDS checking for current ADR profile*****/
            mdsChecker.CheckADRProfileForMDSCheck(ref alertProfileRes, caseNumber);

            #endregion

            var getDrugMdsPropertyHqReq = new GetDrugMdsPropertyHqRequest
            {
                Arg0 = new Arg { ItemCode = new List<string> { drugItem.Billnum } }
            };
            var getDrugMdsPropertyHqRes = soapSvc.GetDrugMdsPropertyHq(getDrugMdsPropertyHqReq);

            if (getDrugMdsPropertyHqRes == null || getDrugMdsPropertyHqRes.Return.Count == 0)
            {
                resultBeforeMDS.errorCode = "8520001002";
                resultBeforeMDS.errorDesc = "System cannot perform Allergy, Drug Master Response is empty.";

                return resultBeforeMDS.ToConvert(drugItem.CommonName);
            }

            var drugProperty = getDrugMdsPropertyHqRes.Return[0].DrugProperty;
            var getPreparationReq = new GetPreparationRequest
            {
                Arg0 = new Arg0
                {
                    DispHospCode = "",
                    DispWorkstore = "",
                    ItemCode = drugItem.Billnum,
                    TrueDisplayname = drugProperty.Displayname,
                    FormCode = drugProperty.FormCode,
                    SaltProperty = drugProperty.SaltProperty,
                    DrugScope = "I",
                    SpecialtyType = "I",
                    PasSpecialty = "",
                    PasSubSpecialty = "",
                    CostIncluded = true,
                    HqFlag = true
                }
            };
            var getPreparationRes = soapSvc.GetPreparation(getPreparationReq);

            if (getPreparationRes == null || getPreparationRes.Return == null)
            {
                resultBeforeMDS.errorCode = "8520001003";
                resultBeforeMDS.errorDesc = "System cannot perform Allergy, Drug Preparation Response is empty.";

                return resultBeforeMDS.ToConvert(drugItem.CommonName);
            }

            #region JUST FOR SIMULATOR
            var patientCache = FullCacheHK.PataientCache[patientEnquiry.CaseList[0].Number.Trim().ToUpper()];
            if (patientCache != null)
            {
                patientCache.DrugMasterCache.DrugMdsPropertyHqReq = getDrugMdsPropertyHqReq;
                patientCache.DrugMasterCache.DrugMdsPropertyHqRes = getDrugMdsPropertyHqRes; ;


                patientCache.DrugMasterCache.PreparationReq = getPreparationReq; ;
                patientCache.DrugMasterCache.PreparationRes = getPreparationRes; ;

            }
            #endregion

            string drugName = drugProperty.Displayname;

            /*if  hasG6pdDeficiency is true or  hasPregnancy is true, then “true”, else “false”*/
            bool hasG6pdDeficiency = mdsChecker.CheckIsG6PD(alertProfileRes.AlertProfile);

            bool checkDdcm = hasG6pdDeficiency;
            bool checkDam = alertProfileRes.AllergyProfile.Count() > 0;
            bool checkAdr = alertProfileRes.AdrProfile.Count() > 0;

            //if no ddcm, no allergy, no adr, then no need do MDS check
            if (!checkDdcm && !checkDam && !checkAdr)
            {
                return new MdsCheckFinalResult();
            }

            MDSCheckInputParm mdsRequest = new MDSCreater().CreateMDSRequest(patientEnquiry,
                alertProfileRes,
                getDrugMdsPropertyHqRes,
                getPreparationRes,
                checkDdcm,
                checkDam,
                checkAdr,
                ref drugName
                );

            /************do Final MDS Check*********************/
            MDSCheckResult mdsCheckResult = restSvc.CheckMDS(mdsRequest);

            //filter final MDS check result
            mdsCheckResult.FilterMdsResult();

            #region JUST FOR SIMULATOR
            if (patientCache != null)
            {
                patientCache.MDSCheck = new MDSCheckResultCache { Req = mdsRequest, Res = mdsCheckResult };
            }
            #endregion

            //convert mds result to message object to show
            return mdsCheckResult.ToConvert(drugName);
        }

        private void CheckItemCodeofMedicationProfile(MedicationProfileResult orders, ref string errorMessage)
        {
            //CHECK ITEM CODES
        }

        static int Max_Retry_Count = 3;
        private static T CallFuncWithRetry<T>(Func<T> func) where T : class
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

        private string FormatVolumeValue(decimal volumeValue)
        {
            return string.Format("{0:#######.####}", volumeValue);
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
