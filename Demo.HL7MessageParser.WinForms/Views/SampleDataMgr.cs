using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.WinForms.Lexers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class SampleDataMgr : Form
    {
        public SampleDataMgr()
        {
            InitializeComponent();
        }

        private void cbxCaseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearCache();

                var caseNumber = cbxCaseNumber.SelectedItem.ToString();

                LoadPatient(caseNumber);

                var patientOrg = SoapParserHelper.LoadSamplePatientDemoEnquiry(caseNumber, @"Data\PE");

                var hkId = patientOrg.Patient.HKID;

                if (RuleMappingHelper.HKID_ItemCode_Mapping.ContainsKey(hkId))
                {
                    var drugItemCode = RuleMappingHelper.HKID_ItemCode_Mapping[hkId];

                    var fileName = string.Format("{0}_{1}.xml", hkId, drugItemCode);

                    LoadDrugMaster(fileName);

                    LoadPreparation(fileName);
                }

                LoadMedicationProfile(caseNumber);

                LoadAlertProfile(hkId);

                LoadMDSCheckResult(hkId);
            }
            catch (Exception ex)
            {
                ex = ex;
            }

        }



        private void LoadMDSCheckResult(string hkId)
        {
            var file = Path.Combine(BASE_DIR, string.Format(@"MDS\{0}.json", hkId));

            var mdsCheckResult = JsonHelper.JsonToObjectFromFile<MDSCheckResult>(file);

            scintillaMdsCheckRes.Text = JsonHelper.FormatJson(JsonHelper.ToJson(mdsCheckResult));
        }

        private void LoadPreparation(string fileName)
        {
            var file = Path.Combine(BASE_DIR, string.Format(@"DM\getPreparation\{0}", fileName));

            var doc = XElement.Load(file);

            scintillaDrugPreparationRes.Text = XmlHelper.FormatXML(doc.ToString());
        }

        private void LoadDrugMaster(string fileName)
        {
            var file = Path.Combine(BASE_DIR, string.Format(@"DM\getDrugMdsPropertyHq\{0}", fileName));

            var doc = XElement.Load(file);

            scintillaDrugMdsPropertyHqRes.Text = XmlHelper.FormatXML(doc.ToString());
        }

        private void LoadAlertProfile(string hkId)
        {
            var file = Path.Combine(BASE_DIR, string.Format(@"AP\{0}.json", hkId));

            var alertProfile = JsonHelper.JsonToObjectFromFile<AlertProfileResult>(file);

            scintillaAlerts.Text = JsonHelper.FormatJson(JsonHelper.ToJson(alertProfile));

            currentUIData["scintillaAlerts"] = file;
        }

        private void LoadMedicationProfile(string hkId)
        {
            var file = Path.Combine(BASE_DIR, string.Format(@"MP\{0}.json", hkId));

            var medicationProfile = JsonHelper.JsonToObjectFromFile<MedicationProfileResult>(file);

            scintillaProfiles.Text = JsonHelper.FormatJson(JsonHelper.ToJson(medicationProfile));

            currentUIData["scintillaProfiles"] = file;

        }
        private void LoadPatient(string caseNumber)
        {
            var file = Path.Combine(BASE_DIR, string.Format(@"PE\{0}.xml", caseNumber));

            var doc = XElement.Load(file);

            scintillaPatient.Text = doc.ToString();

            scintillaPatient.Text = XmlHelper.FormatXML(doc.ToString());

            currentUIData["scintillaPatient"] = file;

        }

        string BASE_DIR = null;
        private void Initialize()
        {
            BASE_DIR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\");

            var files = Directory.GetFiles(Path.Combine(BASE_DIR, @"PE\"), "*.xml");

            cbxCaseNumber.DataSource = files.Select(o => new FileInfo(o).Name)
                                            .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                            .ToList();


        }

        private void SampleDataMgr_Load(object sender, EventArgs e)
        {
            scintillaPatient.FormatStyle(StyleType.Xml);

            scintillaProfiles.FormatJsonStyle();


            scintillaAlerts.FormatJsonStyle();

            scintillaDrugPreparationRes.FormatStyle(StyleType.Xml);

            scintillaDrugMdsPropertyHqRes.FormatStyle(StyleType.Xml);

            scintillaMdsCheckRes.FormatJsonStyle();

            scintillaAlerts.Text = string.Empty;
            scintillaProfiles.Text = string.Empty;
            scintillaPatient.Text = string.Empty;
            scintillaDrugMdsPropertyHqRes.Text = string.Empty;
            scintillaDrugPreparationRes.Text = string.Empty;
            scintillaMdsCheckRes.Text = string.Empty;

            btnSave.Enabled = false;

            Initialize();
        }

        private void ClearCache()
        {
            currentUIData["scintillaPatient"] = null;
            currentUIData["scintillaProfiles"] = null;
            currentUIData["scintillaAlerts"] = null;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        Dictionary<string, string> currentUIData = new Dictionary<string, string>();

        private void btnSave_Click(object sender, EventArgs e)
        {
            string file = currentUIData["scintillaPatient"];
            if (file != null)
            {
                var patientFullXmlStr = XmlHelper.FormatXML(scintillaPatient.Text);

                File.WriteAllText(file, patientFullXmlStr);

                //var mdsCheckTargetStr = JsonHelper.FormatJson(JsonHelper.ToJson(mdsCheckOrg));

                //var mdsCheckTargetFileName = Path.Combine(baseDir, string.Format(@"Demo\MDS\{0}.json", hkId));
                //File.WriteAllText(mdsCheckTargetFileName, mdsCheckTargetStr);
            }
            btnSave.Enabled = false;
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {

        }

        private void btnVerifyAll_Click(object sender, EventArgs e)
        {
            try
            {
                var files = Directory.GetFiles(Path.Combine(BASE_DIR, @"PE\"), "*.xml");

               var caseNumbers= files.Select(o => new FileInfo(o).Name)
                                                .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                                .ToList();

                foreach (var item in caseNumbers)
                {
                    var caseNumber = item.ToString();

                    if (!caseNumber.StartsWith("HN2020"))
                    {
                        continue;
                    }
                    var file = Path.Combine(BASE_DIR, string.Format(@"PE\{0}.xml", caseNumber));
                    var doc = XElement.Load(file);

                    var patientOrg = SoapParserHelper.LoadSamplePatientDemoEnquiry(caseNumber, @"Data\PE");

                    var hkId = patientOrg.Patient.HKID;

                    System.Diagnostics.Debug.WriteLine(string.Format("CASENUMBER:{0}", caseNumber));

                    System.Diagnostics.Debug.WriteLine(string.Format("HKID:{0}",hkId));

                    var medicationFile = Path.Combine(BASE_DIR, string.Format(@"MP\{0}.json", caseNumber));
                    var medicationProfile = JsonHelper.JsonToObjectFromFile<MedicationProfileResult>(medicationFile);

                    var alertFile = Path.Combine(BASE_DIR, string.Format(@"AP\{0}.json", hkId));
                    var alertProfile = JsonHelper.JsonToObjectFromFile<AlertProfileResult>(alertFile);

                    if (RuleMappingHelper.HKID_ItemCode_Mapping.ContainsKey(hkId))
                    {
                        var drugItemCode = RuleMappingHelper.HKID_ItemCode_Mapping[hkId];
                        var fileName = string.Format("{0}_{1}.xml", hkId, drugItemCode);

                        var drugMasterFile = Path.Combine(BASE_DIR, string.Format(@"DM\getDrugMdsPropertyHq\{0}", fileName));
                        var drugMasterRes = XElement.Load(drugMasterFile);

                        var paprationFile = Path.Combine(BASE_DIR, string.Format(@"DM\getPreparation\{0}", fileName));
                        var paprationRes = XElement.Load(paprationFile);

                        var mdsFile = Path.Combine(BASE_DIR, string.Format(@"MDS\{0}.json", hkId));
                        var mdsCheckRes = JsonHelper.JsonToObjectFromFile<MDSCheckResult>(mdsFile);
                    }

                }

                MessageBox.Show("Verify successfully......");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
