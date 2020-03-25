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
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnMDSCheckResult = new System.Windows.Forms.Button();
            this.btnMDSCheck = new System.Windows.Forms.Button();
            this.cbxItemCodes = new System.Windows.Forms.ComboBox();
            this.cbxCaseNumber = new System.Windows.Forms.ComboBox();
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
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerMDSCheck = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // btnSearchPatient
            // 
            this.btnSearchPatient.Location = new System.Drawing.Point(258, 16);
            this.btnSearchPatient.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(112, 26);
            this.btnSearchPatient.TabIndex = 0;
            this.btnSearchPatient.Text = "SearchPatient";
            this.btnSearchPatient.UseVisualStyleBackColor = true;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.btnMDSCheckResult);
            this.splitContainer1.Panel1.Controls.Add(this.btnMDSCheck);
            this.splitContainer1.Panel1.Controls.Add(this.cbxItemCodes);
            this.splitContainer1.Panel1.Controls.Add(this.cbxCaseNumber);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearchPatient);
            this.splitContainer1.Panel1MinSize = 15;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(1067, 617);
            this.splitContainer1.SplitterDistance = 56;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnMDSCheckResult
            // 
            this.btnMDSCheckResult.Location = new System.Drawing.Point(646, 16);
            this.btnMDSCheckResult.Margin = new System.Windows.Forms.Padding(4);
            this.btnMDSCheckResult.Name = "btnMDSCheckResult";
            this.btnMDSCheckResult.Size = new System.Drawing.Size(179, 26);
            this.btnMDSCheckResult.TabIndex = 5;
            this.btnMDSCheckResult.Text = "Show MDS-Check Result";
            this.btnMDSCheckResult.UseVisualStyleBackColor = true;
            this.btnMDSCheckResult.Click += new System.EventHandler(this.btnMDSCheckResult_Click);
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
            this.cbxItemCodes.Location = new System.Drawing.Point(394, 19);
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
            this.tabControl.Controls.Add(this.tabDrugMaster);
            this.tabControl.Controls.Add(this.tbpMDSCheck);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1067, 557);
            this.tabControl.TabIndex = 0;
            // 
            // tbpPatient
            // 
            this.tbpPatient.Controls.Add(this.splitContainerPatientMedicationAllergy);
            this.tbpPatient.Location = new System.Drawing.Point(4, 24);
            this.tbpPatient.Margin = new System.Windows.Forms.Padding(2);
            this.tbpPatient.Name = "tbpPatient";
            this.tbpPatient.Padding = new System.Windows.Forms.Padding(2);
            this.tbpPatient.Size = new System.Drawing.Size(1059, 529);
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
            this.splitContainerPatientMedicationAllergy.Size = new System.Drawing.Size(1055, 525);
            this.splitContainerPatientMedicationAllergy.SplitterDistance = 400;
            this.splitContainerPatientMedicationAllergy.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scintillaPatient);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 525);
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
            this.scintillaPatient.Size = new System.Drawing.Size(394, 505);
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
            this.splitContainerMedicationAllergy.Size = new System.Drawing.Size(651, 525);
            this.splitContainerMedicationAllergy.SplitterDistance = 338;
            this.splitContainerMedicationAllergy.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scintillaProfiles);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 525);
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
            this.scintillaProfiles.Size = new System.Drawing.Size(332, 505);
            this.scintillaProfiles.TabIndex = 2;
            this.scintillaProfiles.Text = "Patient Profile";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scintillaAlerts);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 525);
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
            this.scintillaAlerts.Size = new System.Drawing.Size(303, 505);
            this.scintillaAlerts.TabIndex = 1;
            this.scintillaAlerts.Text = "Patient Allergy";
            // 
            // tabDrugMaster
            // 
            this.tabDrugMaster.Controls.Add(this.splitContainerDrugMaster);
            this.tabDrugMaster.Location = new System.Drawing.Point(4, 22);
            this.tabDrugMaster.Name = "tabDrugMaster";
            this.tabDrugMaster.Size = new System.Drawing.Size(1059, 531);
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
            this.splitContainerDrugMaster.Size = new System.Drawing.Size(1059, 531);
            this.splitContainerDrugMaster.SplitterDistance = 541;
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
            this.splitContainerDrugMasterLeft.Size = new System.Drawing.Size(541, 531);
            this.splitContainerDrugMasterLeft.SplitterDistance = 205;
            this.splitContainerDrugMasterLeft.TabIndex = 1;
            // 
            // gbxDrugMdsPropertyHqReq
            // 
            this.gbxDrugMdsPropertyHqReq.Controls.Add(this.scintillaDrugMdsPropertyHqReq);
            this.gbxDrugMdsPropertyHqReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugMdsPropertyHqReq.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugMdsPropertyHqReq.Name = "gbxDrugMdsPropertyHqReq";
            this.gbxDrugMdsPropertyHqReq.Size = new System.Drawing.Size(205, 531);
            this.gbxDrugMdsPropertyHqReq.TabIndex = 3;
            this.gbxDrugMdsPropertyHqReq.TabStop = false;
            this.gbxDrugMdsPropertyHqReq.Text = "DrugMdsPropertyHqRequest";
            // 
            // scintillaDrugMdsPropertyHqReq
            // 
            this.scintillaDrugMdsPropertyHqReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugMdsPropertyHqReq.Location = new System.Drawing.Point(3, 17);
            this.scintillaDrugMdsPropertyHqReq.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugMdsPropertyHqReq.Name = "scintillaDrugMdsPropertyHqReq";
            this.scintillaDrugMdsPropertyHqReq.Size = new System.Drawing.Size(199, 511);
            this.scintillaDrugMdsPropertyHqReq.TabIndex = 2;
            this.scintillaDrugMdsPropertyHqReq.Text = "DrugMdsPropertyHqReq";
            // 
            // gbxDrugMdsPropertyHqRes
            // 
            this.gbxDrugMdsPropertyHqRes.Controls.Add(this.scintillaDrugMdsPropertyHqRes);
            this.gbxDrugMdsPropertyHqRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugMdsPropertyHqRes.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugMdsPropertyHqRes.Name = "gbxDrugMdsPropertyHqRes";
            this.gbxDrugMdsPropertyHqRes.Size = new System.Drawing.Size(332, 531);
            this.gbxDrugMdsPropertyHqRes.TabIndex = 4;
            this.gbxDrugMdsPropertyHqRes.TabStop = false;
            this.gbxDrugMdsPropertyHqRes.Text = "DrugMdsPropertyHqResponse";
            // 
            // scintillaDrugMdsPropertyHqRes
            // 
            this.scintillaDrugMdsPropertyHqRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugMdsPropertyHqRes.Location = new System.Drawing.Point(3, 17);
            this.scintillaDrugMdsPropertyHqRes.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugMdsPropertyHqRes.Name = "scintillaDrugMdsPropertyHqRes";
            this.scintillaDrugMdsPropertyHqRes.Size = new System.Drawing.Size(326, 511);
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
            this.splitContainerDrugMasterRight.Size = new System.Drawing.Size(514, 531);
            this.splitContainerDrugMasterRight.SplitterDistance = 206;
            this.splitContainerDrugMasterRight.TabIndex = 0;
            // 
            // gbxDrugPreparationReq
            // 
            this.gbxDrugPreparationReq.Controls.Add(this.scintillaDrugPreparationReq);
            this.gbxDrugPreparationReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugPreparationReq.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugPreparationReq.Name = "gbxDrugPreparationReq";
            this.gbxDrugPreparationReq.Size = new System.Drawing.Size(206, 531);
            this.gbxDrugPreparationReq.TabIndex = 3;
            this.gbxDrugPreparationReq.TabStop = false;
            this.gbxDrugPreparationReq.Text = "DrugPreparationRequest";
            // 
            // scintillaDrugPreparationReq
            // 
            this.scintillaDrugPreparationReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugPreparationReq.Location = new System.Drawing.Point(3, 17);
            this.scintillaDrugPreparationReq.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugPreparationReq.Name = "scintillaDrugPreparationReq";
            this.scintillaDrugPreparationReq.Size = new System.Drawing.Size(200, 511);
            this.scintillaDrugPreparationReq.TabIndex = 2;
            this.scintillaDrugPreparationReq.Text = "DrugPreparationReq";
            // 
            // gbxDrugPreparationRes
            // 
            this.gbxDrugPreparationRes.Controls.Add(this.scintillaDrugPreparationRes);
            this.gbxDrugPreparationRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDrugPreparationRes.Location = new System.Drawing.Point(0, 0);
            this.gbxDrugPreparationRes.Name = "gbxDrugPreparationRes";
            this.gbxDrugPreparationRes.Size = new System.Drawing.Size(304, 531);
            this.gbxDrugPreparationRes.TabIndex = 4;
            this.gbxDrugPreparationRes.TabStop = false;
            this.gbxDrugPreparationRes.Text = "DrugPreparationResponse";
            // 
            // scintillaDrugPreparationRes
            // 
            this.scintillaDrugPreparationRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaDrugPreparationRes.Location = new System.Drawing.Point(3, 17);
            this.scintillaDrugPreparationRes.Margin = new System.Windows.Forms.Padding(2);
            this.scintillaDrugPreparationRes.Name = "scintillaDrugPreparationRes";
            this.scintillaDrugPreparationRes.Size = new System.Drawing.Size(298, 511);
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
            this.tbpMDSCheck.Size = new System.Drawing.Size(1059, 531);
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
            this.splitContainer4.Size = new System.Drawing.Size(1055, 527);
            this.splitContainer4.SplitterDistance = 547;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.scintillaMdsCheckReq);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(547, 527);
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
            this.scintillaMdsCheckReq.Size = new System.Drawing.Size(541, 507);
            this.scintillaMdsCheckReq.TabIndex = 2;
            this.scintillaMdsCheckReq.Text = "MDS Check Request";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.scintillaMdsCheckRes);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(504, 527);
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
            this.scintillaMdsCheckRes.Size = new System.Drawing.Size(498, 507);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(833, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "Show MDS-Check Result";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FullWorkFlowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FullWorkFlowControl";
            this.Size = new System.Drawing.Size(1067, 617);
            this.Load += new System.EventHandler(this.HL7MessageParserFormTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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

        private ScintillaNET.Scintilla scintillaPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchPatient;
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
        private System.Windows.Forms.SplitContainer splitContainerPatientMedicationAllergy;
        private System.Windows.Forms.SplitContainer splitContainerMedicationAllergy;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox4;
        private ScintillaNET.Scintilla scintillaMdsCheckReq;
        private System.Windows.Forms.GroupBox groupBox5;
        private ScintillaNET.Scintilla scintillaMdsCheckRes;
        private System.ComponentModel.BackgroundWorker bgWorkerMDSCheck;
        private System.Windows.Forms.TabPage tabDrugMaster;
        private System.Windows.Forms.Button btnMDSCheckResult;
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
        private System.Windows.Forms.Button button1;
    }
}
