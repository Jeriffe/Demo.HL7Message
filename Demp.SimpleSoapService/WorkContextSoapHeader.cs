using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Demp.SimpleSoapService
{
    [XmlRoot(ElementName = "WorkContext", Namespace = "http://oracle.com/weblogic/soap/workarea/")]
    public class WorkContextSoapHeader : SoapHeader
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
                    xmlns.Add("work", "http://oracle.com/weblogic/soap/workarea/");
                }
                return xmlns;
            }
            set { xmlns = value; }
        }
    }

}