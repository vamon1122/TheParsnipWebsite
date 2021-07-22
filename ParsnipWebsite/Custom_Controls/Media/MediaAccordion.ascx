<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaAccordion.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Media.MediaAccordion" %>
<div runat="server" id="Container" visible="true">
    <button runat="server" id="Expander" class="w3-dark-grey w3-block" style="border:none; cursor: pointer; padding-left:10px; padding-right:10px; padding-top:5px; padding-bottom:5px; max-width:480px; margin:auto" type="button">
        <span style="float:left" runat="server" id="MyText"></span>
        <i runat="server" id="IsExpandedIndicator" class="fa fa-minus" style="float:right; margin-top:4px"></i>
    </button>
    <div runat="server" id="Accordion">
        <div runat="server" id="MediaContainer" style="margin: auto; text-align:center"></div>
    </div>
</div>
