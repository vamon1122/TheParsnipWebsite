﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_Media.aspx.cs" Inherits="ParsnipWebsite.Manage_Media" %>
<%@ Register Src="~/Custom_Controls/Admin/AdminMenu.ascx" TagPrefix="adminControls" TagName="adminMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Media</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Libraries/bootstrap-4.1.3-dist/css/bootstrap.min.css" />
    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Libraries/bootstrap-4.1.3-dist/js/bootstrap.min.js"></script>
    <!-- iPhone FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
    <style>
        .play-button-div 
        {
            position: relative;
            display: block;
        }
        
        .play-button-icon 
        {
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
        }
        
        .play-button
        {
            width: 60px;
            height: 60px
        }
        
        .background-lightest 
        {
            background-color: #f2f2f2;
        }
        ﻿
        .media-control-title 
        {
            width: 100vmin;
            max-width: 480px;
            padding-left: 10px;
            padding-right: 10px
        }
    </style>
</head>
<body style="padding-bottom:2.5%; padding-top:4%">
    <form id="form1" runat="server">
        <div class="container">
                <div class="jumbotron">
                <h1 class="display-4">Media</h1>
                <p class="lead">Manage images and videos which have been uploaded to the site</p>
                <hr class="my-4" />
                <p><adminControls:adminMenu runat="server" id="adminMenu1" /></p>
            </div>  
            
            
            <label>Select whose media to manage:</label>
            <asp:DropDownList ID="SelectUser" runat="server" AutoPostBack="True" CssClass="form-control" 
                onselectedindexchanged="SelectUser_Changed">
            </asp:DropDownList>
            <br />
            </div>
        <div style="text-align:center">
            <!--<asp:Button runat="server" ID="btnDelete"  CssClass="btn btn-primary" Text="Delete" Visible="false" data-toggle="modal" data-target="#confirmDelete" OnClientClick="return false;"></asp:Button>-->
                    <button data-toggle="modal" data-target="#confirmDelete" class="btn btn-danger" onclick="return false" >Remove ALL media from albums</button>
            <br />
            <br />
                
        
        <div runat="server" id="DisplayPhotosDiv">

                </div>
            </div>
        <!-- Modal -->
        <div class="modal fade" id="confirmDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Confirm Removal</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Are you sure that you want to remove all of this user's media from all albums? 
                        (WARNING: THIS IS PERMANENT AND IRREVERSIBLE)
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="BtnDeleteUploads" runat="server" class="btn btn-danger" OnClick="BtnDeleteUploads_Click" Text="REMOVE ALL"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../Javascript/FocusImage.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
</body>
</html>


