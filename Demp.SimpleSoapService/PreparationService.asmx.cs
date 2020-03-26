using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace Demp.SimpleSoapService
{
    /// <summary>
    /// Summary description for PreparationService
    /// </summary>
    [WebService(Namespace = "http://biz.dms.pms.model.ha.org.hk/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PreparationService : System.Web.Services.WebService
    {

        public WorkContextSoapHeader WorkContext { get; set; }

        public PreparationService()
        {
            WorkContext = null;
        }

        [WebMethod]
        [SoapHeader("WorkContext", Direction = SoapHeaderDirection.Out)]
        [SoapDocumentMethod(ParameterStyle = SoapParameterStyle.Bare)]
        public GetPreparationResponse getPreparation(GetPreparationRequest request)
        {
            HttpContext.Current.Request.InputStream.Position = 0;
            var requestStr = new StreamReader(HttpContext.Current.Request.InputStream, Encoding.UTF8).ReadToEnd();


            WorkContext = new WorkContextSoapHeader();
            try
            {
                var relativeItemCode = request.Arg0.ItemCode;
                if (RuleMappingHelper.ItemCode_HKID_Mapping.ContainsKey(relativeItemCode))
                {
                    relativeItemCode = RuleMappingHelper.ItemCode_HKID_Mapping[relativeItemCode];
                }

                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("bin/Data/DM/getPreparation/{0}.xml", relativeItemCode));

                var doc = XDocument.Load(file);


                XNamespace x = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace x2 = "http://biz.dms.pms.model.ha.org.hk/";


                var element = doc.Descendants(x + "Body")
                             .Descendants(x2 + "getPreparationResponse").First();

                var str = element.ToString().Replace("ns2:getPreparationResponse", "getPreparationResponse");
                var response = XmlHelper.XmlDeserialize<GetPreparationResponse>(str);


                /*
                var element1 = doc.Descendants(x + "Body")
                            .Descendants(x2 + "getDrugMdsPropertyHqResponse")
                            .Descendants("return");
                var returnStrs = string.Format(@"<getDrugMdsPropertyHqResponse>{0}</getDrugMdsPropertyHqResponse>", string.Join("", element1.Select(item => item.ToString()).ToArray()));
                var response = XmlHelper.XmlDeserialize<GetDrugMdsPropertyHqResponse>(returnStrs);
                */

                return response;

            }
            catch (Exception ex)
            {
                ex = ex;
                //Logger ex

                return new GetPreparationResponse { };
            }
        }


    }
}
