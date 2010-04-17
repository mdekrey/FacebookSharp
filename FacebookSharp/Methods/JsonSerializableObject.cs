using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FacebookSharp.Methods
{
    public class JsonSerializableObject : JsonBase
    {
        public JsonSerializableObject(object value)
        {
            Value = value;
        }

        public object Value { get; set; }

        public override string ToJsonString()
        {
            return JsonSerializationHelper.ToJsonString(Value);
        }
    }
}
