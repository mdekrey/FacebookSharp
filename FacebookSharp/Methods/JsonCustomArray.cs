using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public class JsonCustomArray : JsonBase
    {
        public JsonCustomArray(IList<JsonBase> dict)
        {
            Properties = new List< JsonBase>(dict);
        }

        public JsonCustomArray()
        {
            Properties = new List< JsonBase>();
        }

        public List<JsonBase> Properties { get; private set; }

        public override string ToJsonString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            bool run = false;
            foreach (var obj in Properties)
            {
                if (run)
                {
                    sb.Append(",");
                }
                run = true;
                sb.Append(JsonBase.SafeSerialize(obj));
            }
            sb.Append("]");
            return sb.ToString();
        }

        internal static JsonBase Parse(System.IO.TextReader json)
        {
            JsonCustomArray result = new JsonCustomArray();
            json.Read();
            while (json.Peek() != ']')
            {
                result.Properties.Add(JsonBase.BaseParse(json));
                char comma = (char)json.Read();
                System.Diagnostics.Debug.Assert(comma == ',');
            }
            return result;
        }
    }
}
