namespace QuotesService
{
    partial class QuotesWin32Tray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
      //  private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuotesWin32Tray));
            this.WSNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContentTimer = new System.Timers.Timer();
            this.QuotesEventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.ContentTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuotesEventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // WSNotifyIcon
            // 
            this.WSNotifyIcon.Visible = true;
            // 
            // ContentTimer
            // 
            this.ContentTimer.Interval = 1000D;
            this.ContentTimer.SynchronizingObject = this;
            this.ContentTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.ContentTimer_Elapsed);
            // 
            // QuotesEventLog
            // 
            this.QuotesEventLog.SynchronizingObject = this;
            // 
            // QuotesWin32Tray
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(64, 31);
            this.ControlBox = false;
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuotesWin32Tray";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.ContentTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuotesEventLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon WSNotifyIcon;
        private System.Timers.Timer ContentTimer;
        private System.Diagnostics.EventLog QuotesEventLog;


    }
}