<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Image.aspx.cs" Inherits="ParsnipWebsite.View_Image" %>
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
    <script src="Javascript/IntersectionObserver.js"></script>

    <title>View Image</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:Menu runat="server" ID="Menu" />

    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="ShareUserSuspendedError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access image. The image has been deleted or the person who shared this image has been suspended!
    </div>

    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="UploadUserSuspendedError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access image. The image has been deleted or the person who uploaded the image has been suspended!
    </div>

    <div class="center_form">
        <div runat="server" id="MediaContainer" class="meme background-lightest" style="display:inline-block; padding-top:8px; padding-bottom:5px">
            <h3><b runat="server" id="ImageTitle"></b></h3>
            <img runat="server" id="ImagePreview" style="width:100%" />
        </div>
        <form id="form1" runat="server" style="width:100%; margin-bottom:5%">
        <br />
        <br />
        <div style="padding-left:2.5%; padding-right:2.5%">
            <asp:Button runat="server" ID="Button_ViewAlbum" class="btn btn-info btn-lg btn-block" Text="CLICK for more like this!" OnClick="Button_ViewAlbum_Click"></asp:Button>
        </div>
        </form>
    </div>
</body>
</html>


