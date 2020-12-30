<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.MediaControl" %>
<div class="center_div" style="margin-bottom:20px" >
    <div runat="server" id="MediaContainer" class="background-lightest" style="display:inline-block; padding-bottom:5px">
        <div class="background-lightest media-control-title" style="padding-top:11px; min-height:38px">
            <h4 runat="server" id="MyTitle" style="word-wrap:break-word; display:inline; position:relative; top:-5px"></h4><i runat="server" id="unprocessed" class="fas fa-circle w3-right fa-2x" style="position:relative; right: -5px; top: -5px" visible="false"></i><i runat="server" id="processing" class="fas fa-circle-notch w3-spin w3-right fa-2x" style="position:relative; right: -5px; top: -5px" visible="false"></i><i runat="server" id="error" class="far fa-times-circle w3-red w3-right fa-2x" style="position:relative; right: -5px; top: -5px" visible="false"></i>
        </div>

        <a runat="server" id="MyAnchorLink">
            <asp:Image runat="server" ID="MyImageHolder" Visible="false" Width="100%" CssClass="lazy block no-bottom-margin" />
        </a>
        <a runat="server" id="a_play_video" visible="false" >
            <div class="play-button-div" style="padding-bottom:5px">
                <img runat="server" id="thumbnail" class="lazy" style="width:100%" />
                <span class="play-button-icon">
                    <img src="../../Resources/Media/Images/Web_Media/play-button-3.svg" class="play-button" />
                </span>
            </div>
        </a>
        <div style="padding:5px">
            <a runat="server" id="MyEdit" class="fas fa-pen fa-lg" style="text-decoration:none;" >
            </a>
            <div style="display:inline-block; width:28px"></div>
            <button runat="server" id="ShareButton" type="button" style="padding:0px; margin:0px; border:none; outline: none; background:none">
                <i class="fa fa-share-alt fa-lg"></i>
            </button>
            </div>
    </div>
</div>

<!-- Modal -->
<div runat="server" id="modalDiv"></div>
