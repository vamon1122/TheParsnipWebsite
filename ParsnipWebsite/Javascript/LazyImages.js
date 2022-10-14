document.addEventListener("DOMContentLoaded", function ()
{
    var lazyImages = [].slice.call(document.querySelectorAll("img.lazy"));

    if ("IntersectionObserver" in window)
    {
        let lazyImageObserver = new IntersectionObserver(function (entries, observer)
        {
            entries.forEach(function (entry)
            {
                if (entry.isIntersecting)
                {
                    let lazyImage = entry.target;
                    lazyImage.src = lazyImage.dataset.src;
                    lazyImage.srcset = lazyImage.dataset.srcset;
                    lazyImage.classList.remove("lazy");
                    //PageMethods.MyMethod(1, myMethodCallBackSuccess, myMethodCallBackFailed)
                    lazyImageObserver.unobserve(lazyImage);
                }
            });
        });
        lazyImages.forEach(function (lazyImage)
        {
            lazyImageObserver.observe(lazyImage);
        });
    }
    else
    {
        //I used Javascript/intersection-observer as a fallback
    }
});

function myMethodCallBackSuccess(response) {
    alert(response);
}

function myMethodCallBackFailed(error) {
    alert(error.get_message());
}