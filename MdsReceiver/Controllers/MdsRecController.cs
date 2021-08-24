using MdsReceiver.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MdsReceiver.Controllers
{
    [Route(ConstDef.API_ROUTE_PREFIX + "/mds")]
    [ApiController]
    public class MdsRecController : ControllerBase
    {
        [HttpPost]
        [Route("test2/{caseNumber}/{drugCode}")]
        public string TEST(string caseNumber, string drugCode, [FromBody] JsonElement mds)
        {                               
            return $"{{\"result\":\"SUCCESS|{ caseNumber} | { drugCode } | {mds.GetRawText()}}}";
        }

        [HttpPost]
        [Route("{caseNumber}/{drugCode}")]
        public string VerifyMDSAsync(string caseNumber, string drugCode, [FromBody] MDSCheckResult mds)
        {
            return $"{{\"result\":\"SUCCESS|{caseNumber}|{drugCode}|{(mds == null).ToString()}\"}}";
        }
    }

    public sealed class ConstDef
    {
        public const string API_ROUTE_PREFIX = "api/v1";
    }
}
