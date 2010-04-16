using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Signature
{
    /// <summary>
    /// Useful for verifying any and all signatures.
    /// See http://wiki.developers.facebook.com/index.php/Verifying_The_Signature
    /// </summary>
    public class FacebookSignature : Dictionary<string, string>
    {
        public FacebookSignature()
        {
        }

        public FacebookSignature(string signature, params KeyValuePair<string, string>[] items)
        {
            Signature = signature;
            foreach (var i in items)
            {
                this.Add(i.Key, i.Value);
            }
        }

        public FacebookSignature(string signature, Dictionary<string, string> items)
        {
            Signature = signature;
            foreach (var i in items)
            {
                this.Add(i.Key, i.Value);
            }
        }

        public string Signature { get; set; }

        public bool Verify(string secret)
        {
            string signatureString = string.Join("", this.Keys.OrderBy(k => k)
                .Select(k => k + "=" + this[k])
                .ToArray());
            return Signature == GetMD5Hash(signatureString + secret);
        }

        private string GetMD5Hash(string p)
        {
            byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(p));
            return string.Join("", hashBytes
                .Select(b => b.ToString("x2"))
                .ToArray());
        }
    }
}
