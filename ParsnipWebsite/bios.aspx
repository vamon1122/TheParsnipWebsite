﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bios.aspx.cs" Inherits="ParsnipWebsite.Bios" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<!DOCTYPE html>

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

    <title>Bios</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:Menu runat="server" ID="Menu" />

    
    
    <div class="padded-text">
    <h2>Bios!!!</h2>
    <h3>All credit goes to Kieron 'Gaz Beadle' Howarth</h3>
    </div>
    <img class="censored" id="kieron" src="http://res.cloudinary.com/lqrrvz3pc/image/upload/v1477059052/Photos/Bios/Fat Kieron.JPG" style="width:300px" />
    <hr class="break" />
    <br />

    <div class="padded-text">
        Kieron - Angel <br />
        Ben - Angel (I assume you didn't aim this at me Kieron, I appreciate it :P)<br />
        Loldred - Cunt<br />
        Marshy - Cunt<br />
        Aaron - Cunt<br />
        Raul - Cunt<br />
        Tom - Cunt<br />
        Dan - Cunt<br />
        Mason - Cunt<br />
        <br />
        Source below \/<br />
    </div>
    <br />
    <img src="Resources/Media/Images/Local/Bios/Kieron_Chat.PNG" id="Kieron_chat" class="image-preview" />
    <br />
    <br />
</body>
</html>
