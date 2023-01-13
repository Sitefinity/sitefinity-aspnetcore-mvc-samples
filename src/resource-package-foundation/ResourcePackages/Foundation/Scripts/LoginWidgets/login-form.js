(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var widgetContainer = document.querySelector('[data-sf-role="sf-login-form-container"]');
        var formContainer = widgetContainer.querySelector('[data-sf-role="form-container"]');
        var form = formContainer.querySelector("form");
        var errorMessageContainer = widgetContainer.querySelector('[data-sf-role="error-message-container"]');

        form.addEventListener('submit', function (event) {
            event.preventDefault();
            setAntiforgeryTokens().then(res => {
                if (validateForm(form)) {
                    event.target.submit();
                }
            }, err => {
                showError("Antiforgery token retrieval failed");
            })
        });

        var validateForm = function (form) {
            var isValid = true;
            resetValidationErrors(form);
            hideElement(errorMessageContainer);

            var requiredInputs = form.querySelectorAll("input[data-sf-role='required']");

            requiredInputs.forEach(function (input) {
                if (!input.value) {
                    invalidateElement(input);
                    isValid = false;
                }
            });

            if (!isValid) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationRequiredMessage']").value;
                showElement(errorMessageContainer);
                return isValid;
            }

            var emailInput = form.querySelector("input[name='username']");
            if (!isValidEmail(emailInput.value)) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationInvalidEmailMessage']").value;
                invalidateElement(emailInput);
                showElement(errorMessageContainer);
                return false;
            }

            return isValid;
        };

        var invalidateElement = function (element) {
            if (element) {
                element.classList.add('is-invalid');
            }
        };

        var resetValidationErrors = function (parentElement) {
            var invalidElements = parentElement.querySelectorAll('.is-invalid');
            invalidElements.forEach(function (element) {
                element.classList.remove('is-invalid');
            });
        };

        var showElement = function (element) {
            if (element) {
                element.classList.add("visible");
                element.classList.remove("hide");
            }
        };

        var hideElement = function (element) {
            if (element) {
                element.classList.add("hide");
                element.classList.remove("visible");
            }
        };

        var isValidEmail = function (email) {
            return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w+)+$/.test(email);
        };

        function setAntiforgeryTokens() {
            return new Promise((resolve, reject) => {
                let xhr = new XMLHttpRequest();
                xhr.open('GET', '/sitefinity/anticsrf');
                xhr.setRequestHeader('X-SF-ANTIFORGERY-REQUEST', 'true')
                xhr.responseType = 'json';
                xhr.onload = function () {
                    const response = xhr.response;
                    if(response != null)
                    {
                        const token = response.Value;
                        document.querySelectorAll("input[name = 'sf_antiforgery']").forEach(i => i.value = token);
                        resolve();
                    }
                    else{
                        resolve();
                    }
                };
                xhr.onerror = function () { reject(); };
                xhr.send();
            });
        }

        function showError(err) {
            document.getElementById('errorContainer').innerText = err;
        }
    });
})();
