using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FacebookSharp.Methods.Errors
{
    [DataContract]
    public class FacebookError
    {
        [DataMember(Name = "error_code")]
        public Int64 ErrorCode { get; set; }
        [DataMember(Name = "error_msg")]
        public string ErrorMessage { get; set; }
        [DataMember(Name = "request_args")]
        public Argument[] RequestArgs { get; set; }
    }
}
