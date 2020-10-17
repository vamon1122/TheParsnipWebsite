try
{
    var url_string = window.location.href
    var url = new URL(url_string);
    var id = document.querySelector('[id*="' + url.searchParams.get("focus") + '"]').id;
    document.getElementById(id).scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById(id).scrollTop = 30;
}
catch (e)
{
    //This does not work on older browsers.
}