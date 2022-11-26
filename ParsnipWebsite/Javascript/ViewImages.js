function doBlurMedia(reason) {
    var bodyId = document.body.id;
    var currentViewCookieId = bodyId + '_CurrentViewId';
    var viewId = getCookie(currentViewCookieId);
    createCookie(currentViewCookieId, null);
    createCookie(bodyId + '_PageHasHadFocusInTheCurrentSession', true);
    createCookie(bodyId + '_CurrentUnfocusedViewMediaId', getCookie(bodyId + '_CurrentViewMediaId'));
    createCookie(bodyId + '_CurrentViewId', null);
    createCookie(bodyId + '_CurrentViewMediaId', null);
    createCookie(bodyId + '_CurrentViewStartTime', null);
    
    PageMethods.WriteDebug('JS: Blur. Current view is ' + viewId);
}

function doFocusMedia(reason) {
    var bodyId = document.body.id;
    
    //TODO - Should I be removed? I think this was to fix an issue with using webmethods to monitor views?
    //if (getCookie(bodyId + '_PageHasHadFocusInTheCurrentSession')) {
    var existingCookie = getCookie(bodyId + '_CurrentUnfocusedViewMediaId');
    PageMethods.WriteDebug('JS: The tab was re-focused (' + reason + '). Current image view will be started... id=' + existingCookie + '.');
    doView('blah_' + existingCookie)
    //}
    //else {
    //    PageMethods.WriteDebug('JS: The tab was focused for the first time in the session (' + reason + ')');
    //}
}


function doView(controlId) {
    var bodyId = document.body.id;
    

    var _StopWatch = new StopWatch();
    _StopWatch.start();

    var currentViewCookieId = bodyId + '_CurrentViewId';

    var mediaId = controlId.split("_").pop();

    if (!document.hasFocus()) {
        PageMethods.WriteDebug('JS: Media was viewed but the tab was not in focus. NOT doing view of mediaID: ' + mediaId);
        createCookie(bodyId + '_CurrentUnfocusedViewMediaId', mediaId);
        return;
    }
    if (controlId.includes('thumbnail')) {
        PageMethods.WriteDebug('JS: Thumbnail was focused. NOT doing view of mediaID: ' + mediaId);
        createCookie(currentViewCookieId, null)
    }
    else {
        //PageMethods.WriteDebug('JS: Doing view of mediaID: ' + mediaId);

        createCookie(bodyId + '_CurrentViewMediaId', mediaId);
        createCookie(bodyId + '_CurrentViewStartTime', _StopWatch.startTime());

        var viewId = uuidv4();

        PageMethods.WriteDebug('JS: Doing view. ViewID = ' + viewId);

        createCookie(currentViewCookieId, viewId)

        checkStillInFocus();

        function uuidv4() {
            return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
        }

        function checkStillInFocus() {
            if (getCookie(currentViewCookieId) === viewId) {
                setTimeout(checkStillInFocus, 1);
            }
            else {
                _StopWatch.stop();
                var duration = _StopWatch.duration();

                PageMethods.OnMediaViewed(mediaId, duration, false);

                
                PageMethods.WriteDebug('JS: The view which was just inserted was ' + viewId);
            }
        }
    }
}

document.addEventListener("DOMContentLoaded", function ()
{
    var bodyId = document.body.id;
    var currentViewCookieId = bodyId + '_CurrentViewId';

    var viewedImages = [].slice.call(document.querySelectorAll("img.lazy"));

    if ("CenterIntersectionObserver" in window) {
        let imageViewObserver = new CenterIntersectionObserver(function (entries, observer)
        {
            entries.forEach(function (entry) {
                if (entry.isIntersecting)
                {
                    if (document.getElementById('mobileNav').style.display == 'block')
                    {
                        document.getElementById('mobileNav').style.display = 'none'
                    }

                    let viewedImage = entry.target;
                    doView(viewedImage.id);
                    PageMethods.OnMediaCenterScreen(viewedImage.id, document.body.id, !document.hasFocus());
                }
            });
        });
        viewedImages.forEach(function (viewedImage)
        {
            imageViewObserver.observe(viewedImage);
        });
    }
    else
    {
        //I used Javascript/intersection-observer as a fallback
    }

    function onBlur()
    {
        doBlurMedia('tab lost focus');
        PageMethods.OnMediaUnFocused("tab / window unfocused", document.body.id);
    };

    function onFocus()
    {
        if (document.getElementById('mobileNav').style.display == 'block')
        {
            PageMethods.OnMenuOpenMediaNotReFocused();
        }
        else
        {
            doFocusMedia('tab gained focus');
            PageMethods.OnMediaReFocused(document.body.id, 'tab back in focus', OnSuccess);
            function OnSuccess(response, userContext, methodName) {
                if (response == true) {
                    document.getElementById('cover').style.display = 'block';
                    disableScroll();
                    window.location.href = window.location.href;
                }
            }
        }
    };

    if (/*@cc_on!@*/false) //TODO - Implement check for Internet Explorer
    {
        document.onfocusin = onFocus;
        document.onfocusout = onBlur;
    } else {
        window.onfocus = onFocus;
        window.onblur = onBlur;
    }

    window.addEventListener('beforeunload', function (e)
    {   
        var mediaId = getCookie(bodyId + '_CurrentViewMediaId');
        var cookieStartTime = getCookie(bodyId + '_CurrentViewStartTime');

        if ((mediaId != 'null' && mediaId != '') || (cookieStartTime != 'null' && cookieStartTime != '')) {
            PageMethods.WriteDebug('Something has gone wrong here. The tab is closing but there is still media in focus!');
        }
        else {
            PageMethods.WriteDebug('Nothing was in focus when the page closed :)');
        }

        //PageMethods.OnMediaViewed(mediaId, _StopWatch.duration(), true);
        PageMethods.WriteDebug('OnMediaUnfocused skipped');
        //PageMethods.OnMediaUnFocused('tab / window closed', document.body.id)
    });
});
