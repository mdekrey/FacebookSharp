using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public class FacebookArgumentString : FacebookArgumentBase
    {
        public FacebookArgumentString(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public override string ToJsonString()
        {
            return "'" + Value.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
        }
    }
}
