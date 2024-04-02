using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;

namespace Demo.HL7MessageParser.Models
{
    /// <summary>
    /// 2.4.2.2 getPreparation
    /// </summary>
    [MessageContract(WrapperName = "getPreparationResponse", WrapperNamespace = "http://biz.dms.pms.model.ha.org.hk/")]
    [XmlRoot(ElementName = "getPreparationResponse", Namespace = "http://biz.dms.pms.model.ha.org.hk/")]
    public class GetPreparationResponse
    {
        private XmlSerializerNamespaces xmlns;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (xmlns == null)
                {

                    xmlns = new XmlSerializerNamespaces();
                    xmlns.Add("ns2", "http://biz.dms.pms.model.ha.org.hk/");
                }
                return xmlns;
            }
            set { xmlns = value; }
        }

        [MessageBodyMember(Name = "return", Namespace = "")]
        [XmlElement(ElementName = "return", Namespace = "")]
        public Return Return { get; set; }

        [MessageHeader(Name = "WorkContext", Namespace = "http://oracle.com/weblogic/soap/workarea/")]
        public string WorkContext;

    }

    [DataContract(Namespace = "")]
    [XmlRoot(ElementName = "return")]
    public class Return
    {
        [DataMember]
        [XmlElement(ElementName = "averageUnitPrice")]
        public string AverageUnitPrice { get; set; }
        [DataMember]
        [XmlElement(ElementName = "baseUnit")]
        public string BaseUnit { get; set; }
        [DataMember]
        [XmlElement(ElementName = "corpDrugPrice")]
        public string CorpDrugPrice { get; set; }
        [DataMember]
        [XmlElement(ElementName = "dangerousDrug")]
        public string DangerousDrug { get; set; }
        [DataMember]
        [XmlElement(ElementName = "drugMds")]
        public DrugMds DrugMds { get; set; }
        [DataMember]
        [XmlElement(ElementName = "formCode")]
        public string FormCode { get; set; }
        [DataMember]
        [XmlElement(ElementName = "formRank")]
        public string FormRank { get; set; }
        [DataMember]
        [XmlElement(ElementName = "fullRouteDesc")]
        public string FullRouteDesc { get; set; }
        [DataMember]
        [XmlElement(ElementName = "groupBaseUnit")]
        public string GroupBaseUnit { get; set; }
        [DataMember]
        [XmlElement(ElementName = "isCommonDose")]
        public string IsCommonDose { get; set; }
        [DataMember]
        [XmlElement(ElementName = "itemCode")]
        public string ItemCode { get; set; }
        [DataMember]
        [XmlElement(ElementName = "moeDesc")]
        public string MoeDesc { get; set; }
        [DataMember]
        [XmlElement(ElementName = "moeRouteFormDesc")]
        public string MoeRouteFormDesc { get; set; }
        [DataMember]
        [XmlElement(ElementName = "pmsFmStatus")]
        public string PmsFmStatus { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ppmi")]
        public string Ppmi { get; set; }
        [DataMember]
        [XmlElement(ElementName = "preparationDesc")]
        public string PreparationDesc { get; set; }
        [DataMember]
        [XmlElement(ElementName = "privateDrugPatTypeCharge")]
        public PrivateDrugPatTypeCharge PrivateDrugPatTypeCharge { get; set; }
        [DataMember]
        [XmlElement(ElementName = "publicCdp")]
        public string PublicCdp { get; set; }
        [DataMember]
        [XmlElement(ElementName = "publicDrugPatTypeCharge")]
        public PublicDrugPatTypeCharge PublicDrugPatTypeCharge { get; set; }
        [DataMember]
        [XmlElement(ElementName = "routeDesc")]
        public string RouteDesc { get; set; }
        [DataMember]
        [XmlElement(ElementName = "routeFormSortSeq")]
        public string RouteFormSortSeq { get; set; }
        [DataMember]
        [XmlElement(ElementName = "routeSortSeq")]
        public string RouteSortSeq { get; set; }
        [DataMember]
        [XmlElement(ElementName = "saltProperty")]
        public string SaltProperty { get; set; }
        [DataMember]
        [XmlElement(ElementName = "sfiCategory")]
        public string SfiCategory { get; set; }
        [DataMember]
        [XmlElement(ElementName = "specRestrict")]
        public string SpecRestrict { get; set; }
        [DataMember]
        [XmlElement(ElementName = "strength")]
        public string Strength { get; set; }
        [DataMember]
        [XmlElement(ElementName = "strengthUnit")]
        public string StrengthUnit { get; set; }
        [DataMember]
        [XmlElement(ElementName = "strengthValue")]
        public string StrengthValue { get; set; }
        [DataMember]
        [XmlElement(ElementName = "trueDisplayname")]
        public string TrueDisplayname { get; set; }
        [DataMember]
        [XmlElement(ElementName = "volumeUnit")]
        public string VolumeUnit { get; set; }
        [DataMember]
        [XmlElement(ElementName = "volumeValue")]
        public string VolumeValue { get; set; }
    }

    [DataContract(Namespace = "")]
    [XmlRoot(ElementName = "drugMds")]
    public class DrugMds
    {
        [DataMember]
        [XmlElement(ElementName = "duplicateFlag")]
        public string DuplicateFlag { get; set; }
        [DataMember]
        [XmlElement(ElementName = "gcnSeqno")]
        public string GcnSeqno { get; set; }
        [DataMember]
        [XmlElement(ElementName = "groupGcnSeqno")]
        public string GroupGcnSeqno { get; set; }
        [DataMember]
        [XmlElement(ElementName = "groupMoeCheckFlag")]
        public string GroupMoeCheckFlag { get; set; }
        [DataMember]
        [XmlElement(ElementName = "groupRoutedGeneric")]
        public string GroupRoutedGeneric { get; set; }
        [DataMember]
        [XmlElement(ElementName = "groupRouteformGeneric")]
        public string GroupRouteformGeneric { get; set; }
        [DataMember]
        [XmlElement(ElementName = "groupSingleIngred")]
        public string GroupSingleIngred { get; set; }
        [DataMember]
        [XmlElement(ElementName = "hiclSeqno")]
        public string HiclSeqno { get; set; }
        [DataMember]
        [XmlElement(ElementName = "moeCheckFlag")]
        public string MoeCheckFlag { get; set; }
        [DataMember]
        [XmlElement(ElementName = "routedGeneric")]
        public string RoutedGeneric { get; set; }
        [DataMember]
        [XmlElement(ElementName = "routeformGeneric")]
        public string RouteformGeneric { get; set; }
    }
    [DataContract(Namespace = "")]
    [XmlRoot(ElementName = "privateDrugPatTypeCharge")]
    public class PrivateDrugPatTypeCharge
    {
        [DataMember]
        [XmlElement(ElementName = "addedCharge")]
        public string AddedCharge { get; set; }
        [DataMember]
        [XmlElement(ElementName = "handleCharge")]
        public string HandleCharge { get; set; }
        [DataMember]
        [XmlElement(ElementName = "markupFactor")]
        public string MarkupFactor { get; set; }
    }

    [DataContract(Namespace = "")]
    [XmlRoot(ElementName = "publicDrugPatTypeCharge")]
    public class PublicDrugPatTypeCharge
    {
        [DataMember]
        [XmlElement(ElementName = "addedCharge")]
        public string AddedCharge { get; set; }
        [DataMember]
        [XmlElement(ElementName = "handleCharge")]
        public string HandleCharge { get; set; }
        [DataMember]
        [XmlElement(ElementName = "markupFactor")]
        public string MarkupFactor { get; set; }
    }
}
/*
 <?xml version='1.0' encoding='UTF-8'?>
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Header>
        <work:WorkContext xmlns:work="http://oracle.com/weblogic/soap/workarea/">rO0ABXdVABx3ZWJsb2dpYy5hcHAuUE1TX0RNU19TVkNfQVBQAAAA1gAAACN3ZWJsb2dpYy53b3JrYXJlYS5TdHJpbmdXb3JrQ29udGV4dAAIMS4xMC40LjYAAA==</work:WorkContext>
    </S:Header>
    <S:Body>
        <ns2:getPreparationResponse xmlns:ns2="http://biz.dms.pms.model.ha.org.hk/">
            <return>
                <averageUnitPrice>19.0</averageUnitPrice>
                <baseUnit>TUBE</baseUnit>
                <corpDrugPrice>19.0</corpDrugPrice>
                <dangerousDrug>N</dangerousDrug>
                <drugMds>
                    <duplicateFlag>false</duplicateFlag>
                    <gcnSeqno>28829</gcnSeqno>
                    <groupGcnSeqno>0</groupGcnSeqno>
                    <groupMoeCheckFlag>Y</groupMoeCheckFlag>
                    <groupRoutedGeneric>5254937</groupRoutedGeneric>
                    <groupRouteformGeneric>19506</groupRouteformGeneric>
                    <groupSingleIngred>Y</groupSingleIngred>
                    <hiclSeqno>12057</hiclSeqno>
                    <moeCheckFlag>Y</moeCheckFlag>
                    <routedGeneric>5254937</routedGeneric>
                    <routeformGeneric>19506</routeformGeneric>
                </drugMds>
                <formCode>OIN</formCode>
                <formRank>6415</formRank>
                <fullRouteDesc>TOPICAL</fullRouteDesc>
                <groupBaseUnit>TUBE</groupBaseUnit>
                <isCommonDose>true</isCommonDose>
                <itemCode>METH66</itemCode>
                <moeDesc>OINTMENT</moeDesc>
                <moeRouteFormDesc>TOPICAL OINTMENT</moeRouteFormDesc>
                <pmsFmStatus>X</pmsFmStatus>
                <ppmi>N</ppmi>
                <preparationDesc>0.1% 10 G</preparationDesc>
                <privateDrugPatTypeCharge>
                    <addedCharge>0.0</addedCharge>
                    <handleCharge>105.0</handleCharge>
                    <markupFactor>1.01</markupFactor>
                </privateDrugPatTypeCharge>
                <publicCdp>19.19</publicCdp>
                <publicDrugPatTypeCharge>
                    <addedCharge>0.0</addedCharge>
                    <handleCharge>105.0</handleCharge>
                    <markupFactor>1.01</markupFactor>
                </publicDrugPatTypeCharge>
                <routeDesc>TOPICAL</routeDesc>
                <routeFormSortSeq>999</routeFormSortSeq>
                <routeSortSeq>999</routeSortSeq>
                <saltProperty>ACEPONATE</saltProperty>
                <sfiCategory>C</sfiCategory>
                <specRestrict>*</specRestrict>
                <strength>0.1%</strength>
                <strengthUnit>%</strengthUnit>
                <strengthValue>0.1</strengthValue>
                <trueDisplayname>METHYLPREDNISOLONE</trueDisplayname>
                <volumeUnit>G</volumeUnit>
                <volumeValue>10.0</volumeValue>
            </return>
        </ns2:getPreparationResponse>
    </S:Body>
</S:Envelope>
*/

