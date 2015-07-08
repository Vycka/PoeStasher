using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.BZip2;
using System.Configuration;

namespace POEStasher.Helpers
{
    public class Serializer<T>
    {
        public static readonly string ConfigurationDirectory;

        static Serializer()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            ConfigurationDirectory = config.FilePath.Remove(config.FilePath.Length - 11);
        }

        public static string SerializeToString(T data)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(ms, data);
            string ret = System.Convert.ToBase64String(ms.ToArray());
            ms.Close();
            ms.Dispose();
            return ret;
        }
        public static T Deserialize(string serializedString)
        {
            MemoryStream ms = new MemoryStream(System.Convert.FromBase64String(serializedString));
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(ms);
            ms.Close();
            ms.Dispose();
            return ret;
        }
        public static byte[] SerializeToBytes(T data)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(ms, data);
            byte[] ret = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return ret;
        }
        public static T Deserialize(byte[] serializedBytes)
        {
            MemoryStream ms = new MemoryStream(serializedBytes);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(ms);
            ms.Close();
            ms.Dispose();
            return ret;
        }
        public static void SerializeToFile(T data, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(fs, data);
            fs.Close();
            fs.Dispose();
        }
        public static T DeserializeFromFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(fs);
            fs.Close();
            fs.Dispose();
            return ret;
        }
        public static byte[] SerializeToBytesGZip(T data)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream gzs = new GZipStream(ms,CompressionMode.Compress);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(gzs, data);
            gzs.Close();
            gzs.Dispose();
            byte[] ret = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return ret;
        }
        public static T DeserializeGZip(byte[] serializedBytesGZip)
        {
            MemoryStream ms = new MemoryStream(serializedBytesGZip);
            GZipStream gzs = new GZipStream(ms, CompressionMode.Decompress);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(gzs);
            gzs.Close();
            gzs.Dispose();
            ms.Close();
            ms.Dispose();
            return ret;
        }

        public static void SerializeToFileGZip(T data, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            GZipStream gzs = new GZipStream(fs, CompressionMode.Compress);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(gzs, data);
            gzs.Close();
            gzs.Dispose();
            fs.Close();
            fs.Dispose();
        }
        public static T DeserializeFromFileGZip(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            GZipStream gzs = new GZipStream(fs, CompressionMode.Decompress);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(gzs);
            gzs.Close();
            gzs.Dispose();
            fs.Close();
            fs.Dispose();
            return ret;
        }

        public static void SerializeToFileBZip2(T data, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BZip2OutputStream bzs = new BZip2OutputStream(fs);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(bzs, data);
            bzs.Close();
            bzs.Dispose();
            fs.Close();
            fs.Dispose();
        }
        public static T DeserializeFromFileBZip2(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            BZip2InputStream bzs = new BZip2InputStream(fs);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(bzs);
            bzs.Close();
            bzs.Dispose();
            fs.Close();
            fs.Dispose();
            return ret;
        }

        public static byte[] SerializeToBytesBZip2(T data)
        {
            MemoryStream ms = new MemoryStream();
            BZip2OutputStream gzs = new BZip2OutputStream(ms);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(gzs, data);
            gzs.Close();
            gzs.Dispose();
            byte[] ret = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return ret;
        }
        public static T DeserializeBZip2(byte[] serializedBytesGZip)
        {
            MemoryStream ms = new MemoryStream(serializedBytesGZip);
            BZip2InputStream bzs = new BZip2InputStream(ms);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(bzs);
            bzs.Close();
            bzs.Dispose();
            ms.Close();
            ms.Dispose();
            return ret;
        }


        public static T DeserializeFromProperties(string keyName)
        {
            FileStream fs = new FileStream(ConfigurationDirectory + keyName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T ret = (T)bFormatter.Deserialize(fs);
            fs.Close();
            fs.Dispose();
            return ret;
        }
        public static void SerializeToProperties(T data, string keyName)
        {
            FileStream fs = new FileStream(ConfigurationDirectory + keyName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(fs, data);
            fs.Close();
            fs.Dispose();
        }
        public static bool PropertiesKeyExists(string keyName)
        {
            return File.Exists(ConfigurationDirectory + keyName);
        }
    }
}
