<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Loader.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Loader" %>
<style>
    .spinner_container 
    {
        display: flex;
        justify-content: center;
        align-items: center;
        text-align: center;
        min-height: 100vh;
    }

    .spinner 
    {
        border: 16px solid #f3f3f3;
        border-radius: 50% !important;
        border-top: 16px solid #3498db;
        width: 120px;
        height: 120px;
        -webkit-animation: spin 2s linear infinite !important; /* Safari */
        animation: spin 2s linear infinite;
    }

    /* Safari */
    @-webkit-keyframes spin 
    {
        0% 
        {
            -webkit-transform: rotate(0deg);
        }

        100% 
        {
            -webkit-transform: rotate(360deg);
        }
    }

    @keyframes spin 
    {
        0% 
        {
            transform: rotate(0deg);
        }

        100% 
        {
            transform: rotate(360deg);
        }
    }

    #cover 
    {
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

<div id="cover">
    <div class="spinner_container">
        <h1>Loading...</h1>
    </div>
</div>

<script>
    // left: 37, up: 38, right: 39, down: 40,
    // spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
    var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

    function preventDefault(e)
    {
        e.preventDefault();
    }

    function preventDefaultForScrollKeys(e)
    {
        if (keys[e.keyCode]) {
            preventDefault(e);
            return false;
        }
    }

    // modern Chrome requires { passive: false } when adding event
    var supportsPassive = false;
    try
    {
        window.addEventListener("test", null, Object.defineProperty({}, 'passive', {
            get: function () { supportsPassive = true; }
        }));
    } catch (e) { }

    var wheelOpt = supportsPassive ? { passive: false } : false;
    var wheelEvent = 'onwheel' in document.createElement('div') ? 'wheel' : 'mousewheel';

    // call this to Disable
    function disableScroll()
    {
        window.addEventListener('DOMMouseScroll', preventDefault, false); // older FF
        window.addEventListener(wheelEvent, preventDefault, wheelOpt); // modern desktop
        window.addEventListener('touchmove', preventDefault, wheelOpt); // mobile
        window.addEventListener('keydown', preventDefaultForScrollKeys, false);
    }

    // call this to Enable
    function enableScroll()
    {
        window.removeEventListener('DOMMouseScroll', preventDefault, false);
        window.removeEventListener(wheelEvent, preventDefault, wheelOpt);
        window.removeEventListener('touchmove', preventDefault, wheelOpt);
        window.removeEventListener('keydown', preventDefaultForScrollKeys, false);
    }
</script>
