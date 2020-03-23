using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class Loading : Form
    {
        private MainForm mainForm;

        public Loading(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }

        internal void ResizeView()
        {
            this.Width = mainForm.Width;
            this.Height = mainForm.Height;
        }
    }
}
