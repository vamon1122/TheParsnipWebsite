/*

*** YOUTUBE EFFICIENT EMBEDDING ***

    Use this snippet of code instead of the default youtube code

        < div class="youtube-container" >
            <div class="youtube-player" data-id="video_id"></div>
</div >

*/

(function ()
{
    var v = document.getElementsByClassName("youtube-player");
    for (var n = 0; n < v.length; n++)
    {
        var p = document.createElement("div");
        p.innerHTML = labnolThumb(v[n].dataset.id);
        //p.onclick = labnolIframe;
        v[n].appendChild(p);
    }
})();

function labnolThumb(id)
{
    return '<a href= "watch_video?data-id=' + id + '"><img class="youtube-thumb" src="//i.ytimg.com/vi/' + id + '/hqdefault.jpg"><div class="play-button"></div></a>';
}

function labnolIframe()
{
    var iframe = document.createElement("iframe");
    //Added &mute=1 attrubute to re-enable autoplay
    var source = "//www.youtube.com/embed/" + this.parentNode.dataset.id;

    if (isMobile)
    {
        source += "?";
    }
    else
    {
        source += "?autoplay=1&mute=1&";
    }

    source += "autohide=2&border=0&wmode=opaque&enablejsapi=1&controls=1";
    
    iframe.setAttribute("src", source);
    iframe.setAttribute("frameborder", "0");
    iframe.setAttribute("id", "youtube-iframe");
    this.parentNode.replaceChild(iframe, this);
}