(function () {
    document.addEventListener('DOMContentLoaded', function () {
        enhanceWidgets(document);
    });

    document.addEventListener('widgetLoaded', function (args) {
        if (args.detail.model.Name === "SitefinityNavigation") {
            enhanceWidgets(args.detail.element);
        }
    });

    function enhanceWidgets(parentElement) {
        parentElement.querySelectorAll('.sc-accordion-link:not(.sf-group)').forEach(function (x) {
            x.addEventListener('mousedown', function (event) {
                if (event.which === 1) {
                    event.target.parentElement.removeAttribute("data-bs-toggle");
                }
            });
        });

        parentElement.querySelectorAll('.sc-accordion-link sf-group').forEach(function (x) {
            x.addEventListener('onclick', function (event) {
                event.preventDefault();
            });
        });
    }
})();
