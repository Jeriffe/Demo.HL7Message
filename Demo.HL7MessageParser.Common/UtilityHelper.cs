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
            ItemCode_HKID_Mapping["DEMO26"] = "H2003026_DEMO26";
            ItemCode_HKID_Mapping["DEMO27"] = "H2003027_DEMO27";
            ItemCode_HKID_Mapping["DEMO28"] = "H2003028_DEMO28";
            ItemCode_HKID_Mapping["DEMO29"] = "H2003029_DEMO29";
            ItemCode_HKID_Mapping["DEMO30"] = "H2003030_DEMO30";
            ItemCode_HKID_Mapping["DEMO31"] = "H2003031_DEMO31";
            ItemCode_HKID_Mapping["DEMO32"] = "H2003032_DEMO32";
            ItemCode_HKID_Mapping["DEMO33"] = "H2003033_DEMO33";
            ItemCode_HKID_Mapping["DEMO34"] = "H2003034_DEMO34";
            ItemCode_HKID_Mapping["DEMO35"] = "H2003035_DEMO35";
            ItemCode_HKID_Mapping["DEMO36"] = "H2003036_DEMO36";
            ItemCode_HKID_Mapping["DEMO37"] = "H2003037_DEMO37";
            ItemCode_HKID_Mapping["DEMO38"] = "H2003038_DEMO38";



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
            HKID_ItemCode_Mapping["H2003026"] = "DEMO26";
            HKID_ItemCode_Mapping["H2003027"] = "DEMO27";
            HKID_ItemCode_Mapping["H2003028"] = "DEMO28";
            HKID_ItemCode_Mapping["H2003029"] = "DEMO29";
            HKID_ItemCode_Mapping["H2003030"] = "DEMO30";
            HKID_ItemCode_Mapping["H2003031"] = "DEMO31";
            HKID_ItemCode_Mapping["H2003032"] = "DEMO32";
            HKID_ItemCode_Mapping["H2003033"] = "DEMO33";
            HKID_ItemCode_Mapping["H2003034"] = "DEMO34";
            HKID_ItemCode_Mapping["H2003035"] = "DEMO35";
            HKID_ItemCode_Mapping["H2003036"] = "DEMO36";
            HKID_ItemCode_Mapping["H2003037"] = "DEMO37";
            HKID_ItemCode_Mapping["H2003038"] = "DEMO38";
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
