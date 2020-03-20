using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo.WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var str = string.Format(@"ASPIRIN - Adverse drug reaction hisotry reported{0}Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn{0}Level of Severity: Severe", Environment.NewLine);
           // growLabel1.Text = str;
            var str2 = @"ASPRIN - Allergy history reported 
Clinical Manifestation: Rash: Urticaria 
Additional information: TEST 1 
Level of Certainty: Certain 
Use of ASPIRIN TABLET may result in allergic reaction.";

          //  growLabel2.Text = str2;
        }
    }
}
