using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public class MDSCreater
    {
        /// <summary>
        /// if moeCheckFlag =="N" && GroupMoeCheckFlag =="N", skip MDS check
        /// </summary>
        /// <param name="getDrugMdsPropertyHqRes"></param>
        /// <param name="getPreparationRes"></param>
        /// <param name="mdsInput"></param>
        /// <returns></returns>
        private CurrentRxDrugProfile CreateMDSRxDrugProfileIfSkipMDS(GetDrugMdsPropertyHqResponse getDrugMdsPropertyHqRes, GetPreparationResponse getPreparationRes, string hospitalCode)
        {
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
                HospCode = hospitalCode,
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
        private string GetDrugErrorDisplayName(string ddimDisplayName, string strength, string volumeValue, string volumeunit)
        {
            string drugErrorDisplayName = ddimDisplayName;
            drugErrorDisplayName += string.IsNullOrEmpty(strength) ? string.Empty : " " + strength.ToLower();
            if (!string.IsNullOrEmpty(volumeValue))
            {
                decimal volumeValueDecimal = 0;
                decimal.TryParse(volumeValue, out volumeValueDecimal);
                if (volumeValueDecimal > 0)
                {
                    drugErrorDisplayName += " " + FormatVolumeValue(volumeValueDecimal) + volumeunit.ToLower();
                }
            }
            return drugErrorDisplayName;
        }

        private string FormatVolumeValue(decimal volumeValue)
        {
            return string.Format("{0:#######.####}", volumeValue);
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
        private MDSCheck_UserInfo CreateMDSUserInfo(PatientDemoEnquiry patientEnquiry)
        {
            return new MDSCheck_UserInfo
            {
                HospCode = patientEnquiry.CaseList[0].HospitalCode,
                PharSpec = patientEnquiry.CaseList[0].Specialty,
                WrkStnID = UtilityHelper.GetLoalIPAddress(),
                WrkStnType = "I"
            };
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

                if (profile.Manifestation.IsNullOrWhiteSpace())
                {
                    continue;
                }

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

                if (adrProfile.Reaction.IsNullOrWhiteSpace())
                {
                    continue;
                }

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

        public MDSCheckInputParm CreateMDSRequest(PatientDemoEnquiry patientEnquiry,
                            AlertProfileResult alertProfileRes,
                            GetDrugMdsPropertyHqResponse getDrugMdsPropertyHqRes,
                            GetPreparationResponse getPreparationRes,
                            bool checkDdcm,
                            bool checkDam,
                            bool checkAdr,
                            ref string drugName
                            )
        {
            var patientCache = FullCacheHK.PataientCache[patientEnquiry.CaseList[0].Number.Trim().ToUpper()];

            var mdsInput = new MDSCheckInputParm
            {

                HasG6pdDeficiency = false,

                PatientInfo = CreateMDSPatientInfo(patientEnquiry),

                UserInfo = CreateMDSUserInfo(patientEnquiry),

                PatientAllergyProfile = CreateMDSAllergy(alertProfileRes),

                PatientAdrProfile = CreateMDSAdr(alertProfileRes),

                CurrentRxDrugProfile = CreateMDSRxDrugProfileIfSkipMDS(getDrugMdsPropertyHqRes, getPreparationRes, patientEnquiry.CaseList[0].HospitalCode),

                CheckDscm = false,
                CheckDrcm = false,
                CheckDlcm = false,
                CheckSteroid = false,
                CheckDiscon = false,
                CheckHepaB = false,
                HasPregnancy = false,
                CheckDdim = false,
                CallerSourceSystem = "PMS",
            };

            //show at msg title: CAUTION for + uppercase[drugDdimDisplayName from 2.5.1]
            drugName = mdsInput.CurrentRxDrugProfile.DrugDdimDisplayName.ToUpper();

            mdsInput.CheckDdcm = checkDdcm;
            mdsInput.CheckDam = checkDam;
            mdsInput.CheckAdr = checkAdr;

            return mdsInput;
        }
    }
}
