using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public interface ISoapWSEService
    {
        void Initialize(string restUri, string userName, string password, string pathospcode);
        PatientDemoEnquiry GetPatientResult(string CaseNum);
    }
}
