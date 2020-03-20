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
    public partial class StringMasterDetailControl : UserControl
    {
        private bool mGrowing;
        public StringMasterDetailControl()
        {
            InitializeComponent();
        }

        public override bool AutoSize
        {
            get { return lblContent.AutoSize; }
            set
            {
                lblContent.AutoSize = value;
                Invalidate();
            }
        }
        [Category("ContentString")]
        [Browsable(true)]
        public string Title_Text
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        [Category("ContentString")]
        [Browsable(true)]
        public string Content_Text
        {
            get { return lblContent.Text; }
            set
            {
                lblContent.Text = string.Format("{0}{1}", Environment.NewLine, value);

                Invalidate();
            }
        }
        private void ResizeLabelContent()
        {
            if (mGrowing) return;
            try
            {
                mGrowing = true;


                Size sz = new Size(this.Width, Int32.MaxValue);

                sz = TextRenderer.MeasureText(this.lblContent.Text, this.Font, sz, TextFormatFlags.Left | TextFormatFlags.Bottom | TextFormatFlags.WordBreak);
                this.lblContent.Height = sz.Height;
                this.Height = lblTitle.Height + sz.Height + +1;
            }
            finally
            {
                mGrowing = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ResizeLabelContent();
        }
    }
}
