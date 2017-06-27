namespace EventsTracker
{
    partial class frmTrendChart
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
            this.scrScroll = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // scrScroll
            // 
            this.scrScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scrScroll.Location = new System.Drawing.Point(924, 4);
            this.scrScroll.Name = "scrScroll";
            this.scrScroll.Size = new System.Drawing.Size(17, 698);
            this.scrScroll.TabIndex = 0;
            this.scrScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrScroll_Scroll);
            // 
            // frmTrendChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 706);
            this.Controls.Add(this.scrScroll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmTrendChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trend Chart";
            this.Load += new System.EventHandler(this.frmTrendChart_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmTrendChart_Paint);
            this.Activated += new System.EventHandler(this.frmTrendChart_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar scrScroll;
    }
}