using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Demo.WCFSoapServices
{
    public class DrugMasterService : IDrugMasterService
    {
        public GetDrugMdsPropertyHqResponse getDrugMdsPropertyHq(GetDrugMdsPropertyHqRequest request)
        {
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("bin/Data/DM/getDrugMdsPropertyHq/{0}.xml", 1));

                var relativeItemCode = request.Arg0.ItemCode[0];

                if (RuleMappingHelper.ItemCode_HKID_Mapping.ContainsKey(relativeItemCode))
                {
                    relativeItemCode = RuleMappingHelper.ItemCode_HKID_Mapping[relativeItemCode];
                }

                file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("Data/DM/getDrugMdsPropertyHq/{0}.xml", relativeItemCode));

                var doc = XDocument.Load(file);


                XNamespace x = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace x2 = "http://biz.dms.pms.model.ha.org.hk/";


                var element = doc.Descendants(x + "Body")
                             .Descendants(x2 + "getDrugMdsPropertyHqResponse").First();

                var str = element.ToString().Replace("ns2:getDrugMdsPropertyHqRespons", "getDrugMdsPropertyHqRespons");
                var response = XmlHelper.XmlDeserialize<GetDrugMdsPropertyHqResponse>(str);

                return response;

            }
            catch (Exception ex)
            {
                ex = ex;
                //Logger ex

                return new GetDrugMdsPropertyHqResponse();
            }

        }

        private T ParserElement<T>(XDocument doc)
        {
            XNamespace x = "http://biz.dms.pms.model.ha.org.hk/";

            var elements = doc.Descendants(x + "RegisterForComInterop").Where(o => o.Name == "getDrugMdsPropertyHqResponse");

            var result = XmlHelper.XmlDeserialize<T>(doc.ToString());

            return result;
        }
    }

}
