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
    public partial class MDSCheckControl : UserControl
    {
        private List<string> hkIds = new List<string>();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        IRestParserSvc restService;

        public MDSCheckControl(MainForm mainForm)
        {
            InitializeComponent();

            bgWorker.DoWork += bgWorker_DoWork;   
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);

            loadForm = new Loading(mainForm);

            InitializeService();
        }


        private void InitializeService()
        {
            restService = new RestParserSvc(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);
        }
        private Loading loadForm;

        private void MDSCheckControl_Load(object sender, EventArgs e)
        {
            scintillaReq.FormatStyle(StyleType.Xml);
            scintillaRes.FormatJsonStyle();
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

                var inputParam = XmlHelper.XmlDeserialize<MDSCheckInputParm>(scintillaReq.Text.Trim());

                bgWorker.RunWorkerAsync(inputParam);

                loadForm.ResizeView();
                loadForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var input = e.Argument as MDSCheckInputParm;
            var result = restService.CheckMDS(input);

            this.BeginInvoke((MethodInvoker)delegate
            {
                scintillaRes.Text = JsonHelper.FormatJson(JsonHelper.ToJson(result));
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
    }
}
