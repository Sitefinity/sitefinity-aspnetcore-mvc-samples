(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var widgetContainer = document.querySelector('[data-sf-role="sf-registration-container"]');
        var formContainer = widgetContainer.querySelector('[data-sf-role="form-container"]');
        var form = formContainer.querySelector("form");
        var errorMessageContainer = widgetContainer.querySelector('[data-sf-role="error-message-container"]');
        var successRegistrationMessageContainer = widgetContainer.querySelector('[data-sf-role="success-registration-message-container"]');
        var confirmRegistrationMessageContainer = widgetContainer.querySelector('[data-sf-role="confirm-registration-message-container"]');

        form.addEventListener('submit', function (event) {
            event.preventDefault();

            if (!validateForm(form)) {
                return;
            }

            submitFormHandler(form, null, postRegistrationAction, onRegistrationError);
        });

        var postRegistrationAction = function () {
            var action = formContainer.querySelector("input[name='PostRegistrationAction']").value;
            var activationMethod = formContainer.querySelector("input[name='ActivationMethod']").value;

            if (action === 'ViewMessage') {
                if (activationMethod == "AfterConfirmation") {
                    showSuccessAndConfirmationSentMessage();
                } else {
                    showSuccessMessage();
                }
            } else if (action === 'RedirectToPage') {
                var redirectUrl = formContainer.querySelector("input[name='RedirectUrl']").value;

                redirect(redirectUrl);
            }
        };

        var onRegistrationError = function (errorMessage, status) {
            errorMessageContainer.innerText = errorMessage;
            showElement(errorMessageContainer);
        };

        var showSuccessMessage = function () {
            hideElement(errorMessageContainer);
            hideElement(formContainer);
            showElement(successRegistrationMessageContainer);
        };

        var showSuccessAndConfirmationSentMessage = function () {
            hideElement(formContainer);
            showElement(confirmRegistrationMessageContainer);

            var activationLinkLabel = confirmRegistrationMessageContainer.querySelector("input[name='ActivationLinkLabel']").value;

            var activationLinkMessageContainer = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-message-container"]');

            var formData = new FormData(form);
            var email = formData.get("Email");

            activationLinkMessageContainer.innerText = activationLinkLabel + " " + email;

            var sendAgainBtn = confirmRegistrationMessageContainer.querySelector('[data-sf-role="sendAgainLink"]');
            var resendUrl = confirmRegistrationMessageContainer.querySelector("input[name='ResendConfirmationEmailUrl']").value;

            sendAgainBtn.addEventListener('click', function (event) {
                event.preventDefault();
                submitFormHandler(form, resendUrl, postResendAction);
            });
        };

        var submitFormHandler = function (form, url, onSuccess, onError) {

            url = url || form.attributes['action'].value;

            var model = { model: serializeForm(form) };

            window.fetch(url, { method: 'POST', body: JSON.stringify(model), headers: { 'Content-Type': 'application/json' } })
                .then((response) => {
                    var status = response.status;
                    if (status === 0 || (status >= 200 && status < 400)) {
                        if (onSuccess) {
                            onSuccess();
                        }
                    } else {
                        response.json().then((res) => {
                            var message = res.error.message;

                            if (onError) {
                                onError(message, status);
                            }
                        });
                }
            });
        };

        var postResendAction = function () {
            var activationLinkMessageContainer = confirmRegistrationMessageContainer.querySelector('[data-sf-role="activation-link-message-container"]');
            var sendAgainLabel = confirmRegistrationMessageContainer.querySelector("input[name='SendAgainLabel']").value;
            var formData = new FormData(form);
            var email = formData.get("Email");
            activationLinkMessageContainer.innerText = sendAgainLabel.replace("{0}", email);
        };

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

            var emailInput = form.querySelector("input[name='Email']");
            if (!isValidEmail(emailInput.value)) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationInvalidEmailMessage']").value;
                invalidateElement(emailInput);
                showElement(errorMessageContainer);
                return false;
            }

            var passwordFields = form.querySelectorAll("[type='password']");

            if (passwordFields[0].value !== passwordFields[1].value) {
                errorMessageContainer.innerText = formContainer.querySelector("input[name='ValidationMismatchMessage']").value;
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

        var redirect = function (redirectUrl) {
            window.location = redirectUrl;
        };
    });
})();
