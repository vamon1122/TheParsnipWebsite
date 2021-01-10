<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadMediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.UploadMediaControl" %>
<div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="YoutubeError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Upload Error</strong> The youtube link which you tried to share didn't look quite right... give the url a check and try again!
</div>
<div runat="server" id="UploadDiv" class="form-group" style="display:none">
    <label class="w3-button w3-black w3-padding-large w3-large w3-margin-top" onclick="document.getElementById('uploadMedia').style.display='block'">            
        <span><strong>Upload Something</strong></span>
    </label>
    
  </div>
<div id="uploadMedia" class="w3-modal" onclick="void(0)">
    <div class="w3-modal-content w3-display-middle" style="background-color: transparent">
      <div class="w3-container">
                    <label runat="server" class="file-upload file-upload-btn">            
                        <span><strong>Upload From Device</strong></span>
                        <asp:FileUpload ID="MediaUpload" runat="server" class="form-control-file" onchange="this.form.submit()" />
                    </label>
                    <br />
                    <label class="file-upload file-upload-btn" onclick="document.getElementById('uploadMedia').style.display='none'; document.getElementById('uploadYoutube').style.display='block';">            
                        <span><strong>Upload From Youtube</strong></span>
                    </label>
            <br />
                        <a href="myuploads" class="file-upload file-upload-btn"><span><strong style="color: white">Manage My Uploads</strong></span></a>
          </div>
      </div>
    </div>
<div id="uploadYoutube" class="w3-modal" onclick="void(0)">
    <div class="w3-modal-content w3-display-middle" style="background-color: transparent;  min-width: 500px">
      <div class="w3-container" >
          <div class="w3-bar">
    <asp:TextBox runat="server" AutoPostback="False" ID="TextBox_UploadDataId" class="w3-input w3-border w3-bar-item" type="text" placeholder="youtu.be/watch?v=XXXXXXXXXXX" />
                        <asp:Button runat="server" ID="Button_UploadDataId"  CssClass="w3-btn w3-black w3-bar-item upload-youtube-button w3-right" Text="Upload" OnClick="Button_UploadDataId_Click" />
              </div>

      </div>
    </div>
  </div>
