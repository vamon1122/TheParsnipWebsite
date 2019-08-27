<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.MediaControl" %>
<div class="center_div" style="margin-bottom:20px" >
    <div runat="server" id="MediaContainer" class="meme" style="background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:5px">

        <h3 runat="server"><b runat="server" id="MyTitle"></b></h3>
        <asp:Image runat="server" ID="MyImageHolder" Width="100%" Visible="false" CssClass="lazy no-bottom-margin" />
        <a runat="server" id="a_play_video" visible="false" >
            <div class="play-button-div" style="padding-bottom:5px">
                <img runat="server" id="thumbnail" class="lazy" style="width:100%" />
                <span class="play-button-icon">
                    <img src="Resources\Media\Images\Web_Media\play_button_2.png" />
                </span>
            </div>
        </a>
        <div runat="server" class="youtube-player" id="YoutubePlayer" style="margin-bottom:5px" visible="false"></div>
        <a runat="server" id="MyEdit" >
            <img src="../../Resources/Media/Images/Web_Media/Edit (2).svg" style="height:25px" /></a>
        <div style="display:inline-block; width:28px"></div>
        <a runat="server" id="MyShare" >
            <img src="../../Resources/Media/Images/Web_Media/Share.svg" style="height:30px" /></a>
    </div>
</div>