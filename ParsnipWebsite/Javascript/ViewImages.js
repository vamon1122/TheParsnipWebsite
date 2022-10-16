PageMethods.set_path(PageMethods.get_path() + '.aspx');


document.addEventListener("DOMContentLoaded", function () {
    var viewedImages = [].slice.call(document.querySelectorAll("img.lazy"));

    if ("TopIntersectionObserver" in window) {
        let imageViewObserver = new TopIntersectionObserver(function (entries, observer) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    let viewedImage = entry.target;
                    PageMethods.OnMediaCenterScreen(viewedImage.id);
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
});