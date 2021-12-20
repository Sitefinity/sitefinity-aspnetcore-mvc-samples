(function () {
    document.addEventListener('widgetLoaded', function (args) {
        if (args.detail.model.Name === "Chart") {
            document.location.reload(true)
        }
    });
})();
