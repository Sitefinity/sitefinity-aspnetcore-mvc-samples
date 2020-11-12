document.addEventListener("DOMContentLoaded", function() {
    var formSelector = "get-free-demo";

    document.querySelectorAll("a.btn").forEach(function (anchor) {
        anchor.href = "#" + formSelector;

        anchor.addEventListener("click", function (e) {
            e.preventDefault();

            document.querySelector(this.getAttribute("href")).scrollIntoView({
                behavior: "smooth"
            });
        });
    });
});