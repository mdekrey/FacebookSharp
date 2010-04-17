﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FacebookSharp.Signature
{
    public static class WebSignatureHelper
    {
        const string fbCanvasPrefix = "fb_sig_";
        const string fbCanvasSignature = "fb_sig";
        const string fbConnectPrefix = "fbsetting_";

        public static FacebookSignature BuildCanvas(HttpRequest request)
        {
            string[] keys = request.QueryString.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(fbCanvasPrefix))
                .ToArray();
            if (keys.Length > 0)
            {
                return new FacebookSignature(keys.ToDictionary(k => k.Substring(fbCanvasPrefix.Length), k => request.QueryString[k])) { Signature = request.QueryString[fbCanvasSignature] };
            }

            keys = request.Form.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(fbCanvasPrefix))
                .ToArray();
            if (keys.Length > 0)
            {
                return new FacebookSignature(keys.ToDictionary(k => k.Substring(fbCanvasPrefix.Length), k => request.Form[k])) { Signature = request.Form[fbCanvasSignature] };
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
                .Where(s => s.StartsWith(fbConnectPrefix))
                .Where(k => Configuration.ConfigurationSection.GetSection().ApiKeys.Contains(k.Substring(fbConnectPrefix.Length)))
                .ToArray();
            return connectApiKeys;
        }

        public static FacebookSignature BuildConnect(HttpRequest request, string apiKey)
        {
            string[] keys = request.Cookies.Keys
                .OfType<string>()
                .Where(s => s.StartsWith(apiKey + "_"))
                .ToArray();
            if (keys.Length > 0)
            {
                return new FacebookSignature(keys.ToDictionary(k => k.Substring(apiKey.Length + 1), k => request.Cookies[k].Value)) { Signature = request.Form[apiKey] };
            }

            return null;
        }
    }
}