using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.HL7MessageParser.Models;

namespace Demo.HL7MessageParser.Service
{
    public class CheckBeforeMDSChecker
    {
        public CheckBeforeMDSChecker() { }

        /// <summary>
        /// 2.5.3 2: ADR record (1.4.2) if its severity is “Mild”, not perform MDS checking for current ADR profile
        /// [IGNORE]2.5.3 4-2 ADR record,o	both hiclSeqNo and hicSeqNos is EMPTY or zero; and drugType = “D”
        /// Message: System cannot perform adverse drug reaction checking for the following ADR record(s) in the alert function: [drug from 1.4.2]
        /// </summary>
        /// <param name="alertProfileRes"></param>
        public void CheckADRProfileForMDSCheck(ref AlertProfileResult alertProfileRes, string caseNumber)
        {
            if (alertProfileRes.AdrProfile == null || alertProfileRes.AdrProfile.Count() == 0)
            {
                return;
            }

            AdrProfile[] adrProfiles = new AdrProfile[alertProfileRes.AdrProfile.Count()];
            alertProfileRes.AdrProfile.CopyTo(adrProfiles);
            //List<string> adrDrugList = new List<string>();
            foreach (var adrProfile in adrProfiles)
            {
                //2.5.3 2 if its severity is “Mild”, not perform MDS checking, no msg
                if ("MILD".Equals(adrProfile.Severity, StringComparison.OrdinalIgnoreCase))
                {
                    alertProfileRes.AdrProfile.Remove(alertProfileRes.AdrProfile.First(a => a.AdrSeqNo == adrProfile.AdrSeqNo));
                }
                //ignore 2.5.3 4
            }


        }
        public bool CheckDrugCodeIfNoNeedMDSCheck(string drugItemCode)
        {
            return string.IsNullOrEmpty(drugItemCode) && drugItemCode.ToUpper().StartsWith("PDF");
        }

        public bool CheckIsG6PD(List<AlertProfile> alertProfiles)
        {
            return alertProfiles.Any(item => item.AlertCode == "A0001");
        }
        /// <summary>
        /// 2.5.3 5 System should ignore the following MDS alerts when suppress flag is true:
        ///Drug allergy alert
        ///G6PD deficiency contraindication alert
        ///ignore the G6PD deficiency contraindication alert when the severityLevelCode  is 2 or 3
        /// </summary>
        /// <param name="mdsResult"></param>
        public void FilterCheckForMDSResult(ref MDSCheckResult mdsResult)
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
        /// <summary>
        /// if moeCheckFlag =="N" && GroupMoeCheckFlag =="N", skip MDS check
        /// </summary>
        /// <param name="getDrugMdsPropertyHqRes"></param>
        /// <param name="getPreparationRes"></param>
        /// <param name="mdsInput"></param>
        /// <returns></returns>
 }
}
