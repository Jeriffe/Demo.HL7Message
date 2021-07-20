namespace Demo.HL7MessageParser.WinForms
{
    partial class StringMasterDetailCollectionControl
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
            this.stringMasterDetailControl1 = new Demo.HL7MessageParser.WinForms.Controls.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // stringMasterDetailControl1
            // 
            this.stringMasterDetailControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stringMasterDetailControl1.Location = new System.Drawing.Point(0, 0);
            this.stringMasterDetailControl1.Name = "stringMasterDetailControl1";
            this.stringMasterDetailControl1.Size = new System.Drawing.Size(111, 40);
            this.stringMasterDetailControl1.TabIndex = 0;
            // 
            // StringMasterDetailCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stringMasterDetailControl1);
            this.Name = "StringMasterDetailCollectionControl";
            this.Size = new System.Drawing.Size(111, 46);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RichTextBoxEx stringMasterDetailControl1;
    }
}
