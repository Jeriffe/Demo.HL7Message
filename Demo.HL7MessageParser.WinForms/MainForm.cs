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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Global.RefreshConfigValues();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                case Keys.F2:
                    {
                        new HL7MessageParserFormTest().ShowDialog();
                        return true;
                    }
                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


            var patientVisitParser = new SoapPatientVisitParser(Global.PatientEnquirySoapUrl, Global.UserName, Global.Password, Global.HospitalCode);

            var profileService = new ProfileRestService(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);

            var drugMasterSoapService = new DrugMasterSoapService(Global.DrugMasterSoapUrl);

            var mdsCheckRestService = new MDSCheckRestService(Global.MDSCheckRestUrl);

            var parser = new HL7MessageParser_NTEC(patientVisitParser, profileService, drugMasterSoapService, mdsCheckRestService);

            TabControl tc = new TabControl { Dock = DockStyle.Fill };

            tc.TabPages.Add(new TabPage { Name = "tbPatientDemographicControl", Text = "PatientDemographic" });
            tc.TabPages["tbPatientDemographicControl"].Controls.Add(new PatientDemographicControl { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbMedicationProfileControl", Text = "MedicationProfile" });
            tc.TabPages["tbMedicationProfileControl"].Controls.Add(new MedicationProfileControl { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbAlertProfileControl", Text = "AlertProfile" });
            tc.TabPages["tbAlertProfileControl"].Controls.Add(new AlertProfileParserControl { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbPatientOrderAlertsControl", Text = "Patient-Order-Allergy" });
            tc.TabPages["tbPatientOrderAlertsControl"].Controls.Add(new PatientOrderAlertControl { Dock = DockStyle.Fill });


            tc.TabPages.Add(new TabPage { Name = "tbDrugMasterControl", Text = "DrugMaster" });
            tc.TabPages["tbDrugMasterControl"].Controls.Add(new DrugMasterControl(parser) { Dock = DockStyle.Fill });

            this.Controls.Add(tc);
            this.Controls.Add(this.menuStrip1);

            Global.IsDirty = false;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SystemConfig().ShowDialog();
        }
    }
}
