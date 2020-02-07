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
    public partial class PatientOrderAlertControl : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private HL7MessageParser_NTEC parser;
        public PatientOrderAlertControl()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);

            var patientVisitParser = new SoapPatientVisitParser(Global.SoapUri, Global.UserName, Global.Password, Global.HospitalCode);

            var medicationProfileParser = new JSONMedicationProfileParser(Global.RestUri, Global.ClientSecret, Global.HospitalCode);

            var allergiesParser = new JSONIAlertProfileParser(Global.RestUri, Global.ClientSecret, Global.ClientId, Global.HospitalCode);

            parser = new HL7MessageParser_NTEC(patientVisitParser, medicationProfileParser, allergiesParser);
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            scintillaAlerts.Text = string.Empty;
            scintillaProfiles.Text = string.Empty;
            scintillaPatient.Text = string.Empty;

            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync(cbxCaseNumber.Text.Trim());

            loadForm = new Loading { Width = this.Width, Height = this.Height };

            loadForm.ShowDialog();
        }

        private void HL7MessageParserFormTest_Load(object sender, EventArgs e)
        {
            var patientsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/PE/");

            var files = Directory.GetFiles(patientsDir, "*.xml");

            cbxCaseNumber.DataSource = files.Select(o => new FileInfo(o).Name)
                                            .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                            .ToList();

            //  cbxCaseNumber.DataSource = new string[] { "HN03191100Y", "HN17000256S", "HN18001140Y", "HN170002512", "HN170002520", };
        }

        Loading loadForm;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var result = new EventResult();
            e.Result = result;

            var caseNumber = e.Argument as string;

            var pd = parser.GetPatient(caseNumber);

            if (pd != null)
            {
                result.PatientVisit = pd;
                this.BeginInvoke((MethodInvoker)delegate
                {
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
                        WsId = GetCurrentStationIP(),
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
        }
        private static string GetCurrentStationIP()
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        public class EventResult
        {
            public Models.PatientDemoEnquiry PatientVisit { get; set; }
            public MedicationProfileResult Orders { get; set; }
            public AlertProfileResult Allergies { get; set; }
        }
    }
}
