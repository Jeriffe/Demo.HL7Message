namespace Demo.HL7MessageParser.WinForms
{
    partial class MedicationProfileControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxCaseNumber = new System.Windows.Forms.ComboBox();
            this.btnSendMedicationProfile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.scintillaRes = new ScintillaNET.Scintilla();
            this.scintillaReqMP = new ScintillaNET.Scintilla();
            this.panelMain = new System.Windows.Forms.Panel();
            this.gbxRes = new System.Windows.Forms.GroupBox();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.panelMain.SuspendLayout();
            this.gbxRes.SuspendLayout();
            this.gbxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxCaseNumber
            // 
            this.cbxCaseNumber.FormattingEnabled = true;
            this.cbxCaseNumber.Location = new System.Drawing.Point(105, 36);
            this.cbxCaseNumber.Name = "cbxCaseNumber";
            this.cbxCaseNumber.Size = new System.Drawing.Size(234, 23);
            this.cbxCaseNumber.TabIndex = 9;
            // 
            // btnSendMedicationProfile
            // 
            this.btnSendMedicationProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMedicationProfile.Location = new System.Drawing.Point(356, 34);
            this.btnSendMedicationProfile.Name = "btnSendMedicationProfile";
            this.btnSendMedicationProfile.Size = new System.Drawing.Size(114, 25);
            this.btnSendMedicationProfile.TabIndex = 8;
            this.btnSendMedicationProfile.Text = "Send";
            this.btnSendMedicationProfile.UseVisualStyleBackColor = true;
            this.btnSendMedicationProfile.Click += new System.EventHandler(this.btnSendMedicationProfile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "CaseNumber";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scintillaRes
            // 
            this.scintillaRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaRes.Location = new System.Drawing.Point(3, 17);
            this.scintillaRes.Margin = new System.Windows.Forms.Padding(5);
            this.scintillaRes.Name = "scintillaRes";
            this.scintillaRes.Size = new System.Drawing.Size(755, 267);
            this.scintillaRes.TabIndex = 1;
            this.scintillaRes.Text = "scintilla2";
            // 
            // scintillaReqMP
            // 
            this.scintillaReqMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaReqMP.Location = new System.Drawing.Point(4, 4);
            this.scintillaReqMP.Margin = new System.Windows.Forms.Padding(4);
            this.scintillaReqMP.Name = "scintillaReqMP";
            this.scintillaReqMP.Size = new System.Drawing.Size(675, 685);
            this.scintillaReqMP.TabIndex = 0;
            this.scintillaReqMP.Text = "scintilla1";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gbxRes);
            this.panelMain.Controls.Add(this.gbxSearch);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(761, 369);
            this.panelMain.TabIndex = 3;
            // 
            // gbxRes
            // 
            this.gbxRes.Controls.Add(this.scintillaRes);
            this.gbxRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRes.Location = new System.Drawing.Point(0, 82);
            this.gbxRes.Name = "gbxRes";
            this.gbxRes.Size = new System.Drawing.Size(761, 287);
            this.gbxRes.TabIndex = 10;
            this.gbxRes.TabStop = false;
            this.gbxRes.Text = "Response";
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.label3);
            this.gbxSearch.Controls.Add(this.btnSendMedicationProfile);
            this.gbxSearch.Controls.Add(this.cbxCaseNumber);
            this.gbxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearch.Location = new System.Drawing.Point(0, 0);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(761, 82);
            this.gbxSearch.TabIndex = 11;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // MedicationProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MedicationProfileControl";
            this.Size = new System.Drawing.Size(761, 369);
            this.Load += new System.EventHandler(this.MedicationProfileControl_Load);
            this.panelMain.ResumeLayout(false);
            this.gbxRes.ResumeLayout(false);
            this.gbxSearch.ResumeLayout(false);
            this.gbxSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxCaseNumber;
        private System.Windows.Forms.Button btnSendMedicationProfile;
        private System.Windows.Forms.Label label3;
        private ScintillaNET.Scintilla scintillaReqMP;
        private ScintillaNET.Scintilla scintillaRes;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox gbxRes;
        private System.Windows.Forms.GroupBox gbxSearch;
    }
}
