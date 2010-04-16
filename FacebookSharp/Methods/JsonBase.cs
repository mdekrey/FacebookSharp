using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public abstract class JsonBase
    {
        public abstract string ToJsonString();
        
        public static implicit operator JsonBase(string arg)
        {
            return new JsonString(arg);
        }

        public static JsonBase Create(object arg)
        {
            if (arg is JsonBase)
            {
                return (JsonBase)arg;
            }
            return new JsonSerializableObject(arg);
        }

        public static string SafeSerialize(JsonBase jsonBase)
        {
            if (jsonBase == null)
            {
                return "null";
            }
            return jsonBase.ToJsonString();
        }

        public static JsonBase BaseParse(System.IO.TextReader json)
        {
            if (json.Peek() == -1)
            {
                return null;
            }
            switch ((char)json.Peek())
            {
                case '{':
                    return JsonCustomObject.Parse(json);
                case '\'':
                case '\"':
                    return JsonString.Parse(json);
                case '[':
                    return JsonCustomArray.Parse(json);
                default:
                    json.Read();
                    return BaseParse(json);
            }
        }
    }
}
