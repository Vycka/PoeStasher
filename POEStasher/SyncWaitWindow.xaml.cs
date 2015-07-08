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
using POEStasher.ItemsManager;
using POEStasher.AccountManager;
using System.IO;
using POEStasher.Helpers;

namespace POEStasher
{
    /// <summary>
    /// Interaction logic for WaitWindow.xaml
    /// </summary>
    public partial class SyncWaitWindow : Window
    {
        private BackgroundWorker bwSync;
        public bool SyncSucceeded { get; private set; }
        public bool ChangesFound { get; private set; }
        public Dictionary<string,byte[]> DownloadedSerializedStashes { get; private set; }
        private MainWindow MWnd;
        private PoeItemsManager[] ItemsManagers;
        private PoeAccManager AccManager;
        public SyncWaitWindow(MainWindow mWnd, PoeItemsManager[] iManagers, PoeAccManager accManager)
        {
            InitializeComponent();
            SyncSucceeded = false;
            ChangesFound = false;
            MWnd = mWnd;
            ItemsManagers = iManagers;
            AccManager = accManager;
            DownloadedSerializedStashes = new Dictionary<string, byte[]>();
        }

        private void UpdateProgress(string msg)
        {
            lDisplayProgress.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal,
                  new Action(
                    delegate()
                    {
                        lDisplayProgress.Content = msg;
                    }
                ));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bwSync = new BackgroundWorker();
            bwSync.DoWork += new DoWorkEventHandler(bwDownload_DoWork);
            bwSync.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwDownload_RunWorkerCompleted);
            bwSync.RunWorkerAsync();
        }

        void bwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateProgress("Downloading changes index...");
            byte[] list = HttpImportExport.GetList();
            if (list == null)
                return;

            List<SyncDataHelper> RemoteData = new List<SyncDataHelper>();

            MemoryStream ms = new MemoryStream(list);
            StreamReader sr = new StreamReader(ms);
            string line = sr.ReadLine();
            if (line != "OK")
            {
                line = sr.ReadToEnd();
                MessageBox.Show(line);
                sr.Close();
                ms.Close();
                return;
            }
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] data = line.Split(' ');
                SyncDataHelper sdh = new SyncDataHelper();
                sdh.owner = data[0];
                sdh.leagueId = int.Parse(data[1]);
                sdh.version = int.Parse(data[2]);
                RemoteData.Add(sdh);
            }
            sr.Close();
            ms.Close();
            sr.Dispose();
            ms.Dispose();
            //first we check if server has any outdated info (need to disable on release)
            for (int iLeague = 0; iLeague < Helpers.StaticVariables.LeagueNames.Length; iLeague++)
            {
                foreach (var cm in ItemsManagers[iLeague].OwnerItemsListUpdateTime)
                {
                    bool found = false;
                    foreach (SyncDataHelper sdh in RemoteData)
                    {
                        if (sdh.owner == cm.Key && sdh.leagueId == iLeague)
                        {
                            if (sdh.version >= cm.Value)
                                found = true;
                            break;
                        }
                    }
                    if (!found) //if not found equal or newer, upload it to server
                    {
                        bool itsOwnAcc = false;
                        foreach (PoeAccHandler acc in AccManager.Accounts)
                        {
                            if (acc.UserDisplayName == cm.Key)
                            {
                                itsOwnAcc = true;
                                break;
                            }
                        }
                        if (itsOwnAcc)
                        {
                            byte[] exp = ItemsManagers[iLeague].ExportBZip2Bytes(cm.Key);
                            UpdateProgress("Uploading [" + cm.Key + "|" + StaticVariables.LeagueNames[iLeague] + "] stash...");
                            HttpImportExport.Export(exp, cm.Key, iLeague, ItemsManagers[iLeague].OwnerItemsListUpdateTime[cm.Key]);
                        }
                    }
                }
            }
            //now check if we don't need any updates
            foreach (SyncDataHelper sdh in RemoteData)
            {
                bool found = false;
                if (ItemsManagers[sdh.leagueId].OwnerItemsListUpdateTime.ContainsKey(sdh.owner))
                {
                    if (ItemsManagers[sdh.leagueId].OwnerItemsListUpdateTime[sdh.owner] >= sdh.version)
                        found = true;
                }
                if (!found)
                {
                    ChangesFound = true;
                    UpdateProgress("Downloading [" + sdh.owner + "|" + StaticVariables.LeagueNames[sdh.leagueId] + "]");
                    byte[] serializedPibo = HttpImportExport.Import(sdh.owner, sdh.leagueId);
                    if (serializedPibo == null)
                    {
                        MWnd.AddLogEntry("[" + sdh.owner + "|" + StaticVariables.LeagueNames[sdh.leagueId] + "] Unable to retrieve the stash. Aborting");
                        return;
                    }
                    DownloadedSerializedStashes.Add(sdh.owner + "|" + StaticVariables.LeagueNames[sdh.leagueId], serializedPibo);
                }
            }
            SyncSucceeded = true;
        }
    }
    public struct SyncDataHelper
    {
        public string owner;
        public int leagueId;
        public int version;
    }
}
