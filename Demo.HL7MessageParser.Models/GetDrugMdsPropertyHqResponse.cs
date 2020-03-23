using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Demo.HL7MessageParser.Models
{

    /// <summary>
    /// 2.4.1.2 getDrugMdsPropertyHq
    /// </summary>
    [XmlRoot(ElementName = "getDrugMdsPropertyHqResponse")]
    public class GetDrugMdsPropertyHqResponse
    {
        private XmlSerializerNamespaces xmlns;

        public GetDrugMdsPropertyHqResponse()
        {
            Return = new List<ReturnObj>();
        }
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

        [XmlElement(ElementName = "return")]
        public List<ReturnObj> Return { get; set; }
    }


    [XmlRoot(ElementName = "return")]
    public class ReturnObj
    {
        [XmlElement(ElementName = "drugKey")]
        public string DrugKey { get; set; }

        [XmlElement(ElementName = "drugMds")]
        public DrugMDSObj DrugMds { get; set; }

        [XmlElement(ElementName = "drugProperty")]
        public DrugProperty DrugProperty { get; set; }

        [XmlElement(ElementName = "itemCode")]
        public string ItemCode { get; set; }
    }

    [XmlRoot(ElementName = "drugMds")]
    public class DrugMDSObj
    {
        [XmlElement(ElementName = "duplicateFlag")]
        public string DuplicateFlag { get; set; }
        [XmlElement(ElementName = "gcnSeqno")]
        public int GcnSeqno { get; set; }
        [XmlElement(ElementName = "groupGcnSeqno")]
        public int GroupGcnSeqno { get; set; }
        [XmlElement(ElementName = "groupMoeCheckFlag")]
        public string GroupMoeCheckFlag { get; set; }
        [XmlElement(ElementName = "groupRoutedGeneric")]
        public int GroupRoutedGeneric { get; set; }
        [XmlElement(ElementName = "groupRouteformGeneric")]
        public int GroupRouteformGeneric { get; set; }
        [XmlElement(ElementName = "groupSingleIngred")]
        public string GroupSingleIngred { get; set; }
        [XmlElement(ElementName = "hiclSeqno")]
        public string HiclSeqno { get; set; }
        [XmlElement(ElementName = "moeCheckFlag")]
        public string MoeCheckFlag { get; set; }
        [XmlElement(ElementName = "routedGeneric")]
        public int RoutedGeneric { get; set; }
        [XmlElement(ElementName = "routeformGeneric")]
        public int RouteformGeneric { get; set; }
    }

    [XmlRoot(ElementName = "drugProperty")]
    public class DrugProperty
    {
        [XmlElement(ElementName = "displayname")]
        public string Displayname { get; set; }
        [XmlElement(ElementName = "dosageCompul")]
        public string DosageCompul { get; set; }
        [XmlElement(ElementName = "drugKey")]
        public string DrugKey { get; set; }
        [XmlElement(ElementName = "fixedPeriod")]
        public string FixedPeriod { get; set; }
        [XmlElement(ElementName = "formCode")]
        public string FormCode { get; set; }
        [XmlElement(ElementName = "saltProperty")]
        public string SaltProperty { get; set; }
        [XmlElement(ElementName = "screenDisplayname")]
        public string ScreenDisplayname { get; set; }
        [XmlElement(ElementName = "screenSalt")]
        public string ScreenSalt { get; set; }
        [XmlElement(ElementName = "strengthCompul")]
        public string StrengthCompul { get; set; }
        [XmlElement(ElementName = "updateDate")]
        public string UpdateDate { get; set; }
        [XmlElement(ElementName = "updateUser")]
        public string UpdateUser { get; set; }
        [XmlElement(ElementName = "updateVersion")]
        public string UpdateVersion { get; set; }
    }
}

/*
 <?xml version='1.0' encoding='UTF-8'?>
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Header>
        <work:WorkContext xmlns:work="http://oracle.com/weblogic/soap/workarea/">rO0ABXdVABx3ZWJsb2dpYy5hcHAuUE1TX0RNU19TVkNfQVBQAAAA1gAAACN3ZWJsb2dpYy53b3JrYXJlYS5TdHJpbmdXb3JrQ29udGV4dAAIMS4xMC40LjYAAA==</work:WorkContext>
    </S:Header>
    <S:Body>
        <ns2:getDrugMdsPropertyHqResponse xmlns:ns2="http://biz.dms.pms.model.ha.org.hk/">
            <return>
                <drugKey>181</drugKey>
                <drugMds>
                    <duplicateFlag>false</duplicateFlag>
                    <gcnSeqno>16491</gcnSeqno>
                    <groupGcnSeqno>0</groupGcnSeqno>
                    <groupMoeCheckFlag>Y</groupMoeCheckFlag>
                    <groupRoutedGeneric>6292938</groupRoutedGeneric>
                    <groupRouteformGeneric>3740</groupRouteformGeneric>
                    <groupSingleIngred>Y</groupSingleIngred>
                    <hiclSeqno>1482</hiclSeqno>
                    <moeCheckFlag>Y</moeCheckFlag>
                    <routedGeneric>6292938</routedGeneric>
                    <routeformGeneric>3740</routeformGeneric>
                </drugMds>
                <drugProperty>
                    <displayname>AMETHOCAINE</displayname>
                    <dosageCompul>Y</dosageCompul>
                    <drugKey>181</drugKey>
                    <fixedPeriod>N</fixedPeriod>
                    <formCode>MIN</formCode>
                    <saltProperty>HCL</saltProperty>
                    <screenDisplayname>AMETHOCAINE</screenDisplayname>
                    <screenSalt>HCL</screenSalt>
                    <strengthCompul>Y</strengthCompul>
                    <updateDate>2008-04-30T17:37:49+08:00</updateDate>
                    <updateUser>MCW093</updateUser>
                    <updateVersion>0</updateVersion>
                </drugProperty>
                <itemCode>AMET02</itemCode>
            </return>
            <return>
                <drugKey>4461</drugKey>
                <drugMds>
                    <duplicateFlag>false</duplicateFlag>
                    <groupGcnSeqno>0</groupGcnSeqno>
                    <groupMoeCheckFlag>N</groupMoeCheckFlag>
                    <groupRoutedGeneric>0</groupRoutedGeneric>
                    <groupRouteformGeneric>0</groupRouteformGeneric>
                    <groupSingleIngred>0</groupSingleIngred>
                    <moeCheckFlag>N</moeCheckFlag>
                </drugMds>
                <drugProperty>
                    <displayname>DEXTROSE 10% + AMINO ACID 5% + CALCIUM GLUCONATE 2.91MMOL</displayname>
                    <dosageCompul>Y</dosageCompul>
                    <drugKey>4461</drugKey>
                    <fixedPeriod>N</fixedPeriod>
                    <formCode>INF</formCode>
                    <screenDisplayname>DEXTROSE 10% + AMINO ACID 5% + CALCIUM GLUCONATE 2.91MMOL</screenDisplayname>
                    <strengthCompul>Y</strengthCompul>
                    <updateDate>2014-10-13T15:13:11+08:00</updateDate>
                    <updateUser>mcw093</updateUser>
                    <updateVersion>6</updateVersion>
                </drugProperty>
                <itemCode>DEXT1Q</itemCode>
            </return>
        </ns2:getDrugMdsPropertyHqResponse>
    </S:Body>
</S:Envelope>
*/
