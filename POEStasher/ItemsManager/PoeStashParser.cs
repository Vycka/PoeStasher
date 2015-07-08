using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace POEStasher.ItemsManager
{
    public class PoeStashParser
    {
        private static void ParseJsonStash(string json, string owner, ref List<PoeItem> poeItems,  string stashName = null)
        {
            JObject o = JObject.Parse(json);
            JArray items = (JArray)o["items"];
            foreach (JObject item in items)
            {
                int frameType = (int)item["frameType"];
                bool identified = (bool)item["identified"];
                if (frameType != (int)PoeItem.FrameTypeIndex.Orb && identified)
                {
                    string ItemName = (string)item["name"];     //primary name (name)
                    string typeLine = (string)item["typeLine"]; //type name (suffix)
                    if (stashName == null)
                        stashName = ((string)item["inventoryId"]).Remove(0,5); //it goes StashX... We don't need Stash part

                    //sockets
                    List<PoeItem.ItemSocket> sockets = new List<PoeItem.ItemSocket>();
                    JArray jaSockets = (JArray)item["sockets"];
                    foreach (JObject socket in jaSockets)
                    {
                        sockets.Add(new PoeItem.ItemSocket((byte)socket["group"], (char)socket["attr"]));
                    }

                    //properties (main item params) //TODO CHECK IF item["properties"] EXISTS FIRST
                    List<PoeItem.ItemProperty> ItemProperties = ParseProperties(item, "properties");
                    List<PoeItem.ItemProperty> ItemRequirements = ParseProperties(item, "requirements");
                    List<string> ImplicitMods = ParseMods(item, "implicitMods");
                    List<string> ExplicitMods = ParseMods(item, "explicitMods");
                    List<string> FlavourText = ParseMods(item, "flavourText");

                    List<PoeItem.ItemProperty> AdditionalProperties = ParseProperties(item, "additionalProperties");
                    string DescrText = GetStringIfExists(item, "descrText");
                    string SecDescrText = GetStringIfExists(item, "secDescrText");
                    byte x = (byte)item["x"];
                    byte y = (byte)item["y"];

                    byte w = (byte)item["w"];
                    byte h = (byte)item["h"];
                    poeItems.Add(new PoeItem(ItemName, typeLine, owner, stashName, frameType, identified, sockets,
                                             ItemProperties, ItemRequirements, ImplicitMods, ExplicitMods, w, h,
                                             DescrText, SecDescrText, x, y, FlavourText));
                }
            }
        }

        private static string GetStringIfExists(JObject item, string keyName)
        {
            string ret = "";
            try
            {
                ret = (string)item[keyName];
            }
            catch (Exception)
            {
                return "";
            }
            if (ret == null)
                return "";
            return ret;
        }
        private static List<string> ParseMods(JObject item, string keyName)
        {
            List<string> Mods = new List<string>();
            JArray jaMods = null;
            try
            {
                jaMods = (JArray)item[keyName];
            }
            catch (Exception)
            { }
            if (jaMods == null)
                return Mods;

            for (int x = 0; x < jaMods.Count; x++)
                Mods.Add((string)jaMods[x]);

            return Mods;
        }
        private static List<PoeItem.ItemProperty> ParseProperties(JObject item, string keyName)
        {
            List<PoeItem.ItemProperty> ItemProperties = new List<PoeItem.ItemProperty>();
            JArray jaProperties = null;
            try
            {
                jaProperties = (JArray)item[keyName];
            }
            catch (Exception)
            { }
            if (jaProperties == null)
                return ItemProperties;

            foreach (JObject property in jaProperties)
            {
                string propertyName = (string)property["name"];
                byte DisplayMode = (byte)property["displayMode"];
                JArray jaValues = (JArray)property["values"];
                List<PoeItem.ItemProperty.PropertyValue> propertyValues = new List<PoeItem.ItemProperty.PropertyValue>();
                foreach (JArray propertyValue in jaValues)
                {
                    propertyValues.Add(new PoeItem.ItemProperty.PropertyValue((string)propertyValue[0], (byte)propertyValue[1]));
                }
                ItemProperties.Add(new PoeItem.ItemProperty(propertyName, DisplayMode, propertyValues));
            }

            return ItemProperties;
        }

        public static List<PoeItem> DeserializeStash(string json, string owner, string stashName = null)
        {
            if (json == null)
                return null;
            List<PoeItem> poeItems = new List<PoeItem>();
            ParseJsonStash(json, owner, ref poeItems, stashName);
            return poeItems;
        }

        public static List<PoeItem> DeserializeStashes(string[] json, string owner)
        {
            if (json == null)
                return null;
            List<PoeItem> poeItems = new List<PoeItem>();
            JObject o = JObject.Parse(json[0]);
            int numTabs = (int)o["numTabs"];
            string[] stashNames = new string[numTabs];
            JArray ja = (JArray)o["tabs"];
            int x = 0;
            foreach (JObject tab in ja)
            {
                stashNames[x++] = (string)tab["n"];
            }

            x = 0;
            foreach (string stash in json)
            {
                ParseJsonStash(stash, owner, ref poeItems, stashNames[x++]);
            }
            return poeItems;
        }
    }
}
