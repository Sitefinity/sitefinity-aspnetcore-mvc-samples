(function () {    
    window.onloadCallback = renderCaptcha;

    document.addEventListener('widgetLoaded', function (args) {        
        if (args.detail.model.Name === "Captcha2") {
            renderCaptcha();
        }
    });

    function renderCaptcha() {
        grecaptcha.render('captchav2', {
            'sitekey': <Captcha V2 site key>
        });
    }
})();
