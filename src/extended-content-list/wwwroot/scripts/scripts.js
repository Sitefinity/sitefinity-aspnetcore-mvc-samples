(function () {
    document.addEventListener('DOMContentLoaded', function () {
        enhanceWidgets(document);
    });

    document.addEventListener('widgetLoaded', function (args) {
        if (args.detail.model.Name === "ContentList") {
            enhanceWidgets(args.detail.element);
        }
    });

    function enhanceWidgets(parentElement) {
        parentElement.querySelectorAll('[data-pageNumber]').forEach(function (x) {
            x.addEventListener('click', function (e) {

                e.preventDefault();
                e.stopPropagation();
                var selector = parentElement.querySelector(".contentListEntity");
                if (selector) {
                    window.fetch(`/sfrenderer/contentlist?page=${x.getAttribute('data-pageNumber')}`, { method: 'POST', body: JSON.stringify(JSON.parse(selector.innerText)), headers: { 'Content-Type': 'application/json' } }).then(function (response) {
                        response.json().then(function (json) {
                            console.log(json);
                        });
                    })
                }
            })
        });
    }
})();
