using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Messaging;
using System.Text;
using System.Configuration;

namespace Demo.SoapServcie
{
    [SoapActor("*")]
    [Policy("ServerPolicy")]
    [WebService(Namespace = "http://webservice.pas.ha.org.hk/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class PatientService : System.Web.Services.WebService
    {
        public WorkContextSoapHeader WorkContext { get; set; }


        [WebMethod]
        [SoapHeader("WorkContext", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod(ParameterStyle = SoapParameterStyle.Bare)]
        public SearchHKPMIPatientByCaseNoResponse searchHKPMIPatientByCaseNo(SearchHKPMIPatientByCaseNo searchHKPMIPatientByCaseNo)
        {
            WorkContext = new WorkContextSoapHeader();

            HttpContext.Current.Request.InputStream.Position = 0;
            var requestStr = new StreamReader(HttpContext.Current.Request.InputStream, Encoding.UTF8).ReadToEnd();

            var patHospCode = ConfigurationManager.AppSettings["patHospCode"];
            if (false == searchHKPMIPatientByCaseNo.HospitalCode.Equals(patHospCode, StringComparison.OrdinalIgnoreCase))
            {
                return new SearchHKPMIPatientByCaseNoResponse { };
            }

            return new SearchHKPMIPatientByCaseNoResponse
            {
                PatientDemoEnquiry = SoapParserHelper.LoadSamplePatientDemoEnquiry(searchHKPMIPatientByCaseNo.CaseNo)
            };
        }
    }

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
