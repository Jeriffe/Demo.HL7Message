﻿using System;
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
        private Loading loadForm;
        private HL7MessageParser_NTEC parser;

        public FullWorkFlowControl()
        {
            InitializeComponent();

            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            bgWorkerMDSCheck.RunWorkerCompleted += bgWorkerMDSCheck_RunWorkerCompleted;
            loadForm = new Loading { Width = this.Width, Height = this.Height };
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
            var result = new EventResult();
            e.Result = result;

            var caseNumber = e.Argument as string;

            var pd = parser.GetPatientEnquiry(caseNumber);

            if (pd != null)
            {
                result.PatientVisit = pd;

                this.BeginInvoke((MethodInvoker)delegate
                {
                    HK_ID = pd.Patient.HKID;

                    if (RuleMappingHelper.HKID_ItemCode_Mapping.ContainsKey(HK_ID))
                    {
                        RefreshMDSControlseState(true);

                        cbxItemCodes.DataSource = RuleMappingHelper.HKID_ItemCode_Mapping[HK_ID].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    scintillaPatient.FormatStyle(StyleType.Xml);
                    scintillaPatient.Text = XmlHelper.FormatXML(XmlHelper.XmlSerializeToString(result.PatientVisit));
                });
                var orders = parser.GetMedicationProfiles(caseNumber);
                result.Orders = (orders ?? new MedicationProfileResult());
                this.BeginInvoke((MethodInvoker)delegate
                {
                    scintillaProfiles.FormatJsonStyle();
                    scintillaProfiles.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Orders));
                });

                var alertInputParm = new Models.AlertInputParm
                {
                    PatientInfo = new PatientInfo
                    {
                        Hkid = pd.Patient.HKID,
                        Name = pd.Patient.Name,
                        Sex = pd.Patient.Sex,
                        Dob = pd.Patient.DOB,
                        Cccode1 = pd.Patient.CCCode1,
                        Cccode2 = pd.Patient.CCCode2,
                        Cccode3 = pd.Patient.CCCode3,
                        Cccode4 = pd.Patient.CCCode4,
                        Cccode5 = pd.Patient.CCCode5,
                        Cccode6 = pd.Patient.CCCode6
                    },

                    UserInfo = new UserInfo
                    {
                        HospCode = Global.HospitalCode,
                        LoginId = Global.LoginId
                    },
                    SysInfo = new SysInfo
                    {
                        WsId = UtilityHelper.GetLoalIPAddress(),
                        SourceSystem = Global.SourceSystem
                    },
                    Credentials = new Credentials
                    {
                        AccessCode = Global.AccessCode
                    }
                };
                var allergys = parser.GetAlertProfiles(alertInputParm);
                result.Allergies = (allergys ?? new AlertProfileResult());
                this.BeginInvoke((MethodInvoker)delegate
                {

                    scintillaAlerts.FormatJsonStyle();
                    scintillaAlerts.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result.Allergies));
                });
            }
            else
            {
                this.BeginInvoke((MethodInvoker)(() => RefreshMDSControlseState(false)));
            }
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
            var itemCode = e.Argument as string;

            var patient = Cache_HK.PataientCache[HK_ID].PatientDemoEnquiry;
            var alertProfile = Cache_HK.PataientCache[HK_ID].AlertProfileRes;

            var result = parser.MDSCheck(itemCode, patient, alertProfile);

            var resultJson = JsonHelper.ToJson(result);


            this.BeginInvoke((MethodInvoker)delegate
            {
                scintillaMdsCheckRes.FormatJsonStyle();
                scintillaMdsCheckRes.Text = JsonHelper.FormatJson(resultJson);

                if (result.IsPerformMDSCheck)
                {
                    var request = Cache_HK.MDS_CheckCache[HK_ID].Req;
                    var requestXml = XmlHelper.XmlSerializeToString(request);
                    scintillaMdsCheckReq.FormatStyle(StyleType.Xml);
                    scintillaMdsCheckReq.Text = requestXml;
                }
            });
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
            var soapService = new SoapParserSvc(Global.DrugMasterSoapUrl, Global.UserName, Global.Password, Global.HospitalCode);

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
            public Models.PatientDemoEnquiry PatientVisit { get; set; }
            public MedicationProfileResult Orders { get; set; }
            public AlertProfileResult Allergies { get; set; }
        }
    }
}
