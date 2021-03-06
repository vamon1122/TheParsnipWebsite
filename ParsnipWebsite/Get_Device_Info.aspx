﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_Device_Info.aspx.cs" Inherits="ParsnipWebsite.Get_Device_Info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label id="errorLabel" style ="visibility:hidden">default</label>
            <label id="redirectLabel" style ="visibility:hidden">default redirect</label>
        </div>
    </form>
    <script src="../Javascript/Cookies.js"></script>
    <script src="../Javascript/DeviceInfo.js"></script>
    <script>
        var errorLabel = document.getElementById("errorLabel");
        var redirectLabel = document.getElementById("redirectLabel");
        errorLabel.innerHTML = "Creating deviceType cookie..." + deviceDetect();
        createCookie("deviceType", deviceDetect());
        createCookie("isMobile", isMobile())
        errorLabel.innerHTML = "deviceType cooke created successfully! Doing redirect...";

        redirectLabel.innerHTML = "1";

        var url_string = window.location.href
        redirectLabel.innerHTML = "2";
        var url;
        var redirect

        try {
            //More efficient but does not work on older browsers
            redirectLabel.innerHTML = "Try block entered";
            url = new URL(url_string);
            redirect = url.searchParams.get("url");
        }
        catch (e) {
            //More compatible method
            redirectLabel.innerHTML = "Catch block entered";
            url = window.location.href;
            redirectLabel.innerHTML = "Got href";
            redirect = url.split('=')[url.split('=').length - 1];

            redirectLabel.innerHTML = "Caught error. URL now = " + url;
        }
        
        var redirectLabel = document.getElementById("redirectLabel");
        redirectLabel.innerHTML = redirect;

        if (redirect === "" || redirect === null) {
            redirect = "home";
        }
            

        
        redirectLabel.innerHTML = "Redirect = " + redirect;

        errorLabel.innerHTML = "all fine. deviceType = " + deviceDetect() + "Redirect = " + redirect;

        //Use window.location.replace if possible because 
        //you don't want the current url to appear in the 
        //history, or to show - up when using the back button.
        try { window.location.replace(redirect); }
        catch(e) { window.location = redirect;}
        

        
        
    </script>
</body>
</html>
