namespace QuotesService32
{
    partial class SystemTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemTray));
            this.ContentTimer = new System.Timers.Timer();
            this.QuotesEventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.ContentTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuotesEventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentTimer
            // 
            this.ContentTimer.Interval = 1000;
            this.ContentTimer.SynchronizingObject = this;
            // 
            // QuotesEventLog
            // 
            this.QuotesEventLog.SynchronizingObject = this;
            // 
            // SystemTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(85, 50);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SystemTray";
            this.Text = "SystemTray";
            ((System.ComponentModel.ISupportInitialize)(this.ContentTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuotesEventLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Timers.Timer ContentTimer;
        private System.Diagnostics.EventLog QuotesEventLog;

    }
}