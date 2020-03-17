using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser.Service
{
    public class CheckBeforeMDSChecker
    {
        const string ERROR_SYSTEM_CANNOT_PERFORM = "System cannot perform clinical intelligence checking for the following drug(s):";
        const string ERROR_DRUG_INFORMATION_MAPPING_IS_NOT_AVAILABLE = "System's drug information mapping is not yet available.  Please exercise your professional judgement while prescribing.";

        static string[] FORM_STATUS = new string[] { "C", "D", "M", "N", "S", "X" };

        public CheckBeforeMDSChecker() { }

        static CheckBeforeMDSChecker() { }

        /// <summary>
        /// 2.5.3 2: ADR record (1.4.2) if its severity is “Mild”, not perform MDS checking for current ADR profile
        /// 2.5.3 4-2 ADR record,o	both hiclSeqNo and hicSeqNos is EMPTY or zero; and drugType = “D”
        /// Message: System cannot perform adverse drug reaction checking for the following ADR record(s) in the alert function: [drug from 1.4.2]
        /// </summary>
        /// <param name="alertProfileRes"></param>
        public void CheckADRProfileForMDSCheck(ref AlertProfileResult alertProfileRes, ref MDSCheckResult mdsResult)
        {
            if (alertProfileRes.AdrProfile == null || alertProfileRes.AdrProfile.Count() == 0)
            {
                return;
            }

            AdrProfile[] adrProfiles = new AdrProfile[alertProfileRes.AdrProfile.Count()];
            alertProfileRes.AdrProfile.CopyTo(adrProfiles);
            List<string> adrDrugList = new List<string>();
            foreach (var adrProfile in adrProfiles)
            {
                //2.5.3 2 if its severity is “Mild”, not perform MDS checking, no msg
                if ("MILD".Equals(adrProfile.Severity, StringComparison.OrdinalIgnoreCase))
                {
                    alertProfileRes.AdrProfile.Remove(alertProfileRes.AdrProfile.First(a => a.AdrSeqNo == adrProfile.AdrSeqNo));
                }
                //2.5.3 4-2 both hiclSeqNo and hicSeqNos is EMPTY or zero; and drugType = “D”, no MDS check and prompt msg
                if (adrProfile.HiclSeqno == "0" && adrProfile.DrugType == "D")
                {
                    if (CheckIfAllergyProfileHicSeqnoNoNeedMDS(adrProfile.HicSeqno))
                    {
                        alertProfileRes.AdrProfile.RemoveAll(a => a.AdrSeqNo == adrProfile.AdrSeqNo);

                        adrDrugList.Add(adrProfile.Drug);
                    }
                }
            }

            if (adrDrugList.Count() > 0)
            {
                var msg = "System cannot perform adverse drug reaction checking for the following ADR record(s) in the alert function:";
                msg += string.Join("; ", adrDrugList.ToArray()).TrimEnd(new char[] { ';', ' ' });

                mdsResult.adrError = new AdrError
                {
                    errorDesc = msg,
                    hasAdrError = true
                };
            }
        }

        /// <summary>
        /// No need MDS check for allergy profile
        /// </summary>
        /// <param name="allergyProfile"></param>
        /// <param name="mdsResult"></param>
        public void CheckAllergyProfileForMDSCheck(ref AlertProfileResult alertProfileRes, ref MDSCheckResult mdsResult)
        {
            /****2.5.3 4-1:System should alert user and not perform MDS checking on allergen, ADR record or drug item for the following conditions:
            •    For allergen (refer to [Only for allergyProfile from 1.4.2]):
            o    both [hiclSeqNo] and [hicSeqNos] is EMPTY or zero; and
            o    allergenType != “N”, “O”, “X” or “XP”
            Message:
            System cannot perform drug allergy checking for the following allergen record(s) in the alert function: [allergen from 1.4.2]
            */
            if (alertProfileRes.AllergyProfile == null || alertProfileRes.AllergyProfile.Count() == 0)
            {
                return;
            }

            var allergyProfile = alertProfileRes.AllergyProfile;
            IList<string> allergyDrugList = new List<string>();
            for (int index = (allergyProfile.Count() - 1); index >= 0; index--)
            {
                if ((string.IsNullOrEmpty(allergyProfile[index].HiclSeqno) || allergyProfile[index].HiclSeqno == "0")
                    &&
                    CheckIfAllergyProfileHicSeqnoNoNeedMDS(allergyProfile[index].HicSeqno)
                    &&
                    !(new string[] { "N", "O", "X", "XP" }.Contains(allergyProfile[index].AllergenType))
                    )
                {
                    //no MDS check and show message for AllergyProfile
                    allergyDrugList.Add(allergyProfile[index].Allergen);
                    //if current allergy no need MDS check, remove it from allergy profrofile list
                    alertProfileRes.AllergyProfile.Remove(allergyProfile[index]);
                }
            }
            if (allergyDrugList.Count > 0)
            {
                var msg = "System cannot perform drug allergy checking for the following allergen record(s):";
                msg += string.Join("; ", allergyDrugList.ToArray()).TrimEnd(new char[] { ';', ' ' });
                mdsResult.allergyError = new AllergyError()
                {
                    errorDesc = msg,
                    hasAllergyError = true
                };
            }
        }
        public bool CheckDrugCodeIfNeedMDSCheck(string drugItemCode)
        {
            return !string.IsNullOrEmpty(drugItemCode) && !drugItemCode.ToUpper().StartsWith("PDF");
        }

        /// <summary>
        /// System cannot perform clinical intelligence checking for the following drug(s) :
        /// If[pmsFmStatus from 2.4.2.2] = “C”, “D”, “M”, “N”, “S” or “X”,
        ///    then[drugErrorDisplayName] + “ <” +
        ///     when[pmsFmStatus from 2.4.2.2] = “C” or “D” => “Self-financed item”
        ///     when[pmsFmStatus from 2.4.2.2] = “M” => “Self-financed item with Safety Net”
        ///     when[pmsFmStatus from 2.4.2.2] = “N” => “Unregistered Drug:Named Pat Only”
        ///     when[pmsFmStatus from 2.4.2.2] = “S” => “Sample”
        ///     when[pmsFmStatus from 2.4.2.2] = “X” => “Special Drug”
        ///    + “>”
        ///Else[drugErrorDisplayName]         
        /// </summary>
        /// <param name="drugMds"></param>
        public bool CheckDrugMasterResultForMDSCheck(DrugMDSObj drugMds, string pmsFmStatus, string drugErrorDisplayName, ref MDSCheckResult mdsResult)
        {
            if (drugMds.MoeCheckFlag != "N" || drugMds.GroupMoeCheckFlag != "N")
            {
                return false;
            }

            string msg = string.Format("{0}{1}{2}", ERROR_SYSTEM_CANNOT_PERFORM, Environment.NewLine, drugErrorDisplayName);

            if (FORM_STATUS.Contains(pmsFmStatus.ToUpper()))
            {
                var temp = string.Empty;

                switch (pmsFmStatus.ToUpper())
                {
                    case "C":
                    case "D":
                        temp = "Self-financed item";
                        break;
                    case "M":
                        temp = "Self-financed item with Safety Net";
                        break;
                    case "N":
                        temp = "Unregistered Drug:Named Pat Only";
                        break;
                    case "S":
                        temp = "Sample";
                        break;
                    case "X":
                        temp = "Special Drug";
                        break;
                }

                msg += string.Format(" <{0}>", temp);
            }

            msg += ERROR_DRUG_INFORMATION_MAPPING_IS_NOT_AVAILABLE;

            mdsResult.drugError = new DrugError()
            {
                errorDesc = msg,
                hasDrugError = true
            };

            return false;
        }

        public bool CheckIfAllergyProfileHicSeqnoNoNeedMDS(List<string> HicSeqnos)
        {
            if (HicSeqnos == null)
            {
                return false;
            }

            return HicSeqnos.Any(item => string.IsNullOrEmpty(item) || item == "0");
        }

        public bool CheckIsG6PD(List<AlertProfile> alertProfiles)
        {
            return alertProfiles.Any(item => item.AlertCode == "A0001");
        }
    }
}
