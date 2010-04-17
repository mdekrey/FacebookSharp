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

        public FacebookSignature(params KeyValuePair<string, string>[] items)
        {
            foreach (var i in items)
            {
                this.Add(i.Key, i.Value);
            }
        }

        public FacebookSignature(Dictionary<string, string> items)
        {
            foreach (var i in items)
            {
                this.Add(i.Key, i.Value);
            }
        }

        public string Signature { get; set; }

        public void Calculate(string secret)
        {
            string signatureString = GetSignatureString(); 
            Signature = GetMD5Hash(signatureString + secret);
        }

        private string GetSignatureString()
        {
            return string.Join("", this.Keys
                .OrderBy(k => k)
                .Select(k => k + "=" + this[k])
                .ToArray());
        }

        public bool Verify(string secret)
        {
            string signatureString = GetSignatureString();
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
