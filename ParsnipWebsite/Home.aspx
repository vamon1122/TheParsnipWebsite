<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ParsnipWebsite.Home" %>
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
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" 
        integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" 
        integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" crossorigin="anonymous">
    </script>

    <!-- BOOTSTRAP END -->

    <script src="../Javascript/UsefulFunctions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/SharedStyle.css" />
    <script src="../Javascript/ApplyStyle.js"></script>
    <title>Home</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <label class="censored" id="pageId">home.html</label>
    
    <menuControls:Menu runat="server" ID="Menu" />

    <h2>Home</h2>
    <hr class="break" />
    <div class="padded-text center_div">
        <asp:Label runat="server" ID="WelcomeLabel"></asp:Label>
        <hr class="break" />
        <h3>*NEW*</h3>
        - [17/08/19] Fixed slow loading times on the <a href="Videos.aspx">videos page</a><br />
        - [07/08/19] New <a href="Krakow.aspx">Krakow page!</a> Upload, view & share holiday photos! 
        <div id="flightDetailsContainer" style="display:inline">
            View flight details! <label id="countdownToKrakow" style="margin:0; display:inline"></label><label id="countdownInfo" style="margin:0; display:inline"></label>
        </div>
        - [19/07/19] View <a href="https://www.playfootball.net/venues/bury/players-lounge/2886/10389/152">tuesday 7s results</a> from the parsnip menu<br />
        - [24/06/19] Share <a href="Photos.aspx">photos</a> and <a href="Videos.aspx">videos</a>! (Recipient doesn't even need an account to view what you share!!!)<br />
        - [24/06/19] You can now access the home page without logging in
    </div>

    <hr class="break" />

    <div runat="server" id="LatestVideo"></div>

    <hr class="break" />
    <script src="../Javascript/CountDownToKrakow.js"></script>
</body>
</html>
