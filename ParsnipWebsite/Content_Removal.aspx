<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content_Removal.aspx.cs" Inherits="ParsnipWebsite.Content_Removal" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Content Removal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Libraries/w3.css-4.13/w3.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Lato/Lato.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Montserrat/Montserrat.css" />
    <link href="Libraries/fontawesome-free-5.15.1-web/css/all.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="Css/MediaStyle.css" />
    <!-- FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <div class="padded-text">
        In order to make the website enjoyable for everyone, I've decided to take content down on request. As much as the website is just
        meant to be a bit of fun, I understand that jokes can be taken too far and that some people may not appreciate certain things being
        on the website for very legitimate personal reasons. I'll make my best efforts to notify you if anything controversial might be
        uploaded onto the website and I will ofcourse take down anything that you deem to be offensive. Please be reasonable with your requests
        and understand if I can't act immediately.
    </div>

    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
    <script>
        if (isMobile()) {
            /*var body = document.getElementById("body")
            body.style = "margin-top:10%"*/


        }
        else {
            var main = document.getElementById("main")

            //main.style = "width:20%; left:60%; background-color:red"
    }
    </script>

</body >
</html>
