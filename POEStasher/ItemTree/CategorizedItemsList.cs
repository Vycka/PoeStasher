using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using POEStasher.ItemsManager;

namespace POEStasher.ItemTree
{

    [Serializable()]
    public class CategorizedItemsList
    {
        internal ObservableCollection<ItemClassCat> ClassCatList = new ObservableCollection<ItemClassCat>();

        private static string[] PoeItemSlotCatNames = { "Unknown" };
        private static string[] PoeItemMarauderCatNames = { "1H Mace", "2H Mace", "Boots", "Chest", "Helment", "Gloves", "Shield" };
        private static string[] PoeItemRangerCatNames = { "Bow", "1H Sword (Rapier)", "Quiver", "Boots", "Chest", "Helment", "Gloves", "Shield" };
        private static string[] PoeItemWitchCatNames = { "Wand", "Boots", "Chest", "Helment", "Gloves", "Shield" };
        private static string[] PoeItemDuelistCatNames = { "1H Axe", "2H Axe", "1H Sword", "2H Sword", "Boots", "Chest", "Helment", "Gloves", "Shield" };
        private static string[] PoeItemTemplarCatNames = { "1H Mace (Sceptre)", "Staff (Stave)", "Boots", "Chest", "Helment", "Gloves", "Shield" };
        private static string[] PoeItemShadowCatNames = { "Claw", "Dagger", "Boots", "Chest", "Helment", "Gloves", "Shield" };
        private static string[] PoeItemOtherCatNames = {  "Unknown", "Blue Gems", "Blue Modifiers", // 0 1 2
                                                         "Green Gems", "Green Modifiers", // 3 4
                                                         "Red Gems", "Red Modifiers",  // 5 6
                                                         "Amulets", "Rings" ,"Belts","HP Flasks", "Mana Flasks", "Other Flasks", "Maps", "Unique Items" };
        //                                                 7          8          9       10           11              12           13          14
        public static readonly string[] PoeItemClassCatNames = { "STR (Marauder)", "DEX (Ranger)", "INT (Witch)", 
                                                                 "STR/DEX (Duelist)", "STR/INT (Templar)", "DEX/INT (Shadow)", "Misc" };
        public static readonly string[][] PoeItemClassSubCatNames = { PoeItemMarauderCatNames, PoeItemRangerCatNames, PoeItemWitchCatNames, 
                                                                        PoeItemDuelistCatNames, PoeItemTemplarCatNames, PoeItemShadowCatNames,
                                                                        PoeItemOtherCatNames };

        public CategorizedItemsList()
        {


            int x = 0;
            foreach (string s in PoeItemClassCatNames)
            {
                ClassCatList.Add(new ItemClassCat(s,PoeItemClassSubCatNames[x++]));
            }
        }

        public void AddItem(PoeItem item)
        {
            ClassCatList[(int)item.ClassCategory].AddItem(item);
        }

        public void DelItem(PoeItem item)
        {
            ClassCatList[(int)item.ClassCategory].DelItem(item);
        }
    }
}
