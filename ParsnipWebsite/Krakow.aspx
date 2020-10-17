<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Krakow.aspx.cs" Inherits="ParsnipWebsite.Krakow" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="mediaControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <title>Krakow</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Libraries/w3.css-4.13/w3.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Lato/Lato.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Montserrat/Montserrat.css" />
    <link rel="stylesheet" href="Libraries/fontawesome-free-5.15.1-web/css/all.css" />
    <link rel="stylesheet" type="text/css" href="Css/MediaStyle.css" />
    <!-- FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
</head>
<body>
    <menuControls:NewMenu runat="server" ID="NewMenu" />

    <form runat="server">
        <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
            <h1 class="w3-margin w3-jumbo jumbo-line-height" >Krakow</h1>
            <mediaControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        </header>
        <div runat="server" id="DynamicMediaDiv" style="margin: auto; text-align:center"></div>
    </form>    
    
    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
</body>
</html>

