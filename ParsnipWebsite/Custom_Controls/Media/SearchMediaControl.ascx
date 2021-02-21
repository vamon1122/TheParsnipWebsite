<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchMediaControl.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.SearchMediaControl" %>
<div class="w3-card w3-large">
    <div class="main-container">
        <div id="center-content" class="w3-bar" style="text-overflow:ellipsis">
            <input runat="server" style="width:100%" AutoPostback="False" ID="TextBox_SearchNew" class="w3-bar-item search-bar" type="text" />
        </div>
        <div runat="server" id="right_content" class="w3-red" style="min-width:86px">
            <asp:Button runat="server" ID="Button_Search" UseSubmitBehavior="true"  CssClass="w3-btn w3-black w3-bar-item search-button" Text="Search" OnClick="Button_Search_Click" />
        </div>
    </div>
</div>
