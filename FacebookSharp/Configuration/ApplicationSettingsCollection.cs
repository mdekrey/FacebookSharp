using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.Configuration
{
    public class ApplicationSettingsCollection : System.Configuration.ConfigurationElementCollection
    {
        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new ApplicationSettings();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((ApplicationSettings)element).Name;
        }


        public new ApplicationSettings this[string applicationName]
        {
            get { return (ApplicationSettings)BaseGet(applicationName); }
        }

    }
}
