using AutoMapper;
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
                  .ForMember(dest => dest.Barcode, opt => opt.Ignore())
                 // .ForMember(dest => dest.HospitalService, opt => opt.MapFrom(o => o.Number));
                 ;
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
                resultForShow.SystemErrorMessage += mdsResult.allergyError.errorDesc + Environment.NewLine +
                    (string.IsNullOrEmpty(mdsResult.allergyError.errorCause) ? string.Empty : mdsResult.allergyError.errorCause + Environment.NewLine) +
                    (string.IsNullOrEmpty(mdsResult.allergyError.errorAction) ? string.Empty : mdsResult.allergyError.errorAction) +
                    Environment.NewLine;
            }
            if (mdsResult.adrError != null && mdsResult.adrError.hasAdrError)
            {
                resultForShow.SystemErrorMessage += mdsResult.adrError.errorDesc + Environment.NewLine +
                    (string.IsNullOrEmpty(mdsResult.adrError.errorCause) ? string.Empty : mdsResult.adrError.errorCause + Environment.NewLine) +
                    (string.IsNullOrEmpty(mdsResult.adrError.errorAction) ? string.Empty : mdsResult.adrError.errorAction + Environment.NewLine) +
                    Environment.NewLine;
            }
            if (mdsResult.drugError != null && mdsResult.drugError.hasDrugError)
            {
                resultForShow.SystemErrorMessage += mdsResult.drugError.errorDesc + Environment.NewLine +
                    (string.IsNullOrEmpty(mdsResult.drugError.errorCause) ? string.Empty : mdsResult.drugError.errorCause + Environment.NewLine) +
                    (string.IsNullOrEmpty(mdsResult.drugError.errorAction) ? string.Empty : mdsResult.drugError.errorAction + Environment.NewLine) +
                    Environment.NewLine;
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

                    allergyMsg.drugAllergyAlertMessage = AllergyMsg(drugName, allergyMsg.drugAllergyAlertMessage);

                    if (allergyMsg.manifestation.EndsWith(";")) allergyMsg.manifestation.TrimEnd(new char[] { ';' });

                    string line1 = string.Format(allergyMsg.allergen + " - Allergy history reported{0}", Environment.NewLine);
                    string line2 = string.Format("Clinical Manifestation: " + allergyMsg.manifestation + "{0}", Environment.NewLine);
                    string line3 = string.Format("Additional Information: " + allergyMsg.remark + "{0}", Environment.NewLine);
                    string line4 = string.Format("Level of Certainty: " + allergyMsg.certainty + "{0}", Environment.NewLine);

                    resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("Allergy Checking",
                                    line1 + line2 + line3 + line4 + allergyMsg.drugAllergyAlertMessage));
                }
            }
            #endregion

            #region ddcm alert (G6PD alert)
            if (mdsResult.ddcmCheckingResults != null && mdsResult.ddcmCheckingResults.hasDdcmAlert)
            {
                foreach (string ddcmAlert in mdsResult.ddcmCheckingResults.ddcmAlertMessages)
                {
                    resultForShow.MdsCheckAlertDetails.Add(
                        new MdsCheckAlert("G6PD Deficiency Contraindication Checking", ddcmAlert)
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

                    adrAlert.drugAdrAlertMessage = AdrAlertMessage(drugName, adrAlert.drugAdrAlertMessage);

                    string line1 = string.Format(drugName + " - Adverse drug reaction history reported{0}", Environment.NewLine);
                    string line2 = string.Format("Adverse Drug Reaction: " + adrAlert.reaction + "{0}", Environment.NewLine);
                    string line3 = string.Format("Additional Information: " + adrAlert.remark + "{0}", Environment.NewLine);
                    string line4 = string.Format("Level of Severity: " + adrAlert.severity + "{0}", Environment.NewLine);

                    resultForShow.MdsCheckAlertDetails.Add(new MdsCheckAlert("Adverse Drug Reaction Checking",
                        line1 + line2 + line3 + line4 + adrAlert.drugAdrAlertMessage
                        ));
                }
            }
            #endregion

            return resultForShow;
        }

        public static string AllergyMsg(string drugName, string targetMessage)
        {
            if (targetMessage.Contains("an allergic/a cross-sensitivity"))
            {
                return drugName + " may result in allergic/cross-sensitivity reaction.";
            }
            else if (targetMessage.Contains("an idiosyncratic"))
            {
                return drugName + " may result in idiosyncratic reaction.";
            }
            else if (targetMessage.Contains("an allergic"))
            {
                return drugName + " may result in allergic reaction.";
            }
            else if (targetMessage.Contains("a cross-sensitivity"))
            {
                return drugName + " may result in cross-sensitivity reaction.";
            }

            return targetMessage;
        }

        public static string AdrAlertMessage(string drugName, string targetMessage)
        {
            if (targetMessage.Contains("an allergic/a cross-sensitivity"))
            {
                return drugName + " may result in adverse drug/cross-sensitivity reaction.";
            }
            else if (targetMessage.Contains("an idiosyncratic"))
            {
                return drugName + " may result in idiosyncratic reaction.";
            }
            else if (targetMessage.Contains("an allergic"))
            {
                return drugName + " may result in adverse drug reaction.";
            }
            else if (targetMessage.Contains("a cross-sensitivity"))
            {
                return drugName + " may result in cross-sensitivity reaction.";
            }

            return targetMessage;
        }

    }
}
