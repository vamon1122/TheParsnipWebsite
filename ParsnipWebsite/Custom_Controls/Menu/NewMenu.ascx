<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewMenu.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Menu.NewMenu" %>
<style>
    .context-button{
        width:65px; height:51px; padding-top:12px
    }
</style>
<div class="w3-top w3-card w3-red w3-large">
    <div class="main-container">
        <div style="margin:0px">
            <div runat="server" id="loggedInWidth" style="min-width:817.1px; height:0px" class="w3-hide-small w3-hide-medium" Visible="false" />
            <div runat="server" id="loggedOutWidth" style="min-width:800.2px; height:0px" class="w3-hide-small w3-hide-medium" Visible="true" />
            <%--<div style="min-width:56px; height:0px" class="w3-hide-large"></div>--%>
            <div id="left-content" class="w3-bar w3-red">
                <a class="w3-bar-item w3-button w3-hide-large w3-left w3-hover-white w3-large w3-red w3-padding-large" href="javascript:void(0);" onclick="document.getElementById('mobileNav').style.display='block';PageMethods.OnMediaUnFocused('menu opened', document.body.id)" title="Toggle Navigation Menu" style="width:65px"><i class="fa fa-bars"></i></a>
                <a runat="server" id="Desktop_Home" href="../../home" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Home</a>
                <a runat="server" id="Desktop_Latest" href="../../latest" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Latest</a>
                <a runat="server" id="Desktop_Memes" href="../../memes" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Memes</a>
                <a runat="server" id="Desktop_Videos" href="../../videos" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Videos</a>
                <a runat="server" id="Desktop_Photos" href="../../photos" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Photos</a>
                <a runat="server" id="Desktop_AfternoonT" href="../../afternoont.html" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Afternoon T</a>
                <%--<a runat="server" id="Desktop_Bios" href="../../bios" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Bios</a>--%>
                <%--<a runat="server" id="Desktop_Krakow" href="../../krakow" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Krakow</a>--%>
                <a runat="server" id="Desktop_LogOut" href="../../logout" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white" Visible="false">Log Out</a>
                <a runat="server" id="Desktop_LogIn" href="../../login" class="w3-bar-item w3-button w3-hide-small w3-hide-medium w3-padding-large w3-hover-white">Log In</a>
            </div>
        </div>


      
    <div id="center-content" class="w3-bar w3-red" style="text-overflow:ellipsis">
        <a runat="server" id="Selected_Page" href="../../#" class="w3-bar-item w3-button w3-padding-large w3-white" Visible="false" style="max-width:100%; text-overflow:ellipsis; white-space: nowrap">View Tag</a>
    </div>
    <div runat="server" id="right_content" class="w3-red" style="min-width:0px">
        <span runat="server" id="Modal_Upload" class="w3-bar-item w3-button context-button w3-black w3-right" onclick="document.getElementById('uploadMedia').style.display='block'" Visible="false" ><i class="fa fa-cloud-upload-alt fa-lg"></i></span>
        <span runat="server" id="Modal_Share" class="w3-bar-item w3-button context-button w3-black w3-right" onclick="document.getElementById('shareModal').style.display='block'" Visible="false" ><i class="fa fa-share-alt fa-lg"></i></span>
        <a runat="server" id="Search_Button" class="w3-bar-item w3-button context-button w3-black w3-right" href="../../search" ><i class="fas fa-search fa-lg"></i></a>
        <a runat="server" id="Admin" class="w3-bar-item w3-button context-button w3-black w3-right" href="../../admin" Visible="false" ><i class="fas fa-tools fa-lg"></i></a>
    </div>
    </div>
  <!-- Navbar on small screens -->

    <div class="w3-modal" id="mobileNav" onclick="void(0)" style="padding:0px; background-color:transparent">
        <div id="" class="w3-bar-block w3-white w3-hide-large w3-large" style="margin-top: 51px">
            <a runat="server" id="Mobile_Home" href="../../home" class="w3-bar-item w3-button w3-padding-large">Home</a>
            <a runat="server" id="Mobile_Latest" href="../../latest" class="w3-bar-item w3-button w3-padding-large">Latest</a>
              <a runat="server" id="Mobile_Memes" href="../../memes" class="w3-bar-item w3-button w3-padding-large">Memes</a>
            <a runat="server" id="Mobile_Videos" href="../../videos" class="w3-bar-item w3-button w3-padding-large">Videos</a>
            <a runat="server" id="Mobile_Photos" href="../../photos" class="w3-bar-item w3-button w3-padding-large">Photos</a>
            <a runat="server" id="Mobile_AfternoonT" href="../../afternoont.html" class="w3-bar-item w3-button w3-padding-large">Afternoon T</a>
              <%--<a runat="server" id="Mobile_Bios" href="../../bios" class="w3-bar-item w3-button w3-padding-large">Bios</a>--%>
              <%--<a runat="server" id="Mobile_Krakow" href="../../krakow" class="w3-bar-item w3-button w3-padding-large">Krakow</a>--%>
              <a runat="server" id="Mobile_LogOut" href="../../logout" class="w3-bar-item w3-button w3-padding-large" Visible="false">Log Out</a>
              <a runat="server" id="Mobile_LogIn" href="../../login" class="w3-bar-item w3-button w3-padding-large">Log In</a>
        </div>
    </div>
</div>
