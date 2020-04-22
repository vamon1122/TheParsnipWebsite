<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadMediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.UploadMediaControl" %>
<div runat="server" id="UploadDiv" class="form-group" style="display:none">
    <label class="file-upload file-upload-menu-container" data-toggle="modal" data-target="#uploadMedia">                
        <img src="../../Resources/Media/Images/Web_Media/upload-cloud.svg" class="file-upload-menu-button" />
    </label>
    <label class="file-upload file-upload-btn btn" data-toggle="modal" data-target="#uploadMedia">            
        <span><strong>Upload Media</strong></span>
    </label>
    <div class="modal fade" id="uploadMedia" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content" style="margin:0px; padding:0px; background-color:transparent; border-color: transparent">
                <div class="center_div">
                    <label runat="server" class="file-upload file-upload-btn btn">            
                        <span><strong>Upload From Device</strong></span>
                        <asp:FileUpload ID="MediaUpload" runat="server" class="form-control-file" onchange="this.form.submit()" />
                    </label>
                    <br />
                    <label class="file-upload file-upload-btn btn" data-toggle="modal" data-dismiss="modal" data-target="#uploadYoutube">            
                        <span><strong>Upload From Youtube</strong></span>
                    </label>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="uploadYoutube" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content" style="margin:0px; padding:0px;">
                <div class="input-group" style="margin:0px; padding:0px">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="inputGroup-sizing-default">Link</span>
                    </div>
                    <asp:TextBox runat="server"  CssClass="form-control" ID="TextBox_UploadDataId"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button runat="server" ID="Button_UploadDataId"  CssClass="btn btn-primary" Text="Upload" OnClick="Button_UploadDataId_Click" />
                    </span>
                </div>
            </div>
        </div>
    </div>
</div>
