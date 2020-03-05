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
    public class ProfileRestServicerTest
    {
        IRestParserSvc parser;
        string restUri = "http://localhost:8290/pms-asa/1/";

        [TestInitialize]
        public void Initialize()
        {
            parser = new RestParserSvc(restUri, "CLIENT_SECRET", "CLIENT_ID", "VH");
        }

        [TestMethod]
        public void Test_GetMedicationProfile_Successful()
        {
            var caseNumber = "HN170002512";

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("Data/MP/{0}.json", caseNumber));

            var expectedProfile = JsonHelper.JsonToObjectFromFile<MedicationProfileResult>(fileName);

            Assert.IsNotNull(expectedProfile);
            var expectedProfileJSONStr = JsonHelper.ToJson(expectedProfile);


            var actualProfile = parser.GetMedicationProfile(caseNumber);
            Assert.IsNotNull(expectedProfile);

            var actualProfileJSONStr = JsonHelper.ToJson(actualProfile);

            Assert.AreEqual(expectedProfileJSONStr, actualProfileJSONStr);
        }

        [TestMethod]
        public void Test_GetMedicationProfile_Invalid_CasseNumber()
        {
            var caseNumber = "Invalid_CaseNumber";

            var actualProfile = parser.GetMedicationProfile(caseNumber);

            Assert.IsNotNull(actualProfile);
            Assert.IsNull(actualProfile.MedProfileId);
            Assert.IsNull(actualProfile.CaseNum);
            Assert.IsNull(actualProfile.MedProfileMoItems);
        }

        [TestMethod]
        public void Test_GetMedicationProfile_Invalid_PATHOSPCODE()
        {
            var caseNumber = "Any_CaseNumber";
            var localParser = new RestParserSvc(restUri, "CLIENT_SECRET", "CLIENT_ID", "INVALID_PATHOSPCODE");

            var actualProfile = localParser.GetMedicationProfile(caseNumber);

            Assert.IsNotNull(actualProfile);
            Assert.IsNull(actualProfile.MedProfileId);
            Assert.IsNull(actualProfile.CaseNum);
            Assert.IsNull(actualProfile.MedProfileMoItems);
        }

        //https://www.nuget.org/packages/MSTestExtensions/4.0.0
        [TestMethod]
        public void Test_GetMedicationProfile_Invalid_Client_Secret()
        {
            var caseNumber = "INVALID_CLIENT_SECRET";

            var errorMessage = "Unauthorized";
            var httpStatusCode = HttpStatusCode.Unauthorized;

            var localParser = new RestParserSvc(restUri, "INVALID_CLIENT_SECRET", "CLIENT_ID", "PATHOSPCODE");

            var actualException = Assert.ThrowsException<AMException>(() => localParser.GetMedicationProfile(caseNumber));

            Assert.AreEqual(actualException.HttpStatusCode, httpStatusCode);
            Assert.AreEqual(actualException.Message, errorMessage);
        }

        [TestMethod]
        public void Test_GetMedicationProfile_Service_NotFound()
        {
            var caseNumber = "Any_CaseNumber";

            var httpStatusCode = HttpStatusCode.NotFound;

            var localParser = new RestParserSvc(restUri + @"invalidurl/", "CLIENT_SECRET", "CLIENT_ID", "PATHOSPCODE");
            var actualException = Assert.ThrowsException<AMException>(() => localParser.GetMedicationProfile(caseNumber));

            Assert.AreEqual(actualException.HttpStatusCode, httpStatusCode);
        }

        [TestMethod]
        public void Test_GetMedicationProfile_Service_Unavailable()
        {
            var caseNumber = "Any_CaseNumber";

            var httpStatusCode = HttpStatusCode.ServiceUnavailable;

            var localParser = new RestParserSvc("http://localhost:3181/pms-asa/invalidurl/", "CLIENT_SECRET", "CLIENT_ID", "PATHOSPCODE");
            var actualException = Assert.ThrowsException<AMException>(() => localParser.GetMedicationProfile(caseNumber));

            Assert.AreEqual(actualException.HttpStatusCode, httpStatusCode);
        }


        [TestMethod]
        public void Test_GetAlertProfile_Successful()
        {
            var alertInputParam = new AlertInputParm { PatientInfo = new PatientInfo { Hkid = "H0002520" } };

            var relateivePath = string.Format(@"Data\AP\{0}.json", alertInputParam.PatientInfo.Hkid);
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relateivePath);

            var expectedProfile = JsonHelper.JsonToObjectFromFile<AlertProfileResult>(fileName);

            Assert.IsNotNull(expectedProfile);
            var expectedProfileJSONStr = JsonHelper.ToJson(expectedProfile);


            var actualProfile = parser.GetAlertProfile(alertInputParam);
            Assert.IsNotNull(expectedProfile);

            var actualProfileJSONStr = JsonHelper.ToJson(actualProfile);

            Assert.AreEqual(expectedProfileJSONStr, actualProfileJSONStr);
        }

        [TestMethod]
        public void Test_GetAlertProfile_Invalid_HKID()
        {
            var caseNumber = new AlertInputParm { PatientInfo = new PatientInfo { Hkid = "INVALID_HKID" } };

            try
            {
                var actualProfile = parser.GetAlertProfile(caseNumber);
            }
            catch (AMException e)
            {
                Assert.IsTrue(e.Message.Contains("Invalid Request:20001"));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AMException), "20083")]
        public void Test_GetAlertProfile_Invalid_AccessCode()
        {
            var caseNumber = new AlertInputParm { PatientInfo = new PatientInfo { Hkid = "INVALID_ACCESSCODE" } };

            var actualProfile = parser.GetAlertProfile(caseNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(AMException), "20002")]
        public void Test_GetAlertProfile_Invalid_PatientInfo()
        {
            var caseNumber = new AlertInputParm { PatientInfo = new PatientInfo { Hkid = "INVALID_PATIENT" } };

            var actualProfile = parser.GetAlertProfile(caseNumber);

            //Assert.IsNotNull(actualProfile);
            //Assert.AreEqual(actualProfile.AdrProfile.Count, 0);
            //Assert.AreEqual(actualProfile.AlertProfile.Count, 0);
            //Assert.AreEqual(actualProfile.AllergyProfile.Count, 0);

            //Assert.IsNull(actualProfile.MoePatientSteroidTag);

            //Assert.IsNotNull(actualProfile.ErrorMessage);
            //Assert.IsNotNull(actualProfile.ErrorMessage[0].MsgCode, "20002");
        }

        [TestMethod]
        public void Test_GetAlertProfile_Invalid_Client_Secret()
        {
            var alertInput = new AlertInputParm { PatientInfo = new PatientInfo { Hkid = "INVALID_CLIENT_SECRET" } };
            var errorMessage = "Unauthorized";
            var httpStatusCode = HttpStatusCode.Unauthorized;

            var localParser = new RestParserSvc(restUri, "INVALID_CLIENT_SECRET", "CLIENT_ID", "PATHOSPCODE");

            var actualException = Assert.ThrowsException<AMException>(() => localParser.GetAlertProfile(alertInput));

            Assert.AreEqual(actualException.HttpStatusCode, httpStatusCode);
            Assert.AreEqual(actualException.Message, errorMessage);
        }

        [TestMethod]
        public void Test_GetAlertProfile_Service_NotFound()
        {
            var alertInput = new AlertInputParm { };

            var httpStatusCode = HttpStatusCode.NotFound;

            var localParser = new RestParserSvc(restUri + @"invalidurl/", "CLIENT_SECRET", "CLIENT_ID", "PATHOSPCODE");
            var actualException = Assert.ThrowsException<AMException>(() => localParser.GetAlertProfile(alertInput));

            Assert.AreEqual(actualException.HttpStatusCode, httpStatusCode);
        }

        [TestMethod]
        public void Test_GetAlertProfile_Service_Unavailable()
        {
            var alertInput = new AlertInputParm { };

            var httpStatusCode = HttpStatusCode.ServiceUnavailable;

            var localParser = new RestParserSvc("http://localhost:3181/pms-asa/invalidurl/", "CLIENT_SECRET", "CLIENT_ID", "PATHOSPCODE");
            var actualException = Assert.ThrowsException<AMException>(() => localParser.GetAlertProfile(alertInput));

            Assert.AreEqual(actualException.HttpStatusCode, httpStatusCode);
        }

        [TestCleanup]
        public void CleanUp()
        {
        }
    }
}
