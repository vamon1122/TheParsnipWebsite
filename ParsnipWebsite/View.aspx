<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="ParsnipWebsite.View_Image" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>View</title>
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
    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="ShareUserSuspendedError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access image. The image has been deleted or the person who shared this image has been suspended!
    </div>
    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="UploadUserSuspendedError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access image. The image has been deleted or the person who uploaded the image has been suspended!
    </div>
    <div runat="server" id="MediaContainer" style="display:inline-block; padding-top:65px; padding-bottom:5px; text-align:center; align-content:center; margin:auto; width:100%">
        <div runat="server" id="MediaTagContainer" class="center_div" style="margin:auto; padding-bottom:10px; margin:auto">More like this: </div>
        <div runat="server" id="TitleContainer" class="background-lightest media-viewer-title" style="margin:auto; display:block; padding-top:0px; padding-bottom:13px; min-height:38px">
            <h4 style="word-wrap:break-word; position:relative; display:inline; top:7px; margin:0px" runat="server" id="ImageTitle"></h4><i runat="server" id="unprocessed" class="fas fa-circle w3-right fa-2x" style="position:relative; right: -5px; top:6px" visible="false"></i><i runat="server" id="processing" class="fas fa-circle-notch w3-spin w3-right fa-2x" style="position:relative; right:-5px; top: 6px" visible="false"></i><i runat="server" id="error" class="far fa-times-circle w3-red w3-right fa-2x" style="position:relative; right: -5px; top: 6px" visible="false"></i>
        </div>
        <img runat="server" id="ImagePreview" class="media-viewer-landscape" style="max-width:100%" />
        <div runat="server" id="youtube_video_container" class="media-viewer-landscape" style="margin:auto" visible="false">
            <div runat="server" id="youtube_video" class="youtube-player" /></div>
            <video runat="server" id="video_container" controls="controls" class="media-viewer-landscape" style="display:inline-block; object-fit:contain; margin-bottom:0px; max-width:100%" preload="none" visible="false" >
                <source runat="server" id="VideoSource" type="video/mp4" />
                Your browser does not support HTML5 video.
            </video>
        <br />
        <div runat="server" id="viewCount"></div>
    </div>
    <form id="form1" runat="server" style="width:100%; margin-bottom:5%">
        <br />
        <br />
        <div style="padding-left:2.5%; padding-right:2.5%">
            <asp:Button runat="server" ID="Button_ViewAlbum" class="btn btn-info btn-lg btn-block" Text="CLICK for more like this!" OnClick="Button_ViewAlbum_Click"></asp:Button>
        </div>
    </form>
    <div class="w3-modal" id="shareModal" onclick="void(0)">
        <div class="w3-modal-content w3-display-middle modal-content" style="background-color:transparent">
            <div class="w3-container">
                <input runat="server" type="text" id="ShareLink" class="w3-input w3-border" onclick="this.setSelectionRange(0, this.value.length)" />
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
    <script src="Javascript/DeviceInfo.js"></script>
    <script>
        (function () {
            var v = document.getElementsByClassName("youtube-player");
            var p = document.getElementById("youtube_video");
            var iframe = document.createElement("iframe");
            //https://developers.google.com/youtube/player_parameters
            var playbackParameters;
            if (isMobile()) {
                playbackParameters = "?autoplay=0&controls=0&enablejsapi=1";
            }
            else {
                playbackParameters = "?autoplay=1&controls=1&enablejsapi=0";
            }
            playbackParameters += "modestbranding=1&mute=0&playsinline=0&fs=1"
            iframe.setAttribute("src", "//www.youtube.com/embed/" + v[0].dataset.id + playbackParameters);
            iframe.setAttribute("frameborder", "0");
            iframe.setAttribute("allowfullscreen", "allowfullscreen")
            iframe.setAttribute("id", "youtube-iframe");
            p.appendChild(iframe);
        })()
    </script>
</body>
</html>



