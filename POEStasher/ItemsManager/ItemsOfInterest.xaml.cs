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
using POEStasher.AccountManager;
using System.ComponentModel;
using System.Timers;
using POEStasher.Helpers;

namespace POEStasher.ItemsManager
{
    /// <summary>
    /// Interaction logic for ItemsOfInterest.xaml
    /// </summary>
    public partial class ItemsOfInterest : Window
    {
        #region static stuff mainly related for saving checked/unchecked filters
        private static SortedSet<string>[] UncheckedFiltersEx;
        private static readonly string FILTERS_KEY_NAME = "IOIUncheckedFilters";
        static ItemsOfInterest()
        {
            UncheckedFiltersEx = new SortedSet<string>[StaticVariables.LeagueNames.Length];
            for (int x = 0; x < UncheckedFiltersEx.Length; x++)
            {
                UncheckedFiltersEx[x] = new SortedSet<string>();
                try
                {
                    string filePath = Serializer<SortedSet<string>>.ConfigurationDirectory + GetKeyName(x);
                    if (System.IO.File.Exists(filePath))
                        UncheckedFiltersEx[x] = Serializer<SortedSet<string>>.DeserializeFromProperties(GetKeyName(x));
                }
                catch (Exception)
                {
                }

            }
        }
        public static void CleanObseleteFilters(PoeItemsManager itemsManager)
        {
            int leagueIndex = 0;
            foreach (var filterListByLeague in UncheckedFiltersEx)
            {
                List<string> itemsToDel = new List<string>();
                foreach (string filterInfo in filterListByLeague)
                {
                    string[] filter = filterInfo.Split('@');
                    if (itemsManager.OwnerStashNames.ContainsKey(filter[0]))
                    {
                        if (!itemsManager.OwnerStashNames[filter[0]].Contains(filter[1]))
                            itemsToDel.Add(filterInfo);
                    }
                    else
                        itemsToDel.Add(filterInfo);
                }
                if (itemsToDel.Count != 0)
                {
                    foreach (var filterItem in itemsToDel)
                        filterListByLeague.Remove(filterItem);
                    Save(leagueIndex);
                }
                leagueIndex++;
            }
        }
        public static void Save(int leagueIndex)
        {
            Helpers.Serializer<SortedSet<string>>.SerializeToProperties(UncheckedFiltersEx[leagueIndex], GetKeyName(leagueIndex));
        }
        public static string GetKeyName(int leagueIndex)
        {
            return FILTERS_KEY_NAME + "." + StaticVariables.LeagueNames[leagueIndex] + ".league";
        }
        private static bool FirstWindowLoad = true;
        #endregion

        public Helpers.ObservableSortedList<ItemOfInterest> InterestedItems;
        public Helpers.ObservableSortedList<FilterEntry> FiltersList;

        private PoeItemsManager ItemsManager;
        private SortedSet<string> OwnersList;
        private bool FiltersChanged = false;
        private int LeagueIndex;


        public ItemsOfInterest(PoeItemsManager items, PoeAccManager accounts)
        {
            if (FirstWindowLoad)
            {
                CleanObseleteFilters(items);
                FirstWindowLoad = false;
            }

            InitializeComponent();
            //AccountsManager = accounts;
            ItemsManager = items;
            LeagueIndex = (int)ItemsManager.LeagueIndex;
            InterestedItems = new Helpers.ObservableSortedList<ItemOfInterest>();

            FiltersList = new Helpers.ObservableSortedList<FilterEntry>();         

            OwnersList = new SortedSet<string>();
            foreach (var cm in items.OwnerStashNames)
            {
                string owner = cm.Key;
                foreach (string stash in items.OwnerStashNames[owner])
                {
                    FiltersList.Add(new FilterEntry(cm.Key, stash, !UncheckedFiltersEx[LeagueIndex].Contains(cm.Key + '@' + stash)));
                }
            }
            
            foreach (PoeAccHandler acc in accounts)
            {
                OwnersList.Add(acc.UserDisplayName);
            }
            RefreshItemsOfInterest();
            RefreshVisibilityOfFilteredItems();
            lvItems.ItemsSource = InterestedItems;
            lvFilters.ItemsSource = FiltersList;
           

        }

        private void AddItemOfInterest(ItemOfInterest ioi)
        {
            foreach (var item in InterestedItems)
            {
                if (ioi.Equals(item))
                    return;
            }
            InterestedItems.Add(ioi);
        }

        private void RefreshItemsOfInterest()
        {
            int extraParam;
            InterestedItems.Clear();
            foreach (PoeItem i in ItemsManager.AllItems)
            {
                InterestType iType = IsItItemOfInterest(i, ItemsManager, out extraParam);
                if (iType == InterestType.None)
                    continue;
                else
                {
                    int DoIOwnThisKey = (OwnersList.Contains(i.Owner) ? -1 : 1);
                    if (iType == InterestType.HighQualityRare)
                        AddItemOfInterest(new ItemOfInterest(i.TypeLine + " | " + extraParam + "% quality", i.Owner, i.StashName, i.PosX + "x" + i.PosY, iType, DoIOwnThisKey, (int)iType));
                    else if (iType == InterestType.RareNameMatch)
                    {
                        string extraLine = "";
                        foreach (PoeItem i2 in ItemsManager.AllItems)
                        {
                            
                            if (i.Name == i2.Name)
                            { 
                                if (extraLine != "")
                                    extraLine += "\r\n";
                                extraLine += i2.Owner + " " + i2.StashName + " " + i2.PosX + "x" + i2.PosY;
                            }
                        }
                        if (extraParam >= 3)
                            iType = InterestType.RareNameMatch3;
                        AddItemOfInterest(new ItemOfInterest(i.Name + " | " + extraParam + " items with same name", i.Owner, i.StashName, i.PosX + "x" + i.PosY, iType, DoIOwnThisKey, (int)iType, extraLine));
                        
                    }
                }
            }
        }

        static public InterestType IsItItemOfInterest(PoeItem i, PoeItemsManager ItemsManager, out int ExtraParam)
        {
            if (i.FrameType == PoeItem.FrameTypeIndex.Rare)
            {

                if (ItemsManager.RareItemNames[i.Name] > 1)
                {
                    ExtraParam = ItemsManager.RareItemNames[i.Name];
                    return InterestType.RareNameMatch;
                }

                //foreach (PoeItem.ItemProperty prop in i.ItemProperties)
                //{
                //    if (prop.Name == "Quality")
                //    {
                //        int quality = int.Parse(prop.Values[0].Value.Remove(prop.Values[0].Value.Length - 1, 1));
                //        if (quality >= 16)
                //        {
                //            ExtraParam = quality;
                //            return InterestType.HighQualityRare;
                //        }
                //    }
                //}

            }
            ExtraParam = 0;
            return InterestType.None;
        }

        public enum InterestType
        { 
            None,
            RareNameMatch3,
            HighQualityRare,
            RareNameMatch
        }

        public class FilterEntry : INotifyPropertyChanged, IComparable
        {
            public bool IsChecked { get; set; }
            public string Owner { get; private set; }
            public string Stash { get; private set; }
            public FilterEntry(string owner, string stash, bool isChecked)
            {
                IsChecked = isChecked;
                Owner = owner;
                Stash = stash;
            }


            public int CompareTo(object other)
            {
                FilterEntry fi = other as FilterEntry;
                int cmp = Owner.CompareTo(fi.Owner);
                if (cmp != 0)
                    return cmp;
                return Stash.CompareTo(fi.Stash);
            }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public class ItemOfInterest : IComparable, INotifyPropertyChanged, IEquatable<ItemOfInterest>
        {
            public string Title { get; private set; }

            public string Owner { get; private set; }
            public string StashName { get; private set; }
            public string StashPos { get; private set; }
            public string ExtraLine { get; private set; }
            private bool _visible;
            public bool Visible
            {
                get
                {
                    return _visible;
                }
                set
                {
                    _visible = value;
                    OnPropertyChanged("Visible");
                }
            }

            public Visibility DisplayExtraLine
            {
                get
                {
                    return (ExtraLine == "" ? Visibility.Collapsed : Visibility.Visible);
                }
            }

            private int PreAccSortKey;
            private int PreItemNameSortKey;
            private static Brush[] BrushByRareType = { Brushes.White, Brushes.Yellow, Brushes.DarkCyan, Brushes.LightGray };
            public InterestType Type { get; private set; }
            public Brush Color
            {
                get
                {
                    return BrushByRareType[(int)Type];
                }
            }

            public ItemOfInterest(string title, string owner, string stashName, string stashPos, InterestType interestType = InterestType.None, int preAccSortKey = 0, int preItemNameSortKey = 0, string extraLine = "")
            {
                Title = title;
                Owner = owner;
                StashName = stashName;
                StashPos = stashPos;
                PreAccSortKey = preAccSortKey;
                PreItemNameSortKey = preItemNameSortKey;
                Type = interestType;
                ExtraLine = extraLine;
                Visible = true;

            }

            public int CompareTo(object other)
            {
                ItemOfInterest o = other as ItemOfInterest;

                if (PreAccSortKey != o.PreAccSortKey)
                {
                    if (PreAccSortKey < o.PreAccSortKey)
                        return -1;
                    else
                        return 1;
                }

                int cmp = Owner.CompareTo(o.Owner);
                if (cmp != 0)
                    return cmp;

                if (PreItemNameSortKey != o.PreItemNameSortKey)
                {
                    if (PreItemNameSortKey < o.PreItemNameSortKey)
                        return -1;
                    else
                        return 1;
                }
                return Title.CompareTo(o.Title);
            }
            public bool Equals(ItemOfInterest o)
            {
                //return Title == o.Title && Owner == o.Owner;
                return Owner == o.Owner && StashName == o.StashName && StashPos == o.StashPos;
            }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private void IsEnabled_Checked(object sender, RoutedEventArgs e)
        {
            FilterEntry fi = ((FilterEntry)((FrameworkElement)e.OriginalSource).DataContext);
            UncheckedFiltersEx[LeagueIndex].Remove(fi.Owner + '@' + fi.Stash);
            RefreshVisibilityOfFilteredItems();
            FiltersChanged = true;
        }

        private void IsEnabled_Unchecked(object sender, RoutedEventArgs e)
        {
            FilterEntry fi = ((FilterEntry)((FrameworkElement)e.OriginalSource).DataContext);
            UncheckedFiltersEx[LeagueIndex].Add(fi.Owner + '@' + fi.Stash);
            RefreshVisibilityOfFilteredItems();
            FiltersChanged = true;
        }

        private void RefreshVisibilityOfFilteredItems()
        {
            foreach (var ioi in InterestedItems)
            {
                if (ioi.Type == InterestType.RareNameMatch3)
                    continue;
                ioi.Visible = (!UncheckedFiltersEx[LeagueIndex].Contains(ioi.Owner + '@' + ioi.StashName));
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (FiltersChanged)
                Save(LeagueIndex);
        }
    }
}
