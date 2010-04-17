using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FacebookSharp.Configuration
{
    public class ConfigurationSection : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ApplicationSettingsCollection Applications
        {
            get
            {
                return (ApplicationSettingsCollection)base[""];
            }
        }

        public new ApplicationSettings this[string applicationName]
        {
            get { return Applications[applicationName]; }
        }

        public ApplicationSettings FindByApiKey(string apiKey)
        {
            return Applications.OfType<ApplicationSettings>().FirstOrDefault(app => app.ApiKey == apiKey);
        }

        #region Static

        private static ConfigurationSection instance;
        /// <summary>
        /// Gets the configuration section
        /// </summary>
        /// <returns></returns>
        public static ConfigurationSection GetSection()
        {
            if (instance == null)
            {
                if (ConfigurationManager.AppSettings["facebookSharpSectionOverride"] == null)
                {
                    instance = (ConfigurationSection)ConfigurationManager.GetSection("facebookSharp");
                }
                else
                {
                    instance = (ConfigurationSection)ConfigurationManager.GetSection(ConfigurationManager.AppSettings["facebookSharpSectionOverride"]);
                }

                if (instance == null)
                {
                    throw new InvalidOperationException("This operation requires a FacebookSharp configuration section; please make sure your site is fully configured before using this functionality.");
                }
            }
            return instance;
        }

        #endregion
    }
}
