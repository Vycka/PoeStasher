using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POEStasher.Helpers
{
    public class UnixTime
    {
        public static DateTime UnixTimeStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static int Now
        {
            get
            {
                TimeSpan span = (DateTime.Now - UnixTimeStart);
                return (int)span.TotalSeconds;
            }
        }
        public static DateTime UnixTimeToDateTime(int unixTime)
        {
            return UnixTimeStart.AddSeconds(unixTime);
        }
        public static int DateTimeToUnixTime(DateTime dt)
        {
            return (int)(dt - UnixTimeStart).TotalSeconds;
        }
    }
}
