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
using System.Xml;
using System.IO;
using System.Xml.Linq;
using Demo.HL7MessageParser.Models;
using NLog;
using System.Configuration;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class AlertProfileParserControl : UserControl
    {
        private List<string> hkIds = new List<string>();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        IRestParserSvc restService;
        DataLoader<AlertProfileResult> dataLoader;

        public AlertProfileParserControl()
        {
            InitializeComponent();



            InitializeService();
        }

        private void InitializeService()
        {
            restService = new RestParserSvc(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);
        }
        private void AlertProfileParserControl_Load(object sender, EventArgs e)
        {
            Initialize();
        }
       
        private void btnSend_Click(object sender, EventArgs e)
        {
            scintillaRes.Text = string.Empty;
            try
            {
                if (Global.IsDirty)
                {
                    InitializeService();
                }

                var inputParam = XmlHelper.XmlDeserialize<AlertInputParm>(scintillaReq.Text.Trim());

                dataLoader.LoadDataAsync(restService.GetAlertProfile, inputParam);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxHKId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hkId = cbxHKId.Text.Trim();
            if (!string.IsNullOrEmpty(hkId))
            {
                scintillaReq.Focus();

                scintillaReq.Text = XmlHelper.FormatXML(XmlFromFile(hkId));

                scintillaReq.FormatStyle(StyleType.Xml);
            }
        }
        private void cbxHKId_TextChanged(object sender, EventArgs e)
        {
            var hkId = cbxHKId.Text.Trim();
            if (!string.IsNullOrEmpty(hkId))
            {
                if (!hkIds.Contains(hkId.ToUpper()))
                {
                    var str = @"<alertInputParm>
    <patientInfo>
        <hkid>{0}</hkid>
        <name>Cook</name>
        <dob>19430810</dob>
        <sex>F</sex>
        <cccode1>17761</cccode1>
        <cccode2>54301</cccode2>
        <cccode3>54481</cccode3>
        <cccode4></cccode4>
        <cccode5></cccode5>
        <cccode6></cccode6>
    </patientInfo>
    <userInfo>
        <hospCode>VH</hospCode>
        <loginId>itdadmin</loginId>
    </userInfo>
    <sysInfo>
        <wsId>160.68.34.60</wsId>
        <sourceSystem>PMS</sourceSystem>
    </sysInfo>
    <credentials>
        <accessCode>{1}</accessCode>
    </credentials>
</alertInputParm>";

                    scintillaReq.Text = string.Format(str, hkId, Global.AccessCode);
                }
            }
        }

        private void Initialize()
        {
            var alertsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/AP");
            var alerts = Directory.GetFiles(alertsDir, "*.json");

            hkIds = alerts.Select(o => new FileInfo(o).Name)
                                            .Select(o => o.Substring(0, o.Length - ".json".Length))
                                            .ToList();

            dataLoader = new DataLoader<AlertProfileResult>();

            dataLoader.Completed += (AlertProfileResult data) =>
            {
                if (data != null)
                {
                    var responseJsonStr = JsonHelper.ToJson(data);

                    this.SafeInvoke(() =>
                    {
                        scintillaRes.FormatJsonStyle();
                        scintillaRes.Focus();
                        scintillaRes.Text = JsonHelper.FormatJson(responseJsonStr);

                    }, false);
                }
            };

            dataLoader.Exceptioned += (Exception ex) =>
            {
                logger.Error(ex, ex.Message);

                this.SafeInvoke(() =>
                {
                    if (ex is AMException)
                    {
                        var restEx = ex as AMException;

                        MessageBox.Show(string.Format("HttpStatusCode:{1}", restEx.Message, restEx.HttpStatusCode));
                        return;
                    }

                    MessageBox.Show(string.Format("Unknown Exception: {0}", ex.Message));

                }, false);

            };


            cbxHKId.DataSource = hkIds;
            if (hkIds.Count > 0)
            {
                cbxHKId.SelectedIndex = 0;

            }
            scintillaReq.FormatStyle(StyleType.Xml);
        }
        private static string XmlFromFile(string hkId)
        {
            try
            {
                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("Data/AP/RQ/{0}.xml", hkId));

                var doc = XElement.Load(fileName);

                return doc.ToString();
            }
            catch
            {
                var errorStr = string.Format("LoadXmlFromFile - {0}.xml failed!", hkId);

                return string.Empty;
            }
        }

        private void btnShowAllergyInfo_Click(object sender, EventArgs e)
        {
           
        }
    }
}
