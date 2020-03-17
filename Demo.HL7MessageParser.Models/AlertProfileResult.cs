using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser.Models
{
    public class AlertProfileResult
    {
        public AlertProfileResult()
        {
            ErrorMessage = new List<ErrorMessage>();
            AdrProfile = new List<AdrProfile>();
            AlertProfile = new List<AlertProfile>();
            AllergyProfile = new List<AllergyProfile>();
        }
        public List<AdrProfile> AdrProfile { get; set; }
        public List<AlertProfile> AlertProfile { get; set; }
        public List<AllergyProfile> AllergyProfile { get; set; }
        public List<ErrorMessage> ErrorMessage { get; set; }
        public object MoePatientSteroidTag { get; set; }
        public List<SimpleDisplayFormat> SimpleDisplayFormat { get; set; }
    }

    public class AdrProfile
    {
        public AdrProfile()
        {
            HicSeqno = new List<string>();
        }
        public string AdrSeqNo { get; set; }
        public string AliasName { get; set; }
        public string DisplayName { get; set; }
        public string Drug { get; set; }
        public string DrugType { get; set; }
        public List<string> HicSeqno { get; set; }
        public string HiclSeqno { get; set; }
        public string NameType { get; set; }
        public string Reaction { get; set; }
        public string Remark { get; set; }
        public string Salt { get; set; }
        public string Severity { get; set; }
        public string SeverityCode { get; set; }
        public string SourceSystem { get; set; }
        public string TextColor { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDtm { get; set; }
        public string UpdateHospital { get; set; }
        public string UpdateRankDesc { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateUserRank { get; set; }
    }

    public class AlertProfile
    {
        public string AlertCode { get; set; }
        public string AlertDesc { get; set; }
        public string AlertSeqNo { get; set; }
        public string ClassifiedIndicator { get; set; }
        public string Remark { get; set; }
        public string TextColor { get; set; }
        public string UpdateDtm { get; set; }
        public string UpdateHospital { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateUserRank { get; set; }
        public string ValidFrom { get; set; }
        public string ValidPeriod { get; set; }
        public string ValidTo { get; set; }
    }

    public class AllergyProfile
    {
        public AllergyProfile()
        {
            HicSeqno = new List<string>();
        }
        public string AliasName { get; set; }
        public string Allergen { get; set; }
        public string AllergenType { get; set; }
        public string AllergySeqNo { get; set; }
        public string Certainty { get; set; }
        public string CertaintyCode { get; set; }
        public string DisplayName { get; set; }
        public List<string> HicSeqno { get; set; }
        public string HiclSeqno { get; set; }
        public string Manifestation { get; set; }
        public string NameType { get; set; }
        public string Remark { get; set; }
        public string Salt { get; set; }
        public string SourceSystem { get; set; }
        public string TextColor { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDtm { get; set; }
        public string UpdateHospital { get; set; }
        public string UpdateRankDesc { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateUserRank { get; set; }
        public string CreateDtm { get; set; }
        public string AllergyCode { get; set; }
    }

    public class SimpleDisplayFormat
    {
        public string AdrRecord { get; set; }
        public string AlertRecord { get; set; }
        public string AllergyRecord { get; set; }
    }
    public class ErrorMessage
    {
        public string MsgCode { get; set; }
        public string MsgText { get; set; }
        public string StandardMsg { get; set; }
    }

}
/*
 * {
    "adrProfile": [
        {
            "adrSeqNo": "0000004366",
            "aliasName": "",
            "displayName": "ASPIRIN",
            "drug": "ASPIRIN",
            "drugType": "D",
            "hicSeqno": [],
            "hiclSeqno": "1820",
            "nameType": "D",
            "reaction": "Abdominal Pain with Cramps; Others",
            "remark": "~!@#$%^&*()_+`-=][\\}{|;':\",./>?<     \n1234567890\n<RRR\n<?\n< ?\n<\n<RRR>\n<>\nqwertyuiopasdfghjklzxcvbnm\nQWERTYUIOPASDFGHJKLZXCVBNM",
            "salt": "",
            "severity": "Severe",
            "severityCode": "S",
            "sourceSystem": "CMS3",
            "textColor": "#000000",
            "updateBy": "Y2KMED",
            "updateDtm": "27-02-2019 14:30:28",
            "updateHospital": "VH",
            "updateRankDesc": "Analyst Programmer I",
            "updateUser": "AA, FORMAT in IP",
            "updateUserRank": "Analyst Programmer I"
        }
    ],
    "alertProfile": [
        {
            "alertCode": "A0001",
            "alertDesc": "G6PD Deficiency",
            "alertSeqNo": "0000045393",
            "classifiedIndicator": "N",
            "remark": "~!@#$%^&*()_+`-=][}{|;':\",./>?<     \n1234567890\n<RRR\n<?\n< ?\n<\n<RRR>\n<>\nqwertyuiopasdfghjklzxcvbnm\nQWERTYUIOPASDFGHJKLZXCVBNM",
            "textColor": "#000000",
            "updateDtm": "27-02-2019 15:47:07",
            "updateHospital": "VH",
            "updateUser": "AA, FORMAT in IP",
            "updateUserRank": "Analyst Programmer I",
            "validFrom": "",
            "validPeriod": "",
            "validTo": ""
        }
    ],
    "allergyProfile": [
        {
            "aliasName": "",
            "allergen": "METHYLPREDNISOLONE ACEPONATE",
            "allergenType": "D",
            "allergySeqNo": "0000090020",
            "certainty": "Certain",
            "certaintyCode": "C",
            "displayName": "METHYLPREDNISOLONE",
            "hicSeqno": [],
            "hiclSeqno": "12057",
            "manifestation": "Allergic rhinitis; Asthma; Rash",
            "nameType": "D",
            "remark": "Asthma",
            "salt": "ACEPONATE",
            "sourceSystem": "CMS3",
            "textColor": "#000000",
            "updateBy": "Y2KMED",
            "updateDtm": "16-08-2019 17:27:01",
            "updateHospital": "VH",
            "updateRankDesc": "Department Manager (Pharmacy)",
            "updateUser": "AA, FORMAT in IP",
            "updateUserRank": "Department Manager (Pharmacy)"
        }
    ],
    "errorMessage": [],
    "moePatientSteroidTag": null,
    "simpleDisplayFormat": [
        {
            "adrRecord": "1. ASPIRIN",
            "alertRecord": "1. G6PD Deficiency",
            "allergyRecord": "1. METHYLPREDNISOLONE ACEPONATE; 2. ASPIRIN; 3. FEDAC CPD LINCTUS [ACTIFED COMPOUND]; 4. BELZER UW-CSS"
        }
    ]
}
 */
