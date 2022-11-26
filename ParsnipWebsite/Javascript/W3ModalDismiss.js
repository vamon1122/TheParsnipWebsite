window.onclick = function (event) {
    if ($(event.target).attr('class') == "w3-modal") {
        event.target.style.display = "none";
        doFocusMedia('menu closed');
        PageMethods.OnMediaReFocused(document.body.id, 'menu closed', OnSuccess);
        function OnSuccess(response, userContext, methodName) {
            if (response == true) {
                PageMethods.OnWrite('Refreshing the page...');
                window.location.href = window.location.href;
            }
        }
    }
}