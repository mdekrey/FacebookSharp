using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FacebookSharp.Methods.Errors
{
    [global::System.Serializable]
    public class InvalidCallException : Exception
    {
        public InvalidCallException()
        {
        }

        public InvalidCallException(FacebookError error)
            : base(error.ErrorMessage)
        {
            this.Error = error;
        }

        public InvalidCallException(FacebookError error, Exception inner)
            : base(error.ErrorMessage, inner)
        {
            this.Error = error;
        }

        protected InvalidCallException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Error = JsonSerializationHelper.FromJsonString<FacebookError>(info.GetString("InvalidCallExceptionError"));
        }

        public FacebookError Error { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("InvalidCallExceptionError", JsonSerializationHelper.ToJsonString(Error));
        }

        public long ErrorCode { get { return Error.ErrorCode; } }
        public Argument[] RequestArgs { get { return Error.RequestArgs; } }
    }
}
