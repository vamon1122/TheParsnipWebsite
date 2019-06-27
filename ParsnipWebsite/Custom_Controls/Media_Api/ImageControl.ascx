<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media_Api.ImageControl" %>

<div class="center_div">
<div runat="server" id="MyImageContainer" class="meme" style="background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:8px">

<h3 runat="server" id="MyTitle"></h3>
    <asp:Image runat="server" ID="MyImageHolder" Width="100%" />
    <br />
    <a runat="server" id="MyEdit">Edit</a>
    <a runat="server" id="MyShare">Share</a>
    </div>
    <hr class="break" />

    </div>