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
    public class SoapParserSvc : ISoapParserSvc
    {
        private string Url;
        private string userName;
        private string password;
        private string pathospcode;

        DrugMasterServiceProxy drugMasterSoapSvcProxy;
        public SoapParserSvc()
        {
            Url = "http://localhost:8096/DrugMasterService.asmx";
            userName = "pas-appt-ws-user";
            password = "pas-appt-ws-user-pwd";
            pathospcode = "VH";
        }
        public SoapParserSvc(string uri, string userName, string password, string pathospcode)
        {
            Initialize(uri, userName, password, pathospcode);
        }

        public void Initialize(string restUri, string userName, string password, string pathospcode)
        {
            this.Url = restUri;
            this.userName = userName;
            this.password = password;
            this.pathospcode = pathospcode;

            drugMasterSoapSvcProxy = new DrugMasterServiceProxy(Url);
        }

        public GetDrugMdsPropertyHqResponse getDrugMdsPropertyHq(Models.GetDrugMdsPropertyHqRequest request)
        {
            var response = drugMasterSoapSvcProxy.getDrugMdsPropertyHq(request);

            return new GetDrugMdsPropertyHqResponse { Return = response.ToList() };
        }

        public Models.GetPreparationResponse getPreparation(Models.GetPreparationRequest request)
        {
            var returnResponse = drugMasterSoapSvcProxy.getPreparation(request);

            return returnResponse;
        }


    }
}
