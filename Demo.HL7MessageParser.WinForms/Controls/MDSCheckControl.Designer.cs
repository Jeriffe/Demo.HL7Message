namespace Demo.HL7MessageParser.WinForms
{
    partial class MDSCheckControl
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
            this.btnSend = new System.Windows.Forms.Button();
            this.scintillaReq = new ScintillaNET.Scintilla();
            this.scintillaRes = new ScintillaNET.Scintilla();
            this.scintillaInputParam = new ScintillaNET.Scintilla();
            this.splitContainerReqRes = new System.Windows.Forms.SplitContainer();
            this.gbxReq = new System.Windows.Forms.GroupBox();
            this.gbxRes = new System.Windows.Forms.GroupBox();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReqRes)).BeginInit();
            this.splitContainerReqRes.Panel1.SuspendLayout();
            this.splitContainerReqRes.Panel2.SuspendLayout();
            this.splitContainerReqRes.SuspendLayout();
            this.gbxReq.SuspendLayout();
            this.gbxRes.SuspendLayout();
            this.gbxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(34, 28);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(114, 29);
            this.btnSend.TabIndex = 18;
            this.btnSend.Text = "MDSheck";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // scintillaReq
            // 
            this.scintillaReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaReq.Location = new System.Drawing.Point(3, 17);
            this.scintillaReq.Margin = new System.Windows.Forms.Padding(5);
            this.scintillaReq.Name = "scintillaReq";
            this.scintillaReq.Size = new System.Drawing.Size(425, 290);
            this.scintillaReq.TabIndex = 10;
            this.scintillaReq.Text = "scintilla3";
            // 
            // scintillaRes
            // 
            this.scintillaRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaRes.Location = new System.Drawing.Point(3, 17);
            this.scintillaRes.Margin = new System.Windows.Forms.Padding(5);
            this.scintillaRes.Name = "scintillaRes";
            this.scintillaRes.Size = new System.Drawing.Size(447, 290);
            this.scintillaRes.TabIndex = 1;
            this.scintillaRes.Text = "scintilla2";
            // 
            // scintillaInputParam
            // 
            this.scintillaInputParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaInputParam.Location = new System.Drawing.Point(4, 4);
            this.scintillaInputParam.Margin = new System.Windows.Forms.Padding(4);
            this.scintillaInputParam.Name = "scintillaInputParam";
            this.scintillaInputParam.Size = new System.Drawing.Size(605, 792);
            this.scintillaInputParam.TabIndex = 0;
            this.scintillaInputParam.Text = "scintilla1";
            // 
            // splitContainerReqRes
            // 
            this.splitContainerReqRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerReqRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainerReqRes.Location = new System.Drawing.Point(0, 85);
            this.splitContainerReqRes.Name = "splitContainerReqRes";
            // 
            // splitContainerReqRes.Panel1
            // 
            this.splitContainerReqRes.Panel1.Controls.Add(this.gbxReq);
            // 
            // splitContainerReqRes.Panel2
            // 
            this.splitContainerReqRes.Panel2.Controls.Add(this.gbxRes);
            this.splitContainerReqRes.Size = new System.Drawing.Size(889, 310);
            this.splitContainerReqRes.SplitterDistance = 431;
            this.splitContainerReqRes.SplitterWidth = 5;
            this.splitContainerReqRes.TabIndex = 20;
            // 
            // gbxReq
            // 
            this.gbxReq.Controls.Add(this.scintillaReq);
            this.gbxReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxReq.Location = new System.Drawing.Point(0, 0);
            this.gbxReq.Name = "gbxReq";
            this.gbxReq.Size = new System.Drawing.Size(431, 310);
            this.gbxReq.TabIndex = 0;
            this.gbxReq.TabStop = false;
            this.gbxReq.Text = "Request";
            // 
            // gbxRes
            // 
            this.gbxRes.Controls.Add(this.scintillaRes);
            this.gbxRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRes.Location = new System.Drawing.Point(0, 0);
            this.gbxRes.Name = "gbxRes";
            this.gbxRes.Size = new System.Drawing.Size(453, 310);
            this.gbxRes.TabIndex = 0;
            this.gbxRes.TabStop = false;
            this.gbxRes.Text = "Response";
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.btnSend);
            this.gbxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearch.Location = new System.Drawing.Point(0, 0);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(889, 85);
            this.gbxSearch.TabIndex = 21;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // MDSCheckControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerReqRes);
            this.Controls.Add(this.gbxSearch);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MDSCheckControl";
            this.Size = new System.Drawing.Size(889, 395);
            this.Load += new System.EventHandler(this.MDSCheckControl_Load);
            this.splitContainerReqRes.Panel1.ResumeLayout(false);
            this.splitContainerReqRes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReqRes)).EndInit();
            this.splitContainerReqRes.ResumeLayout(false);
            this.gbxReq.ResumeLayout(false);
            this.gbxRes.ResumeLayout(false);
            this.gbxSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ScintillaNET.Scintilla scintillaReq;
        private ScintillaNET.Scintilla scintillaInputParam;
        private ScintillaNET.Scintilla scintillaRes;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.SplitContainer splitContainerReqRes;
        private System.Windows.Forms.GroupBox gbxReq;
        private System.Windows.Forms.GroupBox gbxRes;
        private System.Windows.Forms.GroupBox gbxSearch;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}
