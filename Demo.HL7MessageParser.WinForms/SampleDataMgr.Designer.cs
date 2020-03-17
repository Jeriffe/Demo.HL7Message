namespace Demo.HL7MessageParser.WinForms
{
    partial class SampleDataMgr
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
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.cbxCaseNumber = new System.Windows.Forms.ComboBox();
            this.lblCaseNumber = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tbpPatient = new System.Windows.Forms.TabPage();
            this.splitContainerPatientMedicationAllergy = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scintillaPatient = new ScintillaNET.Scintilla();
            this.splitContainerMedicationAllergy = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scintillaProfiles = new ScintillaNET.Scintilla();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scintillaAlerts = new ScintillaNET.Scintilla();
            this.tabDrugMaster = new System.Windows.Forms.TabPage();
            this.splitContainerDrugMaster = new System.Windows.Forms.SplitContainer();
            this.splitContainerDrugMasterLeft = new System.Windows.Forms.SplitContainer();
            this.gbxDrugMdsPropertyHqReq = new System.Windows.Forms.GroupBox();
            this.scintillaDrugMdsPropertyHqReq = new ScintillaNET.Scintilla();
            this.gbxDrugMdsPropertyHqRes = new System.Windows.Forms.GroupBox();
            this.scintillaDrugMdsPropertyHqRes = new ScintillaNET.Scintilla();
            this.splitContainerDrugMasterRight = new System.Windows.Forms.SplitContainer();
            this.gbxDrugPreparationReq = new System.Windows.Forms.GroupBox();
            this.scintillaDrugPreparationReq = new ScintillaNET.Scintilla();
            this.gbxDrugPreparationRes = new System.Windows.Forms.GroupBox();
            this.scintillaDrugPreparationRes = new ScintillaNET.Scintilla();
            this.tbpMDSCheck = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.scintillaMdsCheckReq = new ScintillaNET.Scintilla();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.scintillaMdsCheckRes = new ScintillaNET.Scintilla();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.gbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tbpPatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPatientMedicationAllergy)).BeginInit();
            this.splitContainerPatientMedicationAllergy.Panel1.SuspendLayout();
            this.splitContainerPatientMedicationAllergy.Panel2.SuspendLayout();
            this.splitContainerPatientMedicationAllergy.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMedicationAllergy)).BeginInit();
            this.splitContainerMedicationAllergy.Panel1.SuspendLayout();
            this.splitContainerMedicationAllergy.Panel2.SuspendLayout();
            this.splitContainerMedicationAllergy.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabDrugMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDrugMaster)).BeginInit();
            this.splitContainerDrugMaster.Panel1.SuspendLayout();
            this.splitContainerDrugMaster.Panel2.SuspendLayout();
            this.splitContainerDrugMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDrugMasterLeft)).BeginInit();
            this.splitContainerDrugMasterLeft.Panel1.SuspendLayout();
            this.splitContainerDrugMasterLeft.Panel2.SuspendLayout();
            this.splitContainerDrugMasterLeft.SuspendLayout();
            this.gbxDrugMdsPropertyHqReq.SuspendLayout();
            this.gbxDrugMdsPropertyHqRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDrugMasterRight)).BeginInit();
            this.splitContainerDrugMasterRight.Panel1.SuspendLayout();
            this.splitContainerDrugMasterRight.Panel2.SuspendLayout();
            this.splitContainerDrugMasterRight.SuspendLayout();
            this.gbxDrugPreparationReq.SuspendLayout();
            this.gbxDrugPreparationRes.SuspendLayout();
            this.tbpMDSCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.btnDeploy);
            this.gbxSearch.Controls.Add(this.btnSave);
            this.gbxSearch.Controls.Add(this.btnVerify);
            this.gbxSearch.Controls.Add(this.cbxCaseNumber);
            this.gbxSearch.Controls.Add(this.lblCaseNumber);
            this.gbxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxSearch.Location = new System.Drawing.Point(0, 0);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(1255, 105);
            this.gbxSearch.TabIndex = 0;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.gbxSearch);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControl);
            this.splitContainerMain.Size = new System.Drawing.Size(1255, 797);
            this.splitContainerMain.SplitterDistance = 105;
            this.splitContainerMain.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(407, 38);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 26);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(283, 38);
            this.btnVerify.Margin = new System.Windows.Forms.Padding(4);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(100, 26);
            this.btnVerify.TabIndex = 10;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // cbxCaseNumber
            // 
            this.cbxCaseNumber.FormattingEnabled = true;
            this.cbxCaseNumber.Location = new System.Drawing.Point(114, 41);
            this.cbxCaseNumber.Margin = new System.Windows.Forms.Padding(2);
            this.cbxCaseNumber.Name = "cbxCaseNumber";
            this.cbxCaseNumber.Size = new System.Drawing.Size(138, 21);
            this.cbxCaseNumber.TabIndex = 8;
            this.cbxCaseNumber.SelectedIndexChanged += new System.EventHandler(this.cbxCaseNumber_SelectedIndexChanged);
            // 
            // lblCaseNumber
            // 
            this.lblCaseNumber.AutoSize = true;
            this.lblCaseNumber.Location = new System.Drawing.Point(28, 45);
            this.lblCaseNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCaseNumber.Name = "lblCaseNumber";
            this.lblCaseNumber.Size = new System.Drawing.Size(71, 13);
            this.lblCaseNumber.TabIndex = 7;
            this.lblCaseNumber.Text = "CaseNumber:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tbpPatient);
            this.tabControl.Controls.Add(this.tabDrugMaster);
            this.tabControl.Controls.Add(this.tbpMDSCheck);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1255, 688);
            this.tabControl.TabIndex = 1;
            // 
            // tbpPatient
            // 
            this.tbpPatient.Controls.Add(this.splitContainerPatientMedicationAllergy);
            this.tbpPatient.Location = new System.Drawing.Point(4, 22);
            this.tbpPatient.Margin = new System.Windows.Forms.Padding(2);
            this.tbpPatient.Name = "tbpPatient";
            this.tbpPatient.Padding = new System.Windows.Forms.Padding(2);
            this.tbpPatient.Size = new System.Drawing.Size(1247, 662);
            this.tbpPatient.TabIndex = 0;
            this.tbpPatient.Text = "Patient & Medication & Allergy";
            this.tbpPatient.UseVisualStyleBackColor = true;
            // 
            // splitContainerPatientMedicationAllergy
            // 
            this.splitContainerPatientMedicationAllergy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPatientMedicationAllergy.Location = new System.Drawing.Point(2, 2);
            this.splitContainerPatientMedicationAllergy.Name = "splitContainerPatientMedicationAllergy";
            // 
            // splitContainerPatientMedicationAllergy.Panel1
            // 
            this.splitContainerPatientMedicationAllergy.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainerPatientMedicationAllergy.Panel2
            // 
            this.splitContainerPatientMedicationAllergy.Panel2.Controls.Add(this.splitContainerMedicationAllergy);
            this.splitContainerPatientMedicationAllergy.Size = new System.Drawing.Size(1243, 658);
            this.splitContainerPatientMedicationAllergy.SplitterDistance = 471;
            this.splitContainerPatientMedicationAllergy.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scintillaPatient);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 658);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PatientInfo";
            // 
            // scintillaPatient
            // 
            this.scintillaPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaPatient.Location = new System.Drawing.Point(3, 16);
            this.scintillaPatient.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaPatient.Name = "scintillaPatient";
            this.scintillaPatient.Size = new System.Drawing.Size(465, 639);
            this.scintillaPatient.TabIndex = 1;
            this.scintillaPatient.Text = "Patient Info";
            // 
            // splitContainerMedicationAllergy
            // 
            this.splitContainerMedicationAllergy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMedicationAllergy.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMedicationAllergy.Name = "splitContainerMedicationAllergy";
            // 
            // splitContainerMedicationAllergy.Panel1
            // 
            this.splitContainerMedicationAllergy.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainerMedicationAllergy.Panel2
            // 
            this.splitContainerMedicationAllergy.Panel2.Controls.Add(this.groupBox3);
            this.splitContainerMedicationAllergy.Size = new System.Drawing.Size(768, 658);
            this.splitContainerMedicationAllergy.SplitterDistance = 398;
            this.splitContainerMedicationAllergy.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scintillaProfiles);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 658);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MedicationProfile";
            // 
            // scintillaProfiles
            // 
            this.scintillaProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaProfiles.Location = new System.Drawing.Point(3, 16);
            this.scintillaProfiles.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaProfiles.Name = "scintillaProfiles";
            this.scintillaProfiles.Size = new System.Drawing.Size(392, 639);
            this.scintillaProfiles.TabIndex = 2;
            this.scintillaProfiles.Text = "Patient Profile";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scintillaAlerts);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(366, 658);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AlertProfile";
            // 
            // scintillaAlerts
            // 
            this.scintillaAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaAlerts.Location = new System.Drawing.Point(3, 16);
            this.scintillaAlerts.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaAlerts.Name = "scintillaAlerts";
            this.scintillaAlerts.Size = new System.Drawing.Size(360, 639);
            this.scintillaAlerts.TabIndex = 1;
            this.scintillaAlerts.Text = "Patient Allergy";
            // 
            // tabDrugMaster
            // 
            this.tabDrugMaster.Controls.Add(this.splitContainerDrugMaster);
            this.tabDrugMaster.Location = new System.Drawing.Point(4, 22);
            this.tabDrugMaster.Name = "tabDrugMaster";
            this.tabDrugMaster.Size = new System.Drawing.Size(1247, 518);
            this.tabDrugMaster.TabIndex = 2;
            this.tabDrugMaster.Text = "Drug Master";
            this.tabDrugMaster.UseVisualStyleBackColor = true;
            // 
            // splitContainerDrugMaster
            // 
            this.splitContainerDrugMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDrugMaster.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDrugMaster.Name = "splitContainerDrugMaster";
            // 
            // splitContainerDrugMaster.Panel1
            // 
            this.splitContainerDrugMaster.Panel1.Controls.Add(this.splitContainerDrugMasterLeft);
            // 
            // splitContainerDrugMaster.Panel2
            // 
            this.splitContainerDrugMaster.Panel2.Controls.Add(this.splitContainerDrugMasterRight);
            this.splitContainerDrugMaster.Size = new System.Drawing.Size(1247, 518);
            this.splitContainerDrugMaster.SplitterDistance = 637;
            this.splitContainerDrugMaster.TabIndex = 6;
            // 
            // splitContainerDrugMasterLeft
            // 
            this.splitContainerDrugMasterLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDrugMasterLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDrugMasterLeft.Name = "splitContainerDrugMasterLeft";
            // 
            // splitContainerDrugMasterLeft.Panel1
            // 
            this.splitContainerDrugMasterLeft.Panel1.Controls.Add(this.gbxDrugMdsPropertyHqReq);
            // 
            // splitContainerDrugMasterLeft.Panel2
            // 
            this.splitContainerDrugMasterLeft.Panel2.Controls.Add(this.gbxDrugMdsPropertyHqRes);
            this.splitContainerDrugMasterLeft.Size = new System.Drawing.Size(637, 518);
            this.splitContainerDrugMasterLeft.SplitterDistance = 241;
            this.splitContainerDrugMasterLeft.TabIndex = 1;
            // 
            // gbxDrugMdsPropertyHqReq
            // 
            this.gbxDrugMdsPropertyHqReq.Controls.Add(this.scintillaDrugMdsPropertyHqReq);
            this.gbxDrugMdsPropertyHqReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugMdsPropertyHqReq.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugMdsPropertyHqReq.Name = "gbxDrugMdsPropertyHqReq";
            this.gbxDrugMdsPropertyHqReq.Size = new System.Drawing.Size(241, 518);
            this.gbxDrugMdsPropertyHqReq.TabIndex = 3;
            this.gbxDrugMdsPropertyHqReq.TabStop = false;
            this.gbxDrugMdsPropertyHqReq.Text = "DrugMdsPropertyHqRequest";
            // 
            // scintillaDrugMdsPropertyHqReq
            // 
            this.scintillaDrugMdsPropertyHqReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugMdsPropertyHqReq.Location = new System.Drawing.Point(3, 16);
            this.scintillaDrugMdsPropertyHqReq.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugMdsPropertyHqReq.Name = "scintillaDrugMdsPropertyHqReq";
            this.scintillaDrugMdsPropertyHqReq.Size = new System.Drawing.Size(235, 499);
            this.scintillaDrugMdsPropertyHqReq.TabIndex = 2;
            this.scintillaDrugMdsPropertyHqReq.Text = "DrugMdsPropertyHqReq";
            // 
            // gbxDrugMdsPropertyHqRes
            // 
            this.gbxDrugMdsPropertyHqRes.Controls.Add(this.scintillaDrugMdsPropertyHqRes);
            this.gbxDrugMdsPropertyHqRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugMdsPropertyHqRes.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugMdsPropertyHqRes.Name = "gbxDrugMdsPropertyHqRes";
            this.gbxDrugMdsPropertyHqRes.Size = new System.Drawing.Size(392, 518);
            this.gbxDrugMdsPropertyHqRes.TabIndex = 4;
            this.gbxDrugMdsPropertyHqRes.TabStop = false;
            this.gbxDrugMdsPropertyHqRes.Text = "DrugMdsPropertyHqResponse";
            // 
            // scintillaDrugMdsPropertyHqRes
            // 
            this.scintillaDrugMdsPropertyHqRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugMdsPropertyHqRes.Location = new System.Drawing.Point(3, 16);
            this.scintillaDrugMdsPropertyHqRes.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugMdsPropertyHqRes.Name = "scintillaDrugMdsPropertyHqRes";
            this.scintillaDrugMdsPropertyHqRes.Size = new System.Drawing.Size(386, 499);
            this.scintillaDrugMdsPropertyHqRes.TabIndex = 1;
            this.scintillaDrugMdsPropertyHqRes.Text = "DrugMdsPropertyHqRes";
            // 
            // splitContainerDrugMasterRight
            // 
            this.splitContainerDrugMasterRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDrugMasterRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDrugMasterRight.Name = "splitContainerDrugMasterRight";
            // 
            // splitContainerDrugMasterRight.Panel1
            // 
            this.splitContainerDrugMasterRight.Panel1.Controls.Add(this.gbxDrugPreparationReq);
            // 
            // splitContainerDrugMasterRight.Panel2
            // 
            this.splitContainerDrugMasterRight.Panel2.Controls.Add(this.gbxDrugPreparationRes);
            this.splitContainerDrugMasterRight.Size = new System.Drawing.Size(606, 518);
            this.splitContainerDrugMasterRight.SplitterDistance = 242;
            this.splitContainerDrugMasterRight.TabIndex = 0;
            // 
            // gbxDrugPreparationReq
            // 
            this.gbxDrugPreparationReq.Controls.Add(this.scintillaDrugPreparationReq);
            this.gbxDrugPreparationReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugPreparationReq.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugPreparationReq.Name = "gbxDrugPreparationReq";
            this.gbxDrugPreparationReq.Size = new System.Drawing.Size(242, 518);
            this.gbxDrugPreparationReq.TabIndex = 3;
            this.gbxDrugPreparationReq.TabStop = false;
            this.gbxDrugPreparationReq.Text = "DrugPreparationRequest";
            // 
            // scintillaDrugPreparationReq
            // 
            this.scintillaDrugPreparationReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugPreparationReq.Location = new System.Drawing.Point(3, 16);
            this.scintillaDrugPreparationReq.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugPreparationReq.Name = "scintillaDrugPreparationReq";
            this.scintillaDrugPreparationReq.Size = new System.Drawing.Size(236, 499);
            this.scintillaDrugPreparationReq.TabIndex = 2;
            this.scintillaDrugPreparationReq.Text = "DrugPreparationReq";
            // 
            // gbxDrugPreparationRes
            // 
            this.gbxDrugPreparationRes.Controls.Add(this.scintillaDrugPreparationRes);
            this.gbxDrugPreparationRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugPreparationRes.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugPreparationRes.Name = "gbxDrugPreparationRes";
            this.gbxDrugPreparationRes.Size = new System.Drawing.Size(360, 518);
            this.gbxDrugPreparationRes.TabIndex = 4;
            this.gbxDrugPreparationRes.TabStop = false;
            this.gbxDrugPreparationRes.Text = "DrugPreparationResponse";
            // 
            // scintillaDrugPreparationRes
            // 
            this.scintillaDrugPreparationRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugPreparationRes.Location = new System.Drawing.Point(3, 16);
            this.scintillaDrugPreparationRes.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugPreparationRes.Name = "scintillaDrugPreparationRes";
            this.scintillaDrugPreparationRes.Size = new System.Drawing.Size(354, 499);
            this.scintillaDrugPreparationRes.TabIndex = 1;
            this.scintillaDrugPreparationRes.Text = "DrugPreparationRes";
            // 
            // tbpMDSCheck
            // 
            this.tbpMDSCheck.Controls.Add(this.splitContainer4);
            this.tbpMDSCheck.Location = new System.Drawing.Point(4, 22);
            this.tbpMDSCheck.Margin = new System.Windows.Forms.Padding(2);
            this.tbpMDSCheck.Name = "tbpMDSCheck";
            this.tbpMDSCheck.Padding = new System.Windows.Forms.Padding(2);
            this.tbpMDSCheck.Size = new System.Drawing.Size(1247, 518);
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
            this.splitContainer4.Size = new System.Drawing.Size(1243, 514);
            this.splitContainer4.SplitterDistance = 644;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.scintillaMdsCheckReq);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(644, 514);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Request";
            // 
            // scintillaMdsCheckReq
            // 
            this.scintillaMdsCheckReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaMdsCheckReq.Location = new System.Drawing.Point(3, 16);
            this.scintillaMdsCheckReq.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaMdsCheckReq.Name = "scintillaMdsCheckReq";
            this.scintillaMdsCheckReq.Size = new System.Drawing.Size(638, 495);
            this.scintillaMdsCheckReq.TabIndex = 2;
            this.scintillaMdsCheckReq.Text = "MDS Check Request";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.scintillaMdsCheckRes);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(595, 514);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Response";
            // 
            // scintillaMdsCheckRes
            // 
            this.scintillaMdsCheckRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaMdsCheckRes.Location = new System.Drawing.Point(3, 16);
            this.scintillaMdsCheckRes.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaMdsCheckRes.Name = "scintillaMdsCheckRes";
            this.scintillaMdsCheckRes.Size = new System.Drawing.Size(589, 495);
            this.scintillaMdsCheckRes.TabIndex = 1;
            this.scintillaMdsCheckRes.Text = "MDS Check Response";
            // 
            // btnDeploy
            // 
            this.btnDeploy.Location = new System.Drawing.Point(548, 38);
            this.btnDeploy.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(173, 26);
            this.btnDeploy.TabIndex = 12;
            this.btnDeploy.Text = "DeploySampletoTargert";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // SampleDataMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 797);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "SampleDataMgr";
            this.Text = "SampleDataMgr";
            this.Load += new System.EventHandler(this.SampleDataMgr_Load);
            this.gbxSearch.ResumeLayout(false);
            this.gbxSearch.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tbpPatient.ResumeLayout(false);
            this.splitContainerPatientMedicationAllergy.Panel1.ResumeLayout(false);
            this.splitContainerPatientMedicationAllergy.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPatientMedicationAllergy)).EndInit();
            this.splitContainerPatientMedicationAllergy.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainerMedicationAllergy.Panel1.ResumeLayout(false);
            this.splitContainerMedicationAllergy.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMedicationAllergy)).EndInit();
            this.splitContainerMedicationAllergy.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabDrugMaster.ResumeLayout(false);
            this.splitContainerDrugMaster.Panel1.ResumeLayout(false);
            this.splitContainerDrugMaster.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDrugMaster)).EndInit();
            this.splitContainerDrugMaster.ResumeLayout(false);
            this.splitContainerDrugMasterLeft.Panel1.ResumeLayout(false);
            this.splitContainerDrugMasterLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDrugMasterLeft)).EndInit();
            this.splitContainerDrugMasterLeft.ResumeLayout(false);
            this.gbxDrugMdsPropertyHqReq.ResumeLayout(false);
            this.gbxDrugMdsPropertyHqRes.ResumeLayout(false);
            this.splitContainerDrugMasterRight.Panel1.ResumeLayout(false);
            this.splitContainerDrugMasterRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDrugMasterRight)).EndInit();
            this.splitContainerDrugMasterRight.ResumeLayout(false);
            this.gbxDrugPreparationReq.ResumeLayout(false);
            this.gbxDrugPreparationRes.ResumeLayout(false);
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

        private System.Windows.Forms.GroupBox gbxSearch;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.ComboBox cbxCaseNumber;
        private System.Windows.Forms.Label lblCaseNumber;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tbpPatient;
        private System.Windows.Forms.SplitContainer splitContainerPatientMedicationAllergy;
        private System.Windows.Forms.GroupBox groupBox1;
        private ScintillaNET.Scintilla scintillaPatient;
        private System.Windows.Forms.SplitContainer splitContainerMedicationAllergy;
        private System.Windows.Forms.GroupBox groupBox2;
        private ScintillaNET.Scintilla scintillaProfiles;
        private System.Windows.Forms.GroupBox groupBox3;
        private ScintillaNET.Scintilla scintillaAlerts;
        private System.Windows.Forms.TabPage tabDrugMaster;
        private System.Windows.Forms.SplitContainer splitContainerDrugMaster;
        private System.Windows.Forms.SplitContainer splitContainerDrugMasterLeft;
        private System.Windows.Forms.GroupBox gbxDrugMdsPropertyHqReq;
        private ScintillaNET.Scintilla scintillaDrugMdsPropertyHqReq;
        private System.Windows.Forms.GroupBox gbxDrugMdsPropertyHqRes;
        private ScintillaNET.Scintilla scintillaDrugMdsPropertyHqRes;
        private System.Windows.Forms.SplitContainer splitContainerDrugMasterRight;
        private System.Windows.Forms.GroupBox gbxDrugPreparationReq;
        private ScintillaNET.Scintilla scintillaDrugPreparationReq;
        private System.Windows.Forms.GroupBox gbxDrugPreparationRes;
        private ScintillaNET.Scintilla scintillaDrugPreparationRes;
        private System.Windows.Forms.TabPage tbpMDSCheck;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox4;
        private ScintillaNET.Scintilla scintillaMdsCheckReq;
        private System.Windows.Forms.GroupBox groupBox5;
        private ScintillaNET.Scintilla scintillaMdsCheckRes;
        private System.Windows.Forms.Button btnDeploy;
    }
}