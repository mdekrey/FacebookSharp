using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FacebookSharp.Signature
{
    public static class WebSignatureHelper
    {
        const string fbCanvasPrefix = "fb_sig_";
        const string fbCanvasApiKey = "fb_sig_api_key";
        const string fbCanvasSignature = "fb_sig";
        const string fbConnectPrefix = "fbsetting_";
        const string fbNewConnectPrefix = "fbs_";

        public static FacebookSignature BuildCanvas(HttpRequest request)
        {
            string[] keys = request.QueryString.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(fbCanvasPrefix))
                .ToArray();
            if (keys.Length > 0)
            {
                var result = new FacebookSignature(keys.ToDictionary(k => k.Substring(fbCanvasPrefix.Length), k => request.QueryString[k])) { Signature = request.QueryString[fbCanvasSignature] };
                result.Secret = Configuration.ConfigurationSection.GetSection().FindByApiKey(request.QueryString[fbCanvasApiKey]).AppSecret;
                return result;
            }

            keys = request.Form.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(fbCanvasPrefix))
                .ToArray();
            if (keys.Length > 0)
            {
                var result = new FacebookSignature(keys.ToDictionary(k => k.Substring(fbCanvasPrefix.Length), k => request.Form[k])) { Signature = request.Form[fbCanvasSignature] };
                result.Secret = Configuration.ConfigurationSection.GetSection().FindByApiKey(request.Form[fbCanvasApiKey]).AppSecret;
                return result;
            }
            
            return null;
        }

        public static FacebookSignature BuildIframe(HttpRequest request)
        {
            return BuildCanvas(request);
        }

        public static string[] GetConnectedApiKeys(HttpRequest request)
        {
            string[] connectApiKeys = request.Cookies.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(fbConnectPrefix) || s.StartsWith(fbNewConnectPrefix))
                .Select(s => { 
                    if (s.StartsWith(fbConnectPrefix)) return s.Substring(fbConnectPrefix.Length);
                    return s.Substring(fbNewConnectPrefix.Length);
                })
                .Where(k => Configuration.ConfigurationSection.GetSection().ApiKeys.Contains(k))
                .ToArray();
            return connectApiKeys;
        }

        public static FacebookSignature BuildConnect(HttpRequest request, string apiKey)
        {
            if (request.Cookies["fbs_" + apiKey] != null)
            {
                var values = request.Cookies["fbs_" + apiKey].Value.Trim('"').Split('&');
                var items = values
                    .Where(v => !v.StartsWith("sig="))
                    .ToDictionary(v => HttpUtility.UrlDecode(v.Split('=')[0]), v => HttpUtility.UrlDecode(v.Split('=')[1]));

                var result = new FacebookSignature(items)
                {
                    Signature = values.First(v => v.StartsWith("sig=")).Substring(4),
                    Secret = Configuration.ConfigurationSection.GetSection().FindByApiKey(apiKey).AppSecret
                };
                return result;
            }

            string[] keys = request.Cookies.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(apiKey + "_"))
                .ToArray();
            if (keys.Length > 0)
            {
                var result = new FacebookSignature(keys.ToDictionary(k => k.Substring(apiKey.Length + 1), k => request.Cookies[k].Value)) { 
                    Signature = request.Form[apiKey] ,
                    Secret = Configuration.ConfigurationSection.GetSection().FindByApiKey(apiKey).AppSecret
                };
                return result;
            }

            return null;
        }
    }
}
