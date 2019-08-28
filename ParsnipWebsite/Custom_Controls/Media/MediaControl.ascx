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
            <img src="../../Resources/Media/Images/Web_Media/Edit.svg" style="height:25px" /></a>
        <div style="display:inline-block; width:28px"></div>
        <button type="button" class="btn btn-link" data-toggle="modal" data-target="#exampleModalCenter">
            <img src="../../Resources/Media/Images/Web_Media/Share.svg" style="height:30px" /></button>
    </div>
</div>

<!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Share Link</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div runat="server" id="ShareLinkContainer" class="input-group mb-3" style="padding-left:5%; padding-right:5%">
        <div class="input-group-prepend">
            <span class="input-group-text" id="inputGroup-sizing-default">Link</span>
        </div>
        <input runat="server" type="text" id="ShareLink" class="form-control" 
            onclick="this.setSelectionRange(0, this.value.length)" />
    </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>