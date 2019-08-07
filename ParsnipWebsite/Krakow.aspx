﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Krakow.aspx.cs" Inherits="ParsnipWebsite.Krakow" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- BOOTSTRAP START -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" crossorigin="anonymous"></script>
    <!-- BOOTSTRAP END -->
    
    <script src="../Javascript/Useful_Functions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/Shared_Style.css" />
    <script src="../Javascript/Apply_Style.js"></script>

    <script src="Javascript/Intersection_Observer.js"></script>

    <title>Krakow</title>
</head>
<body class="fade0p5" id="body" style="text-align:center">
    <menuControls:Menu runat="server" ID="Menu" />

    
    <div class="alert alert-warning alert-dismissible parsnip-alert" style="display: none;" id="AccessWarning">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Access Denied</strong> You cannot edit photos which other people have uploaded!
    </div>
    <div class="alert alert-danger alert-dismissible parsnip-alert" style="display: none;" id="VideoError">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Upload Error</strong> You cannot upload videos to the photos page!
    </div>
    


    <h2>Krakow</h2>
    <hr class="break" />
    <div id="flightDetailsContainer">
    <h4 id="countdownToKrakow"></h4>
        <label id="countdownInfo"></label>


    <hr class="break" />
        
    <img src="Resources/Media/Images/Local/Krakow/Krakow%20Flight%20Times.png" class="image-preview" id="flightDetailsImage" />
    <hr class="break" />
    </div>
    <form runat="server">
        
    <h4>Upload your Krakow photos</h4>
        <div runat="server" id="UploadDiv" class="form-group" style="display:none; ">
            <label class="file-upload btn">                
                <span><strong>Upload Photo</strong></span>
                <asp:FileUpload ID="PhotoUpload" runat="server" class="form-control-file" onchange="this.form.submit()" />
            </label>
        </div>
        <hr class="break" />
        <div runat="server" id="DynamicPhotosDiv">
        </div>
    </form>    
    
    <script src="../Javascript/Useful_Functions.js"></script>
    <script src="../Javascript/Focus_Image.js"></script>
    <script src="../Javascript/Count_Down_To_Krakow.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function ()
        {
            var lazyImages = [].slice.call(document.querySelectorAll("img.lazy"));

            if ("IntersectionObserver" in window)
            {
                let lazyImageObserver = new IntersectionObserver(function (entries, observer)
                {
                    entries.forEach(function (entry)
                    {
                        if (entry.isIntersecting)
                        {
                            let lazyImage = entry.target;
                            lazyImage.src = lazyImage.dataset.src;
                            lazyImage.srcset = lazyImage.dataset.srcset;
                            lazyImage.classList.remove("lazy");
                            lazyImageObserver.unobserve(lazyImage);
                        }
                    });
                });
                lazyImages.forEach(function (lazyImage)
                {
                    lazyImageObserver.observe(lazyImage);
                });
            }
            else
            {
                //I used Javascript/intersection-observer as a fallback
            }
        });
    </script>
    <script>
        var url_string = window.location.href
        var url = new URL(url_string);
        var error = url.searchParams.get("error");
        if (error !== "" && error !== null)
        {
            if (error === "video") {
                document.getElementById("VideoError").style = "display:block";
            }
            else {
                document.getElementById("AccessWarning").style = "display:block";
            }
        }
    </script>
</body>
</html>
