<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Latest.aspx.cs" Inherits="ParsnipWebsite.Latest" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="mediaControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>
<%@ Register Src="~/Custom_Controls/Media/MediaAccordion.ascx" TagPrefix="mediaControls" TagName="MediaAccordion" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Latest Uploads</title>
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
<body id="SomeLatestId">
    <errorHandler:ErrorHandler runat="server" ID="ErrorHandler" />
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <form runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true" />
        <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
            <h1 class="w3-margin w3-jumbo jumbo-line-height" style="overflow-wrap: break-word" id="TagName" runat="server">Latest Uploads</h1>
            <p class="w3-xlarge" id="TagDescription" runat="server"></p>
            <mediaControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        </header>
        <div runat="server" id="NoMediaContainer" visible="false">
            <h3>Nothing to see here :(</h3>
            <div runat="server" style="margin: auto; text-align:center">No media has been uploaded in the last month</div>
        </div>
        <mediaControls:MediaAccordion runat="server" id="LastMinuteAccordion" Visible="false" />
        <mediaControls:MediaAccordion runat="server" id="LastHourAccordion" Visible="false" />
        <mediaControls:MediaAccordion runat="server" id="TodayAccordion" Visible="false" />
        <mediaControls:MediaAccordion runat="server" id="YesterdayAccordion" Visible="false" />
        <mediaControls:MediaAccordion runat="server" id="LastWeekAccordion" Visible="false" />
        <mediaControls:MediaAccordion runat="server" id="LastMonthAccordion" Visible="false" />
        <mediaControls:MediaAccordion runat="server" id="LastThreeMonthsAccordion" Visible="false" />
    </form> 
    <%--<script>
        document.body.id = "somegeneratedid"
    </script>--%>
    <script src="Javascript/BodyId.js"></script>
    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
    <script src="Javascript/Accordion.js"></script>
    <script src="Javascript/CenterIntersectionObserver.js"></script>
    <script src="Javascript/ViewImages.js"></script>
</body>
</html>
