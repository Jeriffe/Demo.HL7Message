using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public interface ISoapParserSvc
    {
        void Initialize(string drugMasterSoapUrl, string preParationSoapUrl, string pathospcode);

        GetPreparationResponse GetPreparation(GetPreparationRequest request);

        GetDrugMdsPropertyHqResponse GetDrugMdsPropertyHq(GetDrugMdsPropertyHqRequest request);
    }
}
