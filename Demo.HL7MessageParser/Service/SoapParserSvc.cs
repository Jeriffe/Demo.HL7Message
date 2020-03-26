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
        private string pathospcode;

        DrugMasterServiceProxy drugMasterSoapSvcProxy;
        PreparationServicerProxy preparationServicerProxy;

        public SoapParserSvc()
        {
            Url = "http://localhost:8096/DrugMasterService.asmx";
        }
        public SoapParserSvc(string uri, string pathospcode)
        {
            Initialize(uri, pathospcode);
        }

        public void Initialize(string restUri, string pathospcode)
        {
            this.Url = restUri;
            this.pathospcode = pathospcode;

            drugMasterSoapSvcProxy = new DrugMasterServiceProxy(Url);

#if DEBUG
            var newUrl = Url.Substring(0, Url.LastIndexOf('/') + 1) + "PreparationService.asmx?op=getPreparation";
            preparationServicerProxy = new PreparationServicerProxy(newUrl);
#else
             preparationServicerProxy = new PreparationServicerProxy(Url);
#endif
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
