using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FacebookSharp
{
    public static class JsonSerializationHelper
    {
        public static string ToJsonString(object data)
        {
            if (data == null)
            {
                return "null";
            }
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                string json = Encoding.Default.GetString(ms.ToArray());
                return json;
            }
        }

        public static T FromJsonString<T>(string data)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(data)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
