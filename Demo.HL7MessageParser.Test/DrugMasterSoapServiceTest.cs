using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Model;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.Test.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.HL7MessageParser.Test
{
    [TestClass]
    public class DrugMasterSoapServiceTest
    {
        ISoapParserSvc parser;
        string restUri = "http://localhost:44368/DrugMasterService.asmx";
        [TestInitialize]
        public void Initialize()
        {
            parser = new SoapParserSvc(restUri,"HOSPITALCODE");
        }
        [TestMethod]
        public void Test_GetDrugMdsPropertyHqResponse_Successful()
        {
            var itemCode = new List<string> { "AMET02", "METH66", "DEXT1Q", "LEVE01" };

            var getDrugMdsPropertyHq = new GetDrugMdsPropertyHqRequest
            {
                Arg0 = new Arg
                {
                    ItemCode = itemCode
                }
            };

            var actualResult = parser.GetDrugMdsPropertyHq(getDrugMdsPropertyHq);

            Assert.IsNotNull(actualResult);
            // var actualProfileJSONStr = JsonHelper.ToJson(actualResult);
        }

        [TestMethod]
        public void Test_getPreparationResponse_Successful()
        {
            var request = new Models.GetPreparationRequest { Arg0 = new Models.Arg0 { ItemCode = "AMET02" } };

            var actualResult = parser.GetPreparation(request);

            Assert.IsNotNull(actualResult);
            // var actualProfileJSONStr = JsonHelper.ToJson(actualResult);
        }


        [TestCleanup]
        public void CleanUp()
        {
        }
    }
}
