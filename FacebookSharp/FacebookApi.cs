using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FacebookSharp
{
    public class FacebookApi
    {
        private string apiKey;
        private string session;
        private string secret;
        private int callId = 0;

        public FacebookApi(string applicationName)
        {
            this.apiKey = Configuration.ConfigurationSection.GetSection()[applicationName].ApiKey;
            this.secret = Configuration.ConfigurationSection.GetSection()[applicationName].AppSecret;
        }
        
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

        public Methods.JsonBase Call(string facebookMethod, Dictionary<string, Methods.JsonBase> arguments, bool ssl, out HttpStatusCode statusCode)
        {
            string raw = CallReturnRawJson(facebookMethod, arguments, ssl, out statusCode);
            return Methods.JsonBase.BaseParse(new System.IO.StringReader(raw));
        }

        public T Call<T>(string facebookMethod, Dictionary<string, Methods.JsonBase> arguments, bool ssl, out HttpStatusCode statusCode)
        {
            string rawJson = CallReturnRawJson(facebookMethod, arguments, ssl, out statusCode);
            Methods.Errors.FacebookError errorObj = JsonSerializationHelper.FromJsonString<Methods.Errors.FacebookError>(rawJson);
            if (errorObj.ErrorCode != 0)
            {
                throw new Methods.Errors.InvalidCallException(errorObj);
            }
            return JsonSerializationHelper.FromJsonString<T>(rawJson);
        }

        protected string CallReturnRawJson(string facebookMethod, Dictionary<string, Methods.JsonBase> arguments, bool ssl, out HttpStatusCode statusCode)
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
            HttpWebResponse webResponse;
            try
            {
                webResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                webResponse = (HttpWebResponse)ex.Response;
            }

            using (webResponse)
            {
                statusCode = webResponse.StatusCode;
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream);

                return sr.ReadToEnd();
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

        public bool IsUserSession
        {
            get { return session != null; }
        }
    }
}
