using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace POEStasher.ItemsManager
{
    public class HttpImportExport
    {
        //private static string key = "fdnf[adfnFfjasdf432165";
        private static int Protocol = 2;
        private const int MaxBytesToReceive = 4194304;
        private const string ServiceUrl = "http://links.kriste.eu/PoE/";
        public static byte[] GetList()
        {
            try
            {
                //Properties.Settings.Default.HttpKey = "fdnf[adfnFfjasdf432165";
                //Properties.Settings.Default.Save();
                String boundary = "B0unD-Ary@..";
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(ServiceUrl);
                httpRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                httpRequest.Method = "POST";

                string initialPostData = "";

                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"key\"";
                initialPostData += "\r\n\r\n" + Properties.Settings.Default.SyncKey;

                initialPostData += "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"protocol\"";
                initialPostData += "\r\n\r\n" + Protocol;

                string endPostData = "\r\n--" + boundary + "--";

                byte[] bInitialPostData = Encoding.ASCII.GetBytes(initialPostData);
                byte[] bEndPostData = Encoding.ASCII.GetBytes(endPostData);

                httpRequest.ContentLength = bInitialPostData.Length + bEndPostData.Length;
                httpRequest.KeepAlive = false;

                Stream oStream = httpRequest.GetRequestStream();
                oStream.Write(bInitialPostData, 0, bInitialPostData.Length);
                oStream.Write(bEndPostData, 0, bEndPostData.Length);
                oStream.Close();
                WebResponse response = null;
                int totalBytesRead;
                byte[] recvData = ExecRequest(httpRequest, out response, out totalBytesRead, MaxBytesToReceive, false);
                byte[] retRecvData = new byte[totalBytesRead];
                Buffer.BlockCopy(recvData,0,retRecvData,0,totalBytesRead);
                return retRecvData;
            }
            catch (ProtocolViolationException ex)
            {
                System.Windows.MessageBox.Show("Protocol violation error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (WebException ex)
            {
                System.Windows.MessageBox.Show("Unknown WebEx error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Unknown error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            return null;
        }
        public static bool Export(byte[] serializedItemsByOwner, string owner, int leagueId, int version)
        {
            try
            {
                String boundary = "B0unD-Ary@..";
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(ServiceUrl);
                httpRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                httpRequest.Method = "POST";

                string initialPostData = "";
                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"owner\"";
                initialPostData += "\r\n\r\n" + owner + "\r\n";

                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"key\"";
                initialPostData += "\r\n\r\n" + Properties.Settings.Default.SyncKey + "\r\n";

                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"leagueId\"";
                initialPostData += "\r\n\r\n" + leagueId + "\r\n";

                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"protocol\"";
                initialPostData += "\r\n\r\n" + Protocol + "\r\n";

                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"version\"";
                initialPostData += "\r\n\r\n" + version + "\r\n";

                initialPostData += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"stash\"; filename=\"stash.poe\"";
                initialPostData += "\r\nContent-Type: application/octet-stream";
                initialPostData += "\r\nContent-Transfer-Encoding: binary";
                initialPostData += "\r\n\r\n";
                //now comes binary file
                string endPostData = "\r\n--" + boundary +"--";

                byte[] bInitialPostData = Encoding.ASCII.GetBytes(initialPostData);
                byte[] bEndPostData = Encoding.ASCII.GetBytes(endPostData);

                httpRequest.ContentLength = bInitialPostData.Length + serializedItemsByOwner.Length + bEndPostData.Length;
                httpRequest.KeepAlive = false;

                Stream oStream = httpRequest.GetRequestStream();
                oStream.Write(bInitialPostData, 0, bInitialPostData.Length);
                oStream.Write(serializedItemsByOwner, 0, serializedItemsByOwner.Length);
                oStream.Write(bEndPostData, 0, bEndPostData.Length);
                oStream.Close();
                WebResponse response = null;
                int totalBytesRead;
                byte[] recv = ExecRequest(httpRequest, out response, out totalBytesRead);
                MemoryStream ms = new MemoryStream(recv, 0, totalBytesRead);
                StreamReader sr = new StreamReader(ms);
                string line = sr.ReadLine();
                if (line != null && line == "OK")
                {
                    sr.Close();
                    ms.Close();
                    return true;
                }
                else
                {
                    line = sr.ReadToEnd();
                    System.Windows.MessageBox.Show("Failed to sync " + owner + " items to central database. Server returned:\r\n" + line);
                    sr.Close();
                    ms.Close();
                    return false;
                }
            }
            catch (ProtocolViolationException)
            {
                //System.Windows.MessageBox.Show("Protocol violation error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (WebException)
            {
                //System.Windows.MessageBox.Show("Unknown WebEx error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("Unknown error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            return false;
        }
        private static byte[] ExecRequest(HttpWebRequest request, out WebResponse response, out int totalBytesRead, int maxRecvSize = 8192, bool silent = true)
        {
            response = request.GetResponse();
            totalBytesRead = 0;
            //totalBytesRead = 0;
            if (response == null)
            {
                if (!silent)
                    System.Windows.MessageBox.Show("Unable to connect to import/export service!");
                return null;
            }
            if (maxRecvSize == 0)
            {
                response.Close();
                return null;
            }
            Stream rStream = response.GetResponseStream();
            byte[] recvData = new byte[maxRecvSize];
            int br = 1;
            while (br != 0 && totalBytesRead < recvData.Length)
            {
                br = rStream.Read(recvData, totalBytesRead, recvData.Length - totalBytesRead);
                totalBytesRead += br;
            }
            rStream.Close();
            response.Close();
            return recvData;
        }
        public static byte[] Import(string owner, int leagueId, string importDir = "stash/")
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(ServiceUrl + importDir + owner + "." + leagueId + ".poe");
                httpRequest.KeepAlive = false;
                WebResponse response = null;
                int totalBytesRead;
                byte[] recvData = ExecRequest(httpRequest, out response, out totalBytesRead, MaxBytesToReceive, false);
                byte[] retRecvData = new byte[totalBytesRead];
                Buffer.BlockCopy(recvData, 0, retRecvData, 0, totalBytesRead);
                return retRecvData;
            }
            catch (ProtocolViolationException ex)
            {
                System.Windows.MessageBox.Show("Protocol violation error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (WebException ex)
            {
                System.Windows.MessageBox.Show("Unknown WebEx error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Unknown error:\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
            return null;
        }
    }
}
