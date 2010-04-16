using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace FacebookSharp
{
    public class FacebookApi
    {
        private string apiKey;
        private string session;
        private string secret;
        private int callId = 0;

        public FacebookApi(string apiKey, string secret)
        {
            this.apiKey = apiKey;
            this.secret = secret;
        }
        
        public FacebookApi(string apiKey, string session, string secret)
        {
            this.apiKey = apiKey;
            this.session = session;
            this.secret = secret;
        }

        public string Call(string facebookMethod, Dictionary<string, Methods.JsonBase> arguments, bool ssl)
        {
            Dictionary<string, string> actualArgs = new Dictionary<string, string>();
            foreach (var arg in arguments)
            {
                actualArgs[arg.Key] = FacebookSharp.Methods.JsonBase.SafeSerialize(arg.Value);
            }
            actualArgs["api_key"] = apiKey;
            if (session != null)
            {
                actualArgs["session_key"] = session;
            }
            if (!actualArgs.ContainsKey("v"))
            {
                actualArgs["v"] = "1.0";
            }
            actualArgs["call_id"] = (callId++).ToString();
            actualArgs["format"] = "JSON";
            actualArgs["method"] = facebookMethod;

            Signature.FacebookSignature sig = new FacebookSharp.Signature.FacebookSignature(actualArgs);
            sig.Calculate(secret);
            actualArgs["sig"] = sig.Signature;

            HttpWebRequest request = CreateRequest(ssl, facebookMethod, actualArgs);
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
                else
                {
                    Stream responseStream = webResponse.GetResponseStream();
                    StreamReader sr = new StreamReader(responseStream);

                    return sr.ReadToEnd();
                }
            }
        }

        private HttpWebRequest CreateRequest(bool ssl, string facebookMethod, Dictionary<string, string> actualArgs)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetFacebookUrl(ssl));

            request.Method = "POST";

            StringBuilder requestBuilder = new StringBuilder(512);
            foreach (KeyValuePair<string, string> param in actualArgs)
            {
                if (requestBuilder.Length != 0)
                {
                    requestBuilder.Append("&");
                }

                requestBuilder.Append(param.Key);
                requestBuilder.Append("=");
                requestBuilder.Append(HttpUtility.UrlEncode(param.Value));
            }

            byte[] requestBytes = Encoding.UTF8.GetBytes(requestBuilder.ToString());

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBytes, 0, requestBytes.Length);
            }
            return request;
        }

        private Uri GetFacebookUrl(bool ssl)
        {
            if (ssl)
            {
                return new Uri("https://api.facebook.com/bestserver.php");
            }
            else
            {
                return new Uri("http://api.facebook.com/bestserver.php");
            }
        }
    }
}
