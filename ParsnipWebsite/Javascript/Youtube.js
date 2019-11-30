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
    return '<a href= "watch?data-id=' + id + '"><img class="youtube-thumb" src="//i.ytimg.com/vi/' + id + '/hqdefault.jpg"><img class="youtube-play-button play-button" src="Resources/Media/Images/Web_Media/play-button-3.svg" /></a>';
}