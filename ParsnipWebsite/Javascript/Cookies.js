function testCookie() {
    document.cookie = "test = yes";
    if (checkCookie("test")) { return true; }
}

function createCookie(cname, cvalue) {
    document.cookie = cname + "=" + cvalue;
}

function createCookiePerm(cname, cvalue) {
    document.cookie = cname + "=" + cvalue + "; expires=Thu, 2 Aug 2020 20:47:11 UTC;";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

//Check Cookie
function checkCookie(p) {
    //alert('got here')
    var checkThis = getCookie(p);
    if (checkThis !== "") {
        return true;
    } else {
        return false;
    }
}