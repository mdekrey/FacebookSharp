<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml">
<head runat="server">
    <title>Testing page</title>
</head>
<body>
    <div id="fb-root"></div>
    <form id="form1" runat="server">
    <div>
    <fb:fbml ID="Fbml1" runat="server" Version="1.0"> 
    <div>
        Hello, <fb:name uid="loggedinuser" useyou="false"></fb:name>!
        <fb:login-button></fb:login-button>

        <pre><asp:Label runat="server" ID="userAgent" /></pre>
    </div>
        </fb:fbml>
    </div>
    <fb:init runat="server" ApplicationName="sandbox" status="true" cookie="true" xfbml="true" />
    </form>
    <script type="text/javascript">
        FB.Event.subscribe('auth.sessionChange', function(response) {
            if (response.session) {
                // A user has logged in, and a new cookie has been saved
            } else {
                // The user has logged out, and the cookie has been cleared
            }
        });
        FB.Event.subscribe('auth.login', function(response) {
            window.location.reload();
        });
    </script>
</body>
</html>
