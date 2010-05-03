<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml">
<head runat="server">
    <title>Testing page</title>
</head>
<body>
    <form id="form1" runat="server">
        <fb:fbml ID="Fbml1" runat="server" Version="1.0"> 
    <div>
        Hello, <fb:name uid="loggedinuser" />!
        <pre><asp:Label runat="server" ID="userAgent" /></pre>
    </div>
        </fb:fbml>
    </form>
</body>
</html>
