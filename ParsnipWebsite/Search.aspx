<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ParsnipWebsite.Search_Website" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="mediaControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/Media/SearchMediaControl.ascx" TagPrefix="mediaControls" TagName="SearchMediaControl" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>View Tag</title>
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
    <style>
        /*.search-input{
            width:70%
        }*/
        .search-bar{
            width:100%
        }
    </style>
</head>
<body>
    <errorHandler:ErrorHandler runat="server" ID="ErrorHandler" />
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <form runat="server">
        <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
        <h1 class="w3-margin w3-jumbo jumbo-line-height" style="overflow-wrap: break-word" runat="server">Search</h1>
            <mediaControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        </header>
        <div class="w3-container">
            <mediaControls:SearchMediaControl runat="server" id="SearchMediaControl" />
        </div>
        <br />
        <div style="text-align:center">
            <h5 runat="server" id="TagsTitle"></h5>
            <div runat="server" id="MediaTagContainer" style="overflow:hidden" />
            <br />
            <h5 runat="server" id="MediaTitle"></h5>
        </div>
        <div runat="server" id="MediaContainer" style="margin: auto; text-align:center" />
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
