<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ParsnipWebsite.Home" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="menuControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Home</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
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
<body>
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <header class="w3-container w3-red w3-center" style="padding:60px 16px 0px 16px">
        <h1 class="w3-margin w3-jumbo jumbo-line-height">#The<wbr />Parsnip Website</h1>
        <p class="w3-xlarge" id="MOTD" runat="server"></p>
        <form runat="server" id="uploadForm" style="height:70px" Visible="false">
            <menuControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        </form>
        <div id="UploadButtonPadding" runat="server" style="padding-bottom: 20px" class="w3-hide-large w3-hide-medium" Visible="false" />
        <div id="NoArrowPadding" runat="server" style="padding-bottom: 40px" class="w3-hide-small" />
	    <div id="ArrowPadding" runat="server" style="padding-bottom: 100px" class="w3-hide-large w3-hide-medium" />
        <div onclick="document.getElementById('section2').scrollIntoView({behavior: 'smooth', block: 'start'})" style="text-decoration:none; color:white;" class="bounce">
            <i class="fas fa-chevron-down fa-5x w3-hide-large w3-hide-medium"></i>
        </div>
    </header>
    <div id="section2" class="w3-row-padding w3-padding-64 w3-container">
        <div class="w3-content">
            <div class="w3-twothird">
                <h1><i class="fa fa-hashtag w3-text-red"></i>Awesome</h1>
                <h5 class="w3-padding-32">There's no need to break a sweat trying to find *THAT* video anymore.</h5>
                <p class="w3-text-grey">With 1000s of pictures and videos from the past 15 years, conveniently sorted into albums, there's no need to trawl through your camera roll and hope you haven't deleted it anymore (facepalm). You can even tag people! Have a go yourself... whenever you see the cloud button (top right), you can upload from your camera-roll or youtube, and tag a freind. It couldn't be easier!</p>
                <div runat="server" id="MediaTagContainer" style="overflow:hidden" />
            </div>
            <div class="w3-third w3-center">
            </div>
        </div>
    </div>
    <div class="w3-row-padding w3-light-grey w3-padding-64 w3-container">
        <div class="w3-content">
            <div class="w3-third w3-center"></div>
            <div class="w3-twothird w3-right">
                <h1>Look to the <br class="w3-hide-large w3-hide-medium" />clouds <i class="fa fa-cloud w3-text-red"></i></h1>
                <h5 class="w3-padding-32">Have you noticed the cloud floating in the top right of your screen?</h5>
                <p class="w3-text-grey">Any time you see the cloud icon, you can tap it to upload some new content! Found a juicy old video? Don't let it get lost in the group chat... 
                    imortalise it here! Found a cringey old youtube video? No worries! You can upload directly from youtube! Just click the cloud to get started!
                </p>
            </div>
        </div>
    </div>

    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
</body>
</html>


