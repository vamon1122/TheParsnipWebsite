function isMobile() {
    if (navigator.userAgent.match(/Android/i)
        || navigator.userAgent.match(/webOS/i)
        || navigator.userAgent.match(/iPhone/i)
        || navigator.userAgent.match(/iPad/i)
        || navigator.userAgent.match(/iPod/i)
        || navigator.userAgent.match(/BlackBerry/i)
        || navigator.userAgent.match(/Windows Phone/i)
    ) {
        return true;
    }
    else {
        return false;
    }
}

function getLocation() {
    createCookie("deviceLocation", "Geolocation function was called.");
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        createCookie("deviceLocation", "Geolocation is not supported by this browser.");
    }
}

function deviceDetect() {
    if (navigator.userAgent.match(/Android/i)) { return 'Android'; }
    else if (navigator.userAgent.match(/webOS/i)) { return 'webOS'; }
    else if (navigator.userAgent.match(/iPhone/i)) { return 'iPhone'; }
    else if (navigator.userAgent.match(/iPad/i)) { return 'iPad'; }
    else if (navigator.userAgent.match(/iPod/i)) { return 'iPod'; }
    else if (navigator.userAgent.match(/BlackBerry/i)) { return 'BlackBerry'; }
    else if (navigator.userAgent.match(/Windows Phone/i)) { return 'Windows Phone'; }
    else if (navigator.appVersion.indexOf("Win") !== -1) { return "Windows"; }
    else if (navigator.appVersion.indexOf("Mac") !== -1) { return "MacOS"; }
    else if (navigator.appVersion.indexOf("X11") !== -1) { return "UNIX"; }
    else if (navigator.appVersion.indexOf("Linux") !== -1) { return "Linux"; }
}

function userAgent() {
    if (navigator.userAgent.match(/Android/i)) { return Android; }
    else if (navigator.userAgent.match(/webOS/i)) { return webOS; }
    else if (navigator.userAgent.match(/iPhone/i)) { return iPhone; }
    else if (navigator.userAgent.match(/iPad/i)) { return iPad; }
    else if (navigator.userAgent.match(/iPod/i)) { return iPod; }
    else if (navigator.userAgent.match(/BlackBerry/i)) { return BlackBerry; }
    else if (navigator.userAgent.match(/Windows Phone/i)) { return WindowsPhone; }
    else if (navigator.appVersion.indexOf("Win") !== -1) { return Windows; }
    else if (navigator.appVersion.indexOf("Mac") !== -1) { return MacOS; }
    else if (navigator.appVersion.indexOf("X11") !== -1) { return UNIX; }
    else if (navigator.appVersion.indexOf("Linux") !== -1) { return Linux; }
}