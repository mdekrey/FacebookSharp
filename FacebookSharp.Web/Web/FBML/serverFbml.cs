using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace FacebookSharp.Web.FBML
{
    public class serverFbml : HtmlContainerControl
    {
        public serverFbml() : base("fb:serverFbml")
        {

        }

        public string Condition
        {
            get { return Attributes["condition"]; }
            set { Attributes["condition"] = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);
            writer.WriteBeginTag("script");
            writer.WriteAttribute("type", "text/fbml");
            writer.Write(System.Web.UI.HtmlTextWriter.TagRightChar);

            base.RenderChildren(writer);

            writer.WriteEndTag("script");

            base.RenderEndTag(writer);
        }
    }
}
