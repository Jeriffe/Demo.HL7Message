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
using System.Xml.Linq;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class DrugMasterControl : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string reqDir;

        private IHL7MessageParser hl7messageParser;

        GetPreparationRequest SelectedGetPreparationRequest = null;

        static DrugMasterControl()
        {
            reqDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\DM");
        }

        public DrugMasterControl(IHL7MessageParser hl7messageParser)
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                Initialize();
            }

            this.hl7messageParser = hl7messageParser;

            Initialize();
        }

        private void Initialize()
        {
            chxCustomDrugMdsReq.Checked = false;
            chxCustomPreparationReq.Checked = false;


            RefreshPreparationReqUI(false);

            RefreshCustomDrugMdsReqUI(false);


            try
            {
                var files = Directory.GetFiles(Path.Combine(reqDir, @"getDrugMdsPropertyHq\Req"), "*.xml");

                cbxCaseNumber.DataSource = files.Select(o => new FileInfo(o).Name)
                                                .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                                .ToList();



                var files2 = Directory.GetFiles(Path.Combine(reqDir, @"getPreparation\Req"), "*.xml");

                cbxIDrugItemCodes.DataSource = files2.Select(o => new FileInfo(o).Name)
                                                .Select(o => o.Substring(0, o.Length - ".xml".Length))
                                                .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            scintillaRes.Text = string.Empty;

            try
            {
                var str = scintillaReq.Text.Trim();
                GetDrugMdsPropertyHqRequest request = null;

                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    var argElement = XDocument.Load(ms).Descendants("arg0").First();

                    var arg0 = XmlHelper.XmlDeserialize<Arg>(argElement.ToString());

                    request = new GetDrugMdsPropertyHqRequest { Arg0 = arg0 };
                }

                var res = hl7messageParser.GetDrugMdsPropertyHq(request);

                var resStr = XmlHelper.XmlSerializeToString(res);

                scintillaRes.Text = XmlHelper.FormatXML(resStr);
                scintillaRes.FormatStyle(StyleType.Xml);
                scintillaRes.Focus();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);

                MessageBox.Show(ex.Message);
            }
        }
        private void cbxCaseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateReqXml();
        }
        private void chxCustomDrugMdsReq_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCustomDrugMdsReqUI(chxCustomDrugMdsReq.Checked);
        }

        private void GenerateReqXml()
        {
            scintillaReq.Focus();

            try
            {
                var file = Path.Combine(reqDir, string.Format(@"getDrugMdsPropertyHq\Req\{0}.xml", cbxCaseNumber.SelectedItem.ToString()));

                var doc = XDocument.Load(file);

                XNamespace x = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace x2 = "http://biz.dms.pms.model.ha.org.hk/";

                var element = doc.Descendants(x + "Body").Descendants(x2 + "getDrugMdsPropertyHq");

                scintillaReq.Text = XmlHelper.FormatXML(element.First().ToString());

                scintillaReq.FormatStyle(StyleType.Xml);
            }
            catch (Exception ex)
            {

            }
        }
        private void RefreshCustomDrugMdsReqUI(bool enabled)
        {
            cbxCaseNumber.Enabled = !enabled;
            scintillaReq.Enabled = enabled;
        }

        private void btnRequestPreparation_Click(object sender, EventArgs e)
        {

            scintillaResPreparation.Text = string.Empty;

            try
            {
                var str = scintillaReqPreparation.Text.Trim();
                GetPreparationRequest request = null;

                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    var argElement = XDocument.Load(ms);

                    request = XmlHelper.XmlDeserialize<GetPreparationRequest>(argElement.ToString());
                }

                var res = hl7messageParser.GetPreparation(request);

                var resStr = XmlHelper.XmlSerializeToString(res);

                scintillaResPreparation.Focus();

                scintillaResPreparation.Text = XmlHelper.FormatXML(resStr);
                scintillaResPreparation.FormatStyle(StyleType.Xml);

            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);

                MessageBox.Show(ex.Message);
            }

        }
        private void cbxIDrugItemCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateReqPreparationXml();
        }
        private void chxCustomPreparationReq_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreparationReqUI(chxCustomPreparationReq.Checked);
        }

        private void GenerateReqPreparationXml()
        {
            scintillaReqPreparation.Focus();

            try
            {
                var file = Path.Combine(reqDir, string.Format(@"getPreparation\Req\{0}.xml", cbxIDrugItemCodes.SelectedItem.ToString()));

                var doc = XDocument.Load(file);
                XNamespace x = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace x2 = "http://biz.dms.pms.model.ha.org.hk/";

                var element = doc.Descendants(x + "Body").Descendants(x2 + "getPreparation").First();


                scintillaReqPreparation.Text = XmlHelper.FormatXML(element.ToString());
                scintillaReqPreparation.FormatStyle(StyleType.Xml);
            }
            catch (Exception ex)
            {

            }
        }
        private void RefreshPreparationReqUI(bool enabled)
        {
            cbxIDrugItemCodes.Enabled = !enabled;
            scintillaReqPreparation.Enabled = enabled;
        }

    }
}
