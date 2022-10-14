<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tag.aspx.cs" Inherits="ParsnipWebsite.View_Tag" %>
<%@ Register Src="~/Custom_Controls/Media/UploadMediaControl.ascx" TagPrefix="mediaControls" TagName="UploadMediaControl" %>
<%@ Register Src="~/Custom_Controls/ErrorHandler.ascx" TagPrefix="errorHandler" TagName="ErrorHandler" %>
<%@ Register Src="~/Custom_Controls/Menu/NewMenu.ascx" TagPrefix="menuControls" TagName="NewMenu" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>View Tag</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Libraries/w3.css-4.13/w3.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Lato/Lato.css" />
    <link rel="stylesheet" href="Libraries/Fonts/Montserrat/Montserrat.css" />
    <link rel="stylesheet" href="Libraries/fontawesome-free-5.15.1-web/css/all.css" />
    <link rel="stylesheet" type="text/css" href="Css/MediaStyle.css" />
    <!-- FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
</head>
<body>
    <errorHandler:ErrorHandler runat="server" ID="ErrorHandler" />
    <menuControls:NewMenu runat="server" ID="NewMenu" />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <header class="w3-container w3-red w3-center" style="padding:60px 16px 20px 16px; margin-bottom: 20px">
            <h1 class="w3-margin w3-jumbo jumbo-line-height" style="overflow-wrap: break-word" id="TagName" runat="server">View Tag</h1>
            <p class="w3-xlarge" id="TagDescription" runat="server"></p>
            <mediaControls:UploadMediaControl runat="server" ID="UploadMediaControl" />
        </header>
        <div runat="server" id="DynamicMediaDiv" style="margin: auto; text-align:center" />
        <script>
            <%--PageMethods.MyMethod(1,myMethodCallBackSuccess);--%>

            function myMethodCallBackSuccess(response) {
                //alert("something");
                //alert(response);
            }

            function myMethodCallBackFailed(error) {
                alert(error.get_message());
            }

            document.addEventListener("DOMContentLoaded", function () {
                var lazyImages = [].slice.call(document.querySelectorAll("img.lazy"));

                if ("IntersectionObserver" in window) {
                    let lazyImageObserver = new IntersectionObserver(function (entries, observer) {
                        entries.forEach(function (entry) {
                            if (entry.isIntersecting) {
                                let lazyImage = entry.target;
                                lazyImage.src = lazyImage.dataset.src;
                                lazyImage.srcset = lazyImage.dataset.srcset;
                                lazyImage.classList.remove("lazy");
                                //PageMethods.MyMethod(1, myMethodCallBackSuccess, myMethodCallBackFailed);
                                PageMethods.MyMethod(1, myMethodCallBackSuccess);
                                //alert("test");
                                lazyImageObserver.unobserve(lazyImage);
                            }
                        });
                    });
                    lazyImages.forEach(function (lazyImage) {
                        lazyImageObserver.observe(lazyImage);
                    });
                }
                else {
                    //I used Javascript/intersection-observer as a fallback
                }
            });
        </script>
    
    </form>    

    <script src="Libraries/jquery-3.5.1/jquery.min.js"></script>
    <%--<script src="Javascript/LazyImages.js"></script>--%>
    <script src="Javascript/IntersectionObserver.js"></script>
    <script src="Javascript/smoothscroll.min.js"></script>
    <script>smoothscroll.polyfill();</script>
    <script src="Javascript/FocusImage.js"></script>
    <script src="Javascript/W3ModalDismiss.js"></script>
</body>
</html>

