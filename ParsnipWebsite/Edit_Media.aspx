<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Media.aspx.cs" Inherits="ParsnipWebsite.Edit_Media" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- iPhone FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
    <!-- BOOTSTRAP START -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" crossorigin="anonymous"></script>
    <!-- BOOTSTRAP END -->

    <script src="../Javascript/UsefulFunctions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/SharedStyle.css" />
    <script src="../Javascript/ApplyStyle.js"></script>

    <script src="Javascript/IntersectionObserver.js"></script>

    <title>Edit Image</title>
</head>
<body class="fade0p5" id="body" style="text-align:center;"  >
    <menuControls:Menu runat="server" ID="Menu" />

    <div class="center_form" style="padding-bottom:5%" >
        <form id="form1" runat="server" >
            <!-- Title -->
            <div class="form-group" style="padding-left:5%; padding-right: 5%;" >
                <label style="text-align:left; width:100%">Title</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="InputTitleTwo" />
            </div>

            <!-- Album select -->
            <div runat="server" id="DropDownDiv" style="padding-left:5%; padding-right: 5%;">
                <label>Select an album:</label>
                <asp:DropDownList ID="NewAlbumsDropDown" runat="server" AutoPostBack="False" CssClass="form-control" >
                </asp:DropDownList>
                <br />
            </div>

            <!-- Image preview -->
            <asp:Image runat="server" ID="ImagePreview" CssClass="image-preview" Width="100%" visible="false" />
            <div runat="server" id="youtube_video_container" class="large-youtube-container" style="height:30%" visible="false">
                <div runat="server" id="youtube_video" class="youtube-player" />
            </div>
            
            <br />
            <br />

            <!-- Delete / save buttons -->
            <div style="width:100%; padding-left:5%; padding-right:5%;">
                <asp:Button runat="server" ID="btn_AdminDelete"  CssClass="btn btn-primary float-left" Width="100px" Text="Delete" Visible="false" data-toggle="modal" data-target="#confirmDelete" OnClientClick="return false;" UseSubmitBehavior="false"></asp:Button>
                <asp:Button runat="server" ID="ButtonSave" class="btn btn-primary float-right" Text="Save" Width="100px" OnClick="ButtonSave_Click"></asp:Button>
            </div>

            <!-- Delete modal -->
            <div class="modal fade" id="confirmDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true" style="text-align:left">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">Confirm Delete</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Are you sure that you want to DELETE this photo?
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                            <button id="BtnDeleteImage" class="btn btn-primary" onclick="DeletePhoto(); return false;" >Confirm</button>

                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
      
    <script src="../Javascript/Youtube.js"></script>
    <script>
        //Uses url parameter "id" to delete the image whose Id is 
        //sepcified in the url parameter "id". Fired by delete modal.
        function DeletePhoto()
        {
            var url_string = window.location.href
            var url;
            var redirect = "edit_image?"

            try
            {
                //More efficient but does not work on older browsers
                url = new URL(url_string);
                redirect += "id=" + url.searchParams.get("id") + "&delete=true";
            }
            catch (e)
            {
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


