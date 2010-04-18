using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace FacebookSharp.Web.FBML
{
    public class fbml : HtmlContainerControl
    {
        public fbml() : base("fb:fbml")
        {

        }

        public string Version
        {
            get { return Attributes["version"]; }
            set { Attributes["version"] = value; }
        }


    }
}
