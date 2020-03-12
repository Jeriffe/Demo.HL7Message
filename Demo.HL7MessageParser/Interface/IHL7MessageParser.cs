using Demo.HL7MessageParser.DTOs;
using Demo.HL7MessageParser.Model;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public interface IHL7MessageParser
    {
        string SearchRemotePatient(string caseNumber, ref string errorMessage);
        MDSCheckResult MDSCheck(string drugItemCode, PatientDemoEnquiry patientEnquiry, AlertProfileResult alertProfileRes);
    }
}
