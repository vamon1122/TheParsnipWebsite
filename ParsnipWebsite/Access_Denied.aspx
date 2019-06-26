﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Access_Denied.aspx.cs" Inherits="ParsnipWebsite.Access_Denied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" crossorigin="anonymous"></script>

    <script src="../Javascript/Useful_Functions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/Shared_Style.css" />
    <script src="../Javascript/Apply_Style.js"></script>
    
    <title>Access Denied</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="center_div">
                
                <h1 class="h1" style="margin-left: auto; margin-right: auto; text-align: center;">Access Denied</h1>
                <div style="margin-left: auto; margin-right: auto; text-align: center;">
                    <asp:Label runat="server" ID="Info"></asp:Label>
                </div>
                <br />
                <img src="Resources/Media/Images/Web_Media/you_shall_not_pass.jpg" class="rounded mx-auto d-block" style="max-width:100%" />
            </div>
        </div>
    </form>
</body>
</html>

