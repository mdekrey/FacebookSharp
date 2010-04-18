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
    public class FacebookSignature
    {
        public FacebookSignature()
        {
            Values = new Dictionary<string, string>();
        }

        public FacebookSignature(params KeyValuePair<string, string>[] items) 
            : this()
        {
            foreach (var i in items)
            {
                Values.Add(i.Key, i.Value);
            }
        }

        public FacebookSignature(Dictionary<string, string> items)
            : this()
        {
            foreach (var i in items)
            {
                Values.Add(i.Key, i.Value);
            }
        }

        public Dictionary<string, string> Values { get; private set; }
        public string Signature { get; set; }
        public string Secret { private get; set; }

        public void Calculate()
        {
            string signatureString = GetSignatureString(); 
            Signature = GetMD5Hash(signatureString + Secret);
        }

        private string GetSignatureString()
        {
            return string.Join("", Values.Keys
                .OrderBy(k => k)
                .Select(k => k + "=" + Values[k])
                .ToArray());
        }

        public bool Verify()
        {
            string signatureString = GetSignatureString();
            return Signature == GetMD5Hash(signatureString + Secret);
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
