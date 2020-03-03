using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Demo.HL7MessageParser.Models
{
    /// <summary>
    /// 2.4.2.2 getPreparation
    /// </summary>
    [XmlRoot(ElementName = "getPreparationResponse")]
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

        [XmlElement(ElementName = "return",Namespace ="")]
        public Return Return { get; set; }
    }

    [XmlRoot(ElementName = "return")]
    public class Return
    {
        [XmlElement(ElementName = "averageUnitPrice")]
        public string AverageUnitPrice { get; set; }
        [XmlElement(ElementName = "baseUnit")]
        public string BaseUnit { get; set; }
        [XmlElement(ElementName = "corpDrugPrice")]
        public string CorpDrugPrice { get; set; }
        [XmlElement(ElementName = "dangerousDrug")]
        public string DangerousDrug { get; set; }
        [XmlElement(ElementName = "drugMds")]
        public DrugMds DrugMds { get; set; }
        [XmlElement(ElementName = "formCode")]
        public string FormCode { get; set; }
        [XmlElement(ElementName = "formRank")]
        public string FormRank { get; set; }
        [XmlElement(ElementName = "fullRouteDesc")]
        public string FullRouteDesc { get; set; }
        [XmlElement(ElementName = "groupBaseUnit")]
        public string GroupBaseUnit { get; set; }
        [XmlElement(ElementName = "isCommonDose")]
        public string IsCommonDose { get; set; }
        [XmlElement(ElementName = "itemCode")]
        public string ItemCode { get; set; }
        [XmlElement(ElementName = "moeDesc")]
        public string MoeDesc { get; set; }
        [XmlElement(ElementName = "moeRouteFormDesc")]
        public string MoeRouteFormDesc { get; set; }
        [XmlElement(ElementName = "pmsFmStatus")]
        public string PmsFmStatus { get; set; }
        [XmlElement(ElementName = "ppmi")]
        public string Ppmi { get; set; }
        [XmlElement(ElementName = "preparationDesc")]
        public string PreparationDesc { get; set; }
        [XmlElement(ElementName = "privateDrugPatTypeCharge")]
        public PrivateDrugPatTypeCharge PrivateDrugPatTypeCharge { get; set; }
        [XmlElement(ElementName = "publicCdp")]
        public string PublicCdp { get; set; }
        [XmlElement(ElementName = "publicDrugPatTypeCharge")]
        public PublicDrugPatTypeCharge PublicDrugPatTypeCharge { get; set; }
        [XmlElement(ElementName = "routeDesc")]
        public string RouteDesc { get; set; }
        [XmlElement(ElementName = "routeFormSortSeq")]
        public string RouteFormSortSeq { get; set; }
        [XmlElement(ElementName = "routeSortSeq")]
        public string RouteSortSeq { get; set; }
        [XmlElement(ElementName = "saltProperty")]
        public string SaltProperty { get; set; }
        [XmlElement(ElementName = "sfiCategory")]
        public string SfiCategory { get; set; }
        [XmlElement(ElementName = "specRestrict")]
        public string SpecRestrict { get; set; }
        [XmlElement(ElementName = "strength")]
        public string Strength { get; set; }
        [XmlElement(ElementName = "strengthUnit")]
        public string StrengthUnit { get; set; }
        [XmlElement(ElementName = "strengthValue")]
        public string StrengthValue { get; set; }
        [XmlElement(ElementName = "trueDisplayname")]
        public string TrueDisplayname { get; set; }
        [XmlElement(ElementName = "volumeUnit")]
        public string VolumeUnit { get; set; }
        [XmlElement(ElementName = "volumeValue")]
        public float VolumeValue { get; set; }
    }

    [XmlRoot(ElementName = "drugMds")]
    public class DrugMds
    {
        [XmlElement(ElementName = "duplicateFlag")]
        public string DuplicateFlag { get; set; }
        [XmlElement(ElementName = "gcnSeqno")]
        public string GcnSeqno { get; set; }
        [XmlElement(ElementName = "groupGcnSeqno")]
        public string GroupGcnSeqno { get; set; }
        [XmlElement(ElementName = "groupMoeCheckFlag")]
        public string GroupMoeCheckFlag { get; set; }
        [XmlElement(ElementName = "groupRoutedGeneric")]
        public string GroupRoutedGeneric { get; set; }
        [XmlElement(ElementName = "groupRouteformGeneric")]
        public string GroupRouteformGeneric { get; set; }
        [XmlElement(ElementName = "groupSingleIngred")]
        public string GroupSingleIngred { get; set; }
        [XmlElement(ElementName = "hiclSeqno")]
        public string HiclSeqno { get; set; }
        [XmlElement(ElementName = "moeCheckFlag")]
        public string MoeCheckFlag { get; set; }
        [XmlElement(ElementName = "routedGeneric")]
        public string RoutedGeneric { get; set; }
        [XmlElement(ElementName = "routeformGeneric")]
        public string RouteformGeneric { get; set; }
    }

    [XmlRoot(ElementName = "privateDrugPatTypeCharge")]
    public class PrivateDrugPatTypeCharge
    {
        [XmlElement(ElementName = "addedCharge")]
        public string AddedCharge { get; set; }
        [XmlElement(ElementName = "handleCharge")]
        public string HandleCharge { get; set; }
        [XmlElement(ElementName = "markupFactor")]
        public string MarkupFactor { get; set; }
    }

    [XmlRoot(ElementName = "publicDrugPatTypeCharge")]
    public class PublicDrugPatTypeCharge
    {
        [XmlElement(ElementName = "addedCharge")]
        public string AddedCharge { get; set; }
        [XmlElement(ElementName = "handleCharge")]
        public string HandleCharge { get; set; }
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

