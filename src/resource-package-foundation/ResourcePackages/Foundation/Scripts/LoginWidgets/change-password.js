(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var widgetContainer = document.querySelector("[data-sf-role='sf-change-password-container']");
        var form = widgetContainer.querySelector("form");

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
                    if (status === 0 || (status >= 200 && status < 400)) {
                        form.reset();
                        postPasswordChangeAction();
                    } else {
                        response.json().then((res) => {
                            var errorMessage = res.error.message;
                            var element;

                            if (status == 400) {
                                var element = form.querySelector("input[name='NewPassword']");
                            } else if (status == 403) {
                                var element = form.querySelector("input[name='OldPassword']");
                            }

                            invalidateElement(element);
                            showErrorMessage(errorMessage, form);
                        });
                    }
                });
        });

        var postPasswordChangeAction = function () {
            var action = widgetContainer.querySelector("input[name='postChangeAction']").value;

            if (action === 'ViewAMessage') {
                var message = widgetContainer.querySelector("input[name='postChangeMessage']").value;

                showSuccessMessage(message, form);
            } else if (action === 'RedirectToPage') {
                var redirectUrl = widgetContainer.querySelector("input[name='redirectUrl']").value;

                redirect(redirectUrl);
            }
        };

        var serializeForm = function (form) {
            var obj = {};
            var formData = new FormData(form);
            for (var key of formData.keys()) {
                obj[key] = formData.get(key);
            }
            return obj;
        };

        var validateForm = function (form) {
            resetValidationErrors(form);

            var requiredInputs = form.querySelectorAll("input[data-sf-role='required']");

            var isValid = true;

            requiredInputs.forEach(function (input) {
                if (!input.value) {
                    invalidateElement(input);
                    isValid = false;
                }
            });

            if (!isValid) {
                var errorMessage = widgetContainer.querySelector("input[name='validationRequiredMessage']").value;
                showErrorMessage(errorMessage, form);

                return isValid;
            }

            var newPassword = form.querySelector("input[name='NewPassword']");
            var repeatPassword = form.querySelector("input[name='RepeatPassword']");

            if (isValid && repeatPassword.value !== newPassword.value) {
                invalidateElement(repeatPassword);

                isValid = false;

                var errorMessage = widgetContainer.querySelector("input[name='validationMismatchMessage']").value;
                showErrorMessage(errorMessage, form);
            }

            return isValid;
        };

        var invalidateElement = function (element) {
            if (!element) {
                return;
            }

            element.classList.add('is-invalid');
        };

        var resetValidationErrors = function (parentElement) {
            var invalidElements = parentElement.querySelectorAll('.is-invalid');
            invalidElements.forEach(function (element) {
                element.classList.remove('is-invalid');
            });
        }

        var redirect = function (redirectUrl) {
            window.location = redirectUrl;
        };

        var showSuccessMessage = function (message, parentElement) {
            hideErrorMessage(parentElement);
            showMessage(message, parentElement, "[data-sf-role='success-message-container']");
        };

        var showErrorMessage = function (message, parentElement) {
            hideSuccessMessage(parentElement);
            showMessage(message, parentElement, "[data-sf-role='error-message-container']");
        };

        var showMessage = function (message, parentElement, querySelector) {
            var messageElement = parentElement.querySelector(querySelector);
            messageElement.innerText = message;
            messageElement.classList.remove("hide");
        };

        var hideSuccessMessage = function (parentElement) {
            hideMessage(parentElement, "[data-sf-role='success-message-container']");
        };

        var hideErrorMessage = function (parentElement) {
            hideMessage(parentElement, "[data-sf-role='error-message-container']");
        };

        var hideMessage = function (parentElement, querySelector) {
            var element = parentElement.querySelector(querySelector);
            element.classList.add("hide");
        };
    });
})();
