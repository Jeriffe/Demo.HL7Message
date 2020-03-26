using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.WinForms.Lexers;
using NLog;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class FullWorkFlowControl : UserControl
    {
        private string HK_ID = string.Empty;
        private string CASE_NUMBER = string.Empty;

        private Loading loadForm;
        private HL7MessageParser_NTEC parser;
        private MainForm mainForm;
        MdsCheckFinalResult currentMdsResultForShow;
        public FullWorkFlowControl(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            bgWorkerMDSCheck.RunWorkerCompleted += bgWorkerMDSCheck_RunWorkerCompleted;

            loadForm = new Loading(mainForm);

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
            btnMDSCheckResult.Enabled = false;
        }

        private void cbxCaseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMDSControlseState(false);
        }
        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            if (Global.IsDirty)
            {
                Initialize();
            }
            if (cbxCaseNumber.Text.Trim() == string.Empty)
            {
                MSGBox.ShowBox("Invalid CaseNuber!", "SearchPatient", MSGLevel.Warning);
                return;
            }

            ResetPateitnReqRes();

            ResetMDSReqRes();

            ChangeSelectedTabPage(tbpPatient);
            // Start the asynchronous operation.
            bgWorker.RunWorkerAsync(cbxCaseNumber.Text.Trim());

            loadForm.ResizeView();
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
            var patientCache = FullCacheHK.PataientCache[caseNumber];

            if (patientCache == null)
            {
                MSGBox.ShowInfo("NO Patient!", "SearchPatient");

                this.BeginInvoke((MethodInvoker)(() => RefreshMDSControlseState(false)));

                return;
            }

            result.Patient = patientCache.PatientDemoEnquiry;
            result.Allergies = (patientCache.AlertProfileRes ?? new AlertProfileResult());
            result.Orders= patientCache.MedicationProfileRes;
            this.BeginInvoke((MethodInvoker)delegate
            {
                var HK_ID = result.Patient.Patient.HKID;

                RefreshMDSControlseState(true);

                if (RuleMappingHelper.HKID_ItemCode_Mapping.ContainsKey(HK_ID))
                {
                    var itemCodes = RuleMappingHelper.HKID_ItemCode_Mapping[HK_ID].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    cbxItemCodes.DataSource = itemCodes;
                }

                scintillaPatient.Text = XmlHelper.FormatXML(XmlHelper.XmlSerializeToString(result.Patient));
                scintillaProfiles.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Orders));
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
        }

        private void cbxItemCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void btnMDSCheck_Click(object sender, EventArgs e)
        {
            if (Global.IsDirty)
            {
                Initialize();
            }

            if (cbxItemCodes.Text.Trim() == string.Empty)
            {
                MSGBox.ShowBox("Invalid ItemCode!", "NDSCheck", MSGLevel.Warning);

                return;
            }

            ResetMDSReqRes();

            ChangeSelectedTabPage(tbpMDSCheck);

            bgWorkerMDSCheck.RunWorkerAsync(cbxItemCodes.Text.ToString());

            loadForm.ResizeView();
            loadForm.ShowDialog();
        }
        private void btnMDSCheckResult_Click(object sender, EventArgs e)
        {
            MdsCheckFinalResult source = currentMdsResultForShow;

            try
            {
                var dialog = new MDDCheckDialogBox(source, "Clinical Intervention");

                dialog.ShowDialog(this);
                //new MDSDialog(source).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgWorkerMDSCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var itemCode = e.Argument as string;

                var patientCache = FullCacheHK.PataientCache[CASE_NUMBER];

                currentMdsResultForShow = parser.MDSCheck(new InventoryObj { Billnum = itemCode, CommonName = itemCode },
                    CASE_NUMBER,
                    patientCache.PatientDemoEnquiry,
                    patientCache.AlertProfileRes);

                this.BeginInvoke((MethodInvoker)delegate
                {
                    try
                    {
                        if (FullCacheHK.PataientCache[CASE_NUMBER] != null)
                        {
                            if (FullCacheHK.PataientCache[CASE_NUMBER].MDSCache[itemCode] == null)
                            {
                                ResetMDSReqRes();

                                return;
                            }

                            var mds = FullCacheHK.PataientCache[CASE_NUMBER].MDSCache[itemCode];

                            if (mds.DrugMdsPropertyHqReq != null)
                            {
                                scintillaDrugMdsPropertyHqReq.Text = XmlHelper.XmlSerializeToString(mds.DrugMdsPropertyHqReq);
                                scintillaDrugMdsPropertyHqRes.Text = XmlHelper.XmlSerializeToString(mds.DrugMdsPropertyHqRes);
                            }

                            if (mds.PreparationReq != null)
                            {
                                scintillaDrugPreparationReq.Text = XmlHelper.XmlSerializeToString(mds.PreparationReq);
                                scintillaDrugPreparationRes.Text = XmlHelper.XmlSerializeToString(mds.PreparationRes);
                            }

                            if (mds.Res != null)
                            {
                                var requestXml = XmlHelper.XmlSerializeToString(mds.Req);
                                scintillaMdsCheckReq.Text = requestXml;

                                var resultJson = JsonHelper.ToJson(mds.Res);
                                scintillaMdsCheckRes.Text = JsonHelper.FormatJson(resultJson);
                            }
                        }

                        if (currentMdsResultForShow.HasMdsAlert)
                        {
                            btnMDSCheckResult.Enabled = true;
                        }
                        else
                        {
                            btnMDSCheckResult.Enabled = false;
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
            var soapService = new SoapParserSvc(Global.DrugMasterSoapUrl,Global.PreParationSoapUrl, Global.HospitalCode);
            var soapWSEService = new SoapWSEParserSvc(Global.PatientEnquirySoapUrl, Global.UserName, Global.Password, Global.HospitalCode);
            var restService = new RestParserSvc(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);

            parser = new HL7MessageParser_NTEC(soapService, soapWSEService, restService);

            scintillaPatient.FormatStyle(StyleType.Xml);
            scintillaProfiles.FormatJsonStyle();
            scintillaAlerts.FormatJsonStyle();

            scintillaMdsCheckRes.FormatJsonStyle();
            scintillaMdsCheckReq.FormatStyle(StyleType.Xml);

            scintillaDrugMdsPropertyHqReq.FormatStyle(StyleType.Xml);
            scintillaDrugMdsPropertyHqRes.FormatStyle(StyleType.Xml);

            scintillaDrugPreparationReq.FormatStyle(StyleType.Xml);
            scintillaDrugPreparationRes.FormatStyle(StyleType.Xml);
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
        private void ResetPateitnReqRes()
        {
            scintillaAlerts.Text = string.Empty;
            scintillaProfiles.Text = string.Empty;
            scintillaPatient.Text = string.Empty;
        }

        private void ResetMDSReqRes()
        {
            scintillaDrugMdsPropertyHqReq.Text = string.Empty;
            scintillaDrugMdsPropertyHqRes.Text = string.Empty;

            scintillaDrugPreparationReq.Text = string.Empty;
            scintillaDrugPreparationRes.Text = string.Empty;

            scintillaMdsCheckReq.Text = string.Empty;
            scintillaMdsCheckRes.Text = string.Empty;

            btnMDSCheckResult.Enabled = false;
        }

        public class EventResult
        {
            public Models.PatientDemoEnquiry Patient { get; set; }
            public MedicationProfileResult Orders { get; set; }
            public AlertProfileResult Allergies { get; set; }
        }
        private MdsCheckFinalResult InitalData()
        {
            var initSource = new MdsCheckFinalResult();

            initSource.DrugName = "ASPRIN";

            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("Allergy Checking",
                @"ASPRIN - Allergy history reported 
Clinical Manifestation: Rash: Urticaria 
Additional information: TEST 1 
Level of Certainty: Certain 
Use of ASPIRIN TABLET may result in allergic reaction."));

            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("G6PD Deficiency Contraindication Checking",
                @"ASPIRIN TABLET is contraindicated when Hemolytic Anemia from Pyruvate Kinase and G6PD Deficientcies, a condition related to G6PD Deficiency, exists."));

            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("Adverse Drug Reaction Checking",
                @"ASPIRIN - Adverse drug reaction hisotry reported 
Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn 
Level of Severity: Severe 
Use of ASPIRIN TABLET may result in adverse drug reaction."));



            var str = string.Format(@"ASPIRIN - Adverse drug reaction hisotry reported{0}Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn{0}Level of Severity: Severe", Environment.NewLine);


            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("JERIFFE TEST", str));

            return initSource;
        }
    }
}
