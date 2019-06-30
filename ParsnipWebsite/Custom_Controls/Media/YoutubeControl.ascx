<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YoutubeControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.YoutubeControl" %>
<div class="center_div">
    <div runat="server" id="VideoContainer" class="meme" style="background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:8px">

        <h3 runat="server" id="MyTitle"></h3>
        <div runat="server" class="youtube-player" id="YoutubePlayer" style="margin-bottom:8px"></div>
        <!--
    <a runat="server" id="MyEdit">Edit</a>
        -->
    <a runat="server" id="MyShare">Share</a>
    </div>
</div>
<hr class="break" />
