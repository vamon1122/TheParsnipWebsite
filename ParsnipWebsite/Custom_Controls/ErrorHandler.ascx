<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorHandler.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.ErrorHandler" %>
<div class="alert alert-warning alert-dismissible parsnip-alert" style="display: none;" id="AccessWarning">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <strong>Access Denied</strong> You do not have permission to edit media which other people have uploaded!
</div>
<div class="alert alert-warning alert-dismissible parsnip-alert" style="display: none;" id="ExistsWarning">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <strong>Media Not Found</strong> This media has been either been deleted... or it never existed!
</div>
<script>
        var url_string = window.location.href
        var url = new URL(url_string);
        var error = url.searchParams.get("error");
        if (error !== "" && error !== null)
        {
            if (error === "P100") {
                document.getElementById("AccessWarning").style = "display:block";
            }
            if (error === "P101") {
                document.getElementById("ExistsWarning").style = "display:block";
            }
        }
</script>
