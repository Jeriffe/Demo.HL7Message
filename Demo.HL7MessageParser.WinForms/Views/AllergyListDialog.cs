using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class AllergyListDialog : Form
    {
        public AlertProfileResult AlertProfile { get; private set; }

        public AllergyListDialog(AlertProfileResult alertProfile)
        {
            AlertProfile = alertProfile;

            InitializeComponent();
        }

        private void cmdYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;

            dataGridView1.DataSource = AlertProfile.AllergyProfile;
            dataGridView2.DataSource = AlertProfile.AdrProfile;
            dataGridView3.DataSource = AlertProfile.AlertProfile;

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            dataGridView2.ClearSelection();
            dataGridView2.CurrentCell = null;
            dataGridView3.ClearSelection();
            dataGridView3.CurrentCell = null;
        }

        private void InitializeData()
        {
            var result = new List<AllergyProfile>
            {
                new AllergyProfile
                {
                    Allergen="KEPPRA [LEVETIRACETAM]",
                    Certainty="Certain",
                    Manifestation="Asthma; Rash ;Asthma",
                    Remark="Rash",
                    UpdateDtm="29-08-2019 16:43:39",
                    UpdateUser="AA, FORMAT in IP",
                    UpdateUserRank="Department Manager (Pharmacy)",
                    UpdateHospital="VH",
                },
                new AllergyProfile
                {
                    Allergen="CIPREOFLOXACIN",
                    Certainty="Certain",
                    Manifestation="Angicedema",
                    Remark="Rash",
                    UpdateDtm="29-08-2019 16:43:39",
                    UpdateUser="AA, FORMAT in IP",
                    UpdateUserRank="Department Manager (Pharmacy)",
                    UpdateHospital="VH",
               },
                  new AllergyProfile
                {
                    Allergen="NSAID",
                    Certainty="Certain",
                    Manifestation="Angicedema, Asthma",
                    Remark="NSAID) \n (Origninal NIDOL)",
                    UpdateDtm="29-08-2019 16:43:39",
                    UpdateUser="AA, FORMAT in IP",
                    UpdateUserRank="Department Manager (Pharmacy)",
                    UpdateHospital="VH",
                },
                new AllergyProfile
                {
                    Allergen="ASPIRIN",
                    Certainty="Certain",
                    Manifestation="Rash; Urticaria",
                    Remark="Rash",
                    UpdateDtm="29-08-2019 16:43:39",
                    UpdateUser="AA, FORMAT in IP",
                    UpdateUserRank="Department Manager (Pharmacy)",
                    UpdateHospital="VH",
               },
                  new AllergyProfile
                {
                    Allergen="KEPPRA [LEVETIRACETAM]",
                    Certainty="Certain",
                    Manifestation="Asthma; Rash",
                    Remark="Rash",
                    UpdateDtm="29-08-2019 16:43:39",
                    UpdateUser="AA, FORMAT in IP",
                    UpdateUserRank="Department Manager (Pharmacy)",
                    UpdateHospital="VH",
                },
                    new AllergyProfile
                {
                    Allergen="KEPPRA [LEVETIRACETAM]",
                    Certainty="Certain",
                    Manifestation="Asthma; Rash",
                    Remark="Rash",
                    UpdateDtm="29-08-2019 16:43:39",
                    UpdateUser="AA, FORMAT in IP",
                    UpdateUserRank="Department Manager (Pharmacy)",
                    UpdateHospital="VH",
                },
            };

            dataGridView1.DataSource = result;

            dataGridView2.DataSource = result;

            dataGridView3.DataSource = result;

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }

            /*
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() == "")
            {
                e.CellStyle.ForeColor = Color.Red;
            }*/
        }

        private void ckbNoAllergy_CheckedChanged(object sender, EventArgs e)
        {
            this.lblLastVerified.Visible = this.ckbNoAllergy.Checked;
        }
    }

    public class AllergyProfile
    {
        public string Allergen { get; set; }

        public string Certainty { get; set; }

        public string Manifestation { get; set; }

        public string Remark { get; set; }

        public string UpdateDtm { get; set; }

        public string UpdateUser { get; set; }
        public string UpdateUserRank { get; set; }
        public string UpdateHospital { get; set; }
    }
}