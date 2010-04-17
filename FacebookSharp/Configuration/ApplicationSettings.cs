using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace FacebookSharp.Configuration
{
    public class ApplicationSettings : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("apiKey")]
        public string ApiKey
        {
            get
            {
                string apiKey = LoadApiKey();
                if (apiKey != null)
                {
                    return apiKey;
                }
                return (string)base["apiKey"];
            }
            set
            {
                base["apiKey"] = value;
            }
        }

        [ConfigurationProperty("appSecret")]
        public string AppSecret
        {
            get
            {
                string appSecret = LoadAppSecret();
                if (appSecret != null)
                {
                    return appSecret;
                }
                return (string)base["appSecret"];
            }
            set
            {
                base["appSecret"] = value;
            }
        }

        [ConfigurationProperty("apiKeyLoader")]
        public string ApiKeyLoader
        {
            get
            {
                return (string)base["apiKeyLoader"];
            }
            set
            {
                loaded = false;
                base["apiKeyLoader"] = value;
            }
        }

        [ConfigurationProperty("apiKeyLoaderMethod")]
        public string ApiKeyLoaderMethod
        {
            get
            {
                return (string)base["apiKeyLoaderMethod"];
            }
            set
            {
                loaded = false;
                base["apiKeyLoaderMethod"] = value;
            }
        }

        #region Method-based loaders

        public delegate void ApiKeyLoaderDelegate(string name, out string apiKey, out string appSecret);

        bool loaded = false;
        string loadedApiKey;
        string loadedAppSecret;
        private void LoadApiKeyAndSecret()
        {
            if (loaded)
            {
                return;
            }
            loaded = true;
            if (ApiKeyLoader == null || ApiKeyLoaderMethod == null)
            {
                return;
            }
            Type t = Type.GetType(ApiKeyLoader, false);
            if (t == null)
            {
                return;
            }
            MethodInfo mi = t.GetMethod(ApiKeyLoaderMethod, new Type[] { typeof(string), typeof(string).MakeByRefType(), typeof(string).MakeByRefType() });
            if (mi == null)
            {
                return;
            }
            ApiKeyLoaderDelegate del = (ApiKeyLoaderDelegate)Delegate.CreateDelegate(typeof(ApiKeyLoaderDelegate), mi, true);
            if (del == null)
            {
                return;
            }
            del(Name, out loadedApiKey, out loadedAppSecret);
        }

        private string LoadApiKey()
        {
            LoadApiKeyAndSecret();
            return loadedApiKey;
        }

        private string LoadAppSecret()
        {
            LoadApiKeyAndSecret();
            return loadedAppSecret;
        }

        #endregion
    }
}
