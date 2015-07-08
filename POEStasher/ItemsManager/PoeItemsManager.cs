using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using POEStasher.Helpers;

namespace POEStasher.ItemsManager
{
    public enum LeagueId
    {
        Default,
        Hardcore,
        CutThroat
    }
    
    [Serializable()]
    public class PoeItemsManager : ISerializable
    {
        

        public LeagueId LeagueIndex { get; private set; }
        //main data tables
        public SortedDictionary<string, int> OwnerItemsListUpdateTime;
        public Dictionary<string, SortedSet<string>> OwnerStashNames = new Dictionary<string,SortedSet<string>>();
        internal List<PoeItem> AllItems;

        //tables below can be recalculated from main data bales
        public SortedDictionary<string, List<PoeItem>> ItemsByOwner;
        internal SortedDictionary<string, int> RareItemNames;
        public ItemTree.CategorizedItemsList ItemsByCatForListView;
        private MainWindow LogWindow = null;

        public string LastOwner = "";
        public List<PoeItem> ItemsToAdd = new List<PoeItem>();
        public List<PoeItem> ItemsToDel = new List<PoeItem>();
        public DateTime LastTimeChanged = DateTime.Now.AddMinutes(-10);

        public PoeItemsManager(LeagueId lIndex = LeagueId.Default)
        {
            LeagueIndex = lIndex;
            InitializeList();
        }
        public PoeItemsManager(SerializationInfo info, StreamingContext ctxt)
        {
            InitializeList();
            AllItems = (List<PoeItem>)info.GetValue("AllItems", typeof(List<PoeItem>));
            OwnerItemsListUpdateTime = (SortedDictionary<string, int>)info.GetValue("OwnerItemsListUpdateTime", typeof(SortedDictionary<string, int>));
            LeagueIndex = (LeagueId)info.GetValue("LeagueIndex", typeof(LeagueId));
            //LeagueIndex = LeagueId.Default;
        }
        private void InitializeList()
        {
            ItemsByOwner = new SortedDictionary<string, List<PoeItem>>();
            AllItems = new List<PoeItem>();
            RareItemNames = new SortedDictionary<string, int>();
            ItemsByCatForListView = new ItemTree.CategorizedItemsList();
            OwnerItemsListUpdateTime = new SortedDictionary<string, int>();
        }

        public void SetMainWindowWithLog(MainWindow logWindow)
        {
            LogWindow = logWindow;
        }
        public void AddLog(string log)
        {
            if (LogWindow != null)
                LogWindow.AddLogEntry(log);
        }

        public void RecalculateFields()
        {
            if (AllItems.Count == 0)
                return;
            SortedSet<string> iNamePrefix = new SortedSet<string>(), iNameSuffix = new SortedSet<string>();
            foreach (string s in OwnerItemsListUpdateTime.Keys)
            {
                ItemsByOwner.Add(s, new List<PoeItem>());
            }

            foreach (PoeItem i in AllItems)
            {
                DistributeAddedItemToOtherLists(i);
                RecheckStashIndex(i);
                if (i.Identified && i.FrameType == PoeItem.FrameTypeIndex.Rare)
                {
                    string[] splittedName = i.Name.Split();

                    if (splittedName.Length != 2)
                        AddLog("??? -> " + i.Name);
                    {
                        iNamePrefix.Add(splittedName[0]);
                        iNameSuffix.Add(splittedName[1]);
                    }
                }
            }
            AddLog("|-> Possible item name variations: " + iNameSuffix.Count * iNamePrefix.Count);
            AddLog("[" + StaticVariables.LeagueNames[(int)LeagueIndex] + "] Items: " + AllItems.Count + " // 1st wc: " + iNamePrefix.Count + " // 2nd wc: " + iNameSuffix.Count);
        }

        public void DeleteItemsByOwner(string owner)
        {
            for (int x = 0; x < AllItems.Count; x++)
                if (AllItems[x].Owner == owner)
                {
                    DelDistributedItem(AllItems[x]);
                    AllItems.RemoveAt(x);
                    x--;
                }

            OwnerStashNames.Remove(owner);
            ItemsByOwner.Remove(owner);
            OwnerItemsListUpdateTime.Remove(owner);
            LastTimeChanged = DateTime.Now;

        }
        /// <summary>
        /// Distributes item to other tables (except AllItems)
        /// </summary>
        /// <param name="item">Item</param>
        private void DistributeAddedItemToOtherLists(PoeItem item)
        {
            //orb of alchemy name comparsion
            if (item.FrameType == PoeItem.FrameTypeIndex.Rare && item.Identified == true)
            {
                if (!RareItemNames.ContainsKey(item.Name))
                    RareItemNames.Add(item.Name, 1);
                else
                    RareItemNames[item.Name]++;

                int extraParam;
                ItemsOfInterest.InterestType iType = ItemsOfInterest.IsItItemOfInterest(item,this, out extraParam);
                if (iType != ItemsOfInterest.InterestType.None)
                {
                    if (iType == ItemsOfInterest.InterestType.RareNameMatch && extraParam >= 3)
                        FindAlchemyItemsByName(item.Name, extraParam);
                    //else if (iType == ItemsOfInterest.InterestType.HighQualityRare)
                    //    PrintHighQualityItem(item, extraParam);
                }

            } 
            //TreeView in ui to show
            ItemsByCatForListView.AddItem(item);
            ItemsByOwner[item.Owner].Add(item);
        }

        /// <summary>
        /// Deletes item from all distributed tables except AllItems
        /// </summary>
        /// <param name="item">item</param>
        private void DelDistributedItem(PoeItem item)
        {

            ItemsByOwner[item.Owner].Remove(item);
            ItemsByCatForListView.DelItem(item);
            if (item.FrameType == PoeItem.FrameTypeIndex.Rare && item.Identified == true)
            {
                int i = --RareItemNames[item.Name];
                if (i == 0)
                    RareItemNames.Remove(item.Name);
            }
        }

        private void PrintHighQualityItem(PoeItem item, int Quality)
        {
            AddLog("[" + StaticVariables.LeagueNames[(int)LeagueIndex] + "]: [" + item.TypeLine + "] " + Quality + "%: " + item.Name + " [" + item.Owner + " " + item.StashName + " " + item.PosX + "x" + item.PosY + "]");
        }
        private void FindAlchemyItemsByName(string itemName, int cnt)
        { 
            List<PoeItem> items = new List<PoeItem>();
            foreach (PoeItem i in AllItems)
                if (i.Name == itemName)
                    items.Add(i);
            
            foreach (var i in items)
                AddLog("|-> " + i.Owner + " [" + i.StashName +  " " +  i.PosX + "x" + i.PosY + "]");
            AddLog("[" + StaticVariables.LeagueNames[(int)LeagueIndex] + "] " + cnt + " items with same name: " + itemName);
        }



        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("AllItems", AllItems);
            info.AddValue("OwnerItemsListUpdateTime", OwnerItemsListUpdateTime);
            info.AddValue("LeagueIndex", LeagueIndex);
        }

        public void ExportBZip2File(string owner, string fileName)
        {
            PoeItemsByOwner pibo = new PoeItemsByOwner();
            pibo.Owner = owner;
            pibo.ListVersion = OwnerItemsListUpdateTime[owner];
            pibo.Items = ItemsByOwner[owner];
            pibo.League = LeagueIndex;
            Serializer<PoeItemsByOwner>.SerializeToFileBZip2(pibo, fileName);
        }
        public byte[] ExportBZip2Bytes(string owner)
        {
            PoeItemsByOwner pibo = new PoeItemsByOwner();
            pibo.Owner = owner;
            pibo.ListVersion = OwnerItemsListUpdateTime[owner];
            pibo.Items = ItemsByOwner[owner];
            pibo.League = LeagueIndex;
            return Serializer<PoeItemsByOwner>.SerializeToBytesBZip2(pibo);
        }

        public string GetPropertiesKeyName()
        {
            return StaticVariables.LeagueNames[(int)LeagueIndex] + ".league";
        }
        public void Save()
        {
            //Properties.Settings.Default.ItemsEx[(int)LeagueIndex] = Serializer<PoeItemsManager>.SerializeToString(this);
            //Properties.Settings.Default.Save();
            Serializer<PoeItemsManager>.SerializeToProperties(this, GetPropertiesKeyName());
        }



        /// <summary>
        /// Refreshes item list for specified owner
        /// Adds items not found in in ListByOwner
        /// Deletes items from ListByOwner, that are not found in given stash
        /// Saves updated list to Properties if changes have been made
        /// ALL ITEMS IN THE LIST MUST BELONG TO THE SAME OWNER
        /// </summary>
        /// <param name="stash">stash with all items of owner</param>
        ///<param name="owner">stash owner</param>
        /// <param name="version">stash version</param>
        /// <returns>returns true if any changes have been made to ListByOwner</returns>
        public bool ParseStashList(List<PoeItem> stash, int version = -1)
        {
            if (stash == null || stash.Count == 0)
                return false;

            if (version == -1)
                version = (int)UnixTime.Now;

            string owner = stash.First().Owner;
            LastOwner = owner;
            ItemsToAdd.Clear();
            ItemsToDel.Clear();
            if (!OwnerItemsListUpdateTime.ContainsKey(owner))
            {
                OwnerItemsListUpdateTime.Add(owner, version);
                ItemsByOwner.Add(owner, new List<PoeItem>());
            }
            else if (OwnerItemsListUpdateTime[owner] == version)
                return false;
            //stash.Sort();

            var ownerItems = ItemsByOwner[owner];


            foreach (PoeItem i in stash)
            {
                if (!ownerItems.Contains<PoeItem>(i))
                {
                    ItemsToAdd.Add(i);
                    RecheckStashIndex(i);
                }
            }

            foreach (PoeItem i in ownerItems)
            {
                if (!stash.Contains(i))
                    ItemsToDel.Add(i);
            }

            if (ItemsToAdd.Count == 0 && ItemsToDel.Count == 0)
                return false;
            //ItemsAdded = itemsToAdd.Count;
            //ItemsDeleted = itemsToDel.Count;

            foreach (PoeItem item in ItemsToDel)
            {
                DelDistributedItem(item);
                AllItems.Remove(item);
            }

            foreach (PoeItem item in ItemsToAdd)
            {
                AllItems.Add(item);
                DistributeAddedItemToOtherLists(item);
            }

            OwnerItemsListUpdateTime[owner] = version;
            LastTimeChanged = DateTime.Now;

            return true;
        }

        private void RecheckStashIndex(PoeItem item)
        {
            if (!OwnerStashNames.ContainsKey(item.Owner))
                OwnerStashNames.Add(item.Owner, new SortedSet<string>());
            if (!OwnerStashNames[item.Owner].Contains(item.StashName))
                OwnerStashNames[item.Owner].Add(item.StashName);
        }
    }
}
