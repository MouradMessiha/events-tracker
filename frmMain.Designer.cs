namespace EventsTracker
{
    partial class frmMain
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
            this.txtEventCode = new System.Windows.Forms.TextBox();
            this.btnViewTrend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtEventCode
            // 
            this.txtEventCode.Location = new System.Drawing.Point(63, 26);
            this.txtEventCode.Name = "txtEventCode";
            this.txtEventCode.Size = new System.Drawing.Size(42, 20);
            this.txtEventCode.TabIndex = 0;
            this.txtEventCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEventCode_KeyPress);
            // 
            // btnViewTrend
            // 
            this.btnViewTrend.Location = new System.Drawing.Point(154, 3);
            this.btnViewTrend.Name = "btnViewTrend";
            this.btnViewTrend.Size = new System.Drawing.Size(17, 19);
            this.btnViewTrend.TabIndex = 1;
            this.btnViewTrend.UseVisualStyleBackColor = true;
            this.btnViewTrend.Click += new System.EventHandler(this.btnViewTrend_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 71);
            this.Controls.Add(this.btnViewTrend);
            this.Controls.Add(this.txtEventCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = " Event";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEventCode;
        private System.Windows.Forms.Button btnViewTrend;
    }
}

