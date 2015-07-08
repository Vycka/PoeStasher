using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using POEStasher.Helpers;

namespace POEStasher.AccountManager
{
    [Serializable()]
    public class PoeAccHandler : ISerializable, INotifyPropertyChanged
    {
        private const string LoginFullUrl = "https://www.pathofexile.com/login"; //used for logging in
        private const string RootFullUrl = "https://www.pathofexile.com/"; //used for checking if logged in
        private const string StashUrl = "http://www.pathofexile.com/character-window/get-stash-items";
        private const int MaxReadBuffer = 1024 * 512;
        
        private CookieContainer cookies = new CookieContainer();

        
        private string _UserLogin, _UserPassword;
        public string UserLogin
        {
            get
            {
                return _UserLogin;
            }
            set
            {
                _UserLogin = value;
                UserDisplayName = null;
                cookies = new CookieContainer();
                OnPropertyChanged("UserDisplayName");
            }
        }
        public string UserPassword
        {
            get
            {
                return _UserPassword;
            }
            set
            {
                _UserPassword = value;
                UserDisplayName = null;
                cookies = new CookieContainer();
                OnPropertyChanged("UserDisplayName");
            }
        }
        private string loggedInName;
        
        public bool LoggedIn
        {
            get
            {
                //System.Windows.MessageBox.Show(loggedInName);
                return loggedInName != null && loggedInName != UserLogin;
            }
        }

        public PoeAccHandler(string userLogin, string userPassword)
        {
            this.UserLogin = userLogin;
            this.UserPassword = userPassword;
            LastStashRefresh = UnixTime.UnixTimeStart;
        }

        public PoeAccHandler(SerializationInfo info, StreamingContext ctxt)
        {
            cookies = (CookieContainer)info.GetValue("cookies", typeof(CookieContainer));
            UserDisplayName = info.GetString("UserDisplayName");
            _UserLogin = info.GetString("userLogin");
            _UserPassword = info.GetString("userPassword");
            LastStashRefresh = info.GetDateTime("LastStashRefresh");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("UserDisplayName", UserDisplayName);
            info.AddValue("cookies", cookies);
            info.AddValue("userLogin", UserLogin);
            info.AddValue("userPassword", UserPassword);
            info.AddValue("LastStashRefresh", LastStashRefresh);
        }

        public bool CheckLogin()
        {
            string recvStr = RequestGET(RootFullUrl);
            UserDisplayName = FindDisplayName(recvStr);
            return LoggedIn;
        }

        public bool Login()
        {
            string postData = "login_email=" + UserLogin + "&login_password=" + UserPassword + "&remember_me=1&login=Login";
            string recvStr = RequestPOST(LoginFullUrl, postData, false);
            if (recvStr == null)
                return false;

            UserDisplayName = FindDisplayName(recvStr);
            if (!LoggedIn)
            {
                System.Windows.MessageBox.Show("Bad Username and/or password");
                return false;
            }

            return true;
        }

        public delegate void StashRetrieveProgressReport(object sender, int currentStash, int totalStashes);
        public event StashRetrieveProgressReport StashProgressChanged = null;
        private void ReportStashChange(int currentStash, int totalStashes)
        {
            if (StashProgressChanged != null)
                StashProgressChanged(this, currentStash, totalStashes);
        }

        /// <summary>
        /// returns all stash tabs
        /// </summary>
        /// <param name="league">league name</param>
        /// <returns>returns array[4-n] if ok, null if connection error, array[1] if bad data received</returns>
        public string[] RetrieveAllItemTabs(string league)
        {
            string firstTab = RetrieveItemTab(league, 0);
            if (firstTab == null)
                return null;
            if (firstTab == "")
            {
                return new string[1];
            }

            int p1 = firstTab.IndexOf(',');
            int tabCount = int.Parse(firstTab.Substring(11, p1 - 11));

            string[] tabs = new string[tabCount];
            tabs[0] = firstTab;
            for (int x = 1; x < tabCount; x++)
            {
                ReportStashChange(x, tabCount);
                string tab = RetrieveItemTab(league, x);
                if (tab == null)
                    return null;
                tabs[x] = tab;
                
            }
            ReportStashChange(tabCount, tabCount);
            LastStashRefresh = DateTime.Now;
            return tabs;
        }

        private string RetrieveItemTab(string league, int tabIndex)
        {
            string postData = "league=" + league + "&tabIndex=" + tabIndex + "&tabs=" + (tabIndex == 0 ? "1" : "0");
            string ret = RequestPOST(StashUrl, postData);
            if (ret == null)
            {
                //System.Windows.MessageBox.Show(ret);
                return null;
            }
            if (!ret.StartsWith("{\"numTabs\":"))
            {
                return "";
            }
            return ret;
        }

        private string RequestGET(string url, bool silent = true)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.KeepAlive = false;
                httpRequest.CookieContainer = cookies;
                WebResponse response = null;
                return ExecRequest(httpRequest, out response, MaxReadBuffer, silent);
            }
            catch (ProtocolViolationException ex)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Protocol violation error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (WebException ex)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Unknown WebEx error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Unknown error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            return null;
        }
        private string RequestPOST(string url, string postData, bool silent = true)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Method = "POST";
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(postData);
                httpRequest.ContentLength = bytes.Length;
                httpRequest.KeepAlive = false;
                httpRequest.CookieContainer = cookies;
                //httpRequest.Accept = "application/json, text/javascript, */*; q=0.01";
                Stream oStream = httpRequest.GetRequestStream();
                oStream.Write(bytes, 0, bytes.Length);
                oStream.Close();
                WebResponse response = null;
                return ExecRequest(httpRequest, out response, MaxReadBuffer, silent);

            }
            catch (ProtocolViolationException ex)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Protocol violation error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (WebException ex)
            {
                if (!silent)
                {
                    if (ex.Message == "The remote server returned an error: (405) Method Not Allowed.")
                        System.Windows.MessageBox.Show("Unable to connect, looks like PoE servers are down!");
                    else
                        System.Windows.MessageBox.Show("Unknown WebEx error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Unknown error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            return null;
        }
        /// <summary>
        /// Executes given HttpWebRequest
        /// </summary>
        /// <param name="request">request to be executed</param>
        /// <param name="response">response retrieved from executing request or null on fail</param>
        /// <param name="maxRecvSize">max bytes to read (header not counted, it is read anyway), from the response</param>
        /// <param name="silent">Shows pupup on error</param>
        /// <returns>data retrieved by reading the response</returns>
        private string ExecRequest(HttpWebRequest request, out WebResponse response, int maxRecvSize = MaxReadBuffer, bool silent = true)
        {
            response = request.GetResponse();
            if (response == null)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Unable to connect!");
                return null;
            }
            if (maxRecvSize == 0)
            {
                response.Close();
                return "";
            }
            Stream rStream = response.GetResponseStream();
            byte[] recvData = new byte[maxRecvSize];
            int totalBytesRead = 0;
            int br = 1;
            while (br != 0 && totalBytesRead < recvData.Length)
            {
                br = rStream.Read(recvData, totalBytesRead, recvData.Length - totalBytesRead);
                totalBytesRead += br;
            }
            rStream.Close();
            response.Close();
            return System.Text.Encoding.UTF8.GetString(recvData, 0, totalBytesRead);
        }
        private string FindDisplayName(string recvStr)
        {
            int i = recvStr.IndexOf("profile-link");
            if (i == -1)
                return null;
            i = recvStr.IndexOf('>', i);
            if (i == -1)
                return null;
            int i2 = recvStr.IndexOf('<', i);
            if (i2 == -1)
                return null;
            return recvStr.Substring(i+1, i2 - i-1);
        }

        private DateTime _LastStashRefresh;
        public DateTime LastStashRefresh
        {
            get
            {
                return _LastStashRefresh;
            }
            private set
            {
                _LastStashRefresh = value;
                OnPropertyChanged("LastStashRefreshText");
                OnPropertyChanged("IsRefreshButtonEnabled");
            }
        }
        public void UpdateLastRefreshText()
        { 
            OnPropertyChanged("LastStashRefreshText");
            OnPropertyChanged("IsRefreshButtonEnabled");
        }
        public bool IsRefreshButtonEnabled
        {
            get
            { 
                return ((DateTime.Now - LastStashRefresh).TotalMinutes >= 2);
            }
        }
        public string LastStashRefreshText
        {
            get
            {
                if (_LastStashRefresh == UnixTime.UnixTimeStart)
                    return "never";
                int mins = (int)((DateTime.Now - _LastStashRefresh).TotalMinutes);
                if (mins > 119)
                    return (mins/60) + "hours ago.";
                else if (mins > 2879)
                    return (int)(mins/1440) + "days ago.";
                return mins + "mins ago.";
            }
        }
        public string UserDisplayName
        {
            get
            {
                return (loggedInName != null ? loggedInName : UserLogin);
            }
            private set
            {
                loggedInName = value;
                OnPropertyChanged("UserDisplayName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
