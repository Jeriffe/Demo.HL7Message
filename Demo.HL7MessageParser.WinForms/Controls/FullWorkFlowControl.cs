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
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
            var patientCache = FullCacheHK.PataientCache[caseNumber];

            result.Patient = patientCache.PatientDemoEnquiry;
            //result.Orders = (patientCache.MedicationProfileRes ?? new MedicationProfileResult());
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
            if (FullCacheHK.DrugMasterCache == null) FullCacheHK.DrugMasterCache = new CacheHK<DrugMasterCache>();
            if (FullCacheHK.MDS_CheckCache == null) FullCacheHK.MDS_CheckCache = new CacheHK<MDSCheckResultCache>();
            if (FullCacheHK.PataientCache == null) FullCacheHK.PataientCache = new CacheHK<Patient_AlertProfile>();
            // Start the asynchronous operation.
            bgWorkerMDSCheck.RunWorkerAsync(cbxItemCodes.SelectedItem.ToString());

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

                currentMdsResultForShow = parser.MDSCheck(new InventoryObj { Billnum = itemCode, CommonName = itemCode }, CASE_NUMBER, patientCache.PatientDemoEnquiry, patientCache.AlertProfileRes);

                this.BeginInvoke((MethodInvoker)delegate
                {
                    try
                    {
                        if (FullCacheHK.PataientCache[CASE_NUMBER] != null)
                        {
                            if (FullCacheHK.PataientCache[CASE_NUMBER].MDSCheck != null)
                            {
                                var mdsResponse = FullCacheHK.PataientCache[CASE_NUMBER].MDSCheck.Res;
                                var resultJson = JsonHelper.ToJson(mdsResponse);
                                scintillaMdsCheckRes.Text = JsonHelper.FormatJson(resultJson);
                            }
                            else
                            {
                                scintillaMdsCheckRes.Text = "ERROR";
                            }

                            DrugMasterCache drugMaserCache = FullCacheHK.PataientCache[CASE_NUMBER].DrugMasterCache;
                            if (drugMaserCache != null)
                            {
                                scintillaDrugMdsPropertyHqReq.Text = XmlHelper.XmlSerializeToString(drugMaserCache.DrugMdsPropertyHqReq);
                                scintillaDrugMdsPropertyHqRes.Text = XmlHelper.XmlSerializeToString(drugMaserCache.DrugMdsPropertyHqRes);

                                scintillaDrugPreparationReq.Text = XmlHelper.XmlSerializeToString(drugMaserCache.PreparationReq);
                                scintillaDrugPreparationRes.Text = XmlHelper.XmlSerializeToString(drugMaserCache.PreparationRes);
                            }
                            else
                            {
                                scintillaDrugMdsPropertyHqReq.Text = "ERROR";
                                scintillaDrugMdsPropertyHqRes.Text = "ERROR";


                                scintillaDrugPreparationReq.Text = "ERROR";
                                scintillaDrugPreparationRes.Text = "ERROR";
                            }
                        }

                        if (FullCacheHK.PataientCache[CASE_NUMBER] != null)
                        {
                            if (FullCacheHK.PataientCache[CASE_NUMBER].MDSCheck != null)
                            {
                                var request = FullCacheHK.PataientCache[CASE_NUMBER].MDSCheck.Req;
                                var requestXml = XmlHelper.XmlSerializeToString(request);
                                scintillaMdsCheckReq.FormatStyle(StyleType.Xml);
                                scintillaMdsCheckReq.Text = requestXml;

                                var resJson = JsonHelper.ToJson(request);
                                scintillaMdsCheckReq.FormatStyle(StyleType.Xml);
                                scintillaMdsCheckReq.Text = requestXml;
                            }
                            else
                            {
                                scintillaMdsCheckReq.Text = "no request";
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

            scintillaMdsCheckRes.FormatJsonStyle();
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
