function onBlur() {
    PageMethods.OnMediaUnFocused("tab / window unfocused", document.body.id);
};

function onFocus() {
    if (document.getElementById('mobileNav').style.display == 'block') {
        PageMethods.OnMenuOpenMediaNotReFocused();
    }
    else {
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
    PageMethods.WriteLine('Im NOT adding the blur / focus handlers');
    document.onfocusin = onFocus;
    document.onfocusout = onBlur;
} else {
    window.onfocus = onFocus;
    window.onblur = onBlur;
}

window.addEventListener('beforeunload', function (e) {
    PageMethods.OnMediaUnFocused('tab / window closed', document.body.id)
});

