using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.WCFSoapServices
{
    public class PreparationService : IPreparationService
    {
        public GetPreparationResponse getPreparation(GetPreparationRequest request)
        {
            try
            {
                var relativeItemCode = request.Arg0.ItemCode;
                if (RuleMappingHelper.ItemCode_HKID_Mapping.ContainsKey(relativeItemCode))
                {
                    relativeItemCode = RuleMappingHelper.ItemCode_HKID_Mapping[relativeItemCode];
                }

                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("Data/DM/getPreparation/{0}.xml", relativeItemCode));

                var doc = XDocument.Load(file);


                XNamespace x = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace x2 = "http://biz.dms.pms.model.ha.org.hk/";


                var element = doc.Descendants(x + "Body")
                             .Descendants(x2 + "getPreparationResponse").First();

                //  var str = element.ToString().Replace("ns2:getPreparationResponse", "getPreparationResponse");

                var response = XmlHelper.XmlDeserialize<GetPreparationResponse>(element.ToString());
                response.WorkContext = "rO0ABXdVABx3ZWJsb2dpYy5hcHAuUE1TX0RNU19TVkNfQVBQAAAA1gAAACN3ZWJsb2dpYy53b3JrYXJlYS5TdHJpbmdXb3JrQ29udGV4dAAIMS4xMC40LjYAAA==";
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
