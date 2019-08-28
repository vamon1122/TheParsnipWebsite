<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Watch_Video.aspx.cs" Inherits="ParsnipWebsite.Watch_Video" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- iPhone FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
    <!-- BOOTSTRAP START -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" 
        integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" 
        integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" 
        crossorigin="anonymous"></script>
    <!-- BOOTSTRAP END -->
    
    <script src="../Javascript/UsefulFunctions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/SharedStyle.css" />
    <script src="../Javascript/ApplyStyle.js"></script>

    <title>Video Player</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:Menu runat="server" ID="Menu" />
    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="ShareUserSuspendedError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access video. The person who shared this video has been suspended!
    </div>

    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="UploadUserSuspendedError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access video. The person who uploaded this video has been suspended!
    </div>
    <div runat="server" id="MediaContainer" class="meme" style="background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:5px">
        <div runat="server" id="VideoContainer">

        </div>
        
        <h3><b runat="server" id="VideoTitle"></b></h3>
        <div style="width:100%">
            <video runat="server" id="video_container" controls="controls" style="width:100%" preload="none" autoplay="autoplay" visible="false">
                <source runat="server" id="VideoSource" type="video/mp4" />
                Your browser does not support HTML5 video.
            </video>
            </div>
        <div runat="server" id="youtube_video_container" style="margin-bottom:6px; width:100%" visible="false">
            <div runat="server" id="youtube_video" class="youtube-player" />
        </div>
        
        <a runat="server" id="MyEdit" >
            <img src="../../Resources/Media/Images/Web_Media/Edit.svg" style="height:25px" /></a>
        <div style="display:inline-block; width:28px"></div>
        <button type="button" class="btn btn-link" data-toggle="modal" data-target="#exampleModalCenter" style="padding:0px">
            <img src="../../Resources/Media/Images/Web_Media/Share.svg" style="height:30px" /></button>
    </div>
    <form id="form1" runat="server">
        <div style="padding-left:2.5%; padding-right:2.5%; padding-top:10px">
            <asp:Button runat="server" ID="Button_ViewAlbum" class="btn btn-info btn-lg" 
                Text="CLICK for more like this!" OnClick="Button_ViewAlbum_Click" />
        </div>
    </form>
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content" style="margin:0px; padding:0px">
      <div runat="server" id="ShareLinkContainer" class="input-group" style="margin:0px; padding:0px">
        <div class="input-group-prepend">
            <span class="input-group-text" id="inputGroup-sizing-default">Link</span>
        </div>
        <input runat="server" type="text" id="ShareLink" class="form-control" 
            onclick="this.setSelectionRange(0, this.value.length)" />
    </div>
          </div>
  </div>
</div>
    <script>
        (function ()
        {
            var v = document.getElementsByClassName("youtube-player");
            var p = document.getElementById("youtube_video");
            var iframe = document.createElement("iframe");
            iframe.setAttribute("src", "//www.youtube.com/embed/" + v[0].dataset.id + "?autoplay=1&autohide=2&border=0&wmode=opaque&enablejsapi=1&controls=0&showinfo=0");
            iframe.setAttribute("frameborder", "0");
            iframe.setAttribute("id", "youtube-iframe");
            p.appendChild(iframe);
        })()
    </script>
</body>
</html>

