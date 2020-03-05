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
    public class SoapParserSvcTest
    {
        ISoapWSEService parser;

        string Uri = "http://localhost:8096/PatientService.asmx";
        [TestInitialize]
        public void Initialize()
        {
            parser = new FakeSoapPatientVisitParser();
        }

        [TestMethod]
        public void Test_GetPatientDemographic_Successful()
        {
            var caseNumber = "HN170002512";

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Data\PE\{0}.xml", caseNumber));

            var expetctedObject = SoapParserHelper.LoadSamplePatientDemoEnquiry(caseNumber, @"Data\PE");

            Assert.IsNotNull(expetctedObject);

            var expectedXmlStr = XmlHelper.XmlSerializeToString(expetctedObject);


            var actualObject = parser.GetPatientResult(caseNumber);
            Assert.IsNotNull(expetctedObject);

            var actualXmlStr = XmlHelper.XmlSerializeToString(actualObject);

            Assert.AreEqual(expectedXmlStr, actualXmlStr);
        }

        [TestMethod]
        public void Test_GetPatientDemographic_Invalid_CasseNumber()
        {
            var caseNumber = "Invalid_CaseNumber";

            var actualProfile = parser.GetPatientResult(caseNumber);

            Assert.IsNotNull(actualProfile);
        }
        

        [TestCleanup]
        public void CleanUp()
        {
        }
    }
}
