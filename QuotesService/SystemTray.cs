using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using QuotesService32.Class;


namespace QuotesService32
{
    public partial class SystemTray : Form
    {
        private System.Windows.Forms.NotifyIcon WSNotifyIcon;
        private System.ComponentModel.IContainer components;
        //private System.ComponentModel.IContainer components;

        private Icon mDirIcon = new Icon(typeof(SystemTray).Assembly.GetManifestResourceStream("QuotesService32.servmgR.ico"));

        public SystemTray()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("Application"))
            {
                this.QuotesEventLog.WriteEntry("The QuotesWin32 services was successfully initialized component.", EventLogEntryType.Information);

            }

            this.Hide();

        }
        private void InitializeNotifyIcon()
        {
            NotifyIcon WSNotifyIcon = new NotifyIcon();
            this.WSNotifyIcon.Icon = mDirIcon;
            this.WSNotifyIcon.Text = "Outlook signature quotes service.";
            this.WSNotifyIcon.Visible = true;

            //WSBallonTips
            this.WSBallonTips();
            //Create the MenuItem objects and add them to
            //the context menu of the NotifyIcon.

            MenuItem[] mnuItems = new MenuItem[11];

            mnuItems[0] = new MenuItem("&Start", new EventHandler(this.OnStart));
            mnuItems[0].Enabled = false;
            mnuItems[1] = new MenuItem("&Pause", new EventHandler(this.OnStop));
            mnuItems[1].Enabled = false;
            mnuItems[2] = new MenuItem("&Stop", new EventHandler(this.OnStop));
            mnuItems[2].Enabled = false;
            mnuItems[3] = new MenuItem("-");
            mnuItems[3] = new MenuItem("&Exit", new EventHandler(this.ExitTray));

            //add the menu items to the context menu of the NotifyIcon
            ContextMenu notifyIconMenu = new ContextMenu(mnuItems);
            this.WSNotifyIcon.ContextMenu = notifyIconMenu;

        }

        protected void OnStart(object sender, EventArgs e)
        {
            this.ContentTimer.Enabled = true;
            this.ContentTimer.Interval = 60000;
            this.QuotesEventLog.WriteEntry("The QuotesWin32 services started successfully at "
                                      + Convert.ToString(System.DateTime.Now)
                                      + ".", EventLogEntryType.Information);
            this.ContentTimer.Start();

        }

        protected void OnStop(object sender, EventArgs e)
        {
            this.ContentTimer.Stop();
            this.ContentTimer.Enabled = false;
            this.QuotesEventLog.WriteEntry("The QuotesWin32 services stopted successfully at "
                                      + Convert.ToString(System.DateTime.Now)
                                      + ".", EventLogEntryType.Information);
        }

        private void ContentTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                QuotesService32.Class.IAccessOutlook7 accessOutlook7 = new AccessOutlook7();
                accessOutlook7.SignatureRawText(GetQuotesNo());
            }
            catch (Exception exception)
            {
                this.QuotesEventLog.WriteEntry("The QuotesWin32 services discover an unexpected error: "
                              + Convert.ToString(System.DateTime.Now)
                              + ". "
                              + exception.Message,
                              EventLogEntryType.Information);

            }
        }

        private int GetQuotesNo()
        {
            System.Random RandNum = new System.Random();
            return RandNum.Next(1, 20);
        }


        private void WSBallonTips()
        {
            this.WSNotifyIcon.BalloonTipTitle = "Outlook signature quotes service.";
            this.WSNotifyIcon.BalloonTipText = "Outlook signature quotes service.";
            this.WSNotifyIcon.ShowBalloonTip(1000);
        }

        public void ExitTray(object sender, EventArgs e)
        {
            this.WSNotifyIcon.Visible = false;
            this.Close();
        }

    }
}
