﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Videos.aspx.cs" Inherits="ParsnipWebsite.Videos" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- GOOGLE FONTS: Nunito -->
    <link href="https://fonts.googleapis.com/css?family=Nunito&display=swap" rel="stylesheet">
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

    <title>Videos</title>

</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:Menu runat="server" ID="Menu" />

    <div class="alert alert-warning alert-dismissible parsnip-alert" style="display: none;" id="AccessWarning">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Access Denied</strong> You cannot edit media which other people have uploaded!
    </div>
    <div class="alert alert-danger alert-dismissible parsnip-alert" style="display: none;" id="VideoError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Upload Error</strong> You cannot upload videos yet!
    </div>

    <div class="cens_req padded-text"><label>Certain elements of this page were removed by request. <a href="content_removal">Click here</a> to learn more.</label></div>

    <h2><b>Videos</b></h2>
    
    <form runat="server">
        <h5><b>📽️ *NEW* Upload from youtube! 📽️</b></h5>
        <span>You can now share links directly from youtube!</span>
        <br />
        <asp:Button runat="server" ID="Upload_Youtube" CssClass="file-upload file-upload-btn btn" Text="Upload Youtube" Visible="true" data-toggle="modal" data-target="#youtubeModal" OnClientClick="return false;"></asp:Button>
        <br />
        <br />
        
        <div runat="server" id="links_div">
            
        </div>

        <div class="modal fade" id="youtubeModal" tabindex="-1" role="dialog" aria-labelledby="shareMediaLink" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content" style="margin:0px; padding:0px">
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
    </form>

    <!--SCRIPTS-->
    <script src="../Javascript/FocusImage.js"></script>
    <script src="../Javascript/Youtube.js"></script>
    <script src="Javascript/LazyImages.js"></script>
    <script>
        var url_string = window.location.href
        var url = new URL(url_string);
        var error = url.searchParams.get("error");
        if (error !== "" && error !== null)
        {
            if (error === "video")
            {
                document.getElementById("VideoError").style = "display:block";
            }
            else
            {
                document.getElementById("AccessWarning").style = "display:block";
            }
        }
    </script>
</body>
</html>

