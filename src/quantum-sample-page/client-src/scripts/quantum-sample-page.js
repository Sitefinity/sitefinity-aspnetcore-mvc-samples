document.addEventListener("DOMContentLoaded", function() {
    var formHeadingSelector = "get-free-demo";
    var anchorButtonSelector = "a.btn";
    var checkboxWrapperSelector = ".form-check";
    var checboxSelector = "input[type=checkbox]";
    var submitButtonSelector = "submitButton";
    var submitFormSelector = "submitForm";
    var submitSuccessSelector = "submitSuccess";

    var checkboxInvalidClass = "invalid";
    var hiddenClass = "d-none";

    document.querySelectorAll(anchorButtonSelector).forEach(function (anchor) {
        anchor.href = "#" + formHeadingSelector;

        anchor.addEventListener("click", function (e) {
            e.preventDefault();

            document.querySelector(this.getAttribute("href")).scrollIntoView({
                behavior: "smooth"
            });
        });
    });

    var submitButton = document.getElementById(submitButtonSelector);
    var submitForm = document.getElementById(submitFormSelector);
    var privacyPolicyCheckbox = submitForm.querySelector(checboxSelector);
    var submitSuccess = document.getElementById(submitSuccessSelector);

    submitButton.addEventListener("click", function () {
        if (!privacyPolicyCheckbox.checkValidity()) {
            privacyPolicyCheckbox.closest(checkboxWrapperSelector).classList.add(checkboxInvalidClass);
            privacyPolicyCheckbox.focus();
        }
    });

    privacyPolicyCheckbox.addEventListener("change", function (event) {
        if (event.target.checked) {
            privacyPolicyCheckbox.closest(checkboxWrapperSelector).classList.remove(checkboxInvalidClass);
        } else {
            privacyPolicyCheckbox.closest(checkboxWrapperSelector).classList.add(checkboxInvalidClass);
        }
    });

    submitForm.onsubmit = function (event) {
        event.preventDefault();

        if (submitForm.checkValidity()) {
            var formData = new FormData(submitForm);
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState === 4 && (this.status === 200 || this.status === 204)) {
                    submitForm.classList.add(hiddenClass);
                    submitSuccess.classList.remove(hiddenClass);
                }
            };
            xhr.open("post", "/FormValues", true);
            xhr.send(new URLSearchParams(formData));
            return false;
        }
    };
});
