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
using System.Collections.ObjectModel;
using POEStasher.ItemsManager;
using POEStasher.Helpers;

namespace POEStasher
{
    /// <summary>
    /// Interaction logic for ItemOwnerManager.xaml
    /// </summary>
    public partial class ItemOwnerManager : Window
    {
        private ObservableCollection<ItemOwnerListEntry> Items;

        PoeItemsManager ItemManager;
        public ItemOwnerManager(PoeItemsManager pim)
        {
            InitializeComponent();
            ItemManager = pim;
            Items = new ObservableCollection<ItemOwnerListEntry>();
            foreach (var cm in pim.OwnerItemsListUpdateTime)
            { 
                Items.Add(new ItemOwnerListEntry(cm.Key,cm.Value));
            }
            lvOwners.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Owner", System.ComponentModel.ListSortDirection.Ascending));
            lvOwners.ItemsSource = Items;
        }

        private void bDel_Click(object sender, RoutedEventArgs e)
        {
            ItemOwnerListEntry iole = ((ItemOwnerListEntry)((FrameworkElement)e.OriginalSource).DataContext);
            MessageBoxResult result = MessageBox.Show("Do you really want to delete items owned by: " + iole.Owner + "?", "Delete items?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ItemManager.DeleteItemsByOwner(iole.Owner);
                Items.Remove(iole);
            }
        }

        private void bExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemOwnerListEntry iole = ((ItemOwnerListEntry)((FrameworkElement)e.OriginalSource).DataContext);
                ItemManager.ExportBZip2File(iole.Owner, "Export\\" + iole.Owner + "." + Helpers.StaticVariables.LeagueNames[(int)ItemManager.LeagueIndex] + ".poe");
                System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetCurrentDirectory() + "\\Export");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error saving teh file.\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
        }
    }

    public class ItemOwnerListEntry
    {
        public string Owner { get; set; }
        private DateTime _LastRefresh;
        public string LastRefreshDisplay
        {
            get
            {
                return _LastRefresh.ToString();
            }
        }
        public ItemOwnerListEntry(string owner, int version)
        {
            Owner = owner;
            _LastRefresh = UnixTime.UnixTimeToDateTime(version);
        }
    }
}
