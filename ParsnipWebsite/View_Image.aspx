﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Image.aspx.cs" Inherits="ParsnipWebsite.View_Image" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <style>
        .width100{
            width:100%;
        }
    </style>

    <title>View Image</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:Menu runat="server" ID="Menu" />

    <div runat="server" class="alert alert-danger alert-dismissible parsnip-alert" Visible="false" id="NotExistError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error:</strong> Could not access image. The image which you are trying to access has been deleted or the link which you are using has expired!
    </div>

    <div class="center_form">
        <div runat="server" id="ShareLinkContainer" class="input-group mb-3" style="padding-left:5%; padding-right:5%">
  <div class="input-group-prepend">
    <span class="input-group-text" id="inputGroup-sizing-default">Link</span>
  </div>
  <input runat="server" type="text" id="ShareLink" class="form-control" onclick="this.setSelectionRange(0, this.value.length)" />
</div>
    <h2 runat="server" id="ImageTitle"></h2>

    <form id="form1" runat="server" style="width:100%; margin-bottom:5%">
        <asp:Image runat="server" ID="ImagePreview" CssClass="width100" />
        <br />
        <br />
        <div style="padding-left:2.5%; padding-right:2.5%">
        <asp:Button runat="server" ID="Button_ViewAlbum" class="btn btn-info btn-lg btn-block" Text="CLICK for more like this!" OnClick="Button_ViewAlbum_Click"></asp:Button>
            </div>
    </form>

        
    
        </div>
        <script>
            /*
            var url_string = window.location.href
            url = new URL(url_string);
            document.getElementById("ImageTitle").innerHTML = url.searchParams.get("title");

            document.getElementById("ShareLink").value = "https://www.theparsnip.co.uk/photos?imageid=" + url.searchParams.get("imageid");
            */
    </script>

    
    
</body>
</html>


