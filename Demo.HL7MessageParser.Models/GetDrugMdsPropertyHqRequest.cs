using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Demo.HL7MessageParser.Models
{
    [XmlRoot(ElementName = "getDrugMdsPropertyHq", Namespace = "http://biz.dms.pms.model.ha.org.hk/")]
    public class GetDrugMdsPropertyHqRequest
    {
        [XmlElement(ElementName = "arg0")]
        public Arg Arg0 { get; set; }
    }
    //https://stackoverflow.com/questions/10532271/the-xml-element-named-name-from-namespace-references-distinct-types
    /*The problem is that the same element name (namespace="", name="Name") with two different types of content (string type and localstrings type), 
     * which is illegal in xml. This means that every xml parser should raise the fatal error you have printed. 
     * The solution is to change either the name of the element or use the same name but associate them with different namespaces. 
     * 
     * For example instead of:
        [XmlElementAttribute(Namespace = "")]
        you could put:

        [XmlElementAttribute(Namespace = "http://test.com/2010/test")]
    */
    // [XmlRoot(ElementName = "arg0", Namespace = "")]
    [XmlRoot(ElementName = "arg0")]
    public class Arg
    {
        public Arg()
        {
            ItemCode = new List<string>();
        }
        [XmlElement(ElementName = "itemCode")]
        public List<string> ItemCode { get; set; }
    }

}
/*
 <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:biz="http://biz.dms.pms.model.ha.org.hk/">
   <soapenv:Header/>
   <soapenv:Body>
      <biz:getDrugMdsPropertyHq>
         <arg0>
            <itemCode>AMET02</itemCode>
            <itemCode>METH66</itemCode>
            <itemCode>DEXT1Q</itemCode>
            <itemCode>LEVE01</itemCode>
         </arg0>
      </biz:getDrugMdsPropertyHq>
   </soapenv:Body>
</soapenv:Envelope>
*/

