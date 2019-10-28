<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Menu.Menu" %>
<link href="Css/Jonsuh_Hamburgers/Hamburgers.css" rel="stylesheet">

<div id="titleAndMenu"></div>
<div id="menuDiv"></div>

<div style="position:fixed; top:-4px; left: -5px; z-index:2147483647">
    <button class="hamburger hamburger--squeeze" type="button" style="outline:none;">
        <span class="hamburger-box">
            <span class="hamburger-inner"></span>
        </span>
    </button>
</div>

<script>
    //Look for .hamburger
    var hamburger = document.querySelector(".hamburger");
    // On click
    hamburger.addEventListener("click", function ()
    {
        // Toggle class "is-active"
        hamburger.classList.toggle("is-active");

        var list = document.getElementById("list");
        if (list.className === "menHide" || list.className === "menHidden")
        {
            list.className = "menVis";
        }
        else
        {
            //list.style.visibility = "hidden";
            list.className = "menHide";
        }
    });
</script>
<script>
    /////Unchanging/////
    var menuDiv = document.getElementById("menuDiv");
    
    var height;
    var buttWidth;
    var fSize;
    var titleFontSize;
    var dropDownWidth;
    var buttonPadding;

    /////Color Scheme/////
    /*
    var colLightest = "#E2FFFE";
    var colLighter = "#A5D0D6";
    var colLight = "#6DA9B1";
    var colDark = "grey";
    var colDarkest = "dimgrey";
    */

    /////Title/////
    var title = "#TheParsnip";
    var titleColor = "white";

    /////Menu List/////
    var fontCol = "white";

    /////Mobile / Desktop Variables/////
    if (isMobile() === true)
    {
        var w = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
        var h = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);

        buttonPadding = "10px";

        if (h > w)
        {
            //dropDownWidth = "50%";

        }
        else
        {
            //dropDownWidth = "200px";
        }
    }
    else
    {
        buttonPadding = "10px";
        //dropDownWidth = "200px";
    }
    dropDownWidth = "200px";

    height = "45px";
    buttFontSize = "17px";

    /////Buttons/////
    createButton("🏠 Home 🏠", "home");
    createButton("😂 Memes 😂", "memes");
    createButton("📹 Videos 📹", "videos");
    createButton("📷 Photos 📷", "photos");
    createButton("☕ AfternoonT ☕", "afternoont.html");
    createButton("💩 Bios 💩", "bios");    
    createButton("✈😎 KRAKOW <br /> 2K19 😎✈", "krakow");

    if (getCookie("accountType") === "admin")
    {
        createButton("💪 Admin 💪", "admin");
    }

    if (getCookie("accountType") === "admin" || getCookie("accountType") === "media" ||getCookie("accountType") === "member" || getCookie("accountType") === "user")
    {
        createButton("👋 Log Out 👋", "logout");
    }
    else
    {
        createButton("🔓 Log In 🔓", "login");
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    menuDiv.style.zIndex = "2147483646";
    //menuDiv.style.backgroundColor = colDarkest;
    menuDiv.className = "background-darkest"
    menuDiv.style.display = "inline-block";
    menuDiv.style.height = height;
    menuDiv.style.listStyle = "none";
    menuDiv.style.position = "fixed";
    menuDiv.style.top = "0px";
    menuDiv.style.left = "0px";
    menuDiv.style.padding = "0px";
    menuDiv.style.width = "100%";
    menuDiv.style.textAlign = "center";

    var pageTitle = document.createElement("h2");
    pageTitle.innerHTML = title;
    pageTitle.style.fontWeight = "bold";
    pageTitle.style.color = titleColor;
    pageTitle.style.marginTop = "5px";

    menuDiv.appendChild(pageTitle);

    function createButton(title, href)
    {
        if (document.getElementById("list"))
        {
            funcCreateButton(title, href);
        }
        else
        {
            createList();
            funcCreateButton(title, href);
        }
    }

    if (!document.getElementById("list"))
    {
        createList();
    }

    function createList()
    {
        var list = document.createElement("ul");
        //list.style.backgroundColor = colDark;
        list.className = "background-dark"
        list.style.position = "fixed";
        list.style.top = height;
        list.style.width = dropDownWidth;
        list.style.listStyle = "none";
        list.style.padding = "0px";
        list.style.margin = "0px";
        list.className = "menHidden";
        list.id = "list";
        list.style.zIndex = "2147483646";
        document.getElementById("body").appendChild(list);
    }

    var firstButton;
    function funcCreateButton(title, href)
    {
        var butt = document.createElement("li");
        if (firstButton === false)
        {
            butt.style.textAlign = "center";
        }

        butt.style.width = "100%";
        butt.zIndex = "2147483646";
        //butt.style.backgroundColor = colDark;
        butt.className = "background-dark";

        var buttAnk = document.createElement("a");
        buttAnk.style.color = fontCol;
        buttAnk.innerHTML = title;
        buttAnk.href = href;
        buttAnk.style.width = "100%";
        buttAnk.style.padding = "0px";
        buttAnk.style.textDecoration = "none";
        buttAnk.style.display = "block";
        buttAnk.className = "menu";
        buttAnk.style.fontSize = buttFontSize;
        buttAnk.style.paddingTop = buttonPadding;
        buttAnk.style.paddingBottom = buttonPadding;

        butt.appendChild(buttAnk);
        list.appendChild(butt);
        firstButton = false;
    }

    function isMobile()
    {
        if (navigator.userAgent.match(/Android/i)
            || navigator.userAgent.match(/webOS/i)
            || navigator.userAgent.match(/iPhone/i)
            || navigator.userAgent.match(/iPad/i)
            || navigator.userAgent.match(/iPod/i)
            || navigator.userAgent.match(/BlackBerry/i)
            || navigator.userAgent.match(/Windows Phone/i)
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
</script>