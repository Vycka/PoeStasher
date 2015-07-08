using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using POEStasher.AccountManager;

namespace POEStasher
{
    /// <summary>
    /// Interaction logic for WaitWindow.xaml
    /// </summary>
    public partial class StashDownloadWaitWindow : Window
    {
        private PoeAccHandler pwr;
        private string LeagueName;
        public string[] JsonStash = null;
        private BackgroundWorker bwDownload;
        public StashDownloadWaitWindow(PoeAccHandler pwr, string leagueName)
        {
            InitializeComponent();
            this.pwr = pwr;
            LeagueName = leagueName;
        }

        private void pwr_StashProgressChanged(object sender, int currentStash, int totalStashes)
        {
            if (this.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                pbProgress.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal,
                  new Action(
                    delegate()
                    {
                        UpdateProgress(currentStash, totalStashes);
                    }
                ));
            }
            else
                UpdateProgress(currentStash, totalStashes);
        }

        private void UpdateProgress(int currentStash, int totalStashes)
        { 
            if (pbProgress.IsIndeterminate)
            {
                pbProgress.IsIndeterminate = false;
                pbProgress.Minimum = 0;
                pbProgress.Maximum = totalStashes;
            }
            lDisplayProgress.Content = currentStash + "/" + pbProgress.Maximum;
            pbProgress.Value = currentStash;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            pwr.StashProgressChanged -= new PoeAccHandler.StashRetrieveProgressReport(pwr_StashProgressChanged);
            pwr = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pwr.StashProgressChanged += new PoeAccHandler.StashRetrieveProgressReport(pwr_StashProgressChanged);
            RetrieveAllItemTabs();
        }

        public void RetrieveAllItemTabs()
        {
            bwDownload = new BackgroundWorker();
            bwDownload.DoWork += new DoWorkEventHandler(bwDownload_DoWork);
            bwDownload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwDownload_RunWorkerCompleted);
            bwDownload.RunWorkerAsync();
        }

        void bwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            JsonStash = pwr.RetrieveAllItemTabs(LeagueName);
        }
    }
}
