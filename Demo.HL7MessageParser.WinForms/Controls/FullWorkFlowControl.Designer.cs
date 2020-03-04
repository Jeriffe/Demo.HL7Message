namespace Demo.HL7MessageParser.WinForms
{
    partial class FullWorkFlowControl
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
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRequest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnMDSCheck = new System.Windows.Forms.Button();
            this.cbxItemCodes = new System.Windows.Forms.ComboBox();
            this.cbxCaseNumber = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tbpPatient = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scintillaPatient = new ScintillaNET.Scintilla();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scintillaProfiles = new ScintillaNET.Scintilla();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scintillaAlerts = new ScintillaNET.Scintilla();
            this.tbpMDSCheck = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.scintillaMdsCheckReq = new ScintillaNET.Scintilla();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.scintillaMdsCheckRes = new ScintillaNET.Scintilla();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerMDSCheck = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tbpPatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbpMDSCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(258, 16);
            this.btnRequest.Margin = new System.Windows.Forms.Padding(4);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(80, 26);
            this.btnRequest.TabIndex = 0;
            this.btnRequest.Text = "Send";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "CaseNumber";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnMDSCheck);
            this.splitContainer1.Panel1.Controls.Add(this.cbxItemCodes);
            this.splitContainer1.Panel1.Controls.Add(this.cbxCaseNumber);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRequest);
            this.splitContainer1.Panel1MinSize = 15;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(805, 491);
            this.splitContainer1.SplitterDistance = 56;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnMDSCheck
            // 
            this.btnMDSCheck.Location = new System.Drawing.Point(538, 16);
            this.btnMDSCheck.Margin = new System.Windows.Forms.Padding(4);
            this.btnMDSCheck.Name = "btnMDSCheck";
            this.btnMDSCheck.Size = new System.Drawing.Size(100, 26);
            this.btnMDSCheck.TabIndex = 4;
            this.btnMDSCheck.Text = "MDS-Check";
            this.btnMDSCheck.UseVisualStyleBackColor = true;
            this.btnMDSCheck.Click += new System.EventHandler(this.btnMDSCheck_Click);
            // 
            // cbxItemCodes
            // 
            this.cbxItemCodes.FormattingEnabled = true;
            this.cbxItemCodes.Location = new System.Drawing.Point(376, 19);
            this.cbxItemCodes.Margin = new System.Windows.Forms.Padding(2);
            this.cbxItemCodes.Name = "cbxItemCodes";
            this.cbxItemCodes.Size = new System.Drawing.Size(138, 23);
            this.cbxItemCodes.TabIndex = 3;
            this.cbxItemCodes.SelectedIndexChanged += new System.EventHandler(this.cbxItemCodes_SelectedIndexChanged);
            // 
            // cbxCaseNumber
            // 
            this.cbxCaseNumber.FormattingEnabled = true;
            this.cbxCaseNumber.Location = new System.Drawing.Point(112, 17);
            this.cbxCaseNumber.Margin = new System.Windows.Forms.Padding(2);
            this.cbxCaseNumber.Name = "cbxCaseNumber";
            this.cbxCaseNumber.Size = new System.Drawing.Size(138, 23);
            this.cbxCaseNumber.TabIndex = 2;
            this.cbxCaseNumber.SelectedIndexChanged += new System.EventHandler(this.cbxCaseNumber_SelectedIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tbpPatient);
            this.tabControl.Controls.Add(this.tbpMDSCheck);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(805, 431);
            this.tabControl.TabIndex = 0;
            // 
            // tbpPatient
            // 
            this.tbpPatient.Controls.Add(this.splitContainer2);
            this.tbpPatient.Location = new System.Drawing.Point(4, 24);
            this.tbpPatient.Margin = new System.Windows.Forms.Padding(2);
            this.tbpPatient.Name = "tbpPatient";
            this.tbpPatient.Padding = new System.Windows.Forms.Padding(2);
            this.tbpPatient.Size = new System.Drawing.Size(797, 403);
            this.tbpPatient.TabIndex = 0;
            this.tbpPatient.Text = "Patient & Medication & Allergy";
            this.tbpPatient.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(2, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(793, 399);
            this.splitContainer2.SplitterDistance = 301;
            this.splitContainer2.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scintillaPatient);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 399);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PatientInfo";
            // 
            // scintillaPatient
            // 
            this.scintillaPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaPatient.Location = new System.Drawing.Point(3, 17);
            this.scintillaPatient.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaPatient.Name = "scintillaPatient";
            this.scintillaPatient.Size = new System.Drawing.Size(295, 379);
            this.scintillaPatient.TabIndex = 1;
            this.scintillaPatient.Text = "Patient Info";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Size = new System.Drawing.Size(488, 399);
            this.splitContainer3.SplitterDistance = 254;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scintillaProfiles);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 399);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MedicationProfile";
            // 
            // scintillaProfiles
            // 
            this.scintillaProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaProfiles.Location = new System.Drawing.Point(3, 17);
            this.scintillaProfiles.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaProfiles.Name = "scintillaProfiles";
            this.scintillaProfiles.Size = new System.Drawing.Size(248, 379);
            this.scintillaProfiles.TabIndex = 2;
            this.scintillaProfiles.Text = "Patient Profile";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scintillaAlerts);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 399);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AlertProfile";
            // 
            // scintillaAlerts
            // 
            this.scintillaAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaAlerts.Location = new System.Drawing.Point(3, 17);
            this.scintillaAlerts.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaAlerts.Name = "scintillaAlerts";
            this.scintillaAlerts.Size = new System.Drawing.Size(224, 379);
            this.scintillaAlerts.TabIndex = 1;
            this.scintillaAlerts.Text = "Patient Allergy";
            // 
            // tbpMDSCheck
            // 
            this.tbpMDSCheck.Controls.Add(this.splitContainer4);
            this.tbpMDSCheck.Location = new System.Drawing.Point(4, 22);
            this.tbpMDSCheck.Margin = new System.Windows.Forms.Padding(2);
            this.tbpMDSCheck.Name = "tbpMDSCheck";
            this.tbpMDSCheck.Padding = new System.Windows.Forms.Padding(2);
            this.tbpMDSCheck.Size = new System.Drawing.Size(797, 405);
            this.tbpMDSCheck.TabIndex = 1;
            this.tbpMDSCheck.Text = "Medication Drug Check";
            this.tbpMDSCheck.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(2, 2);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer4.Size = new System.Drawing.Size(793, 401);
            this.splitContainer4.SplitterDistance = 412;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.scintillaMdsCheckReq);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(412, 401);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Request";
            // 
            // scintillaMdsCheckReq
            // 
            this.scintillaMdsCheckReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaMdsCheckReq.Location = new System.Drawing.Point(3, 17);
            this.scintillaMdsCheckReq.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaMdsCheckReq.Name = "scintillaMdsCheckReq";
            this.scintillaMdsCheckReq.Size = new System.Drawing.Size(406, 381);
            this.scintillaMdsCheckReq.TabIndex = 2;
            this.scintillaMdsCheckReq.Text = "MDS Check Request";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.scintillaMdsCheckRes);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(377, 401);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Response";
            // 
            // scintillaMdsCheckRes
            // 
            this.scintillaMdsCheckRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaMdsCheckRes.Location = new System.Drawing.Point(3, 17);
            this.scintillaMdsCheckRes.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaMdsCheckRes.Name = "scintillaMdsCheckRes";
            this.scintillaMdsCheckRes.Size = new System.Drawing.Size(371, 381);
            this.scintillaMdsCheckRes.TabIndex = 1;
            this.scintillaMdsCheckRes.Text = "MDS Check Response";
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // bgWorkerMDSCheck
            // 
            this.bgWorkerMDSCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerMDSCheck_DoWork);
            // 
            // FullWorkFlowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FullWorkFlowControl";
            this.Size = new System.Drawing.Size(805, 491);
            this.Load += new System.EventHandler(this.HL7MessageParserFormTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tbpPatient.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tbpMDSCheck.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla scintillaPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cbxCaseNumber;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tbpPatient;
        private System.Windows.Forms.TabPage tbpMDSCheck;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Button btnMDSCheck;
        private System.Windows.Forms.ComboBox cbxItemCodes;
        private System.Windows.Forms.GroupBox groupBox3;
        private ScintillaNET.Scintilla scintillaAlerts;
        private System.Windows.Forms.GroupBox groupBox2;
        private ScintillaNET.Scintilla scintillaProfiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox4;
        private ScintillaNET.Scintilla scintillaMdsCheckReq;
        private System.Windows.Forms.GroupBox groupBox5;
        private ScintillaNET.Scintilla scintillaMdsCheckRes;
        private System.ComponentModel.BackgroundWorker bgWorkerMDSCheck;
    }
}
