using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.Adapters;

namespace FacebookSharp.Web
{
    public class CanvasPageAdapter : PageAdapter
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (Page.Request.Headers["X-FB-USER-REMOTE-ADDR"] != null)
            {
                writer.WriteBeginTag("fb:title");
                writer.Write(System.Web.UI.HtmlTextWriter.TagRightChar);
                writer.Write(Page.Title);
                writer.WriteEndTag("fb:title");
                Page.Form.RenderControl(writer);
            }
            else
            {
                base.Render(writer);
            }
        }
    }
}
