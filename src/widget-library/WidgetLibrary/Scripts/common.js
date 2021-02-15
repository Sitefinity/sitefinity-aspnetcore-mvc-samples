(function () {
    document.addEventListener('DOMContentLoaded', function () {
        document.querySelectorAll('scriptRef').forEach(function (x) {
            x.innerHTML = "Hello World from script";
        });
    });
})();
