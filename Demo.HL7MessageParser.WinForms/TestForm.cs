using Demo.HL7MessageParser.Models;
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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            var str = string.Format(@"ASPIRIN - Adverse drug reaction hisotry reported{0}Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn{0}Level of Severity: Severe", Environment.NewLine);
            var str2 = @"ASPRIN - Allergy history reported 
Clinical Manifestation: Rash: Urticaria 
Additional information: TEST 1 
Level of Certainty: Certain 
Use of ASPIRIN TABLET may result in allergic reaction.";

        }

        private MdsCheckFinalResult InitalData()
        {
            var initSource = new MdsCheckFinalResult();

            initSource.DrugName = "ASPRIN";

            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("Allergy Checking",
                @"ASPRIN - Allergy history reported 
Clinical Manifestation: Rash: Urticaria 
Additional information: TEST 1 
Level of Certainty: Certain 
Use of ASPIRIN TABLET may result in allergic reaction."));

            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("G6PD Deficiency Contraindication Checking",
                @"ASPIRIN TABLET is contraindicated when Hemolytic Anemia from Pyruvate Kinase and G6PD Deficientcies, a condition related to G6PD Deficiency, exists."));

            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("Adverse Drug Reaction Checking",
                @"ASPIRIN - Adverse drug reaction hisotry reported 
Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn 
Level of Severity: Severe 
Use of ASPIRIN TABLET may result in adverse drug reaction."));



            var str = string.Format(@"ASPIRIN - Adverse drug reaction hisotry reported{0}Adverse Drug Reaction: Abdomial Pain With Cramps; Heartburn{0}Level of Severity: Severe", Environment.NewLine);


            initSource.MdsCheckAlertDetails.Add(new MdsCheckAlert("JERIFFE TEST", str));

            return initSource;
        }


    }
}
