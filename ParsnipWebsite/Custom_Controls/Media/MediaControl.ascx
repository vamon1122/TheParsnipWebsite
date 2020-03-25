<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.MediaControl" %>
<div class="center_div" style="margin-bottom:20px" >
    <div runat="server" id="MediaContainer" class="background-lightest" style="display:inline-block; padding-top:11px; padding-bottom:5px">
        <h3 runat="server" style="word-wrap:break-word" class="media-control-title"><b runat="server" id="MyTitle"></b></h3>
        
        <asp:Image runat="server" ID="MyImageHolder" Visible="false" Width="100%" CssClass="lazy block no-bottom-margin" />
        <a runat="server" id="a_play_video" visible="false" >
            <div class="play-button-div" style="padding-bottom:5px">
                <img runat="server" id="thumbnail" class="lazy" style="width:100%" />
                <span class="play-button-icon">
                    <img src="../../Resources/Media/Images/Web_Media/play-button-3.svg" class="play-button" />
                </span>
            </div>
        </a>
        <div runat="server" class="youtube-player" id="YoutubePlayer" style="margin-bottom:5px" visible="false"></div>
        <a runat="server" id="MyEdit" >
            <img src="../../Resources/Media/Images/Web_Media/Edit.svg" style="height:25px" /></a>
        <div style="display:inline-block; width:28px"></div>
        <button runat="server" id="ShareButton" type="button" class="btn btn-link" data-toggle="modal" style="padding:0px">
            <img src="../../Resources/Media/Images/Web_Media/Share.svg" style="height:30px" /></button>
    </div>
</div>

<!-- Modal -->
<div runat="server" id="modalDiv"></div>