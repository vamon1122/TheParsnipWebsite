﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyUploads.aspx.cs" Inherits="ParsnipWebsite.MyUploads" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="mediaControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>
<%@ Register Src="~/Custom_Controls/Loader.ascx" TagPrefix="loader" TagName="Loader" %>
<%@ Register Src="~/Custom_Controls/Media/MediaViewPageScripts.ascx" TagPrefix="scripts" TagName="MediaViewPageScripts" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>My Uploads</title>
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
        <asp:ScriptManager runat="server" EnablePageMethods="true" />
        <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
            <h1 class="w3-margin w3-jumbo" >Uploads</h1>
            <div id="uploadForm" runat="server" Visible="false">
                <mediaControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
            </div>
        </header>
        <div runat="server" id="UploadPrompt" class="w3-container">
            <div class="w3-center">
                <h2>Nothing to see 😢</h2>
                <label>You've not uploaded anything yet! When you get round to it, you can manage your content from here.</label>
            </div>
        </div>
        <div runat="server" id="MyMediaContainer" style="margin: auto; text-align:center"></div>
    </form>

    <scripts:MediaViewPageScripts runat="server" ID="MediaViewPageScripts" />
</body>
</html>



