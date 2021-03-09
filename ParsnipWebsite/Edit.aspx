<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ParsnipWebsite.Edit_Media" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Memes</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Libraries/w3.css-4.13/w3.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Lato/Lato.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Montserrat/Montserrat.css" />
    <link rel="stylesheet" href="Libraries/fontawesome-free-5.15.1-web/css/all.css" />
    <link rel="stylesheet" type="text/css" href="Css/MediaStyle.css" />
    <!-- FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
</head>
<body>
    <menuControls:NewMenu runat="server" ID="NewMenu" />
    <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
        <h1 class="w3-margin w3-jumbo" >Edit</h1>
    </header>
    <div class="w3-center" style="padding-bottom:5%; text-align: center" >
        <form id="form1" runat="server" defaultbutton="ButtonSave" >
            <div class="w3-container">
                <label class="form-label">Title:</label>
                <asp:TextBox CssClass="w3-input w3-border w3-margin-bottom" runat="server" ID="InputTitleTwo" />
                <label class="form-label">Add Hashtags:</label>
                    <div style="display:flex">
                    <asp:DropDownList ID="NewAlbumsDropDown" runat="server" AutoPostBack="False" CssClass="w3-select" style="flex-grow: 1; overflow:hidden"></asp:DropDownList>
                    <asp:Button runat="server" ID="AddMediaTagPair" OnClick="AddMediaTagPair_Click" Text="Add Tag" CssClass="w3-btn w3-black dropdown-button" />
                        </div>
                <br />
                <div runat="server" id="MediaTagContainer" class="w3-margin-bottom"></div>
                <!-- User select -->
                <label class="form-label">Tag People:</label>
                <div style="display:flex">
                    <asp:DropDownList ID="DropDown_SelectUser" runat="server" AutoPostBack="False" CssClass="w3-select" style="flex-grow: 1; overflow:hidden" ></asp:DropDownList>
                    <asp:Button runat="server" ID="AddMediaUserPair" Text="Tag User" CssClass="w3-btn w3-black w3-right dropdown-button" OnClick="AddMediaUserPair_Click" />
                </div>
                    
                <br />
                <div runat="server" id="UserTagContainer" class="w3-margin-bottom"></div>
                <label class="form-label">Date Captured:</label>
                <input runat="server" class="w3-input w3-border w3-margin-bottom" id="input_date_media_captured" name="date" placeholder="DD/MM/YYYY" type="text" />
            </div>
            <a runat="server" ID="ImagePreviewContainer" visible="false">
            <asp:Image runat="server" ID="ImagePreview" CssClass="image-preview" Width="100%" />
                </a>
            <a runat="server" id="a_play_video" visible="false" >
                <div class="play-button-div">
                    <img runat="server" id="thumbnail" style="width:100%" />
                    <span class="play-button-icon">
                        <img src="Resources\Media\Images\Web_Media\play_button_2.png" />
                    </span>
                </div>
            </a>
            <br />
            <div runat="server" id="ThumbnailsAreProcessing" Visible="false" class="w3-container">
                Thumbnails are being generated for your video. Check back in a few minutes to pick your favourite!
            </div>
            <div runat="server" id="ThumbnailSelectorContainer" Visible="false" class="w3-container">
                <label class="form-label">Select a thumbnail:</label>
                <div id="ThumbnailSelector" runat="server">

                </div>
            </div>
            <div runat="server" id="ThumbnailUploadControl" Visible="false">
                <br />
                <label runat="server" class="file-upload file-upload-btn w3-black">            
                    <span><strong>Upload New Thumbnail</strong></span>
                    <asp:FileUpload ID="ThumbnailUpload" runat="server" class="form-control-file" onchange="this.form.submit()" />
                </label>
                <br />
            </div>
            <div class="w3-container">
                <label class="form-label">Search Terms:</label>
                <asp:TextBox CssClass="w3-input w3-border w3-margin-bottom" runat="server" ID="SearchTerms_Input" />
                <!--Submit behaviour not working on final VISIBLE TextBox. This is a blank TextBox which does not require
                    submit behaviour-->
                <input type="text" runat="server" style="height:0px; width:0px; padding:0px; border:0px" />
            </div>
            <asp:Button runat="server" ID="btn_AdminDelete"  CssClass="w3-btn w3-black w3-margin-top" Width="100px" Text="Delete" Visible="false" OnClientClick="document.getElementById('confirmMediaDelete').style.display='block'; return false"></asp:Button>
            <asp:Button runat="server" ID="ButtonSave" class="w3-btn w3-black w3-margin-top" Text="Save" Width="100px" OnClick="ButtonSave_Click"></asp:Button>
            <div class="w3-modal" id="confirmMediaDelete" onclick="void(0)">
                <div class="w3-modal-content w3-display-middle modal-content">
                    <header class="w3-container w3-red">
                        <h3>Confirm Delete</h3>
                    </header>
                    <div class="w3-container">

                        <p>Are you sure that you want to DELETE this media?</p>
                        <div class="w3-margin-bottom">
                            <button id="BtnDeleteImage" class="w3-btn w3-red" onclick="DeletePhoto(); return false;" >Confirm</button>
                            <button type="button" class="w3-btn w3-black" onclick="document.getElementById('confirmMediaDelete').style.display='none'">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <div class="w3-modal" id="shareModal" tabindex="-1" role="dialog" aria-labelledby="shareMediaLink" aria-hidden="true" onclick="void(0)">
        <div class="w3-modal-content w3-display-middle modal-content" role="document" style="background-color: transparent">
			<input runat="server" type="text" id="ShareLink" class="w3-input w3-border" onclick="this.setSelectionRange(0, this.value.length)" />
        </div>
    </div>
    <div runat="server" id="DynamicMediaDiv" style="margin: auto; text-align:center"></div>
    
    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
    <script>
        //Uses url parameter "id" to delete the image whose Id is 
        //sepcified in the url parameter "id". Fired by delete modal.
        function DeletePhoto() {
            var url_string = window.location.href
            var url;
            var redirect = "edit?"

            try {
                //More efficient but does not work on older browsers
                url = new URL(url_string);
                var tagId = url.searchParams.get("tag");
                redirect += "id=" + url.searchParams.get("id") + "&delete=true";
                if (tagId != null && tagId != "") {
                    redirect += "&tag=" + tagId
                }
            }
            catch (e) {
                //More compatible method
                url = window.location.href;
                redirect += "id=" + url.split('=')[url.split('=').length - 1] + "&delete=true";
            }

            //Use window.location.replace if possible because 
            //you don't want the current url to appear in the 
            //history, or to show - up when using the back button.
            try { window.location.replace(redirect); }
            catch (e) { window.location = redirect; }
        }
    </script>
</body>
</html>

