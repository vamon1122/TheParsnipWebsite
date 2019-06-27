<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.VideoControl" %>

<div class="center_div">
    <div runat="server" id="VideoContainer" class="meme" style="background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:8px">

        <h3 runat="server" id="MyTitle"></h3>
        <a runat="server" id="a_play_video" >
            <div class="play-button-div">
                <img runat="server" id="thumbnail" style="width:100%" />
                <span class="play-button-icon">
                    <img src="Resources\Media\Images\Web_Media\play_button_2.png" />
                </span>
            </div>
        </a>
        <!--
    <a runat="server" id="MyEdit">Edit</a>
        -->
    <a runat="server" id="MyShare">Share</a>
    </div>
</div>
<hr class="break" />
