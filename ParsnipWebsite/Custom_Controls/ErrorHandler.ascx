<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorHandler.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.ErrorHandler" %>
<div class="w3-container w3-margin-top" style="width:100%; position:fixed; z-index:100">
    <div class="w3-panel w3-yellow w3-opacity alert" id="AccessWarning">
        <span class="w3-right dismiss" onclick="document.getElementById('AccessWarning').style.display='none'">x</span>
        <h3>Access Denied</h3>
        <p>You do not have permission to edit media which other people have uploaded!</p>
    </div> 
    <div class="w3-panel w3-yellow w3-opacity alert" id="ExistsWarning">
        <span class="w3-right dismiss" onclick="document.getElementById('ExistsWarning').style.display='none'">x</span>
        <h3>Media Not Found</h3>
        <p>The media you are looking for could not be found. This could be because it has been deleted, the person who uploaded it has deleted their account or had it suspended or the subject of the media has asked for it to be removed.</p>
    </div>
    <div class="w3-panel w3-yellow w3-opacity alert" id="LogInWarning">
        <span class="w3-right dismiss" onclick="document.getElementById('LogInWarning').style.display='none'">x</span>
        <h3>Confirm Identity</h3>
        <p>You must log in first!</p>
    </div>
    <div class="w3-panel w3-yellow w3-opacity alert" id="BlankSearchWarning">
        <span class="w3-right dismiss" onclick="document.getElementById('BlankSearchWarning').style.display='none'">x</span>
        <h3>Search was blank!</h3>
        <p>You must type something before clicking the search button</p>
    </div>
    <div class="w3-panel w3-yellow w3-opacity alert" id="TagExistsWarning">
        <span class="w3-right dismiss" onclick="document.getElementById('TagExistsWarning').style.display='none'">x</span>
        <h3>Tag Not Found</h3>
        <p>The # / @ tag which you are looking for could not be found. This could be because it has been deleted, the person who created it has deleted their account or had it suspended, or the subject of the tag has asked for it to be removed.</p>
    </div>
</div>
<script>
    var url_string = window.location.href
    var url = new URL(url_string);
    var error = url.searchParams.get("alert");
    if (error !== "" && error !== null) {
        if (error === "P100") {
            document.getElementById("AccessWarning").style = "display:block";
        }
        if (error === "P101") {
            document.getElementById("ExistsWarning").style = "display:block";
        }
        if (error === "P102") {
            document.getElementById("LogInWarning").style = "display:block";
        }
        if (error === "P103") {
            document.getElementById("BlankSearchWarning").style = "display:block";
        }
        if (error === "P104") {
            document.getElementById("TagExistsWarning").style = "display:block";
        }
    }

    var redirect = url.searchParams.get("url");
    if (redirect !== "" && redirect !== null && url_string.includes("login")) {
            document.getElementById("LogInWarning").style = "display:block"
    }


</script>
