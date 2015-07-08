using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using POEStasher.Helpers;

namespace POEStasher.ItemsManager
{

    //INotifyPropertyChanged is not used, it is only implamented because ObservableSortedList wanted it to be implamented
    [Serializable()]
    public class PoeItem : ISerializable, IComparable, IEquatable<PoeItem>, INotifyPropertyChanged
    {

        public string Name { get; private set; }
        public string TypeLine { get; private set; }
        public string Owner { get; private set; }
        public string StashName { get; private set; }
        public FrameTypeIndex FrameType { get; private set; }
        public bool Identified { get; private set; }
        public List<ItemSocket> Sockets { get; private set; }
        public List<PoeItem.ItemProperty> ItemProperties { get; private set; }
        public List<PoeItem.ItemProperty> ItemRequirements { get; private set; }
        public List<string> ImplicitMods { get; private set; }
        public List<string> ExplicitMods { get; private set; }
        public byte Width { get; private set; }
        public byte Height { get; private set; }
        public string DescrText { get; private set; }
        public string SecDescrText { get; private set; }
        public byte PosX { get; private set; }
        public byte PosY { get; private set; }
        public List<string> FlavourText { get; private set; }

        private ClassIndex ClassCat = ClassIndex.Misc;
        private int SlotCat = 0;
        private byte MinLevelRequired = 0; //used for sort

        public PoeItem(string name, string typeLine, string owner, string stashName, int frameType, bool identified, List<ItemSocket> sockets,
                       List<ItemProperty> itemProperties, List<ItemProperty> itemRequirements,
                       List<string> implicitMods, List<string> explicitMods,
                       byte w, byte h, string descrText, string secDescrText, byte x, byte y, List<string> flavourText)
        {
            Name = name;
            TypeLine = typeLine;
            Owner = owner;
            StashName = stashName;
            FrameType = (FrameTypeIndex)frameType;
            Identified = identified;
            Sockets = sockets;
            ItemProperties = itemProperties;
            ItemRequirements = itemRequirements;
            ImplicitMods = implicitMods;
            ExplicitMods = explicitMods;
            Width = w;
            Height = h;
            DescrText = descrText;
            SecDescrText = secDescrText;
            PosX = x;
            PosY = y;
            FlavourText = flavourText;

            FillOtherFields();
        }

        #region Serialization
        public PoeItem(SerializationInfo info, StreamingContext ctxt)
        {
            Name = info.GetString("Name");
            TypeLine = info.GetString("TypeLine");
            Owner = info.GetString("Owner");
            StashName = info.GetString("StashName");
            FrameType = (FrameTypeIndex)info.GetInt32("FrameType");
            Identified = info.GetBoolean("Identified");
            Sockets = (List<ItemSocket>)info.GetValue("Sockets", typeof(List<ItemSocket>));
            ItemProperties = (List<PoeItem.ItemProperty>)info.GetValue("ItemProperties", typeof(List<PoeItem.ItemProperty>));
            ItemRequirements = (List<PoeItem.ItemProperty>)info.GetValue("ItemRequirements", typeof(List<PoeItem.ItemProperty>));
            ImplicitMods = (List<string>)info.GetValue("ImplicitMods", typeof(List<string>));
            ExplicitMods = (List<string>)info.GetValue("ExplicitMods", typeof(List<string>));
            Width = info.GetByte("Width");
            Height = info.GetByte("Height");
            DescrText = info.GetString("DescrText");
            SecDescrText = info.GetString("SecDescrText");
            PosX = info.GetByte("PosX");
            PosY = info.GetByte("PosY");
            FlavourText = (List<string>)info.GetValue("FlavourText", typeof(List<string>));

            FillOtherFields();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Name", Name);
            info.AddValue("TypeLine", TypeLine);
            info.AddValue("Owner", Owner);
            info.AddValue("StashName", StashName);
            info.AddValue("FrameType", (int)FrameType); //casting to keep compatibility
            info.AddValue("Identified", Identified);
            info.AddValue("Sockets", Sockets);
            info.AddValue("ItemProperties", ItemProperties);
            info.AddValue("ItemRequirements", ItemRequirements);
            info.AddValue("ImplicitMods", ImplicitMods);
            info.AddValue("ExplicitMods", ExplicitMods);
            info.AddValue("Width", Width);
            info.AddValue("Height", Height);
            info.AddValue("DescrText", DescrText);
            info.AddValue("SecDescrText", SecDescrText);
            info.AddValue("PosX", PosX);
            info.AddValue("PosY", PosY);
            info.AddValue("FlavourText", FlavourText);
        }
        #endregion
        #region WPF Interaction Functions
        public Visibility DisplayNameLine
        {
            get
            {
                return (Name != "" ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        public string DisplayHtmlProperties
        {
            get
            {
                if (ItemProperties.Count == 0)
                    return "";
                string ret = "<p style=\"text-align:center; font-size:14px;\">";
                int reqIndex = 0;
                foreach (ItemProperty p in ItemProperties)
                {
                    int valIndex = 0;
                    if (p.DisplayMode == 3)
                    {
                        string s = p.Name;
                        int x = 0;
                        foreach (ItemProperty.PropertyValue val in p.Values)
                        {
                            s = s.Replace("%" + x++, "<font color=\"" + val.ValueColorCode + "\">" + val.Value + "</font>");
                        }
                        ret += s;
                    }
                    else
                    {
                        ret += p.Name + (p.Values.Count > 0 && p.Name.Length != 0 ? ": " : "");
                        foreach (ItemProperty.PropertyValue val in p.Values)
                        {
                            ret += "<font color=\"" + val.ValueColorCode + "\">" + val.Value + "</font>" + (++valIndex != p.Values.Count ? ", " : "");
                        }
                    }
                    ret += (++reqIndex != ItemProperties.Count ? "<br />" : "");
                }
                ret += "</p>";
                return ret;
            }
        }
        public Visibility DisplaySeperatorAfterProperties
        {
            get
            {
                return (ItemProperties.Count > 0 ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        public string DisplayHtmlRequirements
        {
            get
            {
                if (ItemRequirements.Count == 0)
                    return "";
                string ret = "<p style=\"text-align:center; font-size:14px;\"> Requires ";
                int reqIndex = 0;
                foreach (ItemProperty p in ItemRequirements)
                {
                    ret += p.Name + " ";

                    int valIndex = 0;

                    foreach (ItemProperty.PropertyValue val in p.Values)
                    {
                        ret += "<font color=\"" + val.ValueColorCode + "\">" + val.Value + "</font>" + (++valIndex != p.Values.Count ? ", " : "");
                    }
                    ret += (++reqIndex != ItemRequirements.Count ? ", " : "");
                }
                ret += "</p>";
                return ret;
            }
        }
        public Visibility DisplaySeperatorAfterRequirements
        {
            get
            {
                return (ItemRequirements.Count > 0 ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        public Visibility DisplaySeperatorAfterSecDescrText
        {

            get
            {
                return (SecDescrText != null && SecDescrText != "" ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        public string DisplayHtmlImplicitMods
        {
            get
            {
                if (ImplicitMods.Count == 0)
                    return "";
                string ret = "<p style=\"text-align:center; font-size:14px; color:#8888ff\">";
                int modIndex = 0;
                foreach (string mod in ImplicitMods)
                {
                    ret += mod;
                    if (++modIndex != ImplicitMods.Count)
                        ret += "<br />";
                }
                ret += "</p>";
                return ret;
            }
        }
        public Visibility DisplaySeperatorAfterImplicitMods
        {
            get
            {
                return (ImplicitMods.Count > 0 ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        public string DisplayHtmlExplicitMods
        {
            get
            {
                if (ExplicitMods.Count == 0)
                    return "";
                string ret = "<p style=\"text-align:center; font-size:14px; color:#8888ff\">";
                int modIndex = 0;
                foreach (string mod in ExplicitMods)
                {
                    ret += mod;
                    if (++modIndex != ExplicitMods.Count)
                        ret += "<br />";
                }
                ret += "</p>";
                return ret;
            }
        }
        public Visibility DisplaySeperatorAfterExplicitMods
        {
            get
            {
                return (ExplicitMods.Count > 0 ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        private string SocketStr(ItemSocket s)
        { 
            switch (s.Attr)
            {
                case 'D':
                    return "<font color=\"#d8ff42\">●</font>";
                case 'S':
                    return "<font color=\"#ea2c4a\">●</font>";
                case 'I':
                    return "<font color=\"#8888ff\">●</font>";
                default:
                    return "";
            }
        }
        public string DisplayHtmlSockets
        {
            get
            {
                if (Sockets.Count == 0)
                    return "";
                string ret = "<p style=\"text-align:center; font-size:16px;font-weight:900;\">";
                ItemSocket lastSocket = Sockets[0];
                ret += SocketStr(lastSocket);
                for (int x = 1; x < Sockets.Count; x++)
                {
                    ret += (Sockets[x].Group == lastSocket.Group ? "<font color=\"#FFFFFF\">↔</font>" : "&nbsp;&nbsp;&nbsp;") + SocketStr(Sockets[x]);
                    lastSocket = Sockets[x];
                }
                ret += "</p>";
                return ret;
            }
        }
        public Visibility DisplaySeperatorAfterSockets
        {
            get
            {
                return (Sockets.Count > 0 ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        public string DisplayOwnerInfo
        {
            get
            {
                return Owner + " Stash: " + StashName + " Pos: " + PosX + "x" + PosY;
            }
        }
        public Brush DisplayItemColor
        { 
            get
            {

                return ItemQualityBrushes[(int)FrameType];
            }
        }
        public int SlotCategory
        {
            get
            {
                return SlotCat;
            }
        }
        public int ClassCategory
        {
            get
            {
                return (int)ClassCat;
            }
        }
        #endregion

        /// <summary>
        /// Sort comparsion is by Required level (desc), BaseName (asc)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(object other)
        {
            PoeItem cmp = other as PoeItem;
            //int[] comparsions = { Name.CompareTo(cmp.Name), TypeLine.CompareTo(cmp.TypeLine), Owner.CompareTo(cmp.Owner) };
            //for (int x = 0; x < comparsions.Length; x++)
            //    if (comparsions[x] != 0)
            //        return comparsions[x];
            //return 0;

            //sorting gems by typename
            if (FrameType == FrameTypeIndex.Gem)
                return TypeLine.CompareTo(cmp.TypeLine);

            //sorting belts by typename
            if (ClassCat == ClassIndex.Misc && SlotCat == 9)
                return TypeLine.CompareTo(cmp.TypeLine);
 
            if (MinLevelRequired > cmp.MinLevelRequired)
                return -1;
            if (MinLevelRequired == cmp.MinLevelRequired)
                return TypeLine.CompareTo(cmp.TypeLine);
            else
                return 1;
        }
        public bool Equals(PoeItem other)
        {
            //return (Name.CompareTo(other.Name) == 0 && TypeLine.CompareTo(other.TypeLine) == 0 && Owner.CompareTo(other.Owner) == 0);
            //comparing by x,y,stash,name saves cpu cycles, however, if item position is changed in stash, it gets recreated (deleted/added again)
            return (PosX == other.PosX && PosY == other.PosY && Name == other.Name && StashName.CompareTo(other.StashName) == 0);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }


        private readonly Brush[] ItemQualityBrushes = { Brushes.White, new SolidColorBrush(Color.FromRgb(136,136,255)),
                                             new SolidColorBrush(Color.FromRgb(255,255,89)), new SolidColorBrush(Color.FromRgb(175,96,37)), 
                                             new SolidColorBrush(Color.FromRgb(27,162,155)), Brushes.White };


        private ClassIndex FindClassCat()
        {
            ClassIndex[] ClassTable = { ClassIndex.Misc, ClassIndex.Marauder, ClassIndex.Ranger,
                                    ClassIndex.Duelist, ClassIndex.Witch, ClassIndex.Templar, ClassIndex.Shadow, ClassIndex.Misc };
            if (FrameType == FrameTypeIndex.Gem || FrameType == FrameTypeIndex.Unique)
                return ClassIndex.Misc;
            byte Dex = 0, Int = 0, Str = 0;
            foreach (ItemProperty p in ItemRequirements)
            {
                if (p.Name.StartsWith("Dex"))
                    Dex = 2;
                else if (p.Name.StartsWith("Str"))
                    Str = 1;
                else if (p.Name.StartsWith("Int"))
                    Int = 4;
            }
            ClassIndex ret =  ClassTable[Int | Dex | Str];
            if (ret != ClassIndex.Misc)
                return ret;


            foreach (ItemProperty p in ItemProperties)
            {
                if (p.Name == "Evasion Rating")
                    Dex = 2;
                else if (p.Name == "Armour")
                    Str = 1;
                else if (p.Name == "Energy Shield")
                    Int = 4;
            }
            ret = ClassTable[Int | Dex | Str];
            if (ret != ClassIndex.Misc)
                return ret;

            //Quivers goes to rangers..
            if (TypeLine.EndsWith("Quiver"))
                return ClassIndex.Ranger;

            if (ItemProperties.Count == 0)
                return ClassIndex.Misc;
            if (ItemProperties[0].Name == "Bow")
                return ClassIndex.Ranger;
            if (ItemProperties[0].Name == "Staff")
                return ClassIndex.Templar;
            
            return ClassIndex.Misc;
        }
        private int FindSlotCat()
        {
            if (ClassCat == ClassIndex.Misc)
            {
                if (FrameType == FrameTypeIndex.Unique)
                    return 14;
                if (TypeLine.EndsWith("Belt") || TypeLine == "Rustic Sash")
                    return 9;
                else if (TypeLine.EndsWith("Ring"))
                    return 8;
                else if (TypeLine.EndsWith("Amulet"))
                    return 7;
                //else
                //{
                //    ItemsCategoryTable.CatSubCat cat = ItemsCategoryTable.Find(this);
                //    return cat.SlotIndex;
                //}
            }
            else if (ClassCat == ClassIndex.Marauder)
            {
                if (ItemProperties.Count > 0)
                {
                    if (ItemProperties[0].Name == "One Handed Mace")
                        return 0;
                    if (ItemProperties[0].Name == "Two Handed Mace")
                        return 1;
                    if (ItemProperties[0].Name == "One Handed Axe")
                        return 2;
                }
                if (TypeLine.EndsWith("Greaves"))
                    return 3;
                if (TypeLine.EndsWith("late"))
                    return 4;
                if (TypeLine.EndsWith("Casque"))
                    return 5;
                if (TypeLine.EndsWith("Gauntlets"))
                    return 6;
                if (TypeLine.EndsWith("Shield"))
                    return 7;
                return 8;
            }
            else if (ClassCat == ClassIndex.Ranger)
            {
                if (ItemProperties.Count > 0)
                {
                    if (ItemProperties[0].Name == "Bow")
                        return 0;
                    if (ItemProperties[0].Name == "One Handed Sword")
                        return 1;
                }
                if (TypeLine.EndsWith("Quiver"))
                    return 2;
                if (TypeLine.EndsWith("Boots")) //Boots
                    return 3;
                if (TypeLine.EndsWith("Leather") || TypeLine.EndsWith("Tunic") || TypeLine.EndsWith("Garb") || TypeLine.EndsWith("Jerkin")) //Chest
                    return 4;
                if (TypeLine.EndsWith("Hood") || TypeLine.EndsWith("Cap")) //Helment
                    return 5;
                if (TypeLine.EndsWith("Gloves")) //Gloves
                    return 6;
                if (TypeLine.EndsWith("Buckler")) //Shield
                    return 7;
                return 8;
            }
            else if (ClassCat == ClassIndex.Witch)
            { 
                //private static string[] PoeItemWitchCatNames = { "Wand", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
                if (ItemProperties.Count > 0)
                {
                    if (ItemProperties[0].Name == "Wand")
                        return 0;
                }
                if (TypeLine.EndsWith("Boots") || TypeLine.EndsWith("Slippers") || TypeLine.EndsWith("Shoes"))
                    return 1;
                if (TypeLine.EndsWith("Robe") || TypeLine.EndsWith("Vest") || TypeLine.EndsWith("Garb") || TypeLine.EndsWith("Wrap") || TypeLine.EndsWith("Vestment") || TypeLine.EndsWith("Regalia") || TypeLine.EndsWith("Silks"))
                    return 2;
                if (TypeLine.EndsWith("Circlet")) //Helm
                    return 3;
                if (TypeLine.EndsWith("Gloves")) //Gloves
                    return 4;
                if (TypeLine.EndsWith("Shield")) //Shield
                    return 5;
                return 6;
            }
            else if (ClassCat == ClassIndex.Duelist)
            { 
                //private static string[] PoeItemDuelistCatNames = { "1H Axe", "2H Axe", "1H Sword", "2H Sword", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
                if (ItemProperties.Count > 0)
                {
                    if (ItemProperties[0].Name == "One Handed Axe")
                        return 0;
                    if (ItemProperties[0].Name == "Two Handed Axe")
                        return 1;
                    if (ItemProperties[0].Name == "One Handed Sword")
                        return 2;
                    if (ItemProperties[0].Name == "Two Handed Sword")
                        return 3;
                }
                if (TypeLine.EndsWith("Boots")) //boots
                    return 4;
                if (TypeLine.EndsWith("Vest") || TypeLine.EndsWith("Brigandine") || TypeLine.EndsWith("Doublet") || TypeLine.EndsWith("Armor") || TypeLine.EndsWith("Wyrmscale") || TypeLine.EndsWith("Lamellar")) //chest
                    return 5;
                if (TypeLine.EndsWith("Helmet")) //helment
                    return 6;
                if (TypeLine.EndsWith("Gauntlets")) //gloves
                    return 7;
                if (TypeLine.EndsWith("Shield")) //shield
                    return 8;
                return 9;
            }
            else if (ClassCat == ClassIndex.Templar)
            {
                //private static string[] PoeItemTemplarCatNames = { "1H Mace", "Staff", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
                if (ItemProperties.Count > 0)
                {
                    if (ItemProperties[0].Name == "One Handed Mace")
                        return 0;
                    if (ItemProperties[0].Name == "Staff")
                        return 1;
                }
                if (TypeLine.EndsWith("Boots")) //boots
                    return 2;
                if (TypeLine.EndsWith("Vest") || TypeLine.EndsWith("Tunic") || TypeLine.EndsWith("Coat") || TypeLine.EndsWith("Doublet") || TypeLine.EndsWith("Ringmail") || TypeLine.EndsWith("Chainmail") || TypeLine.EndsWith("Hauberk")) //chest
                    return 3;
                if (TypeLine.EndsWith("Coif")) //helment
                    return 4;
                if (TypeLine.EndsWith("Gloves")) //gloves
                    return 5;
                if (TypeLine.EndsWith("Shield") || TypeLine.EndsWith("Bundle")) //shield
                    return 6;
                return 7;
            }
            else if (ClassCat == ClassIndex.Shadow)
            {
                //private static string[] PoeItemShadowCatNames = { "Claw", "Dagger", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
                if (ItemProperties.Count > 0)
                {
                    if (ItemProperties[0].Name == "Claw")
                        return 0;
                    if (ItemProperties[0].Name == "Dagger")
                        return 1;
                }
                if (TypeLine.EndsWith("Boots")) //boots
                    return 2;
                if (TypeLine.EndsWith("Vest") || TypeLine.EndsWith("Jacket") || TypeLine.EndsWith("Coat") || TypeLine.EndsWith("Raiment") || TypeLine.EndsWith("Garb") || TypeLine.EndsWith("Armor")) //chest
                    return 3;
                if (TypeLine.EndsWith("Mask")) //helment
                    return 4;
                if (TypeLine.EndsWith("Mitts")) //gloves
                    return 5;
                if (TypeLine.EndsWith("Shield")) //shield
                    return 6;
                return 7;
            }
            return 0;
        }

        private void FillOtherFields()
        {
            var catcat = Helpers.ItemsCategoryTable.Find(this);
            if (catcat == null)
            {
                //ClassCat = FindClassCat();
                //SlotCat = FindSlotCat();
                //if (SlotCat >= PoeItemClassSubCatNames[(int)ClassCat].Length)
                //{
                //    ClassCat = Helpers.ItemsCategoryTable.DefaultCat.ClassIndex;
                //    SlotCat = Helpers.ItemsCategoryTable.DefaultCat.SlotIndex;
                //}

                ClassCat = Helpers.ItemsCategoryTable.DefaultCat.ClassIndex;
                SlotCat = Helpers.ItemsCategoryTable.DefaultCat.SlotIndex;
            }
            else
            {
                ClassCat = catcat.ClassIndex;
                SlotCat = catcat.SlotIndex;
            }
            if (ItemRequirements.Count != 0 && ItemRequirements[0].Name == "Level") //First reqiurement is always level :) (hopefully chriss won't change it)
            {
                string lvlVal = ItemRequirements[0].Values[0].Value;
                if (lvlVal.EndsWith(" (gem)"))
                    lvlVal = lvlVal.Remove(lvlVal.Length - 6, 6);
                MinLevelRequired = byte.Parse(lvlVal);
                
            }
            
            //if (ItemRequirements.Count > 0)
            //    ItemRequirements.Insert(0, new ItemProperty("Requires ",0,new List<ItemProperty.PropertyValue>()));
        }

        [Serializable()]
        public class ItemProperty
        {
            [Serializable()]
            public class PropertyValue
            {
                public string Value { get; set; }
                public byte Color { get; set; }
                public PropertyValue(string value, byte color)
                {
                    Value = value;
                    Color = color;
                }
                #region WPF Interaction
                public string ValueColorCode
                {
                    get
                    {
                        //                          0         1             2       3          4         5             6          7          8           9
                        string[] ColorTable = { "#FFFFFF", "#8888ff", "#505050", "#505050", "#960000", "#366492", "#ffd700", "#505050", "#505050", "#505050" };
                        return ColorTable[Color];
                    }
                }
                #endregion
            }

            public string Name { get; set; }
            public List<PropertyValue> Values { get; set; }
            public byte DisplayMode { get; set; }
            public ItemProperty(string name, byte displayMode, List<PropertyValue> values)
            {
                Name = name;
                Values = values;
                DisplayMode = displayMode;
            }

            #region WPF Interaction
            #endregion
        }

        [Serializable()]
        public class ItemSocket
        {
            public byte Group { get; set; }
            public char Attr { get; set; }
            public ItemSocket(byte group, char attr)
            {
                Group = group;
                Attr = attr;
            }
        }

        public enum ClassIndex
        {
            Marauder, //0
            Ranger, //1
            Witch, //2
            Duelist, //3
            Templar, //4
            Shadow, //5
            Misc //6
        }

        public enum FrameTypeIndex
        {
            Normal,
            Magical,
            Rare,
            Unique,
            Gem,
            Orb
        }

    }
}
