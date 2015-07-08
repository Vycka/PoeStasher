using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Runtime.Serialization;
using POEStasher.ItemsManager;

namespace POEStasher.ItemTree
{
    [Serializable()]
    public class ItemClassCat : INotifyPropertyChanged
    {
        public string ClassName { get; private set; }
        public ObservableCollection<ItemSlotCat> SlotCatList { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private int ItemCountInThisCat = 0;

        //public bool RequiresResort { get; private set; }
        public ItemClassCat(string className, string[] subCatItems)
        {
            SlotCatList = new ObservableCollection<ItemSlotCat>();
            //RequiresResort = false;
            ClassName = className;
            foreach (string s in subCatItems)
            {
                SlotCatList.Add(new ItemSlotCat(s));
            }
        }

        public void AddItem(PoeItem item)
        {
            //RequiresResort = true;
            SlotCatList[(int)item.SlotCategory].AddItem(item);

            ItemCountInThisCat++;
            OnPropertyChanged("ItemCountDisplay");
        }

        public void DelItem(PoeItem item)
        {
            SlotCatList[(int)item.SlotCategory].DelItem(item);

            ItemCountInThisCat--;
            OnPropertyChanged("ItemCountDisplay");
        }

        public string ItemCountDisplay
        {
            get
            {
                return ItemCountInThisCat.ToString();
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

    }
}
