using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.HL7MessageParser.WinForms.Lexers;
using Demo.HL7MessageParser.Common;

using Microsoft.Web.Services3.Security.Tokens;
using System.Net;
using System.IO;
using Demo.HL7MessageParser.WebProxy;
using NLog;
using System.Configuration;
using Demo.HL7MessageParser.Models;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class FullWorkFlowControl : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string HK_ID = string.Empty;
        private string CASE_NUMBER = string.Empty;

        private Loading loadForm;
        private HL7MessageParser_NTEC parser;
        private MainForm mainForm;
        public FullWorkFlowControl(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            bgWorkerMDSCheck.RunWorkerCompleted += bgWorkerMDSCheck_RunWorkerCompleted;

            loadForm = new Loading
            {
                Width = mainForm.Width,
                Height = mainForm.Height
            };

            Initialize();
        }

        private void HL7MessageParserFormTest_Load(object sender, EventArgs e)
        {
            var patientsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/PE/");

            var files = Directory.GetFiles(patientsDir, "*.xml");

            cbxCaseNumber.DataSource = files.Select(o => new FileInfo(o).Name)
                                            .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                            .ToList();

            btnMDSCheck.Enabled = false;
        }

        private void cbxCaseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMDSControlseState(false);
        }
        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (Global.IsDirty)
            {
                Initialize();
            }

            scintillaAlerts.Text = string.Empty;
            scintillaProfiles.Text = string.Empty;
            scintillaPatient.Text = string.Empty;
            scintillaMdsCheckReq.Text = string.Empty;
            scintillaMdsCheckRes.Text = string.Empty;

            ChangeSelectedTabPage(tbpPatient);
            // Start the asynchronous operation.
            bgWorker.RunWorkerAsync(cbxCaseNumber.Text.Trim());



            loadForm.ShowDialog();
        }
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string errorStr = null;

            var result = new EventResult();
            e.Result = result;

            var caseNumber = e.Argument as string;
            var AccountNumber = parser.SearchRemotePatient(caseNumber, ref errorStr);

            if (!string.IsNullOrEmpty(errorStr))
            {
                this.BeginInvoke((MethodInvoker)(() => RefreshMDSControlseState(false)));

                return;
            }

            CASE_NUMBER = caseNumber.Trim().ToUpper();
            var patientCache = Cache_HK.PataientCache[caseNumber];

            result.Patient = patientCache.PatientDemoEnquiry;
            result.Orders = (patientCache.MedicationProfileRes ?? new MedicationProfileResult());
            result.Allergies = (patientCache.AlertProfileRes ?? new AlertProfileResult());

            this.BeginInvoke((MethodInvoker)delegate
            {
                var HK_ID = result.Patient.Patient.HKID;
                if (RuleMappingHelper.HKID_ItemCode_Mapping.ContainsKey(HK_ID))
                {
                    RefreshMDSControlseState(true);

                    var itemCodes = RuleMappingHelper.HKID_ItemCode_Mapping[HK_ID].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    cbxItemCodes.DataSource = itemCodes;
                }

                scintillaPatient.FormatStyle(StyleType.Xml);
                scintillaPatient.Text = XmlHelper.FormatXML(XmlHelper.XmlSerializeToString(result.Patient));

                scintillaProfiles.FormatJsonStyle();
                scintillaProfiles.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Orders));

                scintillaAlerts.FormatJsonStyle();
                scintillaAlerts.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Allergies));
            });
        }
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (loadForm != null)
            {
                loadForm.Close();
            }

            //如果用户取消了当前操作就关闭窗口。
            if (e.Cancelled)
            {
                return;
            }

            //计算过程中的异常会被抓住，在这里可以进行处理。
            if (e.Error != null)
            {
                if (e.Error is AMException)
                {
                    var amex = e.Error as AMException;

                    MessageBox.Show(string.Format("{0}-{1}", amex.HttpStatusCode, amex.Message));
                }
                else
                {
                    MessageBox.Show(e.Error.Message);
                }

                return;
            }
            /*

            if (e.Result is EventResult)
            {
                var result = e.Result as EventResult;
                scintillaPatient.FormatStyle(StyleType.Xml);
                scintillaPatient.Text = XmlHelper.FormatXML(XmlHelper.XmlSerializeToString(result.PatientVisit));

                scintillaProfiles.FormatJsonStyle();
                scintillaProfiles.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Orders));

                scintillaAlerts.FormatJsonStyle();
                scintillaAlerts.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Allergies));
            }*/
        }

        private void cbxItemCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void btnMDSCheck_Click(object sender, EventArgs e)
        {
            ChangeSelectedTabPage(tbpMDSCheck);

            // Start the asynchronous operation.
            bgWorkerMDSCheck.RunWorkerAsync(cbxItemCodes.SelectedItem.ToString());

            loadForm.ShowDialog();
        }
        private void bgWorkerMDSCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var itemCode = e.Argument as string;

                var patientCache = Cache_HK.PataientCache[CASE_NUMBER];

                var result = parser.MDSCheck(itemCode, patientCache.PatientDemoEnquiry, patientCache.AlertProfileRes);

                var resultJson = JsonHelper.ToJson(result);

                this.BeginInvoke((MethodInvoker)delegate
                {
                    try
                    {
                        scintillaMdsCheckRes.FormatJsonStyle();
                        scintillaMdsCheckRes.Text = JsonHelper.FormatJson(resultJson);

                        if (result.IsPerformMDSCheck)
                        {
                            var request = Cache_HK.MDS_CheckCache[CASE_NUMBER].Req;
                            var requestXml = XmlHelper.XmlSerializeToString(request);
                            scintillaMdsCheckReq.FormatStyle(StyleType.Xml);
                            scintillaMdsCheckReq.Text = requestXml;
                        }
                    }
                    catch (Exception ex)
                    {

                        ex = ex;
                    }

                });
            }
            catch (Exception EX)
            {
                EX = EX;
            }

        }

        private void bgWorkerMDSCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (loadForm != null)
            {
                loadForm.Close();
            }

            //如果用户取消了当前操作就关闭窗口。
            if (e.Cancelled)
            {
                return;
            }

            //计算过程中的异常会被抓住，在这里可以进行处理。
            if (e.Error != null)
            {
                if (e.Error is AMException)
                {
                    var amex = e.Error as AMException;

                    MessageBox.Show(string.Format("{0}-{1}", amex.HttpStatusCode, amex.Message));
                }
                else
                {
                    MessageBox.Show(e.Error.Message);
                }

                return;
            }
        }

        private void Initialize()
        {
            var soapService = new SoapParserSvc(Global.DrugMasterSoapUrl, Global.HospitalCode);

            var soapWSEService = new SoapWSEParserSvc(Global.PatientEnquirySoapUrl, Global.UserName, Global.Password, Global.HospitalCode);

            var restService = new RestParserSvc(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);


            parser = new HL7MessageParser_NTEC(soapService, soapWSEService, restService);
        }

        private void ChangeSelectedTabPage(TabPage tabPage)
        {
            if (tabControl.SelectedTab != tabPage)
            {
                tabControl.SelectedTab = tabPage;
            }
        }

        private void RefreshMDSControlseState(bool enable)
        {
            btnMDSCheck.Enabled = enable;
            cbxItemCodes.Enabled = enable;
            if (!enable)
            {
                cbxItemCodes.DataSource = null;
            }
        }

        public class EventResult
        {
            public Models.PatientDemoEnquiry Patient { get; set; }
            public MedicationProfileResult Orders { get; set; }
            public AlertProfileResult Allergies { get; set; }
        }

        private void btnMDSCheckResult_Click(object sender, EventArgs e)
        {
            MDSource source = InitalData();

            try
            {
                var dialog = new MDDialogBox(source, "MDSCheck",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MDDialogBox.MessageColor.Red);

                dialog.ShowDialog(this);
                //new MDSDialog(source).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private MDSource InitalData()
        {
            var initSource = new MDSource();
            initSource.DrugName = "ASPRIN";
            initSource.AddtionInfo = "NONE";
            initSource.listC.Add(new Category("Allergy Checking",
                @"ASPRIN - Allergy history reported 
Clinical Manifestation: Rash: Urticaria 
Additional information: TEST 1 
Level of Certainty: Certain 
Use of ASPIRIN TABLET may result in allergic reaction."));

            initSource.listC.Add(new Category("G6PD Deficiency Contraindication Checking",
                @"ASPIRIN TABLET is contraindicated when Hemolytic Anemia from Pyruvate Kinase and G6PD Deficientcies, a condition related to G6PD Deficiency, exists."));

            initSource.listC.Add(new Category("Adverse Drug Reaction Checking",
                @"ASPIRIN - Adverse drug reaction hisotry reported 
Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn 
Level of Severity: Severe 
Use of ASPIRIN TABLET may result in adverse drug reaction."));



            var str = string.Format(@"ASPIRIN - Adverse drug reaction hisotry reported{0}Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn{0}Level of Severity: Severe", Environment.NewLine);


            initSource.listC.Add(new Category("JERIFFE TEST", str));

            return initSource;
        }



    }
}
