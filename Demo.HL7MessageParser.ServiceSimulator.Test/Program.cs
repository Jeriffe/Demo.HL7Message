﻿using Demo.HL7MessageParser.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Demo.HL7MessageParser.ServiceSimulator.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:3181/pms-asa/1/");

            Request_AlertProfile(client);

            //Request_MedicationProfile(client);

            //SoapClient_WSS();

            //WCF_Soap_Test();

            Console.ReadLine();
        }

        private static void Request_MedicationProfile(RestClient client)
        {

            foreach (var caseNumber in new string[] { "HN03191100Y", "HN17000256S", "HN18001140Y", "HN170002512", "HN170002520", })
            {
                var request = new RestRequest(string.Format("medProfiles/{0}", caseNumber), Method.GET);
                request.AddHeader("client_secret", "CLIENT_SECRET");
                request.AddHeader("pathospcode", "PATHOSPCODE");

                // var response = client.ExecuteAsGet<MedicationProfileResult>(request, Method.GET.ToString());
                var response = client.Execute<MedicationProfileResult>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("request2 failed!");
                    Console.WriteLine(response.ResponseStatus);
                }

                else
                {
                    var result = response.Data;
                }
            }

        }

        private static void Request_AlertProfile(RestClient client)
        {

            foreach (var hkid in new string[] { "HN03191100Y", "HN170002520" })
            {
                var requestPost = new RestRequest("alertProfile", Method.POST);
                requestPost.AddHeader("client_secret", "CLIENT_SECRET");
                requestPost.AddHeader("client_id", "CLIENT_ID");
                requestPost.AddHeader("pathospcode", "PATHOSPCODE");
                requestPost.AddJsonBody(new AlertInputParm { PatientInfo = new PatientInfo { Hkid = hkid } });

                var response = client.Execute<AlertProfileResult>(requestPost);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("request2 failed!");
                    Console.WriteLine(response.ResponseStatus);
                }

                else
                {
                    var result = response.Data;
                }
            }

        }


        private static void SoapClient_WSS()
        {
            //C# WebClient and Soap Services with WSSe security
            //https://richardscannell.wordpress.com/2015/12/15/c-webclient-and-soap-services-with-wsse-security/

            string url = "http://localhost:8096/PatientService.asmx?op=searchHKPMIPatientByCaseNo";
            string credid = "pas-appt-ws-user";
            string credpassword = "pas-appt-ws-user-pwd";
            StringBuilder rawSOAP = new StringBuilder();
            rawSOAP.Append(BuildSoapHeader(credid, credpassword));
            rawSOAP.Append(@"<soapenv:Body><web:searchHKPMIPatientByCaseNo>");
            rawSOAP.Append(BuildSearchparms("hospitalCode", "VH"));
            rawSOAP.Append(BuildSearchparms("caseNo", "HN03191100Y"));

            rawSOAP.Append(@"</web:searchHKPMIPatientByCaseNo></soapenv:Body></soapenv:Envelope>");

            string SOAPObj = rawSOAP.ToString();
            using (var wb = new WebClient())
            {

                wb.Credentials = new NetworkCredential(credid, credpassword);
                var responseVal = wb.UploadString(url, "POST", SOAPObj);
                //XmlSerializer ser = new XmlSerializer(typeof(Envelope));
                //var reader = new StringReader(responseVal.ToString());
                //var instance = (Envelope)ser.Deserialize(reader);
            }
        }

        private static string BuildSearchparms(string pName, string pvalue)
        {
            string param = string.Format("<{0}>{1}</{0}>", pName, pvalue);

            return param;
        }

        private static string BuildSoapHeader(string credid, string credpassword)
        {
            var nonce = getNonce();
            string nonceToSend = Convert.ToBase64String(Encoding.UTF8.GetBytes(nonce));
            string utc = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); ;
            StringBuilder rawSOAP = new StringBuilder(@"<soapenv:Envelope xmlns:sear=""http://www.remotesite.com/serviceName"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">");
            rawSOAP.Append(@"<soapenv:Header>");
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

        private static void WCF_Soap_Test()
        {
            PatientSoapServiceReference.PatientSoapServiceClient patientClient = new PatientSoapServiceReference.PatientSoapServiceClient();

            var pr = patientClient.searchHKPMIPatientByCaseNo("A", "B");

            MedicationProfileRESTServiceReference.MedicationProfileRESTServiceClient mrClient = new MedicationProfileRESTServiceReference.MedicationProfileRESTServiceClient();

            var mr = mrClient.GetMedicationProfile();


            patientClient.Close();
        }
    }
}
