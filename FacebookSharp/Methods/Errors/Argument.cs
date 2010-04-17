using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FacebookSharp.Methods.Errors
{
    [DataContract]
    public class Argument
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
