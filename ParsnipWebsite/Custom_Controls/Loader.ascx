<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Loader.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Loader" %>
<style>
    .spinner_container {
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
    min-height: 100vh;
}

.spinner {
    border: 16px solid #f3f3f3;
    border-radius: 50% !important;
    border-top: 16px solid #3498db;
    width: 120px;
    height: 120px;
    -webkit-animation: spin 2s linear infinite !important; /* Safari */
    animation: spin 2s linear infinite;
}

/* Safari */
@-webkit-keyframes spin {
    0% {
        -webkit-transform: rotate(0deg);
    }

    100% {
        -webkit-transform: rotate(360deg);
    }
}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}

#cover {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    opacity: 0.80;
    background: #aaa;
    z-index: 10;
    display: none;
}
</style>
<!--Spinner stops once page starts reloading on iphone-->
<%--<div id="desktop_cover" class="cover">
    <div class="spinner_container">
        <div class="spinner"></div>
    </div>
    
</div>--%>

<div id="cover">
    <div class="spinner_container">
        <h1>Loading...</h1>
    </div>
</div>
