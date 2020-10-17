<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_Users.aspx.cs" Inherits="ParsnipWebsite.Manage_Users" %>
<%@ Register Src="~/Custom_Controls/Admin/AdminMenu.ascx" TagPrefix="adminControls" TagName="adminMenu" %>
<%@ Register Src="~/Custom_Controls/Uac/AdminUserForm.ascx" TagPrefix="admin" TagName="AdminUserForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Users</title>
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
            <!-- Alerts -->
            <div class="alert alert-danger alert-dismissible" runat="server" style="display:none" id="Error">
                 <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                 <asp:Label runat="server" ID="ErrorText"></asp:Label>
            </div>
            <div class="alert alert-warning alert-dismissible" runat="server" style="display:none" id="Warning">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <asp:Label runat="server" ID="WarningText"></asp:Label>
            </div>
            <div class="alert alert-success alert-dismissible" runat="server" style="display:none" id="Success">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <asp:Label runat="server" ID="SuccessText"></asp:Label>
            </div>

            <div class="jumbotron">
                <h1 class="display-4">Users</h1>
                <p class="lead">Create & edit theparsnip.co.uk users</p>
                <hr class="my-4" />
                <p><adminControls:adminMenu runat="server" id="adminMenu1" /></p>
            </div>   
           
            <label>Select a user:</label>
            <asp:DropDownList ID="selectUser" runat="server" AutoPostBack="True" CssClass="form-control" 
                onselectedindexchanged="SelectUser_Changed">
            </asp:DropDownList>
            <admin:AdminUserForm runat="server" ID="UserForm" />
            <asp:Button runat="server" ID="Button_Action" OnClick="Button_Action_Click" CssClass="btn btn-primary" Text="Action"></asp:Button>
            <asp:Button runat="server" ID="Button_Delete"  CssClass="btn btn-primary" Text="Delete" Visible="false" data-toggle="modal" data-target="#confirmDelete" OnClientClick="return false;"></asp:Button>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="confirmDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Confirm Delete</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Are you sure that you want to DELETE this user?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <asp:Button runat="server" class="btn btn-primary" ID="Button_DeleteConfirm" OnClick="Button_DeleteConfirm_Click" Text="Confirm"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>


