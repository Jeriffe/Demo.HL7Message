using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser.Service
{
    public class CheckBeforeMDSCheck
    {
        public CheckBeforeMDSCheck() { }
        /// <summary>
        /// 2.5.3 2: ADR record (1.4.2) if its severity is “Mild”, not perform MDS checking for current ADR profile
        /// 2.5.3 4-2 ADR record,o	both hiclSeqNo and hicSeqNos is EMPTY or zero; and drugType = “D”
        /// Message: System cannot perform adverse drug reaction checking for the following ADR record(s) in the alert function: [drug from 1.4.2]
        /// </summary>
        /// <param name="alertProfileRes"></param>
        public void CheckADRProfileForMDSCheck(ref AlertProfileResult alertProfileRes, ref MDSCheckResult mdsResult)
        {
            if (alertProfileRes.AdrProfile != null && alertProfileRes.AdrProfile.Count() > 0)
            {
                AdrProfile[] adrProfiles = new AdrProfile[alertProfileRes.AdrProfile.Count()];
                alertProfileRes.AdrProfile.CopyTo(adrProfiles);
                List<string> adrDrugList = new List<string>();
                foreach (var adrProfile in adrProfiles)
                {
                    //2.5.3 2 if its severity is “Mild”, not perform MDS checking, no msg
                    if (!string.IsNullOrEmpty(adrProfile.Severity) && adrProfile.Severity.ToUpper() == "MILD")
                    {
                        alertProfileRes.AdrProfile.Remove(alertProfileRes.AdrProfile.First(a => a.AdrSeqNo == adrProfile.AdrSeqNo));
                    }
                    //2.5.3 4-2 both hiclSeqNo and hicSeqNos is EMPTY or zero; and drugType = “D”, no MDS check and prompt msg
                    if ((string.IsNullOrEmpty(adrProfile.HiclSeqno) || adrProfile.HiclSeqno.Trim() == "0")
                         &&
                         CheckIfAllergyProfileHicSeqnoNoNeedMDS(adrProfile.HicSeqno)
                         &&
                         adrProfile.DrugType == "D"
                       )
                    {
                        alertProfileRes.AdrProfile.Remove(alertProfileRes.AdrProfile.First(a => a.AdrSeqNo == adrProfile.AdrSeqNo));
                        adrDrugList.Add(adrProfile.Drug);
                    }
                }
                if (adrDrugList.Count() > 0)
                {
                    var msg = "System cannot perform adverse drug reaction checking for the following ADR record(s) in the alert function:";
                    msg += string.Join("; ", adrDrugList.ToArray()).TrimEnd(new char[] { ';', ' ' });
                    mdsResult.hasMdsAlert = true;
                    mdsResult.adrError = new AdrError()
                    {
                        errorDesc = msg,
                        hasAdrError = true
                    };
                }
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
            if (alertProfileRes.AllergyProfile != null && alertProfileRes.AllergyProfile.Count() > 0)
            {
                var allergyProfile = alertProfileRes.AllergyProfile;
                IList<string> allergyDrugList = new List<string>();
                for (int a = (allergyProfile.Count() - 1); a >= 0; a--)
                {
                    if ((string.IsNullOrEmpty(allergyProfile[a].HiclSeqno) || allergyProfile[a].HiclSeqno == "0")
                        &&
                        CheckIfAllergyProfileHicSeqnoNoNeedMDS(allergyProfile[a].HicSeqno)
                        &&
                        !(new string[] { "N", "O", "X", "XP" }.Contains(allergyProfile[a].AllergenType))
                        )
                    {
                        //no MDS check and show message for AllergyProfile
                        allergyDrugList.Add(allergyProfile[a].Allergen);
                        //if current allergy no need MDS check, remove it from allergy profrofile list
                        alertProfileRes.AllergyProfile.Remove(allergyProfile[a]);
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
        }
        public bool CheckDrugCodeIfNeedMDSCheck(string drugItemCode, ref MDSCheckResult mdsResult)
        {
            if (drugItemCode.ToUpper().StartsWith("PDF"))
            {
                //skip MDS checking, and NO MESSAGE
                mdsResult.hasMdsAlert = false;
                //mdsResult.drugError = new DrugError() {
                //    errorDesc = string.Empty
                //};
                return false;
            }
            return true;
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
            if (drugMds.MoeCheckFlag == "N" && drugMds.GroupMoeCheckFlag == "N")
            {
                string msg = "System cannot perform clinical intelligence checking for the following drug(s):";
                if (new string[] { "C", "D", "M", "N", "S", "X" }.Contains(pmsFmStatus))
                {
                    msg += Environment.NewLine + drugErrorDisplayName + " <";
                    if (pmsFmStatus == "C" || pmsFmStatus == "D") { msg += "Self-financed item"; }
                    else if (pmsFmStatus == "M") { msg += "Self-financed item with Safety Net"; }
                    else if (pmsFmStatus == "N") { msg += "Unregistered Drug:Named Pat Only"; }
                    else if (pmsFmStatus == "S") { msg += "Sample"; }
                    else if (pmsFmStatus == "X") { msg += "Special Drug"; }

                    msg += ">" + Environment.NewLine;
                }
                else
                {
                    msg += Environment.NewLine + drugErrorDisplayName;
                }
                msg += "System's drug information mapping is not yet available.  Please exercise your professional judgement while prescribing.";
                mdsResult.drugError = new DrugError()
                {
                    errorDesc = msg,
                    hasDrugError = true
                };

                return false;
            }
            return true;
        }


        public bool CheckIfAllergyProfileHicSeqnoNoNeedMDS(List<string> HicSeqnos)
        {
            var result = false;
            if (HicSeqnos != null)
            {
                foreach (var hicSeqNo in HicSeqnos)
                {
                    if (string.IsNullOrEmpty(hicSeqNo) || hicSeqNo == "0")
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool CheckIsG6PD(List<AlertProfile> alertProfiles)
        {
            foreach (var alertProfile in alertProfiles)
            {
                if (!string.IsNullOrEmpty(alertProfile.AlertCode) && alertProfile.AlertCode == "A0001")
                {
                    return true;
                }
            }
            return false;
        }

    }
}
