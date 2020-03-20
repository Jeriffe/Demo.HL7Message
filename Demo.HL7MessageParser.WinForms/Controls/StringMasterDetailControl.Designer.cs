namespace Demo.HL7MessageParser.WinForms.Controls
{
    partial class StringMasterDetailControl
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
            this.lblTitle = new System.Windows.Forms.LinkLabel();
            this.lblContent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(82, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TabStop = true;
            this.lblTitle.Text = "Master String";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblContent
            // 
            this.lblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContent.Location = new System.Drawing.Point(0, 13);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(119, 18);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = "Details Content String";
            this.lblContent.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // StringMasterDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.lblTitle);
            this.Name = "StringMasterDetailControl";
            this.Size = new System.Drawing.Size(119, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblTitle;
        private System.Windows.Forms.Label lblContent;
    }
}
