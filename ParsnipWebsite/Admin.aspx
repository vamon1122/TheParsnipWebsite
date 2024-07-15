<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ParsnipWebsite.Admin" %>
<%@ Register Src="~/Custom_Controls/Admin/AdminMenu.ascx" TagPrefix="adminControls" TagName="adminMenu" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Admin</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Libraries/bootstrap-4.1.3-dist/css/bootstrap.min.css" />
    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <script src="Libraries/bootstrap-4.1.3-dist/js/bootstrap.min.js"></script>
    <!-- iPhone FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
</head>

<body style="padding-bottom:2.5%; padding-top:4%">
    <form runat="server">
        <div class="container">
            <div class="jumbotron">
                <h1 class="display-4">Admin</h1>
                <p class="lead">Manage theparsnip.co.uk</p>
                <hr class="my-4" />
                <p><adminControls:adminMenu runat="server" id="adminMenu" /></p>
            </div>
            <div class="input-group">
                <asp:TextBox runat="server"  CssClass="form-control" ID="TextBox_MOTD"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button runat="server" ID="Button_SaveMOTD"  CssClass="btn btn-primary" Text="Save" OnClick="Button_UploadDataId_Click" />
                </span>
            </div>
            <div class="input-group">
                <asp:TextBox runat="server"  CssClass="form-control" ID="TextBox_IgnoreSearchTerms"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button runat="server" ID="Button_SaveIgnoreSearchTerms"  CssClass="btn btn-primary" Text="Save" OnClick="Button_IgnoreSearchTerms_Click" />
                </span>
            </div>
            <div class="form-check">
                <asp:CheckBox runat="server" ID="CheckBox_EnableYoutubeUploads" OnCheckedChanged="CheckBox_EnableYoutubeUploads_CheckedChanged" AutoPostBack="true" CssClass="form-check-input" type="checkbox" />
                <label class="form-check-label" style="padding-top:4px" for="flexCheckDefault">Enable uploads from Youtube</label>
            </div>
            <br />
            <asp:Label runat="server" ID="Label_ParsnipWebsiteVersion"></asp:Label>
            <br />
            <asp:Label runat="server" ID="Label_ParsnipDataVersion"></asp:Label>
        </div>
    </form>
</body>
</html>
