namespace Demo.HL7MessageParser.WinForms
{
    partial class DrugMasterControl
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbReq = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.scintillaReq = new ScintillaNET.Scintilla();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.scintillaRes = new ScintillaNET.Scintilla();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chxCustomDrugMdsReq = new System.Windows.Forms.CheckBox();
            this.cbxCaseNumber = new System.Windows.Forms.ComboBox();
            this.btnCallByWebReq = new System.Windows.Forms.Button();
            this.tbRes = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.scintillaReqPreparation = new ScintillaNET.Scintilla();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scintillaResPreparation = new ScintillaNET.Scintilla();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chxCustomPreparationReq = new System.Windows.Forms.CheckBox();
            this.cbxIDrugItemCodes = new System.Windows.Forms.ComboBox();
            this.btnRequestPreparation = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tbReq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbReq);
            this.tcMain.Controls.Add(this.tbRes);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1180, 777);
            this.tcMain.TabIndex = 0;
            // 
            // tbReq
            // 
            this.tbReq.Controls.Add(this.splitContainer2);
            this.tbReq.Controls.Add(this.groupBox1);
            this.tbReq.Location = new System.Drawing.Point(4, 24);
            this.tbReq.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbReq.Name = "tbReq";
            this.tbReq.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbReq.Size = new System.Drawing.Size(1172, 749);
            this.tbReq.TabIndex = 0;
            this.tbReq.Text = "getDrugMdsPropertyHq";
            this.tbReq.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(5, 90);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer2.Size = new System.Drawing.Size(1162, 654);
            this.splitContainer2.SplitterDistance = 527;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 17;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.scintillaReq);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(527, 654);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Request";
            // 
            // scintillaReq
            // 
            this.scintillaReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaReq.Location = new System.Drawing.Point(3, 17);
            this.scintillaReq.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.scintillaReq.Name = "scintillaReq";
            this.scintillaReq.Size = new System.Drawing.Size(521, 634);
            this.scintillaReq.TabIndex = 0;
            this.scintillaReq.Text = "scintilla1";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.scintillaRes);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(630, 654);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Response";
            // 
            // scintillaRes
            // 
            this.scintillaRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaRes.Location = new System.Drawing.Point(3, 17);
            this.scintillaRes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.scintillaRes.Name = "scintillaRes";
            this.scintillaRes.Size = new System.Drawing.Size(624, 634);
            this.scintillaRes.TabIndex = 1;
            this.scintillaRes.Text = "scintillaRes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chxCustomDrugMdsReq);
            this.groupBox1.Controls.Add(this.cbxCaseNumber);
            this.groupBox1.Controls.Add(this.btnCallByWebReq);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1162, 85);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // chxCustomDrugMdsReq
            // 
            this.chxCustomDrugMdsReq.AutoSize = true;
            this.chxCustomDrugMdsReq.Location = new System.Drawing.Point(14, 36);
            this.chxCustomDrugMdsReq.Name = "chxCustomDrugMdsReq";
            this.chxCustomDrugMdsReq.Size = new System.Drawing.Size(74, 19);
            this.chxCustomDrugMdsReq.TabIndex = 18;
            this.chxCustomDrugMdsReq.Text = "Manual";
            this.chxCustomDrugMdsReq.UseVisualStyleBackColor = true;
            this.chxCustomDrugMdsReq.CheckedChanged += new System.EventHandler(this.chxCustomDrugMdsReq_CheckedChanged);
            // 
            // cbxCaseNumber
            // 
            this.cbxCaseNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCaseNumber.FormattingEnabled = true;
            this.cbxCaseNumber.Location = new System.Drawing.Point(88, 34);
            this.cbxCaseNumber.Name = "cbxCaseNumber";
            this.cbxCaseNumber.Size = new System.Drawing.Size(227, 23);
            this.cbxCaseNumber.TabIndex = 16;
            this.cbxCaseNumber.SelectedIndexChanged += new System.EventHandler(this.cbxCaseNumber_SelectedIndexChanged);
            // 
            // btnCallByWebReq
            // 
            this.btnCallByWebReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCallByWebReq.Location = new System.Drawing.Point(331, 32);
            this.btnCallByWebReq.Name = "btnCallByWebReq";
            this.btnCallByWebReq.Size = new System.Drawing.Size(104, 29);
            this.btnCallByWebReq.TabIndex = 14;
            this.btnCallByWebReq.Text = "Request";
            this.btnCallByWebReq.UseVisualStyleBackColor = true;
            this.btnCallByWebReq.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // tbRes
            // 
            this.tbRes.Controls.Add(this.splitContainer1);
            this.tbRes.Controls.Add(this.groupBox2);
            this.tbRes.Location = new System.Drawing.Point(4, 24);
            this.tbRes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbRes.Name = "tbRes";
            this.tbRes.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbRes.Size = new System.Drawing.Size(1172, 749);
            this.tbRes.TabIndex = 1;
            this.tbRes.Text = "getPreparation";
            this.tbRes.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 90);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1162, 654);
            this.splitContainer1.SplitterDistance = 527;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.scintillaReqPreparation);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(527, 654);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Request";
            // 
            // scintillaReqPreparation
            // 
            this.scintillaReqPreparation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaReqPreparation.Location = new System.Drawing.Point(3, 17);
            this.scintillaReqPreparation.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.scintillaReqPreparation.Name = "scintillaReqPreparation";
            this.scintillaReqPreparation.Size = new System.Drawing.Size(521, 634);
            this.scintillaReqPreparation.TabIndex = 0;
            this.scintillaReqPreparation.Text = "scintillaReqPreparation";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scintillaResPreparation);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(630, 654);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Response";
            // 
            // scintillaResPreparation
            // 
            this.scintillaResPreparation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaResPreparation.Location = new System.Drawing.Point(3, 17);
            this.scintillaResPreparation.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.scintillaResPreparation.Name = "scintillaResPreparation";
            this.scintillaResPreparation.Size = new System.Drawing.Size(624, 634);
            this.scintillaResPreparation.TabIndex = 1;
            this.scintillaResPreparation.Text = "scintillaResPreparation";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chxCustomPreparationReq);
            this.groupBox2.Controls.Add(this.cbxIDrugItemCodes);
            this.groupBox2.Controls.Add(this.btnRequestPreparation);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1162, 85);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // chxCustomPreparationReq
            // 
            this.chxCustomPreparationReq.AutoSize = true;
            this.chxCustomPreparationReq.Location = new System.Drawing.Point(13, 36);
            this.chxCustomPreparationReq.Name = "chxCustomPreparationReq";
            this.chxCustomPreparationReq.Size = new System.Drawing.Size(74, 19);
            this.chxCustomPreparationReq.TabIndex = 17;
            this.chxCustomPreparationReq.Text = "Manual";
            this.chxCustomPreparationReq.UseVisualStyleBackColor = true;
            this.chxCustomPreparationReq.CheckedChanged += new System.EventHandler(this.chxCustomPreparationReq_CheckedChanged);
            // 
            // cbxIDrugItemCodes
            // 
            this.cbxIDrugItemCodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIDrugItemCodes.FormattingEnabled = true;
            this.cbxIDrugItemCodes.Location = new System.Drawing.Point(90, 35);
            this.cbxIDrugItemCodes.Name = "cbxIDrugItemCodes";
            this.cbxIDrugItemCodes.Size = new System.Drawing.Size(227, 23);
            this.cbxIDrugItemCodes.TabIndex = 16;
            this.cbxIDrugItemCodes.SelectedIndexChanged += new System.EventHandler(this.cbxIDrugItemCodes_SelectedIndexChanged);
            // 
            // btnRequestPreparation
            // 
            this.btnRequestPreparation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestPreparation.Location = new System.Drawing.Point(332, 32);
            this.btnRequestPreparation.Name = "btnRequestPreparation";
            this.btnRequestPreparation.Size = new System.Drawing.Size(111, 29);
            this.btnRequestPreparation.TabIndex = 14;
            this.btnRequestPreparation.Text = "Request";
            this.btnRequestPreparation.UseVisualStyleBackColor = true;
            this.btnRequestPreparation.Click += new System.EventHandler(this.btnRequestPreparation_Click);
            // 
            // DrugMasterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DrugMasterControl";
            this.Size = new System.Drawing.Size(1180, 777);
            this.tcMain.ResumeLayout(false);
            this.tbReq.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbRes.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tbReq;
        private System.Windows.Forms.TabPage tbRes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCallByWebReq;
        private System.Windows.Forms.ComboBox cbxCaseNumber;
        private System.Windows.Forms.GroupBox groupBox3;
        private ScintillaNET.Scintilla scintillaResPreparation;
        private ScintillaNET.Scintilla scintillaReqPreparation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chxCustomPreparationReq;
        private System.Windows.Forms.ComboBox cbxIDrugItemCodes;
        private System.Windows.Forms.Button btnRequestPreparation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox5;
        private ScintillaNET.Scintilla scintillaReq;
        private System.Windows.Forms.GroupBox groupBox6;
        private ScintillaNET.Scintilla scintillaRes;
        private System.Windows.Forms.CheckBox chxCustomDrugMdsReq;
    }
}
