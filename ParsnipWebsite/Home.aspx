<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ParsnipWebsite.Home" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="menuControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css?family=Nunito|Pacifico&display=swap" rel="stylesheet">
    <!-- iPhone FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
    <!-- BOOTSTRAP START -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" 
        integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" 
        integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" crossorigin="anonymous">
    </script>
    <!-- BOOTSTRAP END -->
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/SharedStyle.css" />
    <script src="../Javascript/ApplyStyle.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <title>Home</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <errorHandler:ErrorHandler runat="server" ID="ErrorHandler" />
    <menuControls:Menu runat="server" ID="Menu" />
    <div runat="server" ID="MOTD_div" class="media-control-text" style="margin: auto; font-family: 'Pacifico', cursive; color:red; font-size:40px; padding-bottom:10px"></div>
    <asp:Label runat="server" ID="WelcomeLabel"></asp:Label>
    <br />
    <br />
    <h2><b>Looking for something?</b></h2>
    <label>Check out the categories below to get started. Notice anything missing? Click the cloud in the top right corner and get uploading!</label>
    <label runat="server" id="LoginNudge" style="color:red" Visible ="false">You must <a href="login">login</a> before you can view or upload content.</label>
    <div runat="server" id="MediaTagContainer"></div>
    <form runat="server">
        <br />
        <menuControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        <div runat="server" id="seeYourself" visible="false"></div>
        <div runat="server" id="LatestVideo" style="margin-top:10px"></div>
        <div class="">
            <div class="media-control-title" style="margin: auto">
                <h2 class="section-title"><b>YOUR WEBSITE NEEDS YOU!</b></h2>
                If you notice anything missing, upload it! I don't have Instagram so the crystal parsnip would be an awesome place to start!
                <h2 class="section-title"><b>*NEW*</b></h2>
                <ol style="list-style-type: square; text-align: left; padding-left:18px">
                    <li style="text-align: left">[22/05/20] <a href="Portugal.aspx">Portugal album</a> finally complete!</li>
                    <li style="text-align: left">[10/05/20] You can now tag people in media!</li>
                    <li style="text-align: left">[08/04/20] NEW videos uploaded to <a href="Amsterdam.aspx">the Amsterdam album</a>!</li>
                    <li style="text-align: left">[27/04/20] You can now add tags to media!</li>
                    <li style="text-align: left">[08/03/20] You can now <a href="Videos.aspx">upload videos directly from Youtube</a></li>
                    <li style="text-align: left">[28/10/19] Fixed bug where page would 'jump around' whilst content was loading</li>
                    <li style="text-align: left">[20/09/19] Fixed bug where video player would take ages to load</li>
                    <li style="text-align: left">[15/09/19] Added time machine! View the website as it was in <a href="http://original.theparsnip.co.uk">2015</a> when it was first conceived and in <a href="http://2016.theparsnip.co.uk">2016</a> when it had it's first major visual overhaul!</li>
                    <li style="text-align: left">[17/08/19] Fixed broken image links on <a href="Photos.aspx">the photos album</a></li>
                    <li style="text-align: left">[17/08/19] Fixed slow loading times on <a href="Videos.aspx">the videos album</a></li>
                    <li style="text-align: left">[07/08/19] New <a href="Krakow.aspx">Krakow album</a>! Upload, view & share holiday photos!</li>
                    <li style="text-align: left">[24/06/19] Share <a href="Photos.aspx">photos</a> and <a href="Videos.aspx">videos</a>! (Recipient doesn't even need an account to view what you share!!!)</li>
                    <li style="text-align: left">[24/06/19] You can now access the home page without logging in</li>
                </ol>
            </div>
        </div>
        <h2><b>My Uploads</b></h2>
        <label runat="server" id="UploadsPlaceholder">When you upload content, it will appear here!</label>
        <div runat="server" id="MyMediaContainer"></div>
    </form>
    <script src="Javascript/LazyImages.js"></script>
    <script src="Javascript/FocusImage.js"></script>
</body>
</html>
