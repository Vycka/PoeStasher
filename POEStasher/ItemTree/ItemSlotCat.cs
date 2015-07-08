using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Runtime.Serialization;
using POEStasher.ItemsManager;
using POEStasher.Helpers;

namespace POEStasher.ItemTree
{
    [Serializable()]
    public class ItemSlotCat : INotifyPropertyChanged
    {
        public string SlotName { get; private set; }
        public ObservableSortedList<PoeItem> ItemsList { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ItemSlotCat(string slotName)
        {
            ItemsList = new ObservableSortedList<PoeItem>();
            SlotName = slotName;
        }

        public void AddItem(PoeItem item)
        {
            ItemsList.Add(item);
            OnPropertyChanged("ItemCountDisplay");
        }

        public void DelItem(PoeItem item)
        {
            ItemsList.Remove(item);
            OnPropertyChanged("ItemCountDisplay");
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        public string ItemCountDisplay
        {
            get
            {
                return ItemsList.Count.ToString();
            }
        }

    }
}
