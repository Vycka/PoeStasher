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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using POEStasher.AccountManager;
using POEStasher.ItemsManager;
using System.ComponentModel;
using System.Windows.Threading;
using POEStasher.Helpers;


namespace POEStasher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        BackgroundWorker bwDoAllTasks = new BackgroundWorker();
        PoeAccManager AccManager;
        private LeagueId ActiveLeagueId = LeagueId.Default;
        private PoeItemsManager[] ItemsManagers;
        private Queue<SyncDataHelper> OwnersNeedingSync = new Queue<SyncDataHelper>();

        public void AddLogEntry(string entry)
        {
            if (this.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                lvLog.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal,
                  new Action(
                    delegate()
                    {
                        lvLog.Items.Insert(0, entry);
                    }
                ));
            }
            else
                lvLog.Items.Insert(0, entry);
        }
        private void LoadItemManager()
        {
            ItemsManagers = new PoeItemsManager[StaticVariables.LeagueNames.Length];
            for (int x = 0; x < StaticVariables.LeagueNames.Length; x++) 
            {
                ItemsManagers[x] = new PoeItemsManager((LeagueId)x);
                try
                {
                    if (Serializer<PoeItemsManager>.PropertiesKeyExists(ItemsManagers[x].GetPropertiesKeyName()))
                    {
                        PoeItemsManager loadedManager = Serializer<PoeItemsManager>.DeserializeFromProperties(ItemsManagers[x].GetPropertiesKeyName());
                        ItemsManagers[x] = loadedManager;
                    }
                }
                catch (Exception ex)
                {
                    AddLogEntry("[" + StaticVariables.LeagueNames[x] + "] Error Loading items (Item Cache is probably lost): " + ex.Message);
                }

                ItemsManagers[x].SetMainWindowWithLog(this);
                ItemsManagers[x].RecalculateFields();
            }
            
        }
        private void SetActiveLeague(LeagueId id)
        {
            ActiveLeagueId = id;
            this.Title = StaticVariables.AppTitle + " - " + StaticVariables.LeagueNames[(int)id] + " League";
            ucItemTree.SetDataSource(ItemsManagers[(int)ActiveLeagueId].ItemsByCatForListView);
        }
        private void SaveAllItemsManagers()
        {
            for (int x = 0; x < ItemsManagers.Length; x++)
                if (ItemsManagers[x].LastTimeChanged.AddMinutes(1) >= DateTime.Now)
                    ItemsManagers[x].Save();
        }
        public MainWindow()
        {
            InitializeComponent();
            AddLogEntry(Serializer<MainWindow>.ConfigurationDirectory);
            //Helpers.ItemsCategoryTable.Init();
            if (!Directory.Exists(@"Export"))
                Directory.CreateDirectory(@"Export");
            AccManager = new PoeAccManager(this);
            LoadItemManager();

            SetActiveLeague((LeagueId)Properties.Settings.Default.ActiveLeagueId);
            lvAccs.ItemsSource = AccManager.Accounts;

            bwDoAllTasks.DoWork += new DoWorkEventHandler(bwDoAllTasks_DoWork);
            bwDoAllTasks.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwDoAllTasks_RunWorkerCompleted);
            bwDoAllTasks.WorkerSupportsCancellation = true;
            bwDoAllTasks.RunWorkerAsync();
        }

        void bwDoAllTasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        void bwDoAllTasks_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime lastAccListRefresh = DateTime.Now.AddDays(-1);
            var UpdateAccListDelegate = new Action(delegate() { lvAccs.ItemsSource = AccManager.Accounts; });
            while (!bwDoAllTasks.CancellationPending)
            {
                DateTime dtNow = DateTime.Now;
                if ((dtNow - lastAccListRefresh).TotalSeconds >= 60)
                {
                    //lvAccs.Dispatcher.Invoke(DispatcherPriority.Normal, UpdateAccListDelegate); 
                    AccManager.UpdateLastRefreshText();
                    lastAccListRefresh = dtNow;

                    int iLastSync = Properties.Settings.Default.LastSync;
                    int mLastSync = ((int)((Helpers.UnixTime.Now - iLastSync) / 60));
                    string sLastSync = (Properties.Settings.Default.LastSync == 0 ? "never" : mLastSync + "mins ago");//Sync items with others (Last sync: 1234mins ago)
                    bSync.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                                  delegate()
                                  {
                                      bSync.Content = "Synchronize item list (Last sync: " + sLastSync + ")";
                                      bSync.IsEnabled = (mLastSync >= 2);
                                  }
                              )
                          );
                }
                if (OwnersNeedingSync.Count > 0)
                {
                    SyncDataHelper sdh = OwnersNeedingSync.Dequeue();
                    AddLogEntry("[" + sdh.owner + "|" + StaticVariables.LeagueNames[sdh.leagueId] + "] Sync: Uploading stash changes to server..");
                    byte[] exp = ItemsManagers[sdh.leagueId].ExportBZip2Bytes(sdh.owner);
                    if (HttpImportExport.Export(exp, sdh.owner, sdh.leagueId, sdh.version))
                        AddLogEntry("[" + sdh.owner + "|" + StaticVariables.LeagueNames[sdh.leagueId] + "] Sync: Done!");
                    else
                        AddLogEntry("[" + sdh.owner + "|" + StaticVariables.LeagueNames[sdh.leagueId] + "] Sync: Failed!");
                }
                System.Threading.Thread.Sleep(250);
            }
        }

        private void bRefreshAccStash_Click(object sender, RoutedEventArgs e)
        {
            PoeAccHandler pwr = ((PoeAccHandler)((FrameworkElement)e.OriginalSource).DataContext);
            bool rechecked = false;
        repeatStashQuery:
            if (!pwr.LoggedIn)
            {
                if (!pwr.Login())
                {
                    AddLogEntry("Can't update stash. Unable to connect or log-in");
                    return;
                }
                else
                    AccManager.SaveAccounts();
            }
            string owner = pwr.UserDisplayName;
            string leagueName = StaticVariables.LeagueNames[(int)ActiveLeagueId];
            StashDownloadWaitWindow sdww = new StashDownloadWaitWindow(pwr, leagueName);
            sdww.Owner = this;
            sdww.ShowDialog();
            string[] jsonStashes = sdww.JsonStash;

            if (jsonStashes == null)
            {
                AddLogEntry("[" + owner + "|" + leagueName + "] Unable to connect. Aborting");
                return;
            }

            if (jsonStashes.Length == 1 && !rechecked)
            {
                if (!pwr.CheckLogin())
                {
                    AddLogEntry("[" + owner + "|" + leagueName + "] Error while downloading stashes. Re-loging in to website and trying again.");
                    rechecked = true;
                    goto repeatStashQuery;
                }
            }

            if (jsonStashes.Length == 0 || jsonStashes[0] == null)
            {
                AddLogEntry("[" + owner + "|" + leagueName + "] No stashes have been found. Probably this account has no characters in [" + leagueName + "] league.");
                return;
            }

            var stashList = PoeStashParser.DeserializeStashes(jsonStashes, owner);
            if (stashList == null)
            {
                AddLogEntry("[" + owner + "|" + leagueName + "] Error while parsing json stashes. Aborting");
                return;
            }
            if (stashList.Count == 0)
            {
                AddLogEntry("[" + owner + "|" + leagueName + "] No acceptable items have been found in the stash. Aborting");
                return;
            }

            bool parseSuccess = ItemsManagers[(int)ActiveLeagueId].ParseStashList(stashList);
            if (parseSuccess && Properties.Settings.Default.SyncKey != "") //save only if changes been made
            {
                //ItemManager.ItemsByCatForListView.SortTree();
                //Properties.Settings.Default.Items = Serializer<PoeItemsManager>.SerializeToString(ItemManager);
                //Properties.Settings.Default.Save();  
                //ItemManager[(int)ActiveLeagueId].Export(owner, "Export\\" + owner + "." + leagueName + ".poe");
                SyncDataHelper sdh = new SyncDataHelper();
                sdh.owner = owner;
                sdh.leagueId = (int)ActiveLeagueId;
                sdh.version = ItemsManagers[(int)ActiveLeagueId].OwnerItemsListUpdateTime[owner];
                OwnersNeedingSync.Enqueue(sdh);
            }
            AccManager.SaveAccounts(); //save changed acc data like login if needed or last refresh.
            PrintItemChanges((int)ActiveLeagueId);
            SaveAllItemsManagers();
        }

        internal void PrintItemChanges(int id)
        {
            string leagueName = StaticVariables.LeagueNames[(int)ItemsManagers[id].LeagueIndex];
            if (ItemsManagers[id].ItemsToAdd.Count != 0 || ItemsManagers[id].ItemsToDel.Count != 0)
            {
                if (ItemsManagers[id].ItemsToDel.Count != 0)
                {
                    //ItemManager[id].ItemsToDel.Reverse();
                    //foreach (PoeItem i in ItemManager[id].ItemsToDel)
                    //{
                    //    if (i.Name == "")
                    //        AddLogEntry("|-> [" + i.TypeLine + "]");
                    //    else
                    //        AddLogEntry("|-> " + i.Name);
                    //}
                    string msg = "[" + ItemsManagers[id].LastOwner + "|" + leagueName + "] Items Deleted: " + ItemsManagers[id].ItemsToDel.Count;
                    AddLogEntry(msg);
                }
                if (ItemsManagers[id].ItemsToAdd.Count != 0)
                {
                    //ItemManager[id].ItemsToAdd.Reverse();
                    //foreach (PoeItem i in ItemManager[id].ItemsToAdd)
                    //{
                    //    if (i.Name == "")
                    //        AddLogEntry("|-> [" + i.TypeLine + "]");
                    //    else
                    //        AddLogEntry("|-> " + i.Name);
                    //}
                    string msg = "[" + ItemsManagers[id].LastOwner + "|" + leagueName + "] Items Added: " + ItemsManagers[id].ItemsToAdd.Count;
                    AddLogEntry(msg);
                }
            }
            else
            {
                string msg = "[" + ItemsManagers[id].LastOwner + "|" + leagueName + "] No changes in the stash";
                AddLogEntry(msg);
            }
        }

        private void bAddAcc_Click(object sender, RoutedEventArgs e)
        {
            AddEditAccount dlgAcc = new AddEditAccount();
            dlgAcc.Owner = this;
            dlgAcc.ShowDialog();
            if (dlgAcc.Changed)
            {
                foreach (PoeAccHandler acc in AccManager)
                    if (String.Compare(acc.UserLogin, dlgAcc.Acc, true) == 0)
                    {
                        AddLogEntry("Account with user name " + dlgAcc.Acc + " already exists");
                        return;
                    }
                AccManager.AddAccount(dlgAcc.Acc, dlgAcc.Pass);
            }
        }

        private void lvAccs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bEditAcc.IsEnabled = bDelAcc.IsEnabled = (lvAccs.SelectedIndex != -1);
        }

        private void bDelAcc_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete account: " + ((PoeAccHandler)lvAccs.SelectedItem).UserLogin
                + "\r\n(Items owned by that account won't be deleted from item tree, use item manager for that)", "Delete Acc",  MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                AccManager.DelAccount(lvAccs.SelectedIndex);
        }

        private void bEditAcc_Click(object sender, RoutedEventArgs e)
        {
            var acc = AccManager.GetAccount(lvAccs.SelectedIndex);
            AddEditAccount dlgAcc = new AddEditAccount(acc.UserLogin,acc.UserPassword);
            dlgAcc.Owner = this;
            dlgAcc.ShowDialog();
            if (dlgAcc.Changed)
            {
                acc.UserLogin = dlgAcc.Acc;
                acc.UserPassword = dlgAcc.Pass;
                AccManager.SaveAccounts();
            }
        }

        private void bClearLog_Click(object sender, RoutedEventArgs e)
        {
            lvLog.Items.Clear();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (bwDoAllTasks.IsBusy)
            {
                bwDoAllTasks.CancelAsync();
                e.Cancel = true;
            }
            if (Properties.Settings.Default.ActiveLeagueId != (int)ActiveLeagueId)
            {
                Properties.Settings.Default.ActiveLeagueId = (int)ActiveLeagueId;
                Properties.Settings.Default.Save();
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                try
                {
                    var pibo = Serializer<PoeItemsByOwner>.DeserializeFromFileBZip2(file);
                    ItemsManagers[(int)pibo.League].ParseStashList(pibo.Items, pibo.ListVersion);
                    PrintItemChanges((int)pibo.League);
                }
                catch (Exception ex)
                {
                    AddLogEntry("Error Parsing import file: " + file + " // " + ex.Message);
                    MessageBox.Show(ex.StackTrace);
                }
                SaveAllItemsManagers();
                //ItemManager[(int)ActiveLeagueId].Import(file);
                //PrintItemChanges();
                //if (ItemManager.ItemsToAdd.Count != 0 || ItemManager.ItemsToDel.Count != 0)
                //{
                //    //ItemManager.ItemsByCatForListView.SortTree();
                //    Properties.Settings.Default.Items = Serializer<PoeItemsManager>.SerializeToString(ItemManager);
                //    Properties.Settings.Default.Save();
                //}
            }
        }

        private void Window_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
            }
            e.Effects = DragDropEffects.Copy;
        }

        private void bItemManager_Click(object sender, RoutedEventArgs e)
        {
            var dlgIOM = new ItemOwnerManager(ItemsManagers[(int)ActiveLeagueId]);
            dlgIOM.Owner = this;
            dlgIOM.ShowDialog();
        }

        private void bSync_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.ItemsEx = null;
            SyncWaitWindow dlgSWW = new SyncWaitWindow(this, ItemsManagers, AccManager);
            dlgSWW.Owner = this;
            dlgSWW.ShowDialog();
            if (dlgSWW.SyncSucceeded)
            {
                foreach (var cm in dlgSWW.DownloadedSerializedStashes)
                {
                    try
                    {
                        var pibo = POEStasher.Helpers.Serializer<PoeItemsByOwner>.DeserializeBZip2(cm.Value);
                        ItemsManagers[(int)pibo.League].ParseStashList(pibo.Items, pibo.ListVersion);
                        PrintItemChanges((int)pibo.League);
                    }
                    catch (Exception ex)
                    {
                        AddLogEntry("[" + cm.Key + "] Unable to deserialize the stash. Aborting. " + ex.Message);
                        return;
                    }
                }
                AddLogEntry("Sync complete!" + (!dlgSWW.ChangesFound ? " (Everythings already up to date)" : ""));
                Properties.Settings.Default.LastSync = UnixTime.Now;
                Properties.Settings.Default.Save();
                bSync.Content = "Synchronize item list (Last sync: 0mins ago)";
                bSync.IsEnabled = false;
            }
            SaveAllItemsManagers();
        }

        private void bItemsOfInterest_Click(object sender, RoutedEventArgs e)
        {
            ItemsOfInterest dlgIOI = new ItemsOfInterest(ItemsManagers[(int)ActiveLeagueId], AccManager);
            dlgIOI.Owner = this;
            dlgIOI.ShowDialog();
        }

        private void bSyncKey_Click(object sender, RoutedEventArgs e)
        {
            EditSyncKey dlgESK = new EditSyncKey();
            dlgESK.Owner = this;
            dlgESK.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                SetActiveLeague((LeagueId)(((int)ActiveLeagueId + 1) % StaticVariables.LeagueNames.Length));
            }
        }

    }
}
