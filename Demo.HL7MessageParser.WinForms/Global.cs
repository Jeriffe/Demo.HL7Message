using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.HL7MessageParser.WinForms
{
    public static class Global
    {
        public static string ProfileRestUrl { get; set; }

        public static string PatientEnquirySoapUrl { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string HospitalCode { get; set; }
        public static string ClientSecret { get; set; }

        public static string AccessCode { get; set; }
        public static string ClientId { get; private set; }
        public static string SourceSystem { get; internal set; }
        public static string LoginId { get; internal set; }

        public static string DrugMasterSoapUrl { get; internal set; }
        public static string MDSCheckRestUrl { get; internal set; }
        public static bool IsDirty { get; internal set; }

        public const string CONFIG_KEY_PROFILERESTURI = "ProfileRestUri";
        public const string CONFIG_KEY_PATIENTENQUIRYSOAPURI = "PatientEnquirySoapUri";
        public const string CONFIG_KEY_DRUGMASTERSOAPURL = "DrugMasterSoapUrl";
        public const string CONFIG_KEY_MDSCHECKRESTURL = "MDSCheckRestUrl";
        public const string CONFIG_KEY_TOKEN_USERNAME = "Token_Username";
        public const string CONFIG_KEY_TOKEN_PASSWORD = "Token_Password";
        public const string CONFIG_KEY_CLIENT_SECRET = "client_secret";
        public const string CONFIG_KEY_HOSPITALCODE = "patHospCode";
        public const string CONFIG_KEY_ACCESSCODE = "AccessCode";
        public const string CONFIG_KEY_LOGINID = "LoginId";
        public const string CONFIG_KEY_SOURCESYSTEM = "SourceSystem";


        public static void RefreshConfigValues()
        {
            ProfileRestUrl = Utility.GetConfigValue(CONFIG_KEY_PROFILERESTURI);

            PatientEnquirySoapUrl = ConfigurationManager.AppSettings[CONFIG_KEY_PATIENTENQUIRYSOAPURI].ToString();

            UserName = Utility.GetConfigValue(CONFIG_KEY_TOKEN_USERNAME);

            Password = Utility.GetConfigValue(CONFIG_KEY_TOKEN_PASSWORD);

            ClientSecret = Utility.GetConfigValue(CONFIG_KEY_CLIENT_SECRET);

            HospitalCode = Utility.GetConfigValue(CONFIG_KEY_HOSPITALCODE);

            AccessCode = Utility.GetConfigValue(CONFIG_KEY_ACCESSCODE);

            LoginId = Utility.GetConfigValue(CONFIG_KEY_LOGINID);

            SourceSystem = Utility.GetConfigValue(CONFIG_KEY_SOURCESYSTEM);

            DrugMasterSoapUrl = Utility.GetConfigValue(CONFIG_KEY_DRUGMASTERSOAPURL);

            MDSCheckRestUrl = Utility.GetConfigValue(CONFIG_KEY_MDSCHECKRESTURL);

            ClientId = "AccessCenter";

            IsDirty = true;

        }

        //public static void UpdateConfigValues()
        //{
        //    Utility.SetConfigValue(Global.CONFIG_KEY_PROFILERESTURI, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_PATIENTENQUIRYSOAPURI, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_TOKEN_USERNAME, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_TOKEN_PASSWORD, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_CLIENT_SECRET, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_PATHOSPCODE, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_ACCESSCODE, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_LOGINID, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_SOURCESYSTEM, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_DRUGMASTERSOAPURL, txtPatientDemographicUrl.Text.Trim());
        //    Utility.SetConfigValue(Global.CONFIG_KEY_MDSCHECKRESTURL, txtPatientDemographicUrl.Text.Trim());
        //}
    }
}
