using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public interface IRestParserSvc
    {
        AlertProfileResult GetAlertProfile(AlertInputParm alertinput);

        MedicationProfileResult GetMedicationProfile(string caseNumber);

        MDSCheckResult CheckMDS(MDSCheckInputParm inputParam);

        void Initialize(string restUri, string client_secret, string client_id, string pathospcode);
    }
}
