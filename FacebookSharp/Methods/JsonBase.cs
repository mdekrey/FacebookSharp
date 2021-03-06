﻿using System;
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
                case '0': case '1': case '2': case '3': case '4': case '5': case '6': case'7': case '8': case '9':
                    return JsonNumber.Parse(json);
                default: // we hit some character we didn't recognize - if it's proper JSON, it's just whitespace.
                    json.Read();
                    return BaseParse(json);
            }
        }

    }
}
