<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ParsnipWebsite.Home" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

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

    <script src="../Javascript/UsefulFunctions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/SharedStyle.css" />
    <script src="../Javascript/ApplyStyle.js"></script>
    <script src="Javascript/IntersectionObserver.js"></script>
    <title>Home</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <label class="censored" id="pageId">home.html</label>
    
    <menuControls:Menu runat="server" ID="Menu" />
    <div class="media-control" style="margin: auto; font-family: 'Pacifico', cursive; color:red; font-size:40px; padding-bottom:10px; padding-left: 10px; padding-right:10px">
            "Devout discipels of Gaz Beadle's penis"
        </div>
    <div class="padded-text center_div">

        

        <asp:Label runat="server" ID="WelcomeLabel"></asp:Label>
    </div>
    <form runat="server">
        <div runat="server" id="LatestVideo" style="margin-top:10px"></div>
    </form>
    <div class="">
    <div class="media-control" style="margin: auto; padding-left: 10px; padding-right:10px">
    <h3 class="section-title"><b>YOUR WEBSITE NEEDS YOU!</b></h3>
    If someone could upload everything from the crystal parsnip, that would be awesome!
    
        <h3 class="section-title"><b>*NEW*</b></h3>
        <ol style="list-style-type: square; text-align: left; padding-left:18px">
            <li style="text-align: left">[28/10/19] Fixed bug where page would 'jump around' whilst content was loading</li>
            <li style="text-align: left">[20/09/19] Fixed bug where video player would take ages to load</li>
            <li style="text-align: left">[15/09/19] Added time machine! View the website as it was in <a href="http://original.theparsnip.co.uk">2015</a> when it was first conceived and in <a href="http://2016.theparsnip.co.uk">2016</a> when it had it's first major visual overhaul!</li>
            <li style="text-align: left">[17/08/19] Fixed broken image links on the <a href="Photos.aspx">photos page</a></li>
            <li style="text-align: left">[17/08/19] Fixed slow loading times on the <a href="Videos.aspx">videos page</a></li>
            <li style="text-align: left">[07/08/19] New <a href="Krakow.aspx">Krakow page!</a> Upload, view & share holiday photos!</li>
            <li style="text-align: left">[24/06/19] Share <a href="Photos.aspx">photos</a> and <a href="Videos.aspx">videos</a>! (Recipient doesn't even need an account to view what you share!!!)</li>
            <li style="text-align: left">[24/06/19] You can now access the home page without logging in</li>
        </ol>
        
    </div>
        </div>
    
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
