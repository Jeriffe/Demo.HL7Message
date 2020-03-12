using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class MDDialogBox : Form
    {
        Region rg = null;
        private const byte ICON_ERROR = 0;
        private const byte ICON_QUESTION = 1;
        private const byte ICON_WARNING = 2;
        private const byte ICON_INFORMATION = 3;

        private Color Red = Color.FromArgb(192, 0, 0);
        private Color Blue = Color.FromArgb(34, 47, 100);
        private Color Green = Color.FromArgb(0, 100, 45);

        public enum MessageColor { Red, Blue, Green };

        public MDDialogBox(MdsCheckFinalResult mds, string SubTitle, MessageBoxButtons Buttons, MessageBoxIcon Icon, MessageColor bkColor)
        {
            InitializeComponent();
            lblSubTitle.Text = SubTitle;

            switch ((MessageBoxButtons)Buttons)
            {
                case MessageBoxButtons.YesNo:
                    cmdYes.Visible = true;
                    cmdNo.Visible = true;
                    cmdYes.Text = "YES";
                    cmdNo.Text = "NO";
                    break;
                case MessageBoxButtons.YesNoCancel:
                    cmdYes.Visible = true;
                    cmdNo.Visible = true;
                    cmdYes.Text = "YES";
                    cmdNo.Text = "CANCEL";
                    break;
                case MessageBoxButtons.OKCancel:
                    cmdYes.Text = "OK";
                    cmdNo.Text = "Cancel";
                    cmdYes.Visible = true;
                    cmdNo.Visible = true;
                    break;
                default:
                    cmdYes.Visible = true;
                    cmdNo.Visible = false;
                    cmdYes.Text = "OK";
                    break;
            }

            for (int i = mds.MdsCheckAlertDetails.Count - 1; i >= 0; i--)
            {
                var newLine = new Label();
                newLine.Dock = DockStyle.Top;
                pnlCheckList.Controls.Add(newLine);

                var Glabel = new GrowLabel();
                Glabel.Text = mds.MdsCheckAlertDetails[i].CheckAlertMessage;
                Glabel.Dock = DockStyle.Top;
                Glabel.Font = new Font("Segoe UI", 7, FontStyle.Bold);
                pnlCheckList.Controls.Add(Glabel);

                var lkLabel = new LinkLabel();
                lkLabel.Text = mds.MdsCheckAlertDetails[i].CategoryName;
                lkLabel.Dock = DockStyle.Top;
                lkLabel.Font = new Font("Segoe UI", 7, FontStyle.Bold);
                pnlCheckList.Controls.Add(lkLabel);
            }

            this.TransparencyKey = Color.Azure;
            //lblSubTitle.ForeColor = this.pnlIcon.BackColor;
            Type(this, 40, 0.06);
            this.TopMost = true;
        }

        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(new Point[] { new Point(0, sender.Height / p_1), new Point(sender.Width / p_1, 0), new Point(sender.Width - sender.Width / p_1, 0), new Point(sender.Width, sender.Height / p_1), new Point(sender.Width, sender.Height - sender.Height / p_1), new Point(sender.Width - sender.Width / p_1, sender.Height), new Point(sender.Width / p_1, sender.Height), new Point(0, sender.Height - sender.Height / p_1) }, (float)p_2);
            rg = new Region(oPath);
            sender.Region = rg;
        }

        private void cmdYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }

    public class MdsCheckFinalResult
    {
        public string DrugName = string.Empty;

        public string SystemErrorMessage { get; set; }

        public bool HasMdsAlert { get; set; }

        public List<MdsCheckAlert> MdsCheckAlertDetails;

        public MdsCheckFinalResult()
        {
            MdsCheckAlertDetails = new List<MdsCheckAlert>();
        }
    }

    public class MdsCheckAlert
    {
        public string CategoryName;
        public string CheckAlertMessage;

        public MdsCheckAlert(string name, string detail)
        {
            CategoryName = name;
            CheckAlertMessage = detail;
        }
    }
}
