(function ()
{
    var v = document.getElementsByClassName("youtube-player");
    for (var n = 0; n < v.length; n++)
    {
        var p = document.createElement("div");
        p.innerHTML = labnolThumb(v[n].dataset.id);
        v[n].appendChild(p);
    }
})();

function labnolThumb(id)
{
    return '<a href= "watch_video?data-id=' + id + '"><img class="youtube-thumb" src="//i.ytimg.com/vi/' + id + '/hqdefault.jpg"><div class="play-button"></div></a>';
}