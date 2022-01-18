(function () {    
    window.onloadCallback = function () {
        grecaptcha.render('captchav2', {
            'sitekey': <Captcha V2 site key>
        });
    };
})();
