using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public class JsonCustomObject : JsonBase
    {
        public JsonCustomObject(IDictionary<string, JsonBase> dict)
        {
            Properties = new Dictionary<string, JsonBase>(dict);
        }

        public JsonCustomObject()
        {
            Properties = new Dictionary<string, JsonBase>();
        }

        public Dictionary<string, JsonBase> Properties { get; private set; }

        public override string ToJsonString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            bool run = false;
            foreach (string key in Properties.Keys)
            {
                if (run)
                {
                    sb.Append(",");
                }
                run = true;
                sb.Append(JsonString.SerializeString(key)).Append(":").Append(JsonBase.SafeSerialize(Properties[key]));
            }
            sb.Append("}");
            return sb.ToString();
        }

        internal static JsonBase Parse(System.IO.TextReader json)
        {
            JsonCustomObject result = new JsonCustomObject();
            while (json.Peek() != '}')
            {
                char comma = (char)json.Read();
                System.Diagnostics.Debug.Assert(comma == ',' || comma == '{');
                string key = JsonString.Parse(json).Value;
                char c = (char)json.Read();
                System.Diagnostics.Debug.Assert(c == ':');
                result.Properties[key] = JsonBase.BaseParse(json);
            }
            char close = (char)json.Read();
            System.Diagnostics.Debug.Assert(close== '}');
            return result;
        }
    }
}
