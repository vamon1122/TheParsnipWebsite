<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Croatia.aspx.cs" Inherits="ParsnipWebsite.Croatia" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>

<!DOCTYPE html>
<html>
<head>
    <title>Croatia 2023</title>
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
<body style="text-align:center">
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <form runat="server"><asp:ScriptManager runat="server" EnablePageMethods="true" /></form>
    <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
        <h1 class="w3-margin w3-jumbo jumbo-line-height" >Croatia 2023</h1>
    </header>
    <h1>Availability <i class="fas fa-calendar-alt w3-text-red"></i></h1>
    <h5 class="w3-padding-32"><a href="https://1drv.ms/x/s!AraXa_9dEkgUhLxmw2crIsFDQ6tbmA?e=eupDd3">Click here</a> to update your availability</h5>
    <iframe width="404" height="400" style="margin-top:-45px" frameborder="0" scrolling="no" onload="document.getElementById('loading').style.display='none';" src="https://onedrive.live.com/embed?resid=1448125DFF6B97B6%2173318&authkey=%21ACiqvG62O09sEFw&em=2&wdAllowInteractivity=False&Item='Stats'!A1%3AD63&wdHideGridlines=True&wdInConfigurator=True&wdInConfigurator=True&edesNext=false&resen=false"></iframe>
    <p style="margin-top:-200px; padding-bottom:200px" id="loading">Loading <i runat="server" id="processing" class="fas fa-circle-notch w3-spin"></i>
    <div class="w3-container w3-black w3-center w3-opacity w3-padding-64">
        <h1 class="w3-margin w3-xlarge">Activities, flights & booking info coming soon...</h1>
    </div>
    <script src="Javascript/PageMethods.js"></script>
    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
</body>
</html>
