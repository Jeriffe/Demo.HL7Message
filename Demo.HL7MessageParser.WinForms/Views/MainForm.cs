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
            if (keyData == Keys.F5)
            {
                new SampleDataMgr().ShowDialog();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            var soapWSEService = new SoapWSEParserSvc(Global.PatientEnquirySoapUrl, Global.UserName, Global.Password, Global.HospitalCode);

            var soapService = new SoapParserSvc(Global.DrugMasterSoapUrl, Global.PreParationSoapUrl, Global.HospitalCode);


            var restService = new RestParserSvc(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);

            var hl7messageParser = new HL7MessageParser_NTEC(soapService, soapWSEService, restService);

            TabControl tc = new TabControl { Dock = DockStyle.Fill };

            tc.TabPages.Add(new TabPage { Name = "tbPatientDemographicControl", Text = "PatientDemographic" });
            tc.TabPages["tbPatientDemographicControl"].Controls.Add(new PatientDemographicControl { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbMedicationProfileControl", Text = "MedicationProfile" });
            tc.TabPages["tbMedicationProfileControl"].Controls.Add(new MedicationProfileControl { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbAlertProfileControl", Text = "AlertProfile" });
            tc.TabPages["tbAlertProfileControl"].Controls.Add(new AlertProfileParserControl { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbDrugMasterControl", Text = "DrugMaster" });
            tc.TabPages["tbDrugMasterControl"].Controls.Add(new DrugMasterControl(this) { Dock = DockStyle.Fill });

            tc.TabPages.Add(new TabPage { Name = "tbMDSCheckControl", Text = "MDSChecker" });
            tc.TabPages["tbMDSCheckControl"].Controls.Add(new MDSCheckControl(this) { Dock = DockStyle.Fill });


            tc.TabPages.Add(new TabPage { Name = "tbFullWorkFlowControl", Text = "Full Work Flow" });
            tc.TabPages["tbFullWorkFlowControl"].Controls.Add(new FullWorkFlowControl(this) { Dock = DockStyle.Fill });

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
