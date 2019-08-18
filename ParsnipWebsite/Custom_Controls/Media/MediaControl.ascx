<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.MediaControl" %>
<div class="center_div">
    <div runat="server" id="MediaContainer" class="meme" style="background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:8px">

        <h3 runat="server" id="MyTitle"></h3>
        <asp:Image runat="server" ID="MyImageHolder" Width="100%" Visible="false" CssClass="lazy no-bottom-margin" />
        <a runat="server" id="a_play_video" visible="false" >
            <div class="play-button-div">
                <img runat="server" id="thumbnail" class="lazy" style="width:100%" />
                <span class="play-button-icon">
                    <img src="Resources\Media\Images\Web_Media\play_button_2.png" />
                </span>
            </div>
        </a>
        <div runat="server" class="youtube-player" id="YoutubePlayer" style="margin-bottom:8px" visible="false"></div>
    <a runat="server" id="MyEdit">Edit</a>
        
    <a runat="server" id="MyShare">Share</a>
    </div>
</div>
<hr class="break" />