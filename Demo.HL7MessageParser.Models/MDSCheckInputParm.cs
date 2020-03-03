using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Demo.HL7MessageParser.Models
{

    [XmlRoot(ElementName = "inputParm")]
    public class MDSCheckInputParm
    {
        [XmlElement(ElementName = "patientInfo")]
        public MDSCheck_PatientInfo PatientInfo { get; set; }
        [XmlElement(ElementName = "userInfo")]
        public MDSCheck_UserInfo UserInfo { get; set; }
        [XmlElement(ElementName = "patientAllergyProfile")]
        public List<PatientAllergyProfile> PatientAllergyProfile { get; set; }

        [XmlElement(ElementName = "patientAdrProfile")]
        public PatientAdrProfile PatientAdrProfile { get; set; }

        [XmlElement(ElementName = "currentRxDrugProfile")]
        public CurrentRxDrugProfile CurrentRxDrugProfile { get; set; }
        /// <summary>
        /// if AlertProfileResult.AlertProfile= G6PD, then “true”, else “false”
        /// </summary>
        [XmlElement(ElementName = "hasG6pdDeficiency")]
        public bool HasG6pdDeficiency { get; set; }
        /// <summary>
        /// Default: "false"
        /// </summary>
        [XmlElement(ElementName = "hasPregnancy")]
        public bool HasPregnancy { get; set; }
        /// <summary>
        /// “false” for DDIM checking
        /// </summary>
        [XmlElement(ElementName = "checkDdim")]
        public bool CheckDdim { get; set; }
        /// <summary>
        ///if hasG6pdDeficiency or hasPregnancy is true, then “true”,else “false”
        /// </summary>
        [XmlElement(ElementName = "checkDdcm")]
        public bool CheckDdcm { get; set; }
        /// <summary>
        /// “true” for Allergy checking
        /// </summary>
        [XmlElement(ElementName = "checkDam")]
        public bool CheckDam { get; set; }
        /// <summary>
        /// “true” for ADR checking
        /// </summary>
        [XmlElement(ElementName = "checkAdr")]
        public bool CheckAdr { get; set; }
        /// <summary>
        /// DEFAULT:"false"
        /// </summary>
        [XmlElement(ElementName = "checkDscm")]
        public bool CheckDscm { get; set; }
        /// <summary>
        /// “false” for Dosage Range checking
        /// </summary>
        [XmlElement(ElementName = "checkDrcm")]
        public bool CheckDrcm { get; set; }
        /// <summary>
        /// “false” for HLA checking
        /// </summary>
        [XmlElement(ElementName = "checkDlcm")]
        public bool CheckDlcm { get; set; }
        /// <summary>
        /// “false” for Steroid checking
        /// </summary>
        [XmlElement(ElementName = "checkSteroid")]
        public bool CheckSteroid { get; set; }
        /// <summary>
        /// DEFAULT: "PMS"
        /// </summary>
        [XmlElement(ElementName = "callerSourceSystem")]
        public string CallerSourceSystem { get; set; }
        /// <summary>
        /// DEFAULT: "false"
        /// </summary>
        [XmlElement(ElementName = "checkDiscon")]
        public bool CheckDiscon { get; set; }
    }

    [XmlRoot(ElementName = "patientInfo")]
    public class MDSCheck_PatientInfo
    {
        /// <summary>
        /// PatientDemoEnquiry.Patient.HKID
        /// </summary>
        [XmlElement(ElementName = "HKID")]
        public string HKID { get; set; }
        /// <summary>
        ///  PatientDemoEnquiry.Patient.Key
        /// </summary>
        [XmlElement(ElementName = "patientKey")]
        public string PatientKey { get; set; }
        /// <summary>
        /// PatientDemoEnquiry.Patient.Sex
        /// </summary>
        [XmlElement(ElementName = "sex")]
        public string Sex { get; set; }
        /// <summary>
        /// PatientDemoEnquiry.Patient.DOB;
        /// Number of days between Current Date and Patient DOB
        /// </summary>
        [XmlElement(ElementName = "ageInDays")]
        public string AgeInDays { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "heightInCM")]
        public string HeightInCM { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "BSAInM2")]
        public string BSAInM2 { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "weightInKG")]
        public string WeightInKG { get; set; }
        /// <summary>
        /// DEFAULT:"N"
        /// </summary>
        [XmlElement(ElementName = "dataUpdated")]
        public string DataUpdated { get; set; }
        /// <summary>
        /// DEFAULT:"N"
        /// </summary>
        [XmlElement(ElementName = "dataWithinValidPeriod")]
        public string DataWithinValidPeriod { get; set; }
    }

    [XmlRoot(ElementName = "userInfo")]
    public class MDSCheck_UserInfo
    {
        /// <summary>
        /// Patient Hospital Code
        /// </summary>
        [XmlElement(ElementName = "hospCode")]
        public string HospCode { get; set; }
        /// <summary>
        /// PatientDemoEnquiry.CaseList.Case.Specialty;
        /// IPAS specialty code of the medication profile
        /// </summary>
        [XmlElement(ElementName = "pharSpec")]
        public string PharSpec { get; set; }
        /// <summary>
        /// IP address of inquirer
        /// </summary>
        [XmlElement(ElementName = "wrkStnID")]
        public string WrkStnID { get; set; }
        /// <summary>
        /// Default:“I”
        /// </summary>
        [XmlElement(ElementName = "wrkStnType")]
        public string WrkStnType { get; set; }
    }

    [XmlRoot(ElementName = "manifestations")]
    public class Manifestations
    {
        /// <summary>
        /// Default:"0"
        /// </summary>
        [XmlElement(ElementName = "seqNo")]
        public string SeqNo { get; set; }
        /// <summary>
        /// AllergyProfile.Manifestation  (split by “;”);
        /// "manifestation": "Asthma; Rash",
        /// </summary>
        [XmlElement(ElementName = "mDesc")]
        public string MDesc { get; set; }
    }

    /// <summary>
    /// Mapping from AlertProfileResult.AllergyProfile
    /// </summary>
    [XmlRoot(ElementName = "patientAllergyProfile")]
    public class PatientAllergyProfile
    {
        /// <summary>
        /// AllergyProfile.AllergySeqNo
        /// </summary>
        [XmlElement(ElementName = "allergySeqNo")]
        public string AllergySeqNo { get; set; }
        /// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "allergenCode")]
        public string AllergenCode { get; set; }
        ///// <summary>
        /// AllergyProfile.DisplayName
        /// </summary>
        [XmlElement(ElementName = "displayname")]
        public string Displayname { get; set; }
        ///// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "ehrLocalDesc")]
        public string EhrLocalDesc { get; set; }
        ///// <summary>
        /// AllergyProfile.AliasName
        /// </summary>
        [XmlElement(ElementName = "aliasname")]
        public string Aliasname { get; set; }
        ///// <summary>
        /// AllergyProfile.Salt
        /// </summary>
        [XmlElement(ElementName = "salt")]
        public string Salt { get; set; }
        ///// <summary>
        /// AllergyProfile.NameType
        /// </summary>
        [XmlElement(ElementName = "nameType")]
        public string NameType { get; set; }
        ///// <summary>
        /// AllergyProfile.Allergen
        /// </summary>
        [XmlElement(ElementName = "allergen")]
        public string Allergen { get; set; }
        ///// <summary>
        /// AllergyProfile.AllergenType
        /// </summary>
        [XmlElement(ElementName = "allergenType")]
        public string AllergenType { get; set; }
        ///// <summary>
        /// AllergyProfile.Certainty
        /// </summary>
        [XmlElement(ElementName = "certainty")]
        public string Certainty { get; set; }
        ///// <summary>
        /// AllergyProfile.Remark
        /// </summary>
        [XmlElement(ElementName = "remark")]
        public string Remark { get; set; }
        ///// <summary>
        /// Default:"PMS"
        /// </summary>
        [XmlElement(ElementName = "sourceSystem")]
        public string SourceSystem { get; set; }
        /// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "createBy")]
        public string CreateBy { get; set; }
        /// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "createUserName")]
        public string CreateUserName { get; set; }
        /// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "createHosp")]
        public string CreateHosp { get; set; }
        /// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "createRank")]
        public string CreateRank { get; set; }
        /// <summary>
        ///DEFAULT: [BLANK]
        /// </summary>
        [XmlElement(ElementName = "createRankDesc")]
        public string CreateRankDesc { get; set; }
        ///// <summary>
        /// Current Datetime;
        /// Format: "yyyy-MM-dd HH:mm:ss.0"
        /// </summary>
        [XmlElement(ElementName = "createDtm")]
        public string CreateDtm { get; set; }
        ///// <summary>
        /// AllergyProfile.UpdateBy
        /// </summary>
        [XmlElement(ElementName = "updateBy")]
        public string UpdateBy { get; set; }
        ///// <summary>
        /// AllergyProfile.UpdateUser
        /// </summary>
        [XmlElement(ElementName = "updateUserName")]
        public string UpdateUserName { get; set; }
        ///// <summary>
        /// AllergyProfile.UpdateHospital
        /// </summary>
        [XmlElement(ElementName = "updateHosp")]
        public string UpdateHosp { get; set; }
        ///// <summary>
        /// AllergyProfile.UpdateUserRank
        /// </summary>
        [XmlElement(ElementName = "updateRank")]
        public string UpdateRank { get; set; }
        ///// <summary>
        /// AllergyProfile.UpdateRankDesc
        /// </summary>
        [XmlElement(ElementName = "updateRankDesc")]
        public string UpdateRankDesc { get; set; }
        ///// <summary>
        /// AllergyProfile.UpdateDtm;
        /// Format:"yyyy-MM-dd HH:mm:ss.0"
        /// </summary>
        [XmlElement(ElementName = "updateDtm")]
        public string UpdateDtm { get; set; }
        ///// <summary>
        /// List of AllergyProfile Manifestation
        /// </summary>
        [XmlElement(ElementName = "manifestations")]
        public List<Manifestations> Manifestations { get; set; }
        ///// <summary>
        /// AllergyProfile.HiclSeqno
        /// </summary>
        [XmlElement(ElementName = "hiclSeqNo")]
        public string HiclSeqNo { get; set; }
        ///// <summary>
        /// HIC sequence number list from  AllergyProfile.HicSeqno
        /// </summary>
        [XmlElement(ElementName = "hicSeqNos")]
        public HiclSeqNos HicSeqNos { get; set; }
    }

    [XmlRoot(ElementName = "hicSeqNos")]
    public class HiclSeqNos
    {
        [XmlElement(ElementName = "hicSeqNo")]
        public List<string> HicSeqNo { get; set; }
    }

    [XmlRoot(ElementName = "reactions")]
    public class Reactions
    {
        /// <summary>
        /// DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "seqNo")]
        public string SeqNo { get; set; }
        /// <summary>
        ///AdrProfile.Reaction (split by “;”)
        /// </summary>
        [XmlElement(ElementName = "rDesc")]
        public string RDesc { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "severCode")]
        public string SeverCode { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "freqCode")]
        public string FreqCode { get; set; }
    }

    /// <summary>
    /// Mapping from AlertProfileResult.AdrProfile
    /// </summary>
    [XmlRoot(ElementName = "patientAdrProfile")]
    public class PatientAdrProfile
    {
        /// <summary>
        ///AdrProfile.AdrSeqNo
        /// </summary>
        [XmlElement(ElementName = "adrSeqNo")]
        public string AdrSeqNo { get; set; }
        /// <summary>
        ///DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "allergenCode")]
        public string AllergenCode { get; set; }
        /// <summary>
        ///AdrProfile.DisplayName
        /// </summary>
        [XmlElement(ElementName = "displayname")]
        public string Displayname { get; set; }
        /// <summary>
        ///AdrProfile.AliasName
        /// </summary> 
        [XmlElement(ElementName = "aliasname")]
        public string Aliasname { get; set; }
        /// <summary>
        ///AdrProfile.Salt
        /// </summary>
        [XmlElement(ElementName = "salt")]
        public string Salt { get; set; }
        /// <summary>
        ///AdrProfile.NameType
        /// </summary>
        [XmlElement(ElementName = "nameType")]
        public string NameType { get; set; }
        /// <summary>
        ///AdrProfile.Drug
        /// </summary>
        [XmlElement(ElementName = "adr")]
        public string Adr { get; set; }
        /// <summary>
        ///AdrProfile.DrugType
        /// </summary>
        [XmlElement(ElementName = "adrType")]
        public string AdrType { get; set; }
        /// <summary>
        ///AdrProfile.Severity
        /// </summary>
        [XmlElement(ElementName = "severity")]
        public string Severity { get; set; }
        /// <summary>
        ///AdrProfile.Remark
        /// </summary>
        [XmlElement(ElementName = "remark")]
        public string Remark { get; set; }
        /// <summary>
        ///DEFAULT:"PMS"
        /// </summary>
        [XmlElement(ElementName = "sourceSystem")]
        public string SourceSystem { get; set; }
        /// <summary>
        ///DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "createBy")]
        public string CreateBy { get; set; }
        /// <summary>
        ///DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "createUserName")]
        public string CreateUserName { get; set; }
        /// <summary>
        ///DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "createHosp")]
        public string CreateHosp { get; set; }
        /// <summary>
        ///DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "createRank")]
        public string CreateRank { get; set; }
        /// <summary>
        ///DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "createRankDesc")]
        public string CreateRankDesc { get; set; }
        /// <summary>
        ///Current Datetime;
        ///Format "yyyy-MM-dd HH:mm:ss.0"
        /// </summary>
        [XmlElement(ElementName = "createDtm")]
        public string CreateDtm { get; set; }
        /// <summary>
        ///AdrProfile.UpdateBy
        /// </summary>
        [XmlElement(ElementName = "updateBy")]
        public string UpdateBy { get; set; }
        /// <summary>
        ///AdrProfile.UpdateUser
        /// </summary>
        [XmlElement(ElementName = "updateUserName")]
        public string UpdateUserName { get; set; }
        /// <summary>
        ///AdrProfile.UpdateHospital
        /// </summary>
        [XmlElement(ElementName = "updateHosp")]
        public string UpdateHosp { get; set; }
        /// <summary>
        ///AdrProfile.UpdateUserRank
        /// </summary>
        [XmlElement(ElementName = "updateRank")]
        public string UpdateRank { get; set; }
        /// <summary>
        ///AdrProfile.UpdateRankDesc
        /// </summary>
        [XmlElement(ElementName = "updateRankDesc")]
        public string UpdateRankDesc { get; set; }
        /// <summary>
        ///AdrProfile.UpdateDtm;
        ///Format "yyyy-MM-dd HH:mm:ss.0"
        /// </summary>
        [XmlElement(ElementName = "updateDtm")]
        public string UpdateDtm { get; set; }
        /// <summary>
        ///AdrProfile.Reaction
        /// </summary>
        [XmlElement(ElementName = "reactions")]
        public List<Reactions> Reactions { get; set; }
        /// <summary>
        ///AdrProfile.HiclSeqno
        /// </summary>
        [XmlElement(ElementName = "hiclSeqNo")]
        public string HiclSeqNo { get; set; }
        /// <summary>
        ///AdrProfile.HicSeqno
        /// </summary>
        [XmlElement(ElementName = "hicSeqNos")]
        public HiclSeqNos HicSeqNos { get; set; }
    }

    [XmlRoot(ElementName = "currentRxDrugProfile")]
    public class CurrentRxDrugProfile
    {
        /// <summary>
        ///DEFAULT:"false"
        /// </summary>
        [XmlElement(ElementName = "isCapdItem")]
        public string IsCapdItem { get; set; }
        /// <summary>
        /// IF: GcnSeqNo>0 then "S";
        /// ELSE IF (GcnSeqNo <= 0 && (RDfGenId>0 || RGenId>0)), then "R";
        /// ELSE "X" 
        /// </summary>
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        /// <summary>
        ///From DrugMDSObj:
        ///IF MoeCheckFlag="Y" 
        /// --IF RoutedGeneric>0 THEN RoutedGeneric ELSE "0";
        ///ELSE IF GroupMoeCheckFlag="Y" 
        /// --IF GroupRoutedGeneric>0THEN GroupRoutedGeneric ELSE "0";
        ///ELSE Skip MDS checking
        /// </summary>
        [XmlElement(ElementName = "rGenId")]
        public int RGenId { get; set; }
        /// <summary>
        ///From DrugMDSObj:
        ///IF MoeCheckFlag="Y" 
        /// --IF RouteformGeneric>0 THEN RouteformGeneric ELSE "0";
        ///ELSE IF GroupMoeCheckFlag="Y" 
        /// --IF GroupRouteformGeneric>0 THEN GroupRouteformGeneric ELSE "0";
        ///ELSE Skip MDS checking
        /// </summary>
        [XmlElement(ElementName = "rDfGenId")]
        public int RDfGenId { get; set; }
        /// <summary>
        ///From DrugMDSObj:
        ///IF MoeCheckFlag="Y" THEN GcnSeqno ELSE "0";
        ///ELSE IF GroupMoeCheckFlag="Y" THEN GroupGcnSeqno ELSE "0";
        ///ELSE Skip MDS checking
        /// </summary>
        [XmlElement(ElementName = "gcnSeqNo")]
        public int GcnSeqNo { get; set; }
        /// <summary>
        /// DrugProperty.Displayname
        /// </summary>
        [XmlElement(ElementName = "trueDisplayName")]
        public string TrueDisplayName { get; set; }
        /// <summary>
        /// (DrugProperty.Displayname + " " + DrugProperty.SaltProperty) 
        /// Capitalize each word after any <![CDATA[{' ' , '~' , '&' , '-' , '[' , '(' , '<'} is found]]>
        /// </summary>
        [XmlElement(ElementName = "drugDisplayName")]
        public string DrugDisplayName { get; set; }

        [XmlElement(ElementName = "drugErrorDisplayName")]
        public string DrugErrorDisplayName { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "arrayPos")]
        public string ArrayPos { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "indRow")]
        public string IndRow { get; set; }
        /// <summary>
        /// DEFAULT:"0"
        /// </summary>
        [XmlElement(ElementName = "ordNo")]
        public string OrdNo { get; set; }
        /// <summary>
        /// Patient Hospital Code
        /// </summary>
        [XmlElement(ElementName = "hospCode")]
        public string HospCode { get; set; }
        /// <summary>
        /// DEFAULT:"false"
        /// </summary>
        [XmlElement(ElementName = "delete")]
        public string Delete { get; set; }
        /// <summary>
        /// GetDrugMdsPropertyHqResponse.DrugProperty.SaltProperty
        /// </summary>
        [XmlElement(ElementName = "salt")]
        public string Salt { get; set; }
        /// <summary>
        /// GetPreparationResponse.Return.Strength
        /// </summary>
        [XmlElement(ElementName = "strength")]
        public string Strength { get; set; }
        /// <summary>
        /// DrugProperty.Displayname + [(if GetPreparationResponse.MoeDesc is [BLANK], then [BLANK]
        /// ELSE " "+ lowercase(GetPreparationResponse.MoeDesc)]
        /// </summary>
        [XmlElement(ElementName = "drugDdimDisplayName")]
        public string DrugDdimDisplayName { get; set; }
        /// <summary>
        /// DrugProperty.FormCode
        /// </summary>
        [XmlElement(ElementName = "formCode")]
        public string FormCode { get; set; }
        /// <summary>
        /// Drug Item Code;
        /// GetDrugMdsPropertyHqResponse.ItemCode
        /// </summary>
        [XmlElement(ElementName = "itemCode")]
        public string ItemCode { get; set; }
        /// <summary>
        /// DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "specRestrict")]
        public string SpecRestrict { get; set; }
        /// <summary>
        /// DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "specInstruct")]
        public string SpecInstruct { get; set; }
        /// <summary>
        /// DEFAULT:[BLANK]
        /// </summary>
        [XmlElement(ElementName = "pharSpec")]
        public string PharSpec { get; set; }
        /// <summary>
        /// DEFAULT:"N"
        /// </summary>
        [XmlElement(ElementName = "dataUpdate")]
        public string DataUpdate { get; set; }
        /// <summary>
        ///  DEFAULT:"N"
        /// </summary>
        [XmlElement(ElementName = "ddimDosRelatedCheck")]
        public string DdimDosRelatedCheck { get; set; }
    }

}
