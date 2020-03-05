using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.WebProxy;
using Microsoft.Web.Services3.Security.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Demo.HL7MessageParser
{
    public class SoapWSEParserSvc : ISoapWSEService
    {
        private string Url;
        private string userName;
        private string password;
        private string pathospcode;

        PatientServiceProxy patientServiceProxy;
        public SoapWSEParserSvc()
        {
            Url = "http://localhost:8096//PatientService.asmx";
            userName = "pas-appt-ws-user";
            password = "pas-appt-ws-user-pwd";
            pathospcode = "VH";
        }
        public SoapWSEParserSvc(string uri, string userName, string password, string pathospcode)
        {
            Initialize(uri, userName, password, pathospcode);
        }

        public void Initialize(string restUri, string userName, string password, string pathospcode)
        {
            this.Url = restUri;
            this.userName = userName;
            this.password = password;
            this.pathospcode = pathospcode;

            patientServiceProxy = new PatientServiceProxy(Url);
        }

        public Models.PatientDemoEnquiry GetPatientResult(string caseNumber)
        {

            //init UsernameToken, password is the reverted string of username, the same logic in AuthenticateToken
            //  of ServiceUsernameTokenManager class.
            UsernameToken token = new UsernameToken(userName, password, PasswordOption.SendPlainText);

            patientServiceProxy.SetClientCredential(token);

            patientServiceProxy.SetPolicy("ClientPolicy");

            var res = patientServiceProxy.searchHKPMIPatientByCaseNo(new WebProxy.searchHKPMIPatientByCaseNo
            {
                caseNo = caseNumber,
                hospitalCode = pathospcode
            });

            return XmlHelper.XmlDeserialize<Models.PatientDemoEnquiry>(XmlHelper.XmlSerializeToString(res.PatientDemoEnquiryResult));
        }

        private string CallByWebReq(string caseNumber)
        {
            string SOAPObj = BuildRequestSoap(caseNumber);
            string url = Url;

            var request = HttpWebRequest.Create(url + "?op=searchHKPMIPatientByCaseNo") as HttpWebRequest;

            request.ContentType = "text/xml";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(SOAPObj);
                streamWriter.Flush();
            }

            using (var webresponse = request.GetResponse() as HttpWebResponse)
            {
                using (var reader = new StreamReader(webresponse.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();

                    var patientResultXmlElement = SoapParserHelper.ParserPatientDemoEnquiryElement(response);

                    var resStr = XmlHelper.FormatXML(patientResultXmlElement);

                    return resStr;
                }
            }
        }

        private string BuildRequestSoap(string caseNumber)
        {
            var credid = userName;
            var credpassword = password;
            var url = Url;
            var actionName = "http://webservice.pas.ha.org.hk/searchHKPMIPatientByCaseNo";

            StringBuilder rawSOAP = new StringBuilder();
            rawSOAP.Append(BuildSoapHeader(credid, credpassword, true, url, actionName));
            rawSOAP.Append(@"<soapenv:Body><web:searchHKPMIPatientByCaseNo>");
            rawSOAP.Append(BuildSearchparms("hospitalCode", pathospcode));
            rawSOAP.Append(BuildSearchparms("caseNo", caseNumber));

            rawSOAP.Append(@"</web:searchHKPMIPatientByCaseNo></soapenv:Body></soapenv:Envelope>");

            return rawSOAP.ToString();

        }

        private static string BuildSearchparms(string pName, string pvalue)
        {
            string param = string.Format("<{0}>{1}</{0}>", pName, pvalue);

            return param;
        }

        private static string BuildSoapHeader(string credid, string credpassword, bool enableWSAddress, string url, string actionName)
        {
            var nonce = getNonce();
            string nonceToSend = Convert.ToBase64String(Encoding.UTF8.GetBytes(nonce));
            string utc = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); ;
            var rawSOAP = new StringBuilder(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.pas.ha.org.hk/"" ");

            if (enableWSAddress)
            {
                rawSOAP.Append(@"xmlns:wsa = ""http://schemas.xmlsoap.org/ws/2004/08/addressing""");
            }

            rawSOAP.Append(">");
            rawSOAP.Append(@"<soapenv:Header>");

            if (enableWSAddress)
            {
                rawSOAP.Append(string.Format(@"<wsa:Action>{0}</wsa:Action>", actionName));
                rawSOAP.Append(string.Format(@"<wsa:To>{0}</wsa:To>", url));
            }

            rawSOAP.Append(@"<wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">");
            rawSOAP.Append(@"<wsse:UsernameToken wsu:Id=""UsernameToken-D1A5C91F8C11FC7F2614479411111111"">");
            rawSOAP.Append(@"<wsse:Username>" + credid + "</wsse:Username>");
            rawSOAP.Append(@"<wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">" + credpassword + "</wsse:Password>");
            rawSOAP.Append(@"<wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">" + nonceToSend + "</wsse:Nonce>");
            rawSOAP.Append(@"<wsu:Created>" + utc + "</wsu:Created>");
            rawSOAP.Append(@"</wsse:UsernameToken>");
            rawSOAP.Append(@"</wsse:Security>");
            rawSOAP.Append(@"</soapenv:Header>");

            return rawSOAP.ToString();
        }

        private static string getNonce()
        {
            string phrase = Guid.NewGuid().ToString();
            return phrase;
        }
    }
}
