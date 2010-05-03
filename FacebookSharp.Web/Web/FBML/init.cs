using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookSharp.Methods;

namespace FacebookSharp.Web.FBML
{
    public class init : System.Web.UI.HtmlControls.HtmlControl
    {

        protected override void OnPreRender(EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("facebookConnect", "http://connect.facebook.net/en_US/all.js");
            JsonCustomObject obj = new JsonCustomObject();
            foreach (string key in Attributes.Keys)
            {
                obj.Properties.Add(key, new JsonString(Attributes[key]));
            }

            if (!string.IsNullOrEmpty(ApplicationName))
            {
                string apiKey = Configuration.ConfigurationSection.GetSection()[ApplicationName].ApiKey;
                obj.Properties.Add("apiKey", apiKey);
            }

            string json = obj.ToJsonString();
            string initString = "FB.init(" + json + ");";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "fbinit", initString, true);
            base.OnPreRender(e);
        }

        public string ApplicationName { get; set; }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
        }
    }
}
