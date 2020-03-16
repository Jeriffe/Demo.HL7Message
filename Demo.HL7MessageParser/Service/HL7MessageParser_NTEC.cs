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
        private CheckBeforeMDSCheck checkMDS = new CheckBeforeMDSCheck();
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

        public MdsCheckFinalResult MDSCheck(string drugItemCode, PatientDemoEnquiry patientEnquiry, AlertProfileResult alertProfileRes)
        {
            MDSCheckResult mdsResult = new MDSCheckResult();

            if (alertProfileRes == null
                || alertProfileRes.AdrProfile.Count == 0
                || alertProfileRes.AlertProfile.Count == 0
                || alertProfileRes.AllergyProfile.Count == 0
                )
            {
                /*ErrorMessage = "System cannot perform Allergy, ADR and G6PD Deficiency Contraindication checking. 
                Please exercise your professional judgement during the downtime period and contact [vendor contact information].*/
                mdsResult.errorCode = "8520001001";
                //system error, hasMdsAlert = false, only medication alert, hasMdsAlert = true
                mdsResult.hasMdsAlert = false;
                mdsResult.errorDesc = "System cannot perform Allergy, AlertProfile is empty.";
                //here should use drug name, not drugItemCode
                return GenerateFinalResultFromMDSResult(mdsResult, drugItemCode);
            }

            #region check if need MDS check
            /****2.5.3 3:System should not perform MDS checking on a drug item if its itemCode starts with “PDF”, e.g. “PDF 2Q “, “PDF 48”.*****/
            if (!checkMDS.CheckDrugCodeIfNeedMDSCheck(drugItemCode, ref mdsResult))
            {
                //here should use drug name, not drugItemCode
                return GenerateFinalResultFromMDSResult(mdsResult, drugItemCode);
            }
            /*****2.5.3 2: ADR record (1.4.2) if its severity is “Mild”, not perform MDS checking for current ADR profile*****/
            checkMDS.CheckADRProfileForMDSCheck(ref alertProfileRes, ref mdsResult);

            /****2.5.3 4-1:System should alert user and not perform MDS checking ...*******/
            checkMDS.CheckAllergyProfileForMDSCheck(ref alertProfileRes, ref mdsResult);
            #endregion

            var getDrugMdsPropertyHqReq = new GetDrugMdsPropertyHqRequest
            {
                Arg0 = new Arg { ItemCode = new List<string> { drugItemCode } }
            };
            var getDrugMdsPropertyHqRes = soapSvc.GetDrugMdsPropertyHq(getDrugMdsPropertyHqReq);

            if (getDrugMdsPropertyHqRes == null || getDrugMdsPropertyHqRes.Return.Count == 0)
            {
                mdsResult.hasMdsAlert = true;
                mdsResult.drugError = new DrugError()
                {
                    errorDesc = "System cannot perform Allergy, Drug Master Response is empty.",
                    hasDrugError = true
                };
                //here should use drug name, not drugItemCode
                return GenerateFinalResultFromMDSResult(mdsResult, drugItemCode);
            }

            var drugProperty = getDrugMdsPropertyHqRes.Return[0].DrugProperty;
            var getPreparationReq = new GetPreparationRequest
            {
                Arg0 = new Arg0
                {
                    DispHospCode = "",
                    DispWorkstore = "",
                    ItemCode = drugItemCode,
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
                mdsResult.hasMdsAlert = false;
                mdsResult.drugError = new DrugError()
                {
                    errorDesc = "System cannot perform Allergy, Drug Preparation Response is empty.",
                    hasDrugError = true
                };
                return GenerateFinalResultFromMDSResult(mdsResult, drugProperty.Displayname);
            }
            
            if (FullCacheHK.DrugMasterCache[drugItemCode] == null)
            {
                FullCacheHK.DrugMasterCache.Register(drugItemCode, new DrugMasterCache
                {
                    DrugMdsPropertyHqReq = getDrugMdsPropertyHqReq,
                    DrugMdsPropertyHqRes = getDrugMdsPropertyHqRes,
                    DrugPreparationReq = getPreparationReq,
                    DrugPreparationRes = getPreparationRes
                });

            }
            string drugName = drugProperty.Displayname;
            mdsResult = CheckDrugClass(patientEnquiry, alertProfileRes, getDrugMdsPropertyHqRes, getPreparationRes, ref drugName);

            return GenerateFinalResultFromMDSResult(mdsResult, drugName);
        }
        private MdsCheckFinalResult GenerateFinalResultFromMDSResult(MDSCheckResult mdsResult, string drugName)
        {
            MdsCheckFinalResult resultForShow = new MdsCheckFinalResult();
            resultForShow.DrugName = drugName;
            #region system error
            if (false == string.IsNullOrEmpty(mdsResult.errorDesc))
            {
                resultForShow.SystemErrorMessage += mdsResult.errorDesc + Environment.NewLine;
            }
            if (mdsResult.allergyError != null && mdsResult.allergyError.hasAllergyError)
            {
                resultForShow.SystemErrorMessage += mdsResult.allergyError.errorDesc + "\r\n" +
                    (string.IsNullOrEmpty(mdsResult.allergyError.errorCause) ? string.Empty : mdsResult.allergyError.errorCause + "\r\n") +
                    (string.IsNullOrEmpty(mdsResult.allergyError.errorAction) ? string.Empty : mdsResult.allergyError.errorAction) +
                    Environment.NewLine;
            }
            if (mdsResult.adrError != null && mdsResult.adrError.hasAdrError)
            {
                resultForShow.SystemErrorMessage += mdsResult.adrError.errorDesc + "\r\n" +
                    (string.IsNullOrEmpty(mdsResult.adrError.errorCause) ? string.Empty : mdsResult.adrError.errorCause + "\r\n") +
                    (string.IsNullOrEmpty(mdsResult.adrError.errorAction) ? string.Empty : mdsResult.adrError.errorAction + "\r\n") +
                    Environment.NewLine;
            }
            if (mdsResult.drugError != null && mdsResult.drugError.hasDrugError)
            {
                resultForShow.SystemErrorMessage += mdsResult.drugError.errorDesc + "\r\n" +
                    (string.IsNullOrEmpty(mdsResult.drugError.errorCause) ? string.Empty : mdsResult.drugError.errorCause + "\r\n") +
                    (string.IsNullOrEmpty(mdsResult.drugError.errorAction) ? string.Empty : mdsResult.drugError.errorAction + "\r\n") +
                    Environment.NewLine;
            }
            #endregion

            #region allergy alert message
            if (mdsResult.drugAllergyCheckingResults !=null && mdsResult.drugAllergyCheckingResults.hasDrugAllergyAlert)
            {
                resultForShow.HasMdsAlert = true;
                foreach (var allergyMsg in mdsResult.drugAllergyCheckingResults.drugAllergyAlerts)
                {
                    /*
                    1 Clinical Manifestations will be delimited by semicolon ";", if the last clinical manifestation is equal to additional information, system should hide the additional information field.
                    2 System should reconstruct the alert message if the following keyword is found in the message:
                    Sequence of keyword matching	If the following keywords found in [drugAllergyAlertMessages]	Replace the message by the following message
                    1	an allergic/a cross-sensitivity	Use of uppercase[drugDdimDisplayName from 2.5.1] may result in allergic/cross-sensitivity reaction.
                    2	an idiosyncratic	Use of uppercase[drugDdimDisplayName from 2.5.1] may result in idiosyncratic reaction.
                    3	an allergic	Use of uppercase[drugDdimDisplayName from 2.5.1] may result in allergic reaction.
                    4	a cross-sensitivity	Use of uppercase[drugDdimDisplayName from 2.5.1] may result in cross-sensitivity reaction.                     
                     */
                    if (allergyMsg.drugAllergyAlertMessage.Contains("an allergic/a cross-sensitivity"))
                    {
                        allergyMsg.drugAllergyAlertMessage = drugName + " may result in allergic/cross-sensitivity reaction.";
                    }
                    else if (allergyMsg.drugAllergyAlertMessage.Contains("an idiosyncratic"))
                    {
                        allergyMsg.drugAllergyAlertMessage = drugName + " may result in idiosyncratic reaction.";
                    }
                    else if (allergyMsg.drugAllergyAlertMessage.Contains("an allergic"))
                    {
                        allergyMsg.drugAllergyAlertMessage = drugName + " may result in allergic reaction.";
                    }
                    else if (allergyMsg.drugAllergyAlertMessage.Contains("a cross-sensitivity"))
                    {
                        allergyMsg.drugAllergyAlertMessage = drugName + " may result in cross-sensitivity reaction.";
                    }

                    if (allergyMsg.manifestation.EndsWith(";")) allergyMsg.manifestation.TrimEnd(new char[] { ';' });

                    resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("Allergy Checking",
                            allergyMsg.allergen + " - Allergy history reported" + "\r\n" +
                            "Clinical Manifestation: " + allergyMsg.manifestation + "\r\n" +
                            "Additional Information: " + allergyMsg.remark + "\r\n" +
                            "Level of Certainty: " + allergyMsg.certainty + "\r\n" +
                            allergyMsg.drugAllergyAlertMessage));
                }
            }
            #endregion

            #region ddcm alert (G6PD alert)
            if (mdsResult.ddcmCheckingResults != null && mdsResult.ddcmCheckingResults.hasDdcmAlert)
            {
                foreach (string ddcmAlert in mdsResult.ddcmCheckingResults.ddcmAlertMessages)
                {
                    resultForShow.MdsCheckAlertDetails.Add(
                        new MdsCheckAlert(
                                            "G6PD Deficiency Contraindication Checking",
                                            ddcmAlert
                                         )
                        );
                }
            }
            #endregion

            #region adr alert
            if (mdsResult.drugAdrCheckingResults != null && mdsResult.drugAdrCheckingResults.hasDrugAdrAlert)
            {
                foreach (DrugAdrAlert adrAlert in mdsResult.drugAdrCheckingResults.drugAdrAlerts)
                {
                    if (adrAlert.reaction.EndsWith(";")) adrAlert.reaction.TrimEnd(new char[] { ';' });

                    if (adrAlert.drugAdrAlertMessage.Contains("an allergic/a cross-sensitivity"))
                    {
                        adrAlert.drugAdrAlertMessage = drugName + " may result in adverse drug/cross-sensitivity reaction.";
                    }
                    else if (adrAlert.drugAdrAlertMessage.Contains("an idiosyncratic"))
                    {
                        adrAlert.drugAdrAlertMessage = drugName + " may result in idiosyncratic reaction.";
                    }
                    else if (adrAlert.drugAdrAlertMessage.Contains("an allergic"))
                    {
                        adrAlert.drugAdrAlertMessage = drugName + " may result in adverse drug reaction.";
                    }
                    else if (adrAlert.drugAdrAlertMessage.Contains("a cross-sensitivity"))
                    {
                        adrAlert.drugAdrAlertMessage = drugName + " may result in cross-sensitivity reaction.";
                    }
                    resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("Adverse Drug Reaction Checking",
                        drugName + " - Adverse drug reaction history reported" + "\r\n" +
                        "Adverse Drug Reaction: " + adrAlert.reaction + "\r\n" +
                        "Additional Information: " + adrAlert.remark + "\r\n" +
                        "Level of Severity: " + adrAlert.severity + "\r\n" +
                        adrAlert.drugAdrAlertMessage
                        ));
                }
            }
            #endregion

            return resultForShow;
        }
        
        private MDSCheckResult CheckDrugClass(PatientDemoEnquiry patientEnquiry,
            AlertProfileResult alertProfileRes,
            GetDrugMdsPropertyHqResponse getDrugMdsPropertyHqRes,
            GetPreparationResponse getPreparationRes,
            ref string drugName)
        {
            var patientCache = FullCacheHK.PataientCache[patientEnquiry.CaseList[0].Number.Trim().ToUpper()];

            MDSCheckCacheResult medResultCache;

            MDSCheckResult mdsResult = new MDSCheckResult();
            bool skipMDS = false;

            var mdsInput = new MDSCheckInputParm {

                HasG6pdDeficiency = false,

                PatientInfo = CreateMDSPatientInfo(patientEnquiry),

                UserInfo = CreateMDSUserInfo(patientEnquiry),

                PatientAllergyProfile = CreateMDSAllergy(alertProfileRes),

                PatientAdrProfile = CreateMDSAdr(alertProfileRes),

                CurrentRxDrugProfile  = CreateMDSRxDrugProfileIfSkipMDS(getDrugMdsPropertyHqRes, getPreparationRes, ref skipMDS)
            };

            if(skipMDS)
            {
                //in some condition, this
                mdsResult.hasMdsAlert = false;
                drugName = mdsInput.CurrentRxDrugProfile.DrugDdimDisplayName.ToUpper();
                medResultCache = new MDSCheckCacheResult
                {
                    Req = mdsInput,
                    Res = mdsResult
                };
                patientCache.MDSCheck = medResultCache;
                return mdsResult;
            }
            //show at msg title: CAUTION for + uppercase[drugDdimDisplayName from 2.5.1]
            drugName = mdsInput.CurrentRxDrugProfile.DrugDdimDisplayName.ToUpper();

            /*if alertProfile from 1.4.2 = G6PD, then “true”, 
              else “false”
            */
            mdsInput.HasG6pdDeficiency = checkMDS.CheckIsG6PD(alertProfileRes.AlertProfile);
            mdsInput.HasPregnancy = false;

            mdsInput.CheckDdim = false;
            /*if  hasG6pdDeficiency is true or  hasPregnancy is true, then “true”, else “false”*/
            mdsInput.CheckDdcm = mdsInput.HasG6pdDeficiency;//|| mdsInput.HasPregnancy;
            //“true” for Allergy checking
            mdsInput.CheckDam = alertProfileRes.AllergyProfile.Count() > 0;
            //“true” for ADR checking
            mdsInput.CheckAdr = alertProfileRes.AdrProfile.Count() > 0;
            mdsInput.CheckDscm = false;
            mdsInput.CheckDrcm = false;
            mdsInput.CheckDlcm = false;
            mdsInput.CheckSteroid = false;
            mdsInput.CheckDiscon = false;
            mdsInput.CheckHepaB = false;
            mdsInput.CallerSourceSystem = "PMS";

            if (false == mdsInput.CheckDdcm && false == mdsInput.CheckDam && false == mdsInput.CheckAdr)
            {
                //if no ddcm, no allergy, no adr, then no need do MDS check
                mdsResult.hasMdsAlert = false;
            }
            else if(checkMDS.CheckDrugMasterResultForMDSCheck(getDrugMdsPropertyHqRes.Return[0].DrugMds, getPreparationRes.Return.PmsFmStatus, mdsInput.CurrentRxDrugProfile.DrugErrorDisplayName, ref mdsResult))
            {
                mdsResult = restSvc.CheckMDS(mdsInput);
            }
            medResultCache = new MDSCheckCacheResult
            {
                Req = mdsInput,
                Res = mdsResult
            };
            patientCache.MDSCheck = medResultCache;

            FinalCheckForMDSResult(ref mdsResult);
            return mdsResult;

        }
        /// <summary>
        /// if moeCheckFlag =="N" && GroupMoeCheckFlag =="N", skip MDS check
        /// </summary>
        /// <param name="getDrugMdsPropertyHqRes"></param>
        /// <param name="getPreparationRes"></param>
        /// <param name="mdsInput"></param>
        /// <returns></returns>
        private CurrentRxDrugProfile CreateMDSRxDrugProfileIfSkipMDS(GetDrugMdsPropertyHqResponse getDrugMdsPropertyHqRes, GetPreparationResponse getPreparationRes,ref bool skipMDS)
        {
            skipMDS = false;
            var currentRxDrugProfile = new CurrentRxDrugProfile
            {
                IsCapdItem = "false",
                //Initialize in the comming 
                GcnSeqNo = 0,
                RDfGenId = 0,
                RGenId = 0,
                Type = "",

                TrueDisplayName = getPreparationRes.Return.TrueDisplayname,
                //Initialize in the comming
                DrugDisplayName = getPreparationRes.Return.TrueDisplayname + " " + getPreparationRes.Return.SaltProperty,
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
                skipMDS = true;
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
            currentRxDrugProfile.DrugErrorDisplayName =
                        GetDrugErrorDisplayName(currentRxDrugProfile.DrugDdimDisplayName,
                                                getPreparationRes.Return.Strength,
                                                getPreparationRes.Return.VolumeValue,
                                                getPreparationRes.Return.VolumeUnit);


            return currentRxDrugProfile;
        }

        private List<PatientAdrProfile> CreateMDSAdr(AlertProfileResult alertProfileRes)
        {
            List<PatientAdrProfile> patientAdrProfiles = new List<PatientAdrProfile>();
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

                patientAdrProfiles.Add(patientAdrProfile);

            }

            return patientAdrProfiles;
        }

        private List<PatientAllergyProfile> CreateMDSAllergy(AlertProfileResult alertProfileRes)
        {
            List<PatientAllergyProfile> patientAllergyProfiles = new List<PatientAllergyProfile>();
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

                patientAllergyProfiles.Add(patientAllergyProfile);
            }
            return patientAllergyProfiles;
        }

        private MDSCheck_UserInfo CreateMDSUserInfo(PatientDemoEnquiry patientEnquiry)
        {
            return new MDSCheck_UserInfo
            {
                HospCode = HospitalCode,
                PharSpec = patientEnquiry.CaseList[0].Specialty,
                WrkStnID = UtilityHelper.GetLoalIPAddress(),
                WrkStnType = "I"
            };
        }

        private MDSCheck_PatientInfo CreateMDSPatientInfo(PatientDemoEnquiry patientEnquiry)
        {
            return new MDSCheck_PatientInfo
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
        }

        private string GetDrugErrorDisplayName(string ddimDisplayName, string strength, string volumeValue, string volumeunit)
        {
            string drugErrorDisplayName = ddimDisplayName;
            drugErrorDisplayName += string.IsNullOrEmpty(strength) ? string.Empty : " " + strength.ToLower();
            if (!string.IsNullOrEmpty(volumeValue))
            {
                decimal volumeValueDecimal = 0;
                decimal.TryParse(volumeValue,out volumeValueDecimal);
                if (volumeValueDecimal > 0)
                {
                    drugErrorDisplayName += " " + FormatVolumeValue(volumeValueDecimal) + volumeunit.ToLower();
                }
            }
            return drugErrorDisplayName;
        }
        /// <summary>
        /// System should ignore the following MDS alerts when suppress flag is true:
        ///Drug allergy alert
        ///G6PD deficiency contraindication alert
        ///ignore the G6PD deficiency contraindication alert when the severityLevelCode  is 2 or 3
        /// </summary>
        /// <param name="mdsResult"></param>
        private void FinalCheckForMDSResult(ref MDSCheckResult mdsResult)
        {
            var mdsResultForCheck = mdsResult;
            if (mdsResultForCheck.drugAllergyCheckingResults != null && mdsResultForCheck.drugAllergyCheckingResults.hasDrugAllergyAlert)
            {
                for(int a=(mdsResultForCheck.drugAllergyCheckingResults.drugAllergyAlerts.Count() - 1); a >= 0; a--)
                {
                    //System should ignore the allergy alerts when suppress flag is true
                    if (mdsResultForCheck.drugAllergyCheckingResults.drugAllergyAlerts[a].suppress == true)
                    {
                        mdsResult.drugAllergyCheckingResults.drugAllergyAlerts.Remove(mdsResultForCheck.drugAllergyCheckingResults.drugAllergyAlerts[a]);
                    }
                }
            }

            if (mdsResultForCheck.ddcmCheckingResults != null && mdsResultForCheck.ddcmCheckingResults.hasDdcmAlert && mdsResultForCheck.ddcmCheckingResults.hasG6PdDeficiencyAlert)
            {
                for(int d=(mdsResultForCheck.ddcmCheckingResults.ddcmAlerts.Count() - 1);d>=0;d--)
                {
                    //System should ignore the G6PD alerts when suppress flag is true
                    //System should ignore the G6PD deficiency contraindication alert when the severityLevelCode is 2 or 3
                    if (mdsResultForCheck.ddcmCheckingResults.ddcmAlerts[d].suppress = true || (new string[] { "2", "3" }.Contains(mdsResultForCheck.ddcmCheckingResults.ddcmAlerts[d].severityLevelCode)))
                    {
                        mdsResult.ddcmCheckingResults.ddcmAlerts.Remove(mdsResultForCheck.ddcmCheckingResults.ddcmAlerts[d]);
                    }
                }
            }
        }


        private void CheckItemCodeofMedicationProfile(MedicationProfileResult orders, ref string errorMessage)
        {
            //CHECK ITEM CODES
        }

        private static bool IsInvalidAccessCode(AlertProfileResult actualProfile)
        {
            return actualProfile.ErrorMessage.Count > 0 && "20083".Equals(actualProfile.ErrorMessage[0].MsgCode);
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
