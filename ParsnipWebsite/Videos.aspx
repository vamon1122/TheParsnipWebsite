﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Videos.aspx.cs" Inherits="ParsnipWebsite.Videos" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="mediaControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>
<%@ Register Src="~/Custom_Controls/Loader.ascx" TagPrefix="loader" TagName="Loader" %>
<%@ Register Src="~/Custom_Controls/Media/MediaViewPageScripts.ascx" TagPrefix="scripts" TagName="MediaViewPageScripts" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <title>Videos</title>
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
    <errorHandler:ErrorHandler runat="server" ID="ErrorHandler" />
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <loader:Loader runat="server" id="Loader" />
    <form runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true" />
        <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
            <h1 class="w3-margin w3-jumbo jumbo-line-height" style="overflow-wrap: break-word" id="TagName" runat="server">View Tag</h1>
            <p class="w3-xlarge" id="TagDescription" runat="server"></p>
            <mediaControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        </header>
        <div runat="server" id="DynamicMediaDiv" style="margin: auto; text-align:center"></div>
    </form>
    
    <scripts:MediaViewPageScripts runat="server" ID="MediaViewPageScripts" />
</body>
</html>

