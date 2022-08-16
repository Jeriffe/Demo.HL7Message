using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.Common;
using NLog;
using System.Configuration;
using System.IO;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class MedicationProfileControl : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IRestParserSvc restService;
        DataLoader<MedicationProfileResult> dataLoader;

        public MedicationProfileControl()
        {
            InitializeComponent();

            InitializeService();

        }

        private void MedicationProfileControl_Load(object sender, EventArgs e)
        {
            Initialize();

        }
        private void btnSendMedicationProfile_Click(object sender, EventArgs e)
        {
            if (Global.IsDirty)
            {
                InitializeService();
            }

            scintillaRes.Text = string.Empty;

            var caseNumber = cbxCaseNumber.SelectedItem.ToString();

            dataLoader.LoadDataAsync(restService.GetMedicationProfile, caseNumber);
        }

        private void InitializeService()
        {
            restService = new RestParserSvc(Global.ProfileRestUrl, Global.ClientSecret, Global.ClientId, Global.HospitalCode);
        }
        private void Initialize()
        {
            var profilesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/MP");
            var profiles = Directory.GetFiles(profilesDir, "*.json");

            cbxCaseNumber.DataSource = profiles.Select(o => new FileInfo(o).Name)
                                            .Select(o => o.Substring(0, o.Length - ".json".Length))
                                            .ToList();

            dataLoader = new DataLoader<MedicationProfileResult>();
            dataLoader.Completed += (MedicationProfileResult data) =>
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
        }
    }
}