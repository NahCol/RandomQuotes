using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using QuotesService.Class;

namespace QuotesService
{
    public partial class QuotesWin32Tray : Form
    {
        private System.ComponentModel.IContainer components;
        private MenuItem[] mnuItems = new MenuItem[5];

        public QuotesWin32Tray()
        {
            InitializeComponent();
            if (EventLog.SourceExists("Application"))
            {
                this.QuotesEventLog.Source = "Application";
                this.QuotesEventLog.Log = "Application";
                this.QuotesEventLog.WriteEntry("The QuotesWin32 services was successfully initialized component.", EventLogEntryType.Information);

            }

            this.Hide();
            this.InitializeNotifyIcon();
            this.Startup();

        }


        public void ExitTray(object sender, EventArgs e)
        {
            this.WSNotifyIcon.Visible = false;
            this.Close();
        }



        private void InitializeNotifyIcon()
        {

            NotifyIcon WSNotifyIcon = new NotifyIcon();

            this.WSNotifyIcon.Icon = new Icon(GetType(), "Quotes.ico");
            this.WSNotifyIcon.Text = "Outlook signature quotes service.";
            this.WSNotifyIcon.Visible = true;

            this.WSBallonTips();

            mnuItems[0] = new MenuItem("&Start", new EventHandler(this.OnStart));
            mnuItems[0].Enabled = true;
            mnuItems[1] = new MenuItem("&Stop", new EventHandler(this.OnStop));
            mnuItems[1].Enabled = false;
            mnuItems[2] = new MenuItem("-");
            mnuItems[2] = new MenuItem("&Reload Quote", new EventHandler(this.ReLoadQuote));
            mnuItems[2].Enabled = true;
            mnuItems[3] = new MenuItem("-");
            mnuItems[3] = new MenuItem("&Reload Template", new EventHandler(this.ReloadTemplate));
            mnuItems[3].Enabled = true;
            mnuItems[4] = new MenuItem("-");
            mnuItems[4] = new MenuItem("&Exit", new EventHandler(this.ExitTray));

            //add the menu items to the context menu of the NotifyIcon
            ContextMenu notifyIconMenu = new ContextMenu(mnuItems);
            this.WSNotifyIcon.ContextMenu = notifyIconMenu;
          
        }
        protected void OnStart(object sender, EventArgs e)
        {
            Startup();

        }

        protected void Startup()
        {
            this.ContentTimer.Enabled = true;
            this.ContentTimer.Interval = 10000;
            this.QuotesEventLog.WriteEntry("The QuotesWin32 services started successfully at "
                                      + Convert.ToString(System.DateTime.Now)
                                      + ".", EventLogEntryType.Information);
            mnuItems[0].Enabled = false;
            mnuItems[0].Checked = true;
            mnuItems[1].Checked = false;
            mnuItems[1].Enabled = true;
            mnuItems[2].Checked = false;
            mnuItems[2].Enabled = true;
            this.ContentTimer.Start();
        }

        protected void OnStop(object sender, EventArgs e)
        {
            this.ContentTimer.Stop();
            this.ContentTimer.Enabled = false;
            try
            {
                var accessOutlook7 = new AccessOutlook();
                if (EventLog.SourceExists("Application"))
                {
                    this.QuotesEventLog.WriteEntry("The QuotesWin32 services stopted successfully at "
                                              + Convert.ToString(System.DateTime.Now)
                                              + ".", EventLogEntryType.Information);
                }

            }
            catch (Exception exception)
            {
                if (EventLog.SourceExists("Application"))
                {

                    this.QuotesEventLog.WriteEntry("The QuotesWin services discovered an unexpected error: "
                                  + Convert.ToString(DateTime.Now)
                                  + ". "
                                  + exception.Message,
                                  EventLogEntryType.Information);
                }
            }

            mnuItems[0].Enabled = true;
            mnuItems[1].Enabled = false;
            mnuItems[1].Checked = true;
            mnuItems[0].Checked = false;
            mnuItems[2].Checked = false;
            mnuItems[2].Enabled = false;
        }

        private void LoadQuote(bool newTemplate)
        {
            var accessOutlook = new AccessOutlook();

            if (accessOutlook.IsOutlookRunning())
            {
                accessOutlook.SignatureRawText(true, newTemplate);
            }
        }
        private void ReloadTemplate(object sender, EventArgs e)
        {
            LoadQuote(true);
        }
        private void ReLoadQuote(object sender, EventArgs e)
        {

            LoadQuote(false);
        }
        private void ContentTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            { 
                LoadQuote(false);
            }
            catch (Exception exception)
            {
                if (EventLog.SourceExists("Application"))
                {

                    this.QuotesEventLog.WriteEntry("The QuotesWin32 services discover an unexpected error: "
                                  + Convert.ToString(System.DateTime.Now)
                                  + ". "
                                  + exception.Message,
                                  EventLogEntryType.Information);
                }
            }
        }

        private void WSBallonTips()
        {
            this.WSNotifyIcon.BalloonTipTitle = "Outlook signature quotes service.";
            this.WSNotifyIcon.BalloonTipText = "Outlook signature quotes service.";
            this.WSNotifyIcon.ShowBalloonTip(1000);
        }
    }
}
