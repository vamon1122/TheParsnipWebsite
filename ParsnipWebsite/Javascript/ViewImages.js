PageMethods.set_path(PageMethods.get_path() + '.aspx');

document.addEventListener("DOMContentLoaded", function () {
    var viewedImages = [].slice.call(document.querySelectorAll("img.lazy"));

    if ("CenterIntersectionObserver" in window) {
        let imageViewObserver = new CenterIntersectionObserver(function (entries, observer) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {

                    if (document.getElementById('mobileNav').style.display == 'block') {
                        document.getElementById('mobileNav').style.display = 'none'
                    }

                    //if (document.getElementById('mobileNav').style.display == 'block') {
                    //    PageMethods.OnMenuOpenMediaNotFocused();
                    //}
                    //else {
                        let viewedImage = entry.target;
                        PageMethods.OnMediaCenterScreen(viewedImage.id, document.body.id, !document.hasFocus());
                    //}
                }
            });
        });
        viewedImages.forEach(function (viewedImage) {
            imageViewObserver.observe(viewedImage);
        });
    }
    else {
        //I used Javascript/intersection-observer as a fallback
    }

    function onBlur() {
        PageMethods.OnMediaUnFocused("window unfocused", document.body.id);
    };
    function onFocus() {
        if (document.getElementById('mobileNav').style.display == 'block') {
            PageMethods.OnMenuOpenMediaNotReFocused();
        }
        else {
            PageMethods.OnMediaReFocused(document.body.id, 'tab back in focus', OnSuccess);
            function OnSuccess(response, userContext, methodName) {
                //PageMethods.OnWrite('response = ' + response);
                if (response == true) {
                    //document.getElementById('cover').display = 'block';
                    //$("#cover").fadeIn(100);
                    //$("#cover").display='block';

                    //if (isMobile()) {
                    //    document.getElementById('mobile_cover').style.display = 'block'
                    //}
                    //else {
                    //    document.getElementById('desktop_cover').style.display = 'block'
                    //}

                    document.getElementById('cover').style.display = 'block';
                    disableScroll();
                    
                    //alert("something");
                    //$("#cover").fadeOut(100); //after done.
                    PageMethods.OnWrite('Refreshing the page...');
                    window.location.href = window.location.href;
                }
            }
        }
    };

    if (/*@cc_on!@*/false) { // check for Internet Explorer
        document.onfocusin = onFocus;
        document.onfocusout = onBlur;
    } else {
        window.onfocus = onFocus;
        window.onblur = onBlur;
    }

    window.addEventListener('beforeunload', function (e) {
        PageMethods.OnMediaUnFocused('tab / window closed', document.body.id)
        //e.preventDefault();
        //e.returnValue = '';
    });
});

// left: 37, up: 38, right: 39, down: 40,
// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

function preventDefault(e) {
    e.preventDefault();
}

function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}

// modern Chrome requires { passive: false } when adding event
var supportsPassive = false;
try {
    window.addEventListener("test", null, Object.defineProperty({}, 'passive', {
        get: function () { supportsPassive = true; }
    }));
} catch (e) { }

var wheelOpt = supportsPassive ? { passive: false } : false;
var wheelEvent = 'onwheel' in document.createElement('div') ? 'wheel' : 'mousewheel';

// call this to Disable
function disableScroll() {
    window.addEventListener('DOMMouseScroll', preventDefault, false); // older FF
    window.addEventListener(wheelEvent, preventDefault, wheelOpt); // modern desktop
    window.addEventListener('touchmove', preventDefault, wheelOpt); // mobile
    window.addEventListener('keydown', preventDefaultForScrollKeys, false);
}

// call this to Enable
function enableScroll() {
    window.removeEventListener('DOMMouseScroll', preventDefault, false);
    window.removeEventListener(wheelEvent, preventDefault, wheelOpt);
    window.removeEventListener('touchmove', preventDefault, wheelOpt);
    window.removeEventListener('keydown', preventDefaultForScrollKeys, false);
}