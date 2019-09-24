﻿using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.WebProxy;
using Microsoft.Web.Services3.Security.Tokens;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Demo.HL7MessageParser.ServiceSimulator.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //SoapClientProxy();

            Test_HL7Parser();

            var client = new RestClient("http://localhost:3181/pms-asa1/1/");

            // Request_AlertProfile(client);

            //  Request_MedicationProfile(client);

          //  SoapClient_WSS(true);

            

            Console.ReadLine();
        }

        private static void Test_HL7Parser()
        {
            List<string> HKIDs = new List<string>
        {
            "HN03191100Y",
            "HN17000256S",
            "HN18001140Y",
            "HN170002512",
            "HN170002520",
            "INVALID_HKID",
            "INVALID_PATIENT",
            "INVALID_ACCESSCODE"
        };

            IHL7MessageParser hl7Parser = new HL7MessageParser_NTEC();

            var pv = hl7Parser.GetPatient("HN170002520");

            var pr = hl7Parser.GetOrders("HN170002520");

            var ar = hl7Parser.GetAllergies(new AlertInputParm
            {
                PatientInfo = new PatientInfo
                {
                    Hkid = "HN170002520"
                },Credentials=new Credentials { }
            });
        }

        private static void SoapClientProxy()
        {
            //init web service proxy 
            PatientService serviceProxy = new PatientService();

            //init UsernameToken, password is the reverted string of username, the same logic in AuthenticateToken
            //  of ServiceUsernameTokenManager class.
            UsernameToken token = new UsernameToken("pas-appt-ws-user", "pas-appt-ws-user-pwd", PasswordOption.SendPlainText);

            // Set the token onto the proxy
            serviceProxy.SetClientCredential(token);

            // Set the ClientPolicy onto the proxy
            serviceProxy.SetPolicy("ClientPolicy");

            //invoke the HelloMyFriend web service method
            try
            {
                var res = serviceProxy.searchHKPMIPatientByCaseNo(new WebProxy.SearchHKPMIPatientByCaseNo
                {
                    caseNo = "HN03191100Y",
                    hospitalCode = "HV"
                });

                var resStr = XmlHelper.XmlSerializeToString(res);
                Console.WriteLine(resStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                if (response.IsSuccessful())
                {
                    var result = response.Data;
                }
                else
                {
                    try
                    {
                        response.ThrowException();
                    }
                    catch (AMException rex)
                    {
                        //Log exception
                    }
                    catch (Exception ex)
                    {
                        //Log exception
                    }
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


        private static void SoapClient_WSS(bool enableWS_Address)
        {
            //C# WebClient and Soap Services with WSSe security
            //https://richardscannell.wordpress.com/2015/12/15/c-webclient-and-soap-services-with-wsse-security/

            string url = "http://localhost:8096/PatientService.asmx";
            var actionName = "http://webservice.pas.ha.org.hk/searchHKPMIPatientByCaseNo";
            string credid = "pas-appt-ws-user";
            string credpassword = "pas-appt-ws-user-pwd";
            StringBuilder rawSOAP = new StringBuilder();

            rawSOAP.Append(BuildSoapHeader(credid, credpassword, enableWS_Address, url, actionName));
            rawSOAP.Append(@"<soapenv:Body><web:searchHKPMIPatientByCaseNo>");
            rawSOAP.Append(BuildSearchparms("hospitalCode", "VH"));
            rawSOAP.Append(BuildSearchparms("caseNo", "HN03191100Y"));

            rawSOAP.Append(@"</web:searchHKPMIPatientByCaseNo></soapenv:Body></soapenv:Envelope>");

            string SOAPObj = rawSOAP.ToString();

            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url + "?op=searchHKPMIPatientByCaseNo") as HttpWebRequest;

                request.ContentType = "text/xml";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(SOAPObj);
                    streamWriter.Flush();
                }

                using (HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(webresponse.GetResponseStream()))
                    {
                        string response = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
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
            string utc = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); ;
            StringBuilder rawSOAP = new StringBuilder(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.pas.ha.org.hk/"" ");

            if (enableWS_Address)
            {
                rawSOAP.Append(@" xmlns:wsa = ""http://schemas.xmlsoap.org/ws/2004/08/addressing""");
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
    }
}
