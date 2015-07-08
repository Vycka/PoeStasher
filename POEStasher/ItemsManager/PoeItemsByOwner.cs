using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace POEStasher.ItemsManager
{
    [Serializable()]
    public class PoeItemsByOwner : ISerializable
    {
        public string Owner { get; set; }
        public int ListVersion { get; set; }
        public List<PoeItem> Items;
        public LeagueId League;

        public PoeItemsByOwner()
        {
            Owner = "";
            ListVersion = 0;
            Items = new List<PoeItem>();
            League = LeagueId.Default;
        }
        public PoeItemsByOwner(SerializationInfo info, StreamingContext ctxt)
        {
            Owner = info.GetString("Owner");
            ListVersion = info.GetInt32("ListVersion");
            Items = (List<PoeItem>)info.GetValue("Items", typeof(List<PoeItem>));
            League = (LeagueId)info.GetValue("League", typeof(LeagueId));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Items", Items);
            info.AddValue("ListVersion", ListVersion);
            info.AddValue("Owner", Owner);
            info.AddValue("League", League);
        }
    }
}
