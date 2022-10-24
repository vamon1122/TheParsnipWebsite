PageMethods.set_path(PageMethods.get_path() + '.aspx');

document.addEventListener("DOMContentLoaded", function () {
    var viewedImages = [].slice.call(document.querySelectorAll("img.lazy"));

    if ("TopIntersectionObserver" in window) {
        let imageViewObserver = new TopIntersectionObserver(function (entries, observer) {
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
                        PageMethods.OnMediaCenterScreen(viewedImage.id);
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
        PageMethods.OnMediaUnFocused("window unfocused");
    };
    function onFocus() {
        if (document.getElementById('mobileNav').style.display == 'block') {
            PageMethods.OnMenuOpenMediaNotReFocused();
        }
        else {
            PageMethods.OnMediaReFocused();
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
        PageMethods.OnMediaUnFocused('tab / window closed')
        //e.preventDefault();
        //e.returnValue = '';
    });
});