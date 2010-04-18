using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var signature = FacebookSharp.Signature.WebSignatureHelper.BuildCanvas(Request);
        bool verified = signature != null && signature.Verify();

        userAgent.Text = verified ? "Good signature\r\n" : "Bad signature\r\n";
        foreach (string o in Request.Headers)
        {
            userAgent.Text += o.ToString() + ": " + Request.Headers[o] + "\r\n";
        }
    }

}
