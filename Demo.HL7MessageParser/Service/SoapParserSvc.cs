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
        private string DrugMasterSoapUrl;
        private string PreParationSoapUrl;
        private string pathospcode;

        DrugMasterServiceProxy drugMasterSoapSvcProxy;
        PreparationServicerProxy preparationServicerProxy;

        public SoapParserSvc()
        {
            DrugMasterSoapUrl = "http://localhost:8096/DrugMasterService.asmx";
            PreParationSoapUrl = "http://localhost:8096/PreparationService.asmx";
        }
        public SoapParserSvc(string drugMasterSoapUrl, string preParationSoapUrl, string pathospcode)
        {
            Initialize(drugMasterSoapUrl, preParationSoapUrl, pathospcode);
        }

        public void Initialize(string drugMasterSoapUrl, string preParationSoapUrl, string pathospcode)
        {
            this.DrugMasterSoapUrl = drugMasterSoapUrl;
            this.PreParationSoapUrl = preParationSoapUrl;

            this.pathospcode = pathospcode;

            drugMasterSoapSvcProxy = new DrugMasterServiceProxy(DrugMasterSoapUrl);
            preparationServicerProxy = new PreparationServicerProxy(PreParationSoapUrl);
        }

        public GetDrugMdsPropertyHqResponse GetDrugMdsPropertyHq(Models.GetDrugMdsPropertyHqRequest request)
        {
            var response = drugMasterSoapSvcProxy.getDrugMdsPropertyHq(request);

            return new GetDrugMdsPropertyHqResponse { Return = response.ToList() };
        }

        public Models.GetPreparationResponse GetPreparation(Models.GetPreparationRequest request)
        {
            var returnResponse = preparationServicerProxy.getPreparation(request);

            return returnResponse;
        }


    }
}
