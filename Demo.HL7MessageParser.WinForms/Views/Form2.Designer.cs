namespace Demo.HL7MessageParser.WinForms
{
    partial class Form2
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblCaseNumber = new System.Windows.Forms.Label();
            this.txtCaseNumber = new System.Windows.Forms.TextBox();
            this.txtHKID = new System.Windows.Forms.TextBox();
            this.lblHKID = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.stringMasterDetailControl1 = new Demo.HL7MessageParser.WinForms.Controls.RichTextBoxEx();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(520, 96);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblCaseNumber
            // 
            this.lblCaseNumber.AutoSize = true;
            this.lblCaseNumber.Location = new System.Drawing.Point(103, 79);
            this.lblCaseNumber.Name = "lblCaseNumber";
            this.lblCaseNumber.Size = new System.Drawing.Size(68, 13);
            this.lblCaseNumber.TabIndex = 1;
            this.lblCaseNumber.Text = "CaseNumber";
            // 
            // txtCaseNumber
            // 
            this.txtCaseNumber.Location = new System.Drawing.Point(106, 96);
            this.txtCaseNumber.Name = "txtCaseNumber";
            this.txtCaseNumber.Size = new System.Drawing.Size(100, 20);
            this.txtCaseNumber.TabIndex = 2;
            // 
            // txtHKID
            // 
            this.txtHKID.Location = new System.Drawing.Point(258, 96);
            this.txtHKID.Name = "txtHKID";
            this.txtHKID.Size = new System.Drawing.Size(100, 20);
            this.txtHKID.TabIndex = 4;
            // 
            // lblHKID
            // 
            this.lblHKID.AutoSize = true;
            this.lblHKID.Location = new System.Drawing.Point(255, 79);
            this.lblHKID.Name = "lblHKID";
            this.lblHKID.Size = new System.Drawing.Size(33, 13);
            this.lblHKID.TabIndex = 3;
            this.lblHKID.Text = "HKID";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(387, 96);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(100, 20);
            this.txtItemCode.TabIndex = 6;
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(384, 79);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(52, 13);
            this.lblItemCode.TabIndex = 5;
            this.lblItemCode.Text = "ItemCode";
            // 
            // stringMasterDetailControl1
            // 
            this.stringMasterDetailControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stringMasterDetailControl1.Location = new System.Drawing.Point(118, 281);
            this.stringMasterDetailControl1.Margin = new System.Windows.Forms.Padding(4);
            this.stringMasterDetailControl1.Name = "stringMasterDetailControl1";
            this.stringMasterDetailControl1.Size = new System.Drawing.Size(492, 86);
            this.stringMasterDetailControl1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(628, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stringMasterDetailControl1);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.lblItemCode);
            this.Controls.Add(this.txtHKID);
            this.Controls.Add(this.lblHKID);
            this.Controls.Add(this.txtCaseNumber);
            this.Controls.Add(this.lblCaseNumber);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblCaseNumber;
        private System.Windows.Forms.TextBox txtCaseNumber;
        private System.Windows.Forms.TextBox txtHKID;
        private System.Windows.Forms.Label lblHKID;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblItemCode;
        private Controls.RichTextBoxEx stringMasterDetailControl1;
        private System.Windows.Forms.Button button1;
    }
}