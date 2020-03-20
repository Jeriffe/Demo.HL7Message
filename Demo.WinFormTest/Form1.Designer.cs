namespace Demo.WinFormTest
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.growLabel1 = new Demo.WinFormTest.GrowLabel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(321, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 88);
            this.panel1.TabIndex = 3;
            // 
            // growLabel1
            // 
            this.growLabel1.Location = new System.Drawing.Point(182, 106);
            this.growLabel1.Name = "growLabel1";
            this.growLabel1.Size = new System.Drawing.Size(100, 52);
            this.growLabel1.TabIndex = 4;
            this.growLabel1.Text = "growLabel1 asdfas asdfdas  asdf asdfdas asdf asfaf asdfasf  fasd as f";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.growLabel1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private GrowLabel growLabel1;
    }
}

