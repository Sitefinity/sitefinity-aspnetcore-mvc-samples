(function () {
    document.addEventListener('DOMContentLoaded', function () {
        document.querySelectorAll('.scriptRef').forEach(function (x) {
            x.innerHTML = "Overridden script content from web app";
        });
    });
})();
