window.onclick = function (event) {
    if ($(event.target).attr('class') == "w3-modal") {
        event.target.style.display = "none";
    }
}