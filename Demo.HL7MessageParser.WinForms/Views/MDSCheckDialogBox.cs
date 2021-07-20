using Demo.HL7MessageParser.Models;
using Demo.HL7MessageParser.WinForms.Controls;
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
    public partial class MDDCheckDialogBox : Form
    {
        Region rg = null;

        public MDDCheckDialogBox(MdsCheckFinalResult mds, string SubTitle)
        {
            InitializeComponent();
            lblSubTitle.Text = SubTitle;
            lblCaution.Text = "CAUTION for " + mds.DrugName;

            cmdYes.Visible = true;
            cmdNo.Visible = false;
            cmdYes.Text = "OK";

            for (int i = mds.MdsCheckAlertDetails.Count - 1; i >= 0; i--)
            {
                for (int j = mds.MdsCheckAlertDetails[i].CheckAlerts.Count - 1; j >= 0; j--)
                {
                    var currentItem = mds.MdsCheckAlertDetails[i].CheckAlerts[j];

                    var Glabel = new GrowLabel();
                    Glabel.Text = currentItem.CheckAlertMessage;
                    Glabel.Dock = DockStyle.Top;
                    Glabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    Glabel.TextAlign = ContentAlignment.BottomLeft;
                    pnlCheckList.Controls.Add(Glabel);

                    var rTxt = new RichTextBoxEx(currentItem.CheckingDrugName, currentItem.CheckingDrugName + currentItem.CheckingDrugSuffix);
                    rTxt.Dock = DockStyle.Top;
                    rTxt.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    rTxt.SelectionColor = Color.Blue;                  
                    pnlCheckList.Controls.Add(rTxt);
                }

                var lkLabel = new TextBox();
                lkLabel.Text = mds.MdsCheckAlertDetails[i].CategoryName;
                lkLabel.Dock = DockStyle.Top;
                lkLabel.Font = new Font("Segoe UI", 10, FontStyle.Underline | FontStyle.Bold);
                lkLabel.TextAlign = HorizontalAlignment.Left;
                lkLabel.BorderStyle = BorderStyle.None;
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
            oPath.AddClosedCurve(new Point[] { new Point(0, sender.Height / p_1),
                new Point(sender.Width / p_1, 0),
                new Point(sender.Width - sender.Width / p_1, 0),
                new Point(sender.Width, sender.Height / p_1),
                new Point(sender.Width, sender.Height - sender.Height / p_1),
                new Point(sender.Width - sender.Width / p_1, sender.Height),
                new Point(sender.Width / p_1, sender.Height),
                new Point(0, sender.Height - sender.Height / p_1) }, (float)p_2);
            rg = new Region(oPath);
            sender.Region = rg;
        }

        private void cmdYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}
