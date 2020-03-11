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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var glbl = new GrowLabel();
            glbl.Dock = DockStyle.Top;
            glbl.Text = @"they lead mysterious lives of their own as well. They never become submissive like dogs and horses. As a result, humans have learned to respect feline independence. Most cats remain suspicious of humans all their lives. One of the things that fascinates us most about cats is the popular belief that they have nine lives. Apparently, there is a good deal of truth in this idea. A cat's ability to survive falls is based on fact.";
            panel1.Controls.Add(glbl);


            LinkLabel llbl = new LinkLabel();
            llbl.Dock = DockStyle.Top;
            llbl.Text = @"G6PD Deficiency Contraindication Checking";
            panel1.Controls.Add(llbl);

            var glbl2 = new GrowLabel();
            glbl2.Dock = DockStyle.Top;
            glbl2.Text = @"Cats never fail to fascinate human beings. They can be friendly and affectionate towards humans, but they lead mysterious lives of their own as well. They never become submissive like dogs and horses. As a result, humans have learned to respect feline independence. Most cats remain suspicious of humans all their lives. One of the things that fascinates us most about cats is the popular belief that they have nine lives. Apparently, there is a good deal of truth in this idea. A cat's ability to survive falls is based on fact.";
            panel1.Controls.Add(glbl2);


            LinkLabel llbl2 = new LinkLabel();
            llbl2.Text = "AllergyChecking";
            llbl2.Dock = DockStyle.Top;
            panel1.Controls.Add(llbl2);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
