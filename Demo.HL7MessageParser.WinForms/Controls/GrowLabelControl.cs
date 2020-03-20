using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms.Controls
{
    public partial class GrowLabelControl : UserControl
    {
        public Label lblMessage = new System.Windows.Forms.Label();

        private bool mGrowing;
        public GrowLabelControl()
        {
            InitializeComponent();
            this.Controls.Add(lblMessage);

            SetLabel();
            mGrowing = false;

            lblMessage.TextChanged += resizeLabel;
            lblMessage.FontChanged += resizeLabel;
            lblMessage.SizeChanged += resizeLabel;
        }

        private void SetLabel()
        {
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblOrigin";
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label1";
        }

        private void resizeLabel(object sender, EventArgs e)
        {
            if (mGrowing) return;
            try
            {
                mGrowing = true;
                Size sz = new Size(this.Width, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.lblMessage.Text, this.Font, sz, TextFormatFlags.WordBreak);
                this.lblMessage.Height = sz.Height;
            }
            finally
            {
                mGrowing = false;
            }
        }


    }
}
