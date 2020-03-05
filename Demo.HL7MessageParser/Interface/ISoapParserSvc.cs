using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public interface ISoapParserSvc
    {
        void Initialize(string restUri, string userName, string password, string pathospcode);

        GetPreparationResponse getPreparation(GetPreparationRequest request);

        GetDrugMdsPropertyHqResponse getDrugMdsPropertyHq(GetDrugMdsPropertyHqRequest request);
    }
}
