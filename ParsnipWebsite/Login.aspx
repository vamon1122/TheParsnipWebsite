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
    <div style="padding-top: 1.5%; padding-left:1.5%; padding-right:1.5%;">
        
    </div>
    <div class="w3-display-middle">
        <form runat="server">        
            <asp:ScriptManager runat="server" EnablePageMethods="true" />
            <img src="Resources/Media/Images/Local/Fat_Kieron_Cutout.JPG" style="max-width:100px; display:block; margin-left: auto; margin-right:auto;" />
            <div>
                <br />    
                <asp:TextBox runat="server" CssClass="w3-input w3-border" ID="inputUsername" placeholder="username"  />
                <br />
                <div class="form-group">
                    <asp:TextBox runat="server" TextMode="password" CssClass="w3-input w3-border" ID="inputPwd" placeholder="password" />
                </div>
                <div style="text-align:right; width:100%">
                    <label class="form-check-label">Remember me:</label>
                    <input type="checkbox" id="inputRememberPwd" />
                </div>
                <br />
                <div style="float:right;">
                    <button onclick="TryLogin(); return false;" class="w3-btn w3-black">LogIn</button>
                </div>
            </div>
        </form>
    </div>
    <script>
        PageMethods.set_path(PageMethods.get_path() + '.aspx');

        function TryLogin() {
            document.getElementById('inputPwd').focus();

            var username = document.getElementById('inputUsername').value;
            var password = document.getElementById('inputPwd').value;
            var rememberPassword = document.getElementById('inputRememberPwd').checked;
            PageMethods.OnTryLogin(username, password, rememberPassword, OnTryLoginComplete);
        }

        function OnTryLoginComplete(response, userContext, methodName) {
            if (response == true) {
                const params = new Proxy(new URLSearchParams(window.location.search), {
                    get: (searchParams, prop) => searchParams.get(prop),
                });

                window.location.href = params.url ?? 'home';
            }
            else {
                alert('Login failed');
            }
        }
    </script>
</body>
</html>

