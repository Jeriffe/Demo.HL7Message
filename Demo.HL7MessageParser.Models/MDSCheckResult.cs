using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser.Models
{
    public class MdsCheckFinalResult
    {
        public string DrugName = string.Empty;

        public string SystemErrorMessage { get; set; }

        public bool HasMdsAlert { get; set; }

        public List<MdsCheckAlert> MdsCheckAlertDetails;

        public MdsCheckFinalResult()
        {
            MdsCheckAlertDetails = new List<MdsCheckAlert>();
        }
    }

    public class MdsCheckAlert
    {
        public string CategoryName;
        public string CheckAlertMessage;

        public MdsCheckAlert(string name, string detail)
        {
            CategoryName = name;
            CheckAlertMessage = detail;
        }
    }
    public class MDSCheckResult
    {
        public AdrError adrError { get; set; }
        public AllergyError allergyError { get; set; }
        public DdcmCheckingResults ddcmCheckingResults { get; set; }
        public DdimCheckingResults ddimCheckingResults { get; set; }
        public object disconCheckingResults { get; set; }
        public object dlcmCheckingResults { get; set; }
        public object drcmCheckingResults { get; set; }
        public DrugAdrCheckingResults drugAdrCheckingResults { get; set; }
        public DrugAllergyCheckingResults drugAllergyCheckingResults { get; set; }
        public DrugError drugError { get; set; }
        public object dscmCheckingResults { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public bool hasMdsAlert { get; set; }
        public object hepaBCheckingResults { get; set; }
        public object steroidCheckingResults { get; set; }
        public object tenoCheckingResults { get; set; }
    }
    public class AdrError : ErrorBase
    {
        public bool hasAdrError { get; set; }
    }

    public class AllergyError: ErrorBase
    {
        public bool hasAllergyError { get; set; }
    }


    public class DrugError: ErrorBase
    {
        public bool hasDrugError { get; set; }
    }

    public class ErrorBase
    {
        public string errorAction { get; set; }
        public string errorCause { get; set; }
        public string errorDesc { get; set; }
        public List<ErrorDetail> errorDetails { get; set; }
    }

    public class DdcmCheckingResults
    {
 
        public List<string> ddcmAlertMessages { get; set; }
        public List<DdcmAlert> ddcmAlerts { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public bool hasDdcmAlert { get; set; }
        public bool hasG6PdDeficiencyAlert { get; set; }
        public bool hasPregnancyAlert { get; set; }
        public int messageCount { get; set; }
    }
    public class DdcmAlert
    {
        public string conditionId { get; set; }
        public int conditionType { get; set; }
        public string ddcmAlertMessage { get; set; }
        public string descriptionDisease { get; set; }
        public string diseaseType { get; set; }
        public string drugCustomId { get; set; }
        public string drugName { get; set; }
        public string hitConditionDescription { get; set; }
        public string hitConditionId { get; set; }
        public int indexRow { get; set; }
        public string severityLevelCode { get; set; }
        public bool suppress { get; set; }
    }

    public class DdimCheckingResults
    {
        public List<string> ddimAlertMessages { get; set; }
        public List<object> ddimAlerts { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public bool hasDdimAlert { get; set; }
        public int messageCount { get; set; }
    }


    public class DrugAdrCheckingResults
    {
        public List<string> drugAdrAlertMessages { get; set; }
        public List<DrugAdrAlert> drugAdrAlerts { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public bool hasDrugAdrAlert { get; set; }
        public int messageCount { get; set; }
    }
    public class DrugAdrAlert
    {
        public string adr { get; set; }
        public string adrCustomId { get; set; }
        public string drugAdrAlertMessage { get; set; }
        public string drugCustomId { get; set; }
        public string drugName { get; set; }
        public int indexRow { get; set; }
        public string matchType { get; set; }
        public string reaction { get; set; }
        public string remark { get; set; }
        /// <summary>
        /// 2.System should not perform MDS checking on an ADR record if its severity is “Mild”.
        /// </summary>
        public string severity { get; set; }
    }

    public class DrugAllergyCheckingResults
    {
        public List<string> drugAllergyAlertMessages { get; set; }
        public List<DrugAllergyAlert> drugAllergyAlerts { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public bool hasDrugAllergyAlert { get; set; }
        public int messageCount { get; set; }
    }
    public class DrugAllergyAlert
    {
        public string allergen { get; set; }
        public string allergenCustomId { get; set; }
        public string certainty { get; set; }
        public string drugAllergyAlertMessage { get; set; }
        public string drugCustomId { get; set; }
        public string drugName { get; set; }
        public int indexRow { get; set; }
        public string manifestation { get; set; }
        public string matchType { get; set; }
        public string remark { get; set; }
        public bool suppress { get; set; }
    }


    public class ErrorDetail
    {
        public string code { get; set; }
        public string customId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}