(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var firstPageId = document.getElementById("firstPage").value;
        document.querySelectorAll(`[data-pageId='${firstPageId}']`).forEach(function (x) {
            x.addEventListener("mouseover", function () {
                document.getElementById("first").style.display = "";
            });

            x.addEventListener("mouseout", function () {
                document.getElementById("first").style.display = "none";
            });
        });

        var secondPageId = document.getElementById("secondPage").value;
        document.querySelectorAll(`[data-pageId='${secondPageId}']`).forEach(function (x) {
            x.addEventListener("mouseover", function () {
                document.getElementById("second").style.display = "";
            });

            x.addEventListener("mouseout", function () {
                document.getElementById("second").style.display = "none";
            });
        });

        var thirdPageId = document.getElementById("thirdPage").value;
        document.querySelectorAll(`[data-pageId='${thirdPageId}']`).forEach(function (x) {
            x.addEventListener("mouseover", function () {
                document.getElementById("third").style.display = "";
            });

            x.addEventListener("mouseout", function () {
                document.getElementById("third").style.display = "none";
            });
        });
    });
})();
