(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var widgetContainer = document.querySelector('[data-sf-role="sf-reset-password-container"]');
        var formContainer = widgetContainer.querySelector('[data-sf-role="form-container"]');
        var form = formContainer.querySelector("form");
        var errorMessageContainer = widgetContainer.querySelector('[data-sf-role="error-message-container"]');
        var successMessageContainer = widgetContainer.querySelector('[data-sf-role="success-message-container"]');

        form.addEventListener('submit', function (event) {
            event.preventDefault();

            if (!validateForm(form)) {
                return;
            }

            var model = { model: serializeForm(form) };
            var submitUrl = form.attributes['action'].value;
            window.fetch(submitUrl, { method: 'POST', body: JSON.stringify(model), headers: { 'Content-Type': 'application/json' } })
                .then((response) => {
                    var status = response.status;
                    if (status === 204) {
                        hideElement(formContainer);
                        showElement(successMessageContainer);
                    } else {
                        var invalidInput;
                        if (status == 400) {
                            invalidInput = form.querySelector("input[name='NewPassword']");
                        } else if (status == 403) {
                            invalidInput = form.querySelector("input[name='Answer']");
                        }

                        invalidateElement(invalidInput);

                        response.json().then((res) => {
                            var errorMessage = res.error.message;
                            errorMessageContainer.innerText = errorMessage;
                            showElement(errorMessageContainer);
                        });
                    }
                });
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
                errorMessageContainer.innerText = formContainer.querySelector("input[name='AllFieldsAreRequiredErrorMessage']").value;
                showElement(errorMessageContainer);

                return isValid;
            }

            var passwordFields = form.querySelectorAll("[type='password']");

            if (passwordFields[0].value !== passwordFields[1].value) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='PasswordsMismatchErrorMessage']").value;
                invalidateElement(passwordFields[1]);
                showElement(errorMessageContainer);

                return false;
            }

            return isValid;
        };

        var serializeForm = function (form) {
            var obj = {};
            var formData = new FormData(form);
            for (var key of formData.keys()) {
                obj[key] = formData.get(key);
            }
            return obj;
        };

        var invalidateElement = function (element) {
            if (element) {
                element.setAttribute('data-sf-invalid', '');
                element.style.border = "1px solid red";
            }
        };

        var resetValidationErrors = function (parentElement) {
            var invalidElements = parentElement.querySelectorAll('[data-sf-invalid]');
            invalidElements.forEach(function (element) {
                element.style.border = ""
            });
        };

        var showElement = function (element) {
            if (element) {
                element.style.display = "";
            }
        };

        var hideElement = function (element) {
            if (element) {
                element.style.display = "none";
            }
        };
    });
})();
