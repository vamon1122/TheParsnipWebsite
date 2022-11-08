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
                    $("#cover").fadeIn(100);
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