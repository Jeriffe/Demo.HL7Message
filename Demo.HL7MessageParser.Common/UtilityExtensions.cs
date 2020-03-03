using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Demo.HL7MessageParser.Common
{

    public static class UtilityExtensions
    {
        public static string GetLoalIPAddress()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }

            return AddressIP;
        }
    }
    public static class ExtensionMethods
    {
        public static DateTime ToDateTime(this string dateTimeStr)
        {
            DateTime dateTime = DateTime.MinValue;

            DateTime.TryParse(dateTimeStr, out dateTime);

            return dateTime;

        }
    }
}
