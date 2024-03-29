﻿using AutoMapper;
using Demo.HL7MessageParser.DTOs;
using Demo.HL7MessageParser.Model;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public static class ObjectAutoMapping
    {
        static Dictionary<string, string> AdrDict;
        static Dictionary<string, string> AllergyDict;

        static ObjectAutoMapping()
        {
            Mapper.CreateMap<PatientDemoEnquiry, PatientObj>()
                            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(o => o.Patient.Name))
                            .ForMember(dest => dest.LastName, opt => opt.MapFrom(o => o.Patient.Name))
                            .ForMember(dest => dest.MiddleInitial, opt => opt.Ignore())
                            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(o => DateTime.ParseExact(o.Patient.DOB, "yyyyMMdd", CultureInfo.InvariantCulture)))
                            .ForMember(dest => dest.Gender, opt => opt.MapFrom(o => o.Patient.Sex))
                            .ForMember(dest => dest.Height, opt => opt.Ignore())
                            .ForMember(dest => dest.Weight, opt => opt.Ignore())
                            .ForMember(dest => dest.Alias, opt => opt.Ignore())
                            .ForMember(dest => dest.MedicalRecordNumber, opt => opt.MapFrom(o => o.Patient.HKID))
                            .ForMember(dest => dest.Physician, opt => opt.Ignore())
                            .ForMember(dest => dest.PatientType, opt => opt.Ignore())  // need to be ignore, data type cause failure with AutoMapper.
                            .ForMember(dest => dest.PatientFacilityID, opt => opt.Ignore())
                            .ForMember(dest => dest.MDSiteName, opt => opt.Ignore());

            Mapper.CreateMap<Case, VisitObj>()
                  .ForMember(dest => dest.PatientVisitId, opt => opt.Ignore())
                  .ForMember(dest => dest.PatientID, opt => opt.Ignore())
                  // wardCode -- wardID need to get from DB by searching.
                  // .ForMember(dest => dest.CareUnitId, opt => opt.MapFrom(o => int.Parse(o.WardCode)))
                  .ForMember(dest => dest.AdmitDate, opt => opt.MapFrom(o => DateTime.ParseExact(o.AdmissionDatetime, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture)))
                  .ForMember(dest => dest.DischargeDate, opt => opt.Ignore())
                  .ForMember(dest => dest.Room, opt => opt.Ignore())
                  .ForMember(dest => dest.Bed, opt => opt.MapFrom(o => o.BedNo))
                  .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
                  .ForMember(dest => dest.Barcode, opt => opt.Ignore());


            AdrDict = new Dictionary<string, string>
            {
                {"an allergic/a cross-sensitivity",  " may result in adverse drug/cross-sensitivity reaction."},
                {"an idiosyncratic", " may result in idiosyncratic reaction."},
                {"an allergic", " may result in adverse drug reaction."},
                {"a cross-sensitivity", " may result in cross-sensitivity reaction."},
            };

            AllergyDict = new Dictionary<string, string>
            {
                {"an allergic/a cross-sensitivity",  " may result in allergic/cross-sensitivity reaction."},
                {"an idiosyncratic", " may result in idiosyncratic reaction."},
                {"an allergic", " may result in allergic reaction."},
                {"a cross-sensitivity", " may result in cross-sensitivity reaction."},
             };
        }
        public static Order ToConvert(this MedProfileMoItem mp)
        {
            return new Order();
        }

        public static IEnumerable<Order> ToConvert(this IEnumerable<MedProfileMoItem> mps)
        {
            return new List<Order>();
        }

        public static PatientVisit ToConvert(this PatientDemoEnquiry pr)
        {
            PatientVisit pv = new PatientVisit();
            PatientObj p = Mapper.Map<PatientDemoEnquiry, PatientObj>(pr);

            pv.Pateint = p;

            foreach (var item in pr.CaseList)
            {
                VisitObj v = Mapper.Map<Case, VisitObj>(item);
                v.PatientID = p.PatientId;

                pv.Vists.Add(v);
            }

            return pv;
        }
        public static IEnumerable<PatientAllergyObj> ToConvert(this AlertProfileResult apr)
        {
            return new List<PatientAllergyObj>();
        }
        /// <summary>
        /// to generate final MDS check message
        /// </summary>
        /// <param name="mdsResult"></param>
        /// <param name="drugName"></param>
        /// <returns></returns>
        public static MdsCheckFinalResult ToConvert(this MDSCheckResult mdsResult, string drugName)
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
                resultForShow.SystemErrorMessage += ResultBuilder(resultForShow, mdsResult.allergyError);
            }
            if (mdsResult.adrError != null && mdsResult.adrError.hasAdrError)
            {
                resultForShow.SystemErrorMessage += ResultBuilder(resultForShow, mdsResult.adrError);
            }
            if (mdsResult.drugError != null && mdsResult.drugError.hasDrugError)
            {
                resultForShow.SystemErrorMessage += ResultBuilder(resultForShow, mdsResult.drugError);
            }
            #endregion

            #region allergy alert message
            if (mdsResult.drugAllergyCheckingResults != null && mdsResult.drugAllergyCheckingResults.hasDrugAllergyAlert)
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

                    allergyMsg.drugAllergyAlertMessage = WrapperAllergyCheckResultMessage(drugName, allergyMsg.drugAllergyAlertMessage);

                    allergyMsg.manifestation.TrimEnd(new char[] { ';' });

                    StringBuilder sbuilder = new StringBuilder();
                    sbuilder.AppendLine(string.Format("{0} - Allergy history reported", allergyMsg.allergen));
                    sbuilder.AppendLine(string.Format("Clinical Manifestation: {0}", allergyMsg.manifestation));
                    sbuilder.AppendLine(string.Format("Additional Information: {0}", allergyMsg.remark));
                    sbuilder.AppendLine(string.Format("Level of Certainty: {0}", allergyMsg.certainty));
                    sbuilder.AppendLine(allergyMsg.drugAllergyAlertMessage);

                 //   resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("Allergy Checking", sbuilder.ToString()));
                }
            }
            #endregion

            #region ddcm alert (G6PD alert)
            if (mdsResult.ddcmCheckingResults != null && mdsResult.ddcmCheckingResults.hasDdcmAlert)
            {
                foreach (string ddcmAlert in mdsResult.ddcmCheckingResults.ddcmAlertMessages)
                {
                  //  resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("G6PD Deficiency Contraindication Checking", ddcmAlert));
                }
            }
            #endregion

            #region adr alert
            if (mdsResult.drugAdrCheckingResults != null && mdsResult.drugAdrCheckingResults.hasDrugAdrAlert)
            {
                foreach (DrugAdrAlert adrAlert in mdsResult.drugAdrCheckingResults.drugAdrAlerts)
                {
                    adrAlert.reaction.TrimEnd(new char[] { ';' });

                    adrAlert.drugAdrAlertMessage = WrapperAdrCheckResultAlertMessage(drugName, adrAlert.drugAdrAlertMessage);

                    StringBuilder sbuilder = new StringBuilder();
                    sbuilder.AppendLine(string.Format("{0} - Adverse drug reaction history reported", drugName));
                    sbuilder.AppendLine(string.Format("Adverse Drug Reaction: {0}", adrAlert.reaction));
                    sbuilder.AppendLine(string.Format("Additional Information: {0}", adrAlert.remark));
                    sbuilder.AppendLine(string.Format("Level of Certainty: {0}", adrAlert.severity));
                    sbuilder.AppendLine(adrAlert.drugAdrAlertMessage);

                  //  resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("Adverse Drug Reaction Checking", sbuilder.ToString()));
                }
            }
            #endregion

            return resultForShow;
        }
        /// <summary>
        /// 2.5.3 5 System should ignore the following MDS alerts when suppress flag is true:
        ///Drug allergy alert
        ///G6PD deficiency contraindication alert
        ///ignore the G6PD deficiency contraindication alert when the severityLevelCode  is 2 or 3
        /// </summary>
        /// <param name="mdsResult"></param>
        public static void FilterMdsResult(this MDSCheckResult mdsResult)
        {
            var mdsResultForCheck = mdsResult;
            if (mdsResultForCheck.drugAllergyCheckingResults != null && mdsResultForCheck.drugAllergyCheckingResults.hasDrugAllergyAlert)
            {
                for (int a = (mdsResultForCheck.drugAllergyCheckingResults.drugAllergyAlerts.Count() - 1); a >= 0; a--)
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
                for (int d = (mdsResultForCheck.ddcmCheckingResults.ddcmAlerts.Count() - 1); d >= 0; d--)
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
     
        private static string ResultBuilder(MdsCheckFinalResult result, ErrorBase error)
        {
            StringBuilder sBuilder = new StringBuilder();

            sBuilder.AppendLine(error.errorDesc);
            sBuilder.AppendLine(error.errorCause.IsNullOrWhiteSpace() ? string.Empty : error.errorCause);
            sBuilder.AppendLine(error.errorAction.IsNullOrWhiteSpace() ? string.Empty : error.errorAction);
            sBuilder.AppendLine();

            return sBuilder.ToString();
        }
        private static string WrapperAdrCheckResultAlertMessage(string drugName, string sourceMessage)
        {
            foreach (var item in AdrDict)
            {
                if (sourceMessage.Contains(item.Key))
                {
                    return drugName + item.Value;
                }
            }

            return sourceMessage;
        }
        private static string WrapperAllergyCheckResultMessage(string drugName, string sourceMessage)
        {
            foreach (var item in AllergyDict)
            {
                if (sourceMessage.Contains(item.Key))
                {
                    return drugName + item.Value;
                }
            }

            return sourceMessage;
        }
    }
}
