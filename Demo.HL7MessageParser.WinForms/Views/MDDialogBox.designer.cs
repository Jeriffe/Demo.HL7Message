namespace Demo.HL7MessageParser.WinForms
{
    partial class MDDialogBox
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

            this.BackgroundImage = null;

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDDialogBox));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCaution = new System.Windows.Forms.Label();
            this.lblPSCC = new System.Windows.Forms.Label();
            this.lblMDSTitle = new System.Windows.Forms.Label();
            this.pnlCheckList = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdNo = new System.Windows.Forms.Button();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.cmdYes = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.Controls.Add(this.panel2);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Name = "pnlContent";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblCaution);
            this.panel2.Controls.Add(this.lblPSCC);
            this.panel2.Controls.Add(this.lblMDSTitle);
            this.panel2.Controls.Add(this.pnlCheckList);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lblCaution
            // 
            resources.ApplyResources(this.lblCaution, "lblCaution");
            this.lblCaution.BackColor = System.Drawing.Color.Red;
            this.lblCaution.ForeColor = System.Drawing.Color.White;
            this.lblCaution.Name = "lblCaution";
            // 
            // lblPSCC
            // 
            resources.ApplyResources(this.lblPSCC, "lblPSCC");
            this.lblPSCC.BackColor = System.Drawing.Color.LightBlue;
            this.lblPSCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPSCC.ForeColor = System.Drawing.Color.Black;
            this.lblPSCC.Name = "lblPSCC";
            // 
            // lblMDSTitle
            // 
            this.lblMDSTitle.BackColor = System.Drawing.Color.LightBlue;
            this.lblMDSTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblMDSTitle, "lblMDSTitle");
            this.lblMDSTitle.ForeColor = System.Drawing.Color.Black;
            this.lblMDSTitle.Name = "lblMDSTitle";
            // 
            // pnlCheckList
            // 
            resources.ApplyResources(this.pnlCheckList, "pnlCheckList");
            this.pnlCheckList.BackColor = System.Drawing.Color.White;
            this.pnlCheckList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCheckList.Name = "pnlCheckList";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.panel1.Controls.Add(this.cmdNo);
            this.panel1.Controls.Add(this.lblSubTitle);
            this.panel1.Controls.Add(this.cmdYes);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // cmdNo
            // 
            this.cmdNo.DialogResult = System.Windows.Forms.DialogResult.No;
            resources.ApplyResources(this.cmdNo, "cmdNo");
            this.cmdNo.ForeColor = System.Drawing.Color.Black;
            this.cmdNo.Name = "cmdNo";
            this.cmdNo.UseVisualStyleBackColor = true;
            // 
            // lblSubTitle
            // 
            resources.ApplyResources(this.lblSubTitle, "lblSubTitle");
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSubTitle.Name = "lblSubTitle";
            // 
            // cmdYes
            // 
            this.cmdYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            resources.ApplyResources(this.cmdYes, "cmdYes");
            this.cmdYes.ForeColor = System.Drawing.Color.Black;
            this.cmdYes.Name = "cmdYes";
            this.cmdYes.UseVisualStyleBackColor = true;
            this.cmdYes.Click += new System.EventHandler(this.cmdYes_Click);
            // 
            // MDDialogBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MDDialogBox";
            this.pnlContent.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdYes;
        private System.Windows.Forms.Button cmdNo;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCaution;
        private System.Windows.Forms.Label lblPSCC;
        private System.Windows.Forms.Label lblMDSTitle;
        private System.Windows.Forms.Panel pnlCheckList;
    }
}