<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ParsnipWebsite.Login" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="error" TagName="ErrorHandler" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Libraries/Fonts/Lato/Lato.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Montserrat/Montserrat.css" />
    <link rel="stylesheet" href="Libraries/w3.css-4.13/w3.css" />
    <link href="Libraries/fontawesome-free-5.15.1-web/css/all.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="Css/MediaStyle.css" />
    <!-- FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
</head>
<body>
    <error:ErrorHandler runat="server" id="ErrorHandler" />
    <form id="test" runat="server" defaultbutton="ButLogIn">        
        <asp:TextBox runat="server" CssClass="w3-input w3-border" ID="inputUsername" placeholder="username"  />
        <asp:TextBox runat="server" TextMode="password" CssClass="w3-input w3-border" ID="inputPwd" />
        <asp:Button runat="server" ID="ButLogIn" OnClick="ButLogIn_Click" CssClass="w3-btn w3-black" Text="Log In"></asp:Button>
    </form>
</body>
</html>

