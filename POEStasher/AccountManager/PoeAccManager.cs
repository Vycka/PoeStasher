using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using POEStasher.Helpers;

namespace POEStasher.AccountManager
{
    public class PoeAccManager : IEnumerator, IEnumerable 
    {
        public ObservableCollection<PoeAccHandler> Accounts = new ObservableCollection<PoeAccHandler>();
        private int enumeratorPos = -1;
        MainWindow LogWindow;
        public PoeAccManager(MainWindow logWindow)
        {
            LogWindow = logWindow;
            LoadAccounts();
        }
        private void LoadAccounts()
        {
            try
            {
                if (Properties.Settings.Default.Accounts != "")
                    Accounts = Serializer<ObservableCollection<PoeAccHandler>>.Deserialize(Properties.Settings.Default.Accounts);
            }
            catch (Exception ex)
            {
                AddLog("Error Loading Accounts (Account list is lost): " + ex.Message);
                Accounts = new ObservableCollection<PoeAccHandler>();
                SaveAccounts();
            }
        }

        public void SaveAccounts()
        {
            Properties.Settings.Default.Accounts = Serializer<ObservableCollection<PoeAccHandler>>.SerializeToString(Accounts);
            Properties.Settings.Default.Save();
        }

        public void UpdateLastRefreshText()
        {
            foreach (PoeAccHandler pwr in Accounts)
                pwr.UpdateLastRefreshText();
        }

        public void AddAccount(string login, string pass)
        {
            Accounts.Add(new PoeAccHandler(login, pass));
            SaveAccounts();
        }
        public void DelAccount(int i)
        {
            Accounts.RemoveAt(i);
            SaveAccounts();
        }


        public PoeAccHandler GetAccount(int i)
        {
            return Accounts[i];
        }
        public int Count
        {
            get
            {
                return Accounts.Count;
            }
        }
        public bool MoveNext()
        {
            enumeratorPos++;
            if (enumeratorPos >= Accounts.Count)
            {
                Reset();
                return false;
            }
            return (enumeratorPos < Accounts.Count);
        }
        public void Reset()
        {
            enumeratorPos = -1;
        }
        public object Current
        {
            get
            {
                return Accounts[enumeratorPos];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
        public void AddLog(string log)
        {
            if (LogWindow != null)
                LogWindow.AddLogEntry(log);
        }
    }
}
