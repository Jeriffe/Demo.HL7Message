using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class SystemConfig : Form
    {
        public SystemConfig()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            txtPatientEnquirySoapUrl.Text = Global.PatientEnquirySoapUrl;
            txtProfileRestUrl.Text = Global.ProfileRestUrl;
            txtDrugMasterUrl.Text = Global.DrugMasterSoapUrl;
            txtMDSCheckRestUrl.Text = Global.MDSCheckRestUrl;

            txtUserName.Text = Global.UserName;
            txtPassword.Text = Global.Password;
            txtClientSecret.Text = Global.ClientSecret;
            txtAccessCode.Text = Global.AccessCode;
            txtHospitalCode.Text = Global.HospitalCode;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateConfigValues();

            Global.RefreshConfigValues();

            DialogResult = DialogResult.OK;
        }
        public void UpdateConfigValues()
        {
            Utility.SetConfigValue(Global.CONFIG_KEY_PROFILERESTURI, txtProfileRestUrl.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_PATIENTENQUIRYSOAPURI, txtPatientEnquirySoapUrl.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_DRUGMASTERSOAPURL, txtDrugMasterUrl.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_MDSCHECKRESTURL, txtMDSCheckRestUrl.Text.Trim()); 
            
            Utility.SetConfigValue(Global.CONFIG_KEY_TOKEN_USERNAME, txtUserName.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_TOKEN_PASSWORD, txtPassword.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_CLIENT_SECRET, txtClientSecret.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_HOSPITALCODE, txtHospitalCode.Text.Trim());
            Utility.SetConfigValue(Global.CONFIG_KEY_ACCESSCODE, txtAccessCode.Text.Trim());

        }


        private void SystemConfig_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
