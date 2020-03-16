using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Demo.HL7MessageParser.Common
{

    public static class UtilityHelper
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

    public static class RuleMappingHelper
    {
        static RuleMappingHelper()
        {
            ItemCode_HKID_Mapping = new Dictionary<string, string>();
            ItemCode_HKID_Mapping["DEMO01"] = "H2003001_DEMO01";
            ItemCode_HKID_Mapping["DEMO02"] = "H2003002_DEMO02";
            ItemCode_HKID_Mapping["DEMO03"] = "H2003003_DEMO03";
            ItemCode_HKID_Mapping["DEMO04"] = "H2003004_DEMO04";
            ItemCode_HKID_Mapping["DEMO01"] = "H2003001_DEMO01";
            ItemCode_HKID_Mapping["DEMO02"] = "H2003002_DEMO02";
            ItemCode_HKID_Mapping["DEMO03"] = "H2003003_DEMO03";
            ItemCode_HKID_Mapping["DEMO04"] = "H2003004_DEMO04";
            ItemCode_HKID_Mapping["DEMO05"] = "H2003005_DEMO05";
            ItemCode_HKID_Mapping["DEMO06"] = "H2003006_DEMO06";
            ItemCode_HKID_Mapping["DEMO07"] = "H2003007_DEMO07";
            ItemCode_HKID_Mapping["DEMO08"] = "H2003008_DEMO08";
            ItemCode_HKID_Mapping["DEMO09"] = "H2003009_DEMO09";
            ItemCode_HKID_Mapping["DEMO10"] = "H2003010_DEMO10";
            ItemCode_HKID_Mapping["DEMO11"] = "H2003011_DEMO11";
            ItemCode_HKID_Mapping["DEMO12"] = "H2003012_DEMO12";
            ItemCode_HKID_Mapping["DEMO13"] = "H2003013_DEMO13";
            ItemCode_HKID_Mapping["DEMO14"] = "H2003014_DEMO14";
            ItemCode_HKID_Mapping["DEMO15"] = "H2003015_DEMO15";
            ItemCode_HKID_Mapping["DEMO16"] = "H2003016_DEMO16";
            ItemCode_HKID_Mapping["DEMO17"] = "H2003017_DEMO17";
            ItemCode_HKID_Mapping["DEMO18"] = "H2003018_DEMO18";
            ItemCode_HKID_Mapping["DEMO19"] = "H2003019_DEMO19";
            ItemCode_HKID_Mapping["DEMO20"] = "H2003020_DEMO20";
            ItemCode_HKID_Mapping["DEMO21"] = "H2003021_DEMO21";
            ItemCode_HKID_Mapping["DEMO22"] = "H2003022_DEMO22";
            ItemCode_HKID_Mapping["DEMO23"] = "H2003023_DEMO23";
            ItemCode_HKID_Mapping["DEMO24"] = "H2003024_DEMO24";
            ItemCode_HKID_Mapping["DEMO25"] = "H2003025_DEMO25";

            HKID_ItemCode_Mapping = new Dictionary<string, string>();
            HKID_ItemCode_Mapping["H2003001"] = "DEMO01";
            HKID_ItemCode_Mapping["H2003002"] = "DEMO02";
            HKID_ItemCode_Mapping["H2003003"] = "DEMO03";
            HKID_ItemCode_Mapping["H2003004"] = "DEMO04";
            HKID_ItemCode_Mapping["H2003005"] = "DEMO05";
            HKID_ItemCode_Mapping["H2003006"] = "DEMO06";
            HKID_ItemCode_Mapping["H2003007"] = "DEMO07";
            HKID_ItemCode_Mapping["H2003008"] = "DEMO08";
            HKID_ItemCode_Mapping["H2003009"] = "DEMO09";
            HKID_ItemCode_Mapping["H2003010"] = "DEMO10";
            HKID_ItemCode_Mapping["H2003011"] = "DEMO11";
            HKID_ItemCode_Mapping["H2003012"] = "DEMO12";
            HKID_ItemCode_Mapping["H2003013"] = "DEMO13";
            HKID_ItemCode_Mapping["H2003014"] = "DEMO14";
            HKID_ItemCode_Mapping["H2003015"] = "DEMO15";
            HKID_ItemCode_Mapping["H2003016"] = "DEMO16";
            HKID_ItemCode_Mapping["H2003017"] = "DEMO17";
            HKID_ItemCode_Mapping["H2003018"] = "DEMO18";
            HKID_ItemCode_Mapping["H2003019"] = "DEMO19";
            HKID_ItemCode_Mapping["H2003020"] = "DEMO20";
            HKID_ItemCode_Mapping["H2003021"] = "DEMO21";
            HKID_ItemCode_Mapping["H2003022"] = "DEMO22";
            HKID_ItemCode_Mapping["H2003023"] = "DEMO23";
            HKID_ItemCode_Mapping["H2003024"] = "DEMO24";
            HKID_ItemCode_Mapping["H2003025"] = "DEMO25";
        }

        public static Dictionary<string, string> ItemCode_HKID_Mapping { get; private set; }

        public static Dictionary<string, string> HKID_ItemCode_Mapping { get; private set; }
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
