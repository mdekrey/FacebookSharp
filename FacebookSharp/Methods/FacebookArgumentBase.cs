using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Methods
{
    public abstract class FacebookArgumentBase
    {
        public abstract string ToJsonString();
        
        public static implicit operator FacebookArgumentBase(string arg)
        {
            return new FacebookArgumentString(arg);
        }

        public static FacebookArgumentBase Create(object arg)
        {
            if (arg is FacebookArgumentBase)
            {
                return (FacebookArgumentBase)arg;
            }
            return new FacebookArgumentObject(arg);
        }
    }
}
