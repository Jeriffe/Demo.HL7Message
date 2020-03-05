using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.HL7MessageParser.WinForms.Lexers;
using Demo.HL7MessageParser.Common;

using Microsoft.Web.Services3.Security.Tokens;
using System.Net;
using System.IO;
using Demo.HL7MessageParser.WebProxy;
using NLog;
using System.Configuration;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class PatientDemographicControl : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ISoapWSEService soapWSEService;

        public PatientDemographicControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                InitializePE();
            }

            InitializePE();

            InitializeService();
        }

        private void InitializeService()
        {
            soapWSEService = new SoapWSEParserSvc(Global.PatientEnquirySoapUrl, Global.UserName, Global.Password, Global.HospitalCode);
        }

        private void InitializePE()
        {
            btnCallByWebReq.Enabled = chxEnableWSAddress.Checked = false;

            var patientDemoEnquiryXmlDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/PE/");

            var files = Directory.GetFiles(patientDemoEnquiryXmlDir, "*.xml");

            cbxCaseNumber.DataSource = files.Select(o => new FileInfo(o).Name)
                                            .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                            .ToList();
        }

        private void btnCallByProxy_Click(object sender, EventArgs e)
        {
            scintillaRes.Text = string.Empty;

            var caseNumber = cbxCaseNumber.Text.Trim();

            try
            {
                var res = soapWSEService.GetPatientResult(caseNumber);

                var resStr = XmlHelper.XmlSerializeToString(res);

                scintillaRes.Focus();

                scintillaRes.Text = XmlHelper.FormatXML(resStr);
                scintillaRes.FormatStyle(StyleType.Xml);

            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);

                MessageBox.Show(ex.Message);
            }
        }

        private string BuildRequestSoap(bool enableWSAddress)
        {
            string credid = Global.UserName;
            string credpassword = Global.Password;
            string url = Global.PatientEnquirySoapUrl;

            var actionName = "http://webservice.pas.ha.org.hk/searchHKPMIPatientByCaseNo";

            StringBuilder rawSOAP = new StringBuilder();
            rawSOAP.Append(BuildSoapHeader(credid, credpassword, enableWSAddress, url, actionName));
            rawSOAP.Append(@"<soapenv:Body><web:searchHKPMIPatientByCaseNo>");
            rawSOAP.Append(BuildSearchparms("hospitalCode", Global.HospitalCode));
            rawSOAP.Append(BuildSearchparms("caseNo", cbxCaseNumber.Text.Trim()));

            rawSOAP.Append(@"</web:searchHKPMIPatientByCaseNo></soapenv:Body></soapenv:Envelope>");

            return rawSOAP.ToString();

        }

        private static string BuildSearchparms(string pName, string pvalue)
        {
            string param = string.Format("<{0}>{1}</{0}>", pName, pvalue);

            return param;
        }

        private static string BuildSoapHeader(string credid, string credpassword, bool enableWS_Address, string url, string actionName)
        {
            var nonce = getNonce();
            string nonceToSend = Convert.ToBase64String(Encoding.UTF8.GetBytes(nonce));
            string utc = DateTime.Now.AddMinutes(-30).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); ;
            StringBuilder rawSOAP = new StringBuilder(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.pas.ha.org.hk/"" ");

            if (enableWS_Address)
            {
                rawSOAP.Append(@"xmlns:wsa = ""http://schemas.xmlsoap.org/ws/2004/08/addressing""");
            }

            rawSOAP.Append(">");
            rawSOAP.Append(@"<soapenv:Header>");

            if (enableWS_Address)
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

        private void btnCallByWebReq_Click(object sender, EventArgs e)
        {
            if (!chxEnableWSAddress.Checked)
            {
                MessageBox.Show("You must check 'WS-Address Header' before CallByWebReq");

                return;
            }

            string SOAPObj = BuildRequestSoap(chxEnableWSAddress.Checked);
            string url = Global.PatientEnquirySoapUrl;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url + "?op=searchHKPMIPatientByCaseNo") as HttpWebRequest;

                request.ContentType = "text/xml";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(SOAPObj);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(webresponse.GetResponseStream()))
                    {
                        string response = reader.ReadToEnd();


                        scintillaRes.Text = string.Empty;
                        scintillaRes.Focus();

                        scintillaRes.Text = XmlHelper.FormatXML(response);
                        scintillaRes.FormatStyle(StyleType.Xml);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);

                MessageBox.Show(ex.Message);
            }
        }

        private void cbxCaseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateReqXml();
        }

        private void GenerateReqXml()
        {
            string SOAPObj = BuildRequestSoap(chxEnableWSAddress.Checked);

            scintillaReq.Focus();

            scintillaReq.Text = XmlHelper.FormatXML(SOAPObj);

            scintillaReq.FormatStyle(StyleType.Xml);
        }

        private void chxEnableWSAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chxEnableWSAddress.Checked)
            {
                GenerateReqXml();
            }

            btnCallByWebReq.Enabled = chxEnableWSAddress.Checked;
        }
    }
}
