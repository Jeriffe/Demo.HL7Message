using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Demo.HL7MessageParser.Models
{
    [MessageContract(WrapperName = "getPreparation", WrapperNamespace = "http://biz.dms.pms.model.ha.org.hk/")]
    [XmlRoot(ElementName = "getPreparation")]
    public class GetPreparationRequest
    {
        [MessageBodyMember(Name = "arg0", Namespace = "")]
        [DataMember] [XmlElement(ElementName = "arg0", Namespace = "")]
        public Arg0 Arg0 { get; set; }

        private XmlSerializerNamespaces xmlns;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (xmlns == null)
                {
                    xmlns = new XmlSerializerNamespaces();
                    xmlns.Add("biz", "http://biz.dms.pms.model.ha.org.hk/");
                }
                return xmlns;
            }
            set { xmlns = value; }
        }
    }
    [DataContract(Name = "arg0", Namespace ="")]
    [XmlRoot(ElementName = "arg0")]
    public class Arg0
    {
        [DataMember] [XmlElement(ElementName = "dispHospCode")]
        public string DispHospCode { get; set; }

        [DataMember] [XmlElement(ElementName = "dispWorkstore")]
        public string DispWorkstore { get; set; }

        [DataMember] [XmlElement(ElementName = "itemCode")]
        public string ItemCode { get; set; }

        [DataMember] [XmlElement(ElementName = "trueDisplayname")]
        public string TrueDisplayname { get; set; }

        [DataMember] [XmlElement(ElementName = "formCode")]
        public string FormCode { get; set; }

        [DataMember] [XmlElement(ElementName = "saltProperty")]
        public string SaltProperty { get; set; }

        [DataMember] [XmlElement(ElementName = "drugScope")]
        public string DrugScope { get; set; }

        [DataMember] [XmlElement(ElementName = "specialtyType")]
        public string SpecialtyType { get; set; }

        [DataMember] [XmlElement(ElementName = "pasSpecialty")]
        public string PasSpecialty { get; set; }

        [DataMember] [XmlElement(ElementName = "pasSubSpecialty")]
        public string PasSubSpecialty { get; set; }

        [DataMember] [XmlElement(ElementName = "costIncluded")]
        public bool CostIncluded { get; set; }

        [DataMember] [XmlElement(ElementName = "hqFlag")]
        public bool HqFlag { get; set; }
    }

    /*
   <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:biz="http://biz.dms.pms.model.ha.org.hk/">
   <soapenv:Header/>
   <soapenv:Body>
      <biz:getPreparation>
         <arg0>
            <dispHospCode></dispHospCode>
            <dispWorkstore></dispWorkstore>
            <itemCode>METH66</itemCode>
            <trueDisplayname>METHYLPREDNISOLONE</trueDisplayname>
            <formCode>OIN</formCode>
            <saltProperty>ACEPONATE</saltProperty>
            <drugScope>I</drugScope>
            <specialtyType>I</specialtyType>
            <pasSpecialty></pasSpecialty>
            <pasSubSpecialty></pasSubSpecialty>
            <costIncluded>true</costIncluded>>
            <hqFlag>true</hqFlag>
         </arg0>
      </biz:getPreparation>
   </soapenv:Body>
</soapenv:Envelope>
*/
}
