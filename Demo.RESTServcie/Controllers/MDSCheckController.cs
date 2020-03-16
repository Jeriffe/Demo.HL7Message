using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.RESTServcie.Controllers
{
    [RoutePrefix(Const.ROUNT_PREFIX + "mdsCheck")]
    public class MDSCheckController : ApiController
    {

        [Route("{hkId}")]
        public MDSCheckResult Get(string hkId)
        {
            return JsonFromFile(hkId);
        }


        [Route("")]
        public async Task<MDSCheckResult> PostAsync()
        {
            var contenttype = Request.Content.Headers.ContentType;

            System.Diagnostics.Debug.WriteLine(Request.Content.Headers.ToString());

            string requestXml = await Request.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(requestXml))
            {
                this.ThrowHttpResponseExceptions(HttpStatusCode.BadRequest, "alertInputParm is null!");
            }

            MDSCheckInputParm inputParam = null;
            try
            {
                inputParam = XmlHelper.XmlDeserialize<MDSCheckInputParm>(requestXml);
            }
            catch
            {
                this.ThrowHttpResponseExceptions(HttpStatusCode.BadRequest, "alertInputParm is null!");
            }


            if (inputParam == null)
            {
                this.ThrowHttpResponseExceptions(HttpStatusCode.BadRequest, "alertInputParm is null!");
            }
            var tempHKID = inputParam.PatientInfo.HKID.ToUpper();

            return JsonFromFile(tempHKID);

        }

        private static MDSCheckResult JsonFromFile(string hkId)
        {
            try
            {
                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("bin/Data/MDS/{0}.json", hkId));

                var result = JsonHelper.JsonToObjectFromFile<MDSCheckResult>(fileName);
                return result;
            }
            catch (Exception)
            {
                var errorStr = string.Format("JsonToObjectFromFile - {0}.json failed!", hkId);

                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(errorStr),
                    ReasonPhrase = errorStr,
                });
            }
        }

    }
}
