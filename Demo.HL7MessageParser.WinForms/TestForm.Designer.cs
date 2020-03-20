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
            this.growLabel1 = new Demo.HL7MessageParser.WinForms.GrowLabel();
            this.growLabel2 = new Demo.HL7MessageParser.WinForms.GrowLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // growLabel1
            // 
            this.growLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.growLabel1.Location = new System.Drawing.Point(18, 14);
            this.growLabel1.Name = "growLabel1";
            this.growLabel1.Size = new System.Drawing.Size(85, 13);
            this.growLabel1.TabIndex = 0;
            this.growLabel1.Text = "SFSAASFDDDDDDDDDDDDDDDDDDDFDSADASFAS";
            // 
            // growLabel2
            // 
            this.growLabel2.AutoSize = true;
            this.growLabel2.Location = new System.Drawing.Point(369, 219);
            this.growLabel2.Name = "growLabel2";
            this.growLabel2.Size = new System.Drawing.Size(62, 13);
            this.growLabel2.TabIndex = 1;
            this.growLabel2.Text = "growLabel2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.growLabel1);
            this.panel1.Location = new System.Drawing.Point(123, 255);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 88);
            this.panel1.TabIndex = 2;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.growLabel2);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GrowLabel growLabel1;
        private GrowLabel growLabel2;
        private System.Windows.Forms.Panel panel1;
    }
}