using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public class JsonString : JsonBase
    {
        public JsonString(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public override string ToJsonString()
        {
            return SerializeString(Value);
        }

        public static string SerializeString(string v)
        {
            return "'" + v.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
        }

        public static JsonString Parse(System.IO.TextReader json)
        {
            char startString = (char)json.Read();
            StringBuilder sb = new StringBuilder();
            while (startString != json.Peek() && json.Peek() != -1)
            {
                char curr = (char)json.Read();
                if (curr == '\\')
                {
                    curr = (char)json.Read();
                }
                sb.Append(curr);
            }
            char endString = (char)json.Read();
            System.Diagnostics.Debug.Assert(endString == startString);
            return new JsonString(sb.ToString());
        }
    }
}
