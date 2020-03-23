namespace Demo.HL7MessageParser.WinForms
{
    partial class TestForm
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.stringMasterDetailControl1 = new Demo.HL7MessageParser.WinForms.Controls.StringMasterDetailControl();
            this.SuspendLayout();
            // 
            // stringMasterDetailControl1
            // 
            this.stringMasterDetailControl1.Content_Text = "\r\n\r\nABC SaDFD  SD FF SDF DS SFDD SF SFDS SFS SDF SSF SF D 123 678,900";
            this.stringMasterDetailControl1.Location = new System.Drawing.Point(101, 46);
            this.stringMasterDetailControl1.Name = "stringMasterDetailControl1";
            this.stringMasterDetailControl1.Size = new System.Drawing.Size(157, 79);
            this.stringMasterDetailControl1.TabIndex = 4;
            this.stringMasterDetailControl1.Title_Text = "TITLE";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stringMasterDetailControl1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Controls.StringMasterDetailControl stringMasterDetailControl1;
    }
}