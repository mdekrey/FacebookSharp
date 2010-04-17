using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public class JsonNumber : JsonBase
    {
        public JsonNumber(Int64 value)
        {
            Value = value;
        }

        public Int64 Value { get; set; }

        public override string ToJsonString()
        {
            return Value.ToString();
        }

        public static JsonNumber Parse(System.IO.TextReader json)
        {
            StringBuilder sb = new StringBuilder();
            while (json.Peek()>='0' && json.Peek() <= '9')
            {
                sb.Append((char)json.Read());
            }
            return new JsonNumber(Int64.Parse(sb.ToString()));
        }
    }
}
