function init(event) {
    if (event && event.type === "widgetLoaded") {
        var container = event.detail.element.querySelector("[data-sf-role='form-container']");
        if (container) {
            initFields(container);
        }
    } else {
        document.querySelectorAll("[data-sf-role='form-container']").forEach(function (formContainer) {
            initFields(formContainer);
        });
    }
}

function initFields(formContainer) {
    initTextbox(formContainer);
    initParagraph(formContainer);
    initSubmit(formContainer);
    initMultipleChoice(formContainer);
    initCheckboxes(formContainer)
    initDropdown(formContainer)
    initFileField(formContainer);
    formHiddenFieldsInitialization(formContainer);
    processFormRules(formContainer, true);
}

document.removeEventListener("widgetLoaded", init);
document.addEventListener("widgetLoaded", init);

var delayTimer;
function dispatchValueChanged(e) {
    clearTimeout(delayTimer);
    delayTimer = setTimeout(function () {
        if (typeof sfFormValueChanged === 'function') {
            sfFormValueChanged(e.srcElement);
        }
    }, 300);
}

var shouldScrollToFirstErrorField = false;

function initTextbox(formContainer) {
    initTextField("text-field-container", "text-field-input", formContainer);
}

function initParagraph(formContainer) {
    initTextField("paragraph-text-field-container", "paragraph-text-field-textarea", formContainer)
}

function initMultipleChoice(formContainer) {
    initChoiceField("multiple-choice-field-container", "multiple-choice-field-input", formContainer);
}

function initCheckboxes(formContainer) {
    initChoiceField("checkboxes-field-container", "checkboxes-field-input", formContainer);
}

function initSubmit(formContainer) {
    var successMessage = formContainer.querySelector('[data-sf-role="success-message"]');
    var errorMessage = formContainer.querySelector('[data-sf-role="error-message"]');
    var loadingSpinner = formContainer.querySelector('[data-sf-role="loading"]');
    var fieldsContainer = formContainer.querySelector('[data-sf-role="fields-container"]');
    var customSubmitAction = formContainer.querySelector('[data-sf-role="custom-submit-action"]')?.value;
    var redirectUrl = formContainer.querySelector('[data-sf-role="redirect-url"]')?.value;
    var skipDataSubmission = formContainer.querySelector('[data-sf-role="skip-data-submission"]');
    var form = formContainer.closest("form");
    var submitButton = formContainer.querySelectorAll('[data-sf-role="submit-button-container"] button');

    function showSuccessMessage() {
        loadingSpinner.classList.add("hide");
        loadingSpinner.classList.remove("visible");
        successMessage.classList.add("visible");
    }

    function showErrorMessage(message) {
        errorMessage.innerText = message;
        errorMessage.classList.add("visible");

        loadingSpinner.classList.add("hide");
        loadingSpinner.classList.remove("visible");

        fieldsContainer.classList.remove("hide");
        fieldsContainer.classList.add("visible");
    }

    function triggerLoading() {
        errorMessage.classList.remove("visible");

        loadingSpinner.classList.remove("hide");
        loadingSpinner.classList.add("visible");

        successMessage.classList.add("visible");

        fieldsContainer.classList.add("hide");
        fieldsContainer.classList.remove("visible");
    }

    function handleResponse(redirectUrl, successMessageVal) {
        if (redirectUrl) {
            document.location.replace(redirectUrl);
        } else {
            if (successMessageVal)
                successMessage.innerText = successMessageVal;

            showSuccessMessage();
        }
    }

    submitButton.forEach(button => button.addEventListener("click", function (e) {
        shouldScrollToFirstErrorField = true;
    }));


    form.onsubmit = function (e) {
        e?.preventDefault();

        var formFields = e.target.querySelectorAll('[data-sf-role*="field-container"');
        var isValid = true;
        Array.from(formFields).filter(x => !x.classList.contains("hide")).forEach(formField => {
            var fieldType = formField.getAttribute("data-sf-role");
            var localResult = true;
            switch (fieldType) {
                case "file-field-container":
                    localResult = handleFileValidation(formField);
                    break;
                case "text-field-container":
                case "paragraph-text-field-container":
                    localResult = handleTextValidation(formField);
                    break;
                case "multiple-choice-field-container":
                    localResult = handleChoiceValidation(formField, "multiple-choice-field-input");
                    break;
                case "checkboxes-field-container":
                    localResult = handleChoiceValidation(formField, "checkboxes-field-input");
                    break;
                case "dropdown-list-field-container":
                    localResult = handleDropdownValidation(formField);
                    break;
                default:
                    break;
            }

            isValid = isValid && localResult;
        });

        if (!isValid) {
            return false;
        }

        triggerLoading();

        if (skipDataSubmission) {
            handleResponse(redirectUrl);
            return false;
        }

        var headerName = "X-SF-ANTIFORGERY-REQUEST";
        var headers = {};
        headers[headerName] = "";

        fetch("/sitefinity/anticsrf", { headers: headers }).then(function (csrfResponse) {
            function sendSubmitRequest(headerValue) {
                if (headerValue) {
                    headers[headerName] = headerValue;
                }

                var formData = new FormData(form);
                formData.set("sf_antiforgery", headerValue);

                fetch(form.getAttribute('action'), { method: "POST", body: formData }).then(function (formSubmitResponse) {
                    formSubmitResponse.json().then(function (jsonFormSubmitResponse) {
                        // Successfull request statuses
                        if (formSubmitResponse.status >= 200 && formSubmitResponse.status < 300) {
                            if (jsonFormSubmitResponse.success) {
                                if (customSubmitAction === "True") {
                                    handleResponse(redirectUrl);
                                } else {
                                    handleResponse(jsonFormSubmitResponse.redirectUrl, jsonFormSubmitResponse.message);
                                }
                            }
                            else {
                                showErrorMessage(jsonFormSubmitResponse.error);
                            }
                        }
                        // Client and Server error request statuses
                        else if (formSubmitResponse.status >= 400 && formSubmitResponse.status < 600) {
                            showErrorMessage(jsonFormSubmitResponse.error);
                        }
                    }, function (error) {
                        showErrorMessage("Form submit response was not in json format and could not be parsed");
                    });
                }, function (error) {
                    showErrorMessage(JSON.stringify(error));
                });
            }
            if (csrfResponse.status === 200) {
                csrfResponse.json().then(function (jsonCsrfResponse) {
                    sendSubmitRequest(jsonCsrfResponse.Value);
                });
            } else if (csrfResponse.status === 204) {
                sendSubmitRequest();
            } else {
                showErrorMessage("Failed to submit form due to lack of csrf token");
            }
        });

        return false;
    };
}

function initChoiceField(dataSfRoleContainer, dataSfRoleInput, formContainer) {
    var choiceFields = formContainer.querySelectorAll(`[data-sf-role='${dataSfRoleContainer}']`);
    choiceFields.forEach((choiceField) => {
        var inputs = choiceField.querySelectorAll(`[data-sf-role='${dataSfRoleInput}']`);

        inputs.forEach(function (input) {
            handleRequiredChoice(choiceField, inputs);
            input.addEventListener('change', function () {
                inputs.forEach(function (input) {
                    clearErrorMessage(input);
                });

                toggleOtherChoiceInputVisibility(choiceField);
                handleRequiredChoice(choiceField, inputs);
            });
            input.addEventListener('input', onInput);
        });

        addOtherChoiceInputsEventListener(choiceField);
    });
}

function toggleOtherChoiceInputVisibility(choiceField) {
    var otherChoice = choiceField.querySelector(("[id*='choiceOption-other']"));
    var otherChoiceInput = choiceField.querySelector("[data-sf-role='choice-other-input']");

    if (otherChoiceInput) {
        clearErrorMessage(otherChoiceInput);
        var isOtherChoiceChecked = otherChoice.checked;
        if (!isOtherChoiceChecked) {
            otherChoiceInput.required = false;
            otherChoiceInput.classList.add("hide");
            otherChoiceInput.classList.remove("visible");
        } else {
            var isRequired = otherChoice.required || choiceField.querySelector('[data-sf-role="required-validator"]')?.value === 'True';
            otherChoiceInput.required = isRequired;
            otherChoiceInput.classList.remove("hide");
            otherChoiceInput.classList.add("visible");
        }
    }
}

function addOtherChoiceInputsEventListener(choiceField) {
    var otherChoice = choiceField.querySelector(("[id*='choiceOption-other']"));
    var otherChoiceInput = choiceField.querySelector("[data-sf-role='choice-other-input']");

    if (otherChoiceInput) {
        otherChoiceInput.addEventListener('input', function () {
            otherChoice.setAttribute('value', otherChoiceInput.value);
            clearErrorMessage(otherChoiceInput);
        });
    }
}

function handleRequiredChoice(choiceField, inputs) {
    var requiredValidator = choiceField.querySelector('[data-sf-role="required-validator"]');
    if (requiredValidator) {
        var isRequired = requiredValidator.value === 'True';
        var hasChecked = Array.from(inputs).find(x => x.checked);

        if (hasChecked || !isRequired) {
            inputs.forEach((input) => {
                input.removeAttribute('required');
            });
        }
        else {
            inputs.forEach((input) => {
                input.setAttribute('required', 'required');
            });
        }
    }
}

function initDropdown(formContainer) {
    var dropdownFields = formContainer.querySelectorAll('[data-sf-role="dropdown-list-field-container"]');
    dropdownFields.forEach((dropdownField) => {
        var select = dropdownField.querySelector(`[data-sf-role="dropdown-list-field-select"]`);
        select.addEventListener('change', handleDropdownValidation);
        select.addEventListener('input', onInput);
    });
}

function initFileField(formContainer) {
    var containers = formContainer.querySelectorAll(`[data-sf-role="file-field-container"]`);

    if (!containers || containers.length < 1)
        return;

    function adjustVisibility(container) {
        var allRemoveLinks = container.querySelectorAll('[data-sf-role="remove-input"]');
        var show = false;
        if (allRemoveLinks.length > 1) {
            show = true;
        }

        for (let i = 0; i < allRemoveLinks.length; i++) {
            const removeLink = allRemoveLinks[i];
            removeLink.classList.toggle("hide", !show);
            removeLink.classList.toggle("d-inline-block", show);
        }
    };

    function initInput(inputTemplate, container) {
        var fileFieldInputsContainer = container.querySelector('[data-sf-role="file-field-inputs"]');

        const tempHolderElement = document.createElement("div");
        tempHolderElement.innerHTML = inputTemplate;

        const inputContainer = tempHolderElement.firstElementChild;

        fileFieldInputsContainer.appendChild(inputContainer);
        adjustVisibility(fileFieldInputsContainer);
        const inputElement = inputContainer.querySelector('input[type="file"]');

        const removeElement = inputContainer.querySelector('[data-sf-role="remove-input"]');
        if (removeElement) {
            removeElement.addEventListener('click', function (e) {
                e.preventDefault();
                inputElement.removeEventListener("change", handleFileValidation);
                inputElement.removeEventListener("input", onInput);
                inputElement.removeEventListener("invalid", handleFileValidation);
                inputContainer.remove();
                adjustVisibility(fileFieldInputsContainer);
            });
        }

        inputElement.addEventListener('change', handleFileValidation);
        inputElement.addEventListener('input', onInput);
        inputElement.addEventListener('invalid', handleFileValidation);
    }

    function attachAddEvent(inputTemplate, container) {
        var violationRestrictions = JSON.parse(container.querySelector('[data-sf-role="violation-restrictions"]').value);
        if (violationRestrictions.allowMultiple) {
            container.querySelector('[data-sf-role="add-input"]').addEventListener('click', function (e) {
                e.preventDefault();
                initInput(inputTemplate, container);
            });
        }
    }

    for (var i = 0; i < containers.length; i++) {
        var container = containers[i];
        var inputTemplate = container.querySelector('[data-sf-role="file-input-template"]').innerHTML;

        initInput(inputTemplate, container);
        attachAddEvent(inputTemplate, container);
    }
}

function initTextField(dataSfRoleContainer, dataSfRoleText, formContainer) {
    var inputs = getInputs(dataSfRoleContainer, dataSfRoleText, formContainer);

    for (var k = 0; k < inputs.length; k++) {
        inputs[k].addEventListener('change', handleTextValidation);
        inputs[k].addEventListener('input', function (e) {
            handleTextValidation(e);
            dispatchValueChanged(e);
        });
        inputs[k].addEventListener('invalid', handleTextValidation);
    }
}

function getInputs(containerName, inputsName, parentContainer) {
    var containers = parentContainer.querySelectorAll(`[data-sf-role="${containerName}"]`);
    var inputs = [];

    if (containers && containers.length > 0) {
        for (var i = 0; i < containers.length; i++) {
            var currentInputs = containers[i].querySelectorAll(`[data-sf-role="${inputsName}"]`);

            if (!currentInputs || currentInputs.length < 1)
                continue;

            inputs.push(...currentInputs);
        }
    }

    return inputs;
}

function onInput(e) {
    clearErrorMessage(e.target);
    dispatchValueChanged(e);
}

function handleChoiceValidation(element, dataSfRoleInput) {
    var parentContainer = findFieldContainerElement(element);
    var inputs = parentContainer.querySelectorAll(`[data-sf-role='${dataSfRoleInput}']`);
    var otherChoiceInput = parentContainer.querySelector("[data-sf-role='choice-other-input']");

    var isChecked = false;
    inputs.forEach(function (input) {
        isChecked = isChecked || input.checked;
    });

    inputs.forEach(function (input) {
        clearErrorMessage(input);
    });

    var validationMessages = getValidationMessages(inputs[0]);

    if (!isChecked && inputs[0].required) {
        setErrorMessage(inputs[0], validationMessages.required, false, false);

        return false;
    }

    if (otherChoiceInput)
        clearErrorMessage(otherChoiceInput);

    if (otherChoiceInput && otherChoiceInput.required && otherChoiceInput.validity.valueMissing) {
        setErrorMessage(otherChoiceInput, validationMessages.required);
        return false;
    }

    return true;
}

function handleTextValidation(source) {
    if (source instanceof Event) {
        source = source.target;
    }

    var parentContainer = findFieldContainerElement(source);
    var target = parentContainer.querySelector("input[data-sf-role='text-field-input']") || parentContainer.querySelector("textarea[data-sf-role='paragraph-text-field-textarea']");

    var validationMessages = getValidationMessages(target);
    var validationRestrictions = getValidationRestrictions(target);
    if (validationRestrictions) {
        var isValidLength = target.value.length >= validationRestrictions.minLength;

        if (validationRestrictions.maxLength > 0)
            isValidLength &= target.value.length <= validationRestrictions.maxLength;

        isValidLength = isValidLength || target.value.length === 0;
        if (!isValidLength) {
            setErrorMessage(target, validationMessages.invalidLength, true);
            return false;
        }
    }

    if (target.required && target.validity.valueMissing) {
        setErrorMessage(target, validationMessages.required, true);
        return false;
    }
    else if (target.validity.patternMismatch) {
        setErrorMessage(target, validationMessages.regularExpression, true);
        return false;
    }
    else if (!target.validity.valid) {
        setErrorMessage(target, validationMessages.invalid, true);
        return false;
    } else {
        clearErrorMessage(target, true);
    }

    return true;
}

function handleFileValidation(source) {
    if (source instanceof Event) {
        source = source.target;
    }

    var parentContainer = findFieldContainerElement(source);
    var fileInputs = parentContainer.querySelectorAll('input[type="file"]');

    var validationRestrictions = getValidationRestrictions(source);
    if (validationRestrictions.required) {
        var violationMessageContainer = parentContainer.querySelector('[data-sf-role="required-violation-message"]');
        var validInput = null;
        for (var i = 0; i < fileInputs.length; i++) {
            var fileSizeViolationMessageContainer = fileInputs[i].closest('[data-sf-role="single-file-input-wrapper"]').querySelector('[data-sf-role="filesize-violation-message"]');
            var fileTypeViolationMessageContainer = fileInputs[i].closest('[data-sf-role="single-file-input-wrapper"]').querySelector('[data-sf-role="filetype-violation-message"]');
            if (fileSizeViolationMessageContainer)
                fileSizeViolationMessageContainer.classList.remove("visible");

            if (fileTypeViolationMessageContainer)
                fileTypeViolationMessageContainer.classList.remove("visible");

            violationMessageContainer.classList.remove("visible");
            fileInputs[i].classList.remove("is-invalid");

            if (fileInputs[i].value) {
                validInput = true;
            }
        }

        if (!validInput) {
            for (var i = 0; i < fileInputs.length; i++) {
                fileInputs[i].classList.add("is-invalid");
            }

            violationMessageContainer.classList.add("visible");
            return false;
        }
    }

    if (validationRestrictions.maxSize || validationRestrictions.minSize) {
        if (typeof window.File == 'undefined' || typeof window.FileList == 'undefined')
            return true;

        var hasViolations = false;
        var minSize = validationRestrictions.minSize * 1000 * 1000;
        var maxSize = validationRestrictions.maxSize * 1000 * 1000;
        for (var i = 0; i < fileInputs.length; i++) {
            if (fileInputs[i].files.length > 0) {
                var file = fileInputs[i].files[0];
                var fileSizeViolationMessageContainer = fileInputs[i].closest('[data-sf-role="single-file-input-wrapper"]').querySelector('[data-sf-role="filesize-violation-message"]');
                if ((minSize > 0 && file.size < minSize) || (maxSize > 0 && file.size > maxSize)) {
                    fileSizeViolationMessageContainer.classList.add("visible");
                    hasViolations = true;
                    fileInputs[i].focus();
                    continue;
                } else {
                    fileSizeViolationMessageContainer.classList.remove("visible");
                }
            }
        }

        if (hasViolations)
            return false;
    }

    if (validationRestrictions.allowedFileTypes) {
        var hasViolations = false;
        for (var i = 0; i < fileInputs.length; i++) {
            var violationMessage = fileInputs[i].closest('[data-sf-role="single-file-input-wrapper"]').querySelector('[data-sf-role="filetype-violation-message"]');
            if (fileInputs[i].value) {
                var stopIndex = fileInputs[i].value.lastIndexOf('.');
                if (stopIndex >= 0) {
                    var extension = fileInputs[i].value.substring(stopIndex).toLowerCase();
                    if (validationRestrictions.allowedFileTypes.indexOf(extension) < 0) {
                        violationMessage.classList.add("visible");
                        hasViolations = true;
                        fileInputs[i].focus();
                        continue;
                    } else {
                        violationMessage.classList.remove("visible");
                    }
                }
            }
        }

        if (hasViolations)
            return false;
    }

    return true;
}

function handleDropdownValidation(source) {
    if (source instanceof Event) {
        source = source.target;
    }

    var parentContainer = findFieldContainerElement(source);
    var target = parentContainer.querySelector("select[data-sf-role='dropdown-list-field-select']");

    if (target.required && target.value === '') {
        var validationMessages = getValidationMessages(target);
        setErrorMessage(target, validationMessages.required, true);
        return false;
    }
    else {
        clearErrorMessage(target, true);
    }

    return true;
}

function getValidationMessages(input) {
    return getHiddenFieldValue(input, "violation-messages");
}

function getValidationRestrictions(input) {
    return getHiddenFieldValue(input, "violation-restrictions");
}

function getHiddenFieldValue(input, attrVal) {
    var hiddenField = getHiddenField(input, attrVal);
    if (hiddenField) {
        return JSON.parse(hiddenField.value);
    }

    return null;
}

function setErrorMessage(input, message, isErrorContainerSibling = false, setInputInvalidClass = true) {
    var errorMessagesContainer = getErrorMessageContainer(input);

    if (errorMessagesContainer) {
        errorMessagesContainer.innerText = message;

        if (setInputInvalidClass) {
            input.classList.add("is-invalid");
        }

        if (!isErrorContainerSibling) {
            errorMessagesContainer.classList.add("visible");
        }
    } else {
        input.setCustomValidity(message);
    }

    if (shouldScrollToFirstErrorField) {
        shouldScrollToFirstErrorField = false;
        input.parentNode.scrollIntoView();
    }
}

function clearErrorMessage(input, isErrorContainerSibling) {
    var errorMessagesContainer = getErrorMessageContainer(input);

    if (errorMessagesContainer) {
        errorMessagesContainer.innerText = "";
        input.classList.remove("is-invalid");
        if (!isErrorContainerSibling) {
            errorMessagesContainer.classList.remove("visible");
        }
    }
}

function findFieldContainerElement(element) {
    var container = element;
    while (true) {
        if (container.hasAttribute('data-sf-role')) {
            const attributeValue = container.getAttribute('data-sf-role');
            if (attributeValue.indexOf("field-container") !== -1) {
                break;
            }
        }

        container = container.parentElement;
    }

    return container;
}

function getErrorMessageContainer(input) {
    return getHiddenField(input, "error-message");
}

function getHiddenField(input, attrVal) {
    var container = findFieldContainerElement(input);
    if (container) {
        var field = container.querySelector(`[data-sf-role="${attrVal}"]`);
        if (field) {
            return field;
        }
    }

    return null;
}

// ------------------ Define form rule classes - START ----------------------

var FormRulesSettings = (function () {
    // private members
    var ConditionEvaluators = [];
    var InputTypeParsers = [];
    var RuleValueParsers = [];
    var FieldSelectors = [];
    var ActionExecutors = [];

    function ConditionEvaluator(name, conditionEvaluator) {
        this.name = name;
        this.conditionEvaluator = conditionEvaluator;
    }

    ConditionEvaluator.prototype.canProcess = function (name) {
        return name === this.name;
    };

    ConditionEvaluator.prototype.process = function (currentValue, ruleValue, inputType) {
        if (!InputTypeParsers || InputTypeParsers.length === 0) return false;

        var inputTypeParser;
        for (var inputValueIndex = 0; inputValueIndex < InputTypeParsers.length; inputValueIndex++) {
            if (inputValueIndex === 0 || InputTypeParsers[inputValueIndex].canParse(inputType)) {
                inputTypeParser = InputTypeParsers[inputValueIndex];
            }
        }

        var ruleValueParsers;
        for (var ruleValueIndex = 0; ruleValueIndex < RuleValueParsers.length; ruleValueIndex++) {
            if (ruleValueIndex === 0 || RuleValueParsers[ruleValueIndex].canParse(inputType)) {
                ruleValueParsers = RuleValueParsers[ruleValueIndex];
            }
        }

        var parsedCurrentValue = inputTypeParser ? inputTypeParser.parse(currentValue) : currentValue;
        var parsedRuleValue = ruleValueParsers ? ruleValueParsers.parse(ruleValue) : ruleValue;

        return this.conditionEvaluator(parsedCurrentValue, parsedRuleValue);
    };

    function ValueParser(inputType, parser, escape, escapeRegEx) {
        this.inputType = inputType;
        this.parser = parser;
        this.escape = escape;
        this.escapeRegEx = escapeRegEx ? escapeRegEx : /[\-\[\]{}()*+?.,\\\^$|#\s]/g;
    }

    ValueParser.prototype.canParse = function (inputType) {
        return this.inputType === inputType;
    };

    ValueParser.prototype.parse = function (value) {
        var parsedValue = this.parser(value);
        if (this.escape === true && typeof parsedValue === "string")
            return parsedValue.replace(this.escapeRegEx, '\\$&');

        return parsedValue;
    };

    function FieldSelector(fieldContainerDataSfRole, elementSelector, additionalFilter) {
        this.fieldContainerDataSfRole = fieldContainerDataSfRole;
        this.elementSelector = elementSelector;
        this.additionalFilter = additionalFilter;
    }

    FieldSelector.prototype.getFieldContainerDataSfRole = function () {
        return this.fieldContainerDataSfRole;
    };

    FieldSelector.prototype.getFieldValues = function (fieldContainer) {
        var selector = this.elementSelector;
        if (this.additionalFilter)
            selector += this.additionalFilter;

        var elements = fieldContainer.querySelectorAll(selector);
        var fieldValues = [];
        elements.forEach((element) => {
            var value = element?.value?.replace(/^\s+|\s+$/g, "");
            fieldValues.push(value);
        });

        return fieldValues;
    };

    FieldSelector.prototype.getFieldValueElements = function (fieldContainer) {
        return fieldContainer.querySelector(this.elementSelector);
    };

    FieldSelector.prototype.canGetValues = function (fieldContainer) {
        return this.fieldContainerDataSfRole === fieldContainer.getAttribute("data-sf-role");
    };

    // public members
    return {
        addConditionEvaluator: function (name, conditionEvaluator) {
            ConditionEvaluators.push(new ConditionEvaluator(name, conditionEvaluator));
        },
        removeConditionEvaluator: function (name) {
            for (var i = 0; i < ConditionEvaluators.length; i++) {
                if (ConditionEvaluators[i].name === name) {
                    ConditionEvaluators.splice(i, 1);
                    break;
                }
            }
        },
        processConditionEvaluator: function (name, inputType, currentValue, ruleValue) {
            for (var i = 0; i < ConditionEvaluators.length; i++) {
                if (ConditionEvaluators[i].canProcess(name)) {
                    return ConditionEvaluators[i].process(currentValue, ruleValue, inputType);
                }
            }

            return false;
        },
        getConditionEvaluator: function (name) {
            for (var i = 0; i < ConditionEvaluators.length; i++) {
                if (ConditionEvaluators[i].canProcess(name)) {
                    return ConditionEvaluators[i];
                }
            }

            return null;
        },

        addActionExecutor: function (actionName, actionExecutor) {
            ActionExecutors.push({ actionName: actionName, actionExecutor: actionExecutor });
        },
        removeActionExecutor: function (actionName) {
            ActionExecutors = ActionExecutors.filter(function (a) { return a.actionName !== actionName; });
        },
        getActionExecutor: function (actionName) {
            var entry = ActionExecutors.filter(function (a) { return a.actionName === actionName; })[0];
            if (entry) {
                return entry.actionExecutor;
            }

            return null;
        },

        addInputTypeParser: function (inputType, parser, escape, escapeRegEx) {
            InputTypeParsers.push(new ValueParser(inputType, parser, escape, escapeRegEx));
        },
        removeInputTypeParser: function (inputType) {
            for (var i = 0; i < InputTypeParsers.length; i++) {
                if (InputTypeParsers[i].inputType === inputType) {
                    InputTypeParsers.splice(i, 1);
                    break;
                }
            }
        },
        addRuleValueParser: function (inputType, parser, escape, escapeRegEx) {
            RuleValueParsers.push(new ValueParser(inputType, parser, escape, escapeRegEx));
        },
        removeRuleValueParser: function (inputType) {
            for (var i = 0; i < RuleValueParsers.length; i++) {
                if (RuleValueParsers[i].inputType === inputType) {
                    RuleValueParsers.splice(i, 1);
                    break;
                }
            }
        },
        addFieldSelector: function (fieldContainerDataSfRole, elementSelector, additionalFilter) {
            var element = FieldSelectors.map(function (e) { return e.fieldContainerDataSfRole; }).indexOf(fieldContainerDataSfRole);
            if (element > -1)
                throw "Container with attribute [data-sf-role='" + fieldContainerDataSfRole + "'] have been registered already.";
            else
                FieldSelectors.push(new FieldSelector(fieldContainerDataSfRole, elementSelector, additionalFilter));
        },
        removeFieldSelector: function (fieldContainerDataSfRole) {
            for (var i = 0; i < FieldSelectors.length; i++) {
                if (FieldSelectors[i].fieldContainerDataSfRole === fieldContainerDataSfRole) {
                    FieldSelectors.splice(i, 1);
                    break;
                }
            }
        },
        getFieldValues: function (fieldContainer) {
            for (var i = 0; i < FieldSelectors.length; i++) {
                if (FieldSelectors[i].canGetValues(fieldContainer)) {
                    return FieldSelectors[i].getFieldValues(fieldContainer);
                }
            }

            return [];
        },
        getFieldValueElements: function (fieldContainer) {
            for (var i = 0; i < FieldSelectors.length; i++) {
                if (FieldSelectors[i].canGetValues(fieldContainer)) {
                    return FieldSelectors[i].getFieldValueElements(fieldContainer);
                }
            }

            return null;
        },
        getFieldsContainerNames: function () {
            var containers = [];
            for (var i = 0; i < FieldSelectors.length; i++) {
                containers.push(FieldSelectors[i].getFieldContainerDataSfRole());
            }

            return containers;
        }
    };
})();

var FormRuleConstants = {
    Actions: {
        Show: "Show",
        Hide: "Hide",
        Skip: "Skip",
        ShowMessage: "ShowMessage",
        GoTo: "GoTo",
        SendNotification: "SendNotification"
    }
};

function FormRuleActionExecutorBase() { }

FormRuleActionExecutorBase.prototype.applyState = function (context, actionData) {
    throw new Error('applyState() function not implemented');
};

FormRuleActionExecutorBase.prototype.updateState = function (context, actionData) {
    throw new Error('updateState() function not implemented');
};

FormRuleActionExecutorBase.prototype.undoUpdateState = function (context, actionData) {
    throw new Error('undoUpdateState() function not implemented');
};

FormRuleActionExecutorBase.prototype.isConflict = function (actionData, otherActionData) {
    return false;
};

FormRuleActionExecutorBase.prototype.getActionFieldIds = function (actionData) {
    return [];
};

function HideShowFieldFormRuleActionExecutor(actionName) {
    FormRuleActionExecutorBase.call(this);
    if (actionName === FormRuleConstants.Actions.Show || actionName === FormRuleConstants.Actions.Hide) {
        this.actionName = actionName;
    } else {
        throw new Error("Invalid action name! Only " + FormRuleConstants.Actions.Show + " and " + FormRuleConstants.Actions.Hide + " action names are allowed");
    }
}

HideShowFieldFormRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
HideShowFieldFormRuleActionExecutor.prototype.constructor = HideShowFieldFormRuleActionExecutor;

HideShowFieldFormRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var fieldIndex = context.helper.fieldIndexOf(context.fields, actionData.target);
    var fieldControlId = context.fields[fieldIndex].FieldControlId;
    if (context.fields[fieldIndex].Visible) {
        context.helper.showField(context, fieldControlId);
    } else {
        context.helper.hideField(context, fieldControlId);
    }
};

HideShowFieldFormRuleActionExecutor.prototype.updateState = function (context, actionData) {
    var updated = false;
    var fieldIndex = context.helper.fieldIndexOf(context.fields, actionData.target);
    if (this.actionName === FormRuleConstants.Actions.Show && !context.fields[fieldIndex].Visible) {
        context.fields[fieldIndex].Visible = true;
        updated = true;
    } else if (this.actionName === FormRuleConstants.Actions.Hide && context.fields[fieldIndex].Visible) {
        context.fields[fieldIndex].Visible = false;
        updated = true;
    }

    return updated;
};

HideShowFieldFormRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    var fieldIndex = context.helper.fieldIndexOf(context.fields, actionData.target);
    if (this.actionName === FormRuleConstants.Actions.Show) {
        context.fields[fieldIndex].Visible = false;
    } else {
        context.fields[fieldIndex].Visible = true;
    }
};

HideShowFieldFormRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return (otherActionData.name === FormRuleConstants.Actions.Show || otherActionData.name === FormRuleConstants.Actions.Hide) && actionData.target === otherActionData.target;
};

HideShowFieldFormRuleActionExecutor.prototype.getActionFieldIds = function (actionData) {
    return [actionData.target];
};

function SkipToPageFormRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

SkipToPageFormRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
SkipToPageFormRuleActionExecutor.prototype.constructor = SkipToPageFormRuleActionExecutor;

SkipToPageFormRuleActionExecutor.prototype.applyState = function (context, actionData) {
    if (context.skipToPageCollection) {
        context.formContainer.trigger("form-page-skip", [context.skipToPageCollection]);
    }
};

SkipToPageFormRuleActionExecutor.prototype.updateState = function (context, actionData) {
    if (!context.skipToPageCollection) {
        context.skipToPageCollection = [];
    }

    if (actionData.pageIndex < parseInt(actionData.target)) {
        context.skipToPageCollection.push({ SkipFromPage: actionData.pageIndex, SkipToPage: parseInt(actionData.target) });
        return true;
    }

    return false;
};

SkipToPageFormRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    if (context.skipToPageCollection) {
        context.skipToPageCollection = context.skipToPageCollection.filter(function (p) { return p.SkipFromPage !== actionData.pageIndex || p.SkipToPage !== parseInt(actionData.target); });
    }
};

SkipToPageFormRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return actionData.name === otherActionData.name && actionData.pageIndex === otherActionData.pageIndex; // same action, same current page
};


function ShowMessageRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

ShowMessageRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
ShowMessageRuleActionExecutor.prototype.constructor = ShowMessageRuleActionExecutor;

ShowMessageRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var inputSelector = '[data-sf-role="form-rules-message"]';
    var inputElement = context.formContainer.querySelector(inputSelector);
    if (inputElement) {
        if (this.execute) {
            inputElement.value = actionData.target;
        } else {
            var currentValue = inputElement.value;
            if (currentValue === actionData.target) {
                inputElement.value = "";
            }
        }
    }
};

ShowMessageRuleActionExecutor.prototype.updateState = function (context, actionData) {
    this.execute = true;
    return true;
};

ShowMessageRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    this.execute = false;
};

ShowMessageRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return otherActionData.name === FormRuleConstants.Actions.ShowMessage || otherActionData.name === FormRuleConstants.Actions.GoTo;
};


function GoToPageRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

GoToPageRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
GoToPageRuleActionExecutor.prototype.constructor = GoToPageRuleActionExecutor;

GoToPageRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var inputSelector = '[data-sf-role="form-rules-redirect-page"]';
    var inputElement = context.formContainer.querySelector(inputSelector);
    if (inputElement) {
        if (this.execute) {
            inputElement.value = actionData.target;
        } else {
            var currentValue = inputElement.value;
            if (currentValue === actionData.target) {
                inputElement.value = "";
            }
        }
    }
};

GoToPageRuleActionExecutor.prototype.updateState = function (context, actionData) {
    this.execute = true;
    return true;
};

GoToPageRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    this.execute = false;
};

GoToPageRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return otherActionData.name === FormRuleConstants.Actions.ShowMessage || otherActionData.name === FormRuleConstants.Actions.GoTo;
};


function SendNotificationRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

SendNotificationRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
SendNotificationRuleActionExecutor.prototype.constructor = SendNotificationRuleActionExecutor;

SendNotificationRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var inputSelector = '[data-sf-role="form-rules-notification-emails"]';
    var inputElement = context.formContainer.querySelector(inputSelector);
    if (inputElement) {
        if (context.notificationEmails) {
            inputElement.value = context.notificationEmails.join(",");
        } else {
            inputElement.value = "";
        }
    }
};

SendNotificationRuleActionExecutor.prototype.updateState = function (context, actionData) {
    if (!context.notificationEmails) {
        context.notificationEmails = [];
    }

    if (context.notificationEmails.indexOf(actionData.target) === -1) {
        context.notificationEmails.push(actionData.target);
    }

    return true;
};

SendNotificationRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    if (context.notificationEmails) {
        var index = context.notificationEmails.indexOf(actionData.target);
        if (index !== -1) {
            context.notificationEmails.splice(index, 0);
        }
    }
};

SendNotificationRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return actionData.name === otherActionData.name && actionData.target === otherActionData.target;
};


// ------------------ Define form rule classes - END ----------------------

// ------------------ Add rule objects to FormRuleSettings - START ----------------------

(function addConditionEvaluators() {
    FormRulesSettings.addConditionEvaluator('Equal', function (currentValue, ruleValue) {
        if (typeof currentValue === "string") {
            return currentValue.search(new RegExp("^" + ruleValue + "$", "i")) === 0;
        }

        return currentValue === ruleValue;
    });
    FormRulesSettings.addConditionEvaluator('NotEqual', function (currentValue, ruleValue) {
        if (typeof currentValue === "string") {
            return currentValue.search(new RegExp("^" + ruleValue + "$", "i")) === -1;
        }

        return currentValue !== ruleValue;
    });
    FormRulesSettings.addConditionEvaluator('Contains', function (currentValue, ruleValue) { return currentValue.search(new RegExp(ruleValue, "i")) > -1; });
    FormRulesSettings.addConditionEvaluator('NotContains', function (currentValue, ruleValue) { return currentValue.search(new RegExp(ruleValue, "i")) === -1; });

    var isFilledFunction = function (currentValue) {
        // Check if currentValue is NaN
        if (typeof currentValue === "number" && currentValue !== currentValue) {
            return false;
        }

        return currentValue && currentValue.toString().length > 0;
    };

    FormRulesSettings.addConditionEvaluator('IsFilled', isFilledFunction);
    FormRulesSettings.addConditionEvaluator('IsNotFilled', function (currentValue) { return !isFilledFunction(currentValue); });
    FormRulesSettings.addConditionEvaluator('FileSelected', function (currentValue) { return currentValue && currentValue.length > 0; });
    FormRulesSettings.addConditionEvaluator('FileNotSelected', function (currentValue) { return !currentValue || currentValue.length === 0; });
    FormRulesSettings.addConditionEvaluator('IsGreaterThan', function (currentValue, ruleValue) { return currentValue > ruleValue; });
    FormRulesSettings.addConditionEvaluator('IsLessThan', function (currentValue, ruleValue) { return currentValue < ruleValue; });
})();

(function addValueParsers() {
    var dateParserFunction = function (value) {
        var dateTime = new Date(value);
        var date = new Date(dateTime.getFullYear(), dateTime.getMonth(), dateTime.getDate());

        return date.getTime();
    };

    var monthParserFunction = function (value, timezoneOffset) {
        var dateTime = new Date(value);

        if (timezoneOffset) {
            dateTime.setTime(dateTime.getTime() + timezoneOffset);
        }

        var date = new Date(dateTime.getFullYear(), dateTime.getMonth());

        return date.getTime();
    };

    var weekParserFunction = function (value) {
        var date = new Date(parseInt(value.split(['-W'])[0]), 0, 1 + (parseInt(value.split(['-W'])[1]) - 1) * 7);
        date.setDate(date.getDate() - date.getDay() + (date.getDate() <= 4 ? 1 : 8));
        return date.getTime();
    };

    var dateTimeParserFunction = function (value, timezoneOffset) {
        var dateTime = new Date(value);
        var date = new Date(dateTime.getFullYear(), dateTime.getMonth(), dateTime.getDate(), dateTime.getHours(), dateTime.getMinutes());

        if (timezoneOffset) {
            date = new Date(date.getTime() + timezoneOffset);
        }

        return date.getTime();
    };

    var timeParserFunction = function (value, timezoneOffset) {
        var dateTime = new Date(value);

        if (timezoneOffset) {
            dateTime = new Date(dateTime.getTime() + timezoneOffset);
        }

        return dateTime.getHours() * 60 + dateTime.getMinutes();
    };

    FormRulesSettings.addInputTypeParser("text", function (value) { return value; });
    FormRulesSettings.addInputTypeParser("month", function (value) { return monthParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addInputTypeParser("number", function (value) { return parseFloat(value) && Number(value); });
    FormRulesSettings.addInputTypeParser("datetime-local", dateTimeParserFunction);
    FormRulesSettings.addInputTypeParser("date", dateParserFunction);
    FormRulesSettings.addInputTypeParser("time", function (value) { return parseInt(value.split([':'])[0]) * 60 + parseInt(value.split([':'])[1]); });
    FormRulesSettings.addInputTypeParser("week", weekParserFunction);

    FormRulesSettings.addRuleValueParser("text", function (value) { return value; }, true);
    FormRulesSettings.addRuleValueParser("month", function (value) { return monthParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addRuleValueParser("number", function (value) { return parseFloat(value) && Number(value); });
    FormRulesSettings.addRuleValueParser("datetime-local", function (value) { return dateTimeParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addRuleValueParser("date", dateParserFunction);
    FormRulesSettings.addRuleValueParser("time", function (value) { return timeParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addRuleValueParser("week", weekParserFunction);
})();

(function addFieldSelectors() {
    FormRulesSettings.addFieldSelector("text-field-container", "[data-sf-role='text-field-input']");
    FormRulesSettings.addFieldSelector("email-text-field-container", "[data-sf-role='email-text-field-input']");
    FormRulesSettings.addFieldSelector("multiple-choice-field-container", "[data-sf-role='multiple-choice-field-input']", ":checked");
    FormRulesSettings.addFieldSelector("checkboxes-field-container", "[data-sf-role='checkboxes-field-input']", ":checked");
    FormRulesSettings.addFieldSelector("paragraph-text-field-container", "[data-sf-role='paragraph-text-field-textarea']");
    FormRulesSettings.addFieldSelector("dropdown-list-field-container", "[data-sf-role='dropdown-list-field-select']");
    FormRulesSettings.addFieldSelector("file-field-container", "[data-sf-role='single-file-input'] input[type='file']");
})();

(function addActionExecutors() {
    var hideRuleAction = new HideShowFieldFormRuleActionExecutor(FormRuleConstants.Actions.Hide);
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.Hide, hideRuleAction);

    var showRuleAction = new HideShowFieldFormRuleActionExecutor(FormRuleConstants.Actions.Show);
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.Show, showRuleAction);

    var skipRuleActionExecutor = new SkipToPageFormRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.Skip, skipRuleActionExecutor);

    var showMessageRuleActionExecutor = new ShowMessageRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.ShowMessage, showMessageRuleActionExecutor);

    var goToPageRuleActionExecutor = new GoToPageRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.GoTo, goToPageRuleActionExecutor);

    var sendNotificationRuleActionExecutor = new SendNotificationRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.SendNotification, sendNotificationRuleActionExecutor);
})();

// ------------------ Add rule objects to FormRuleSettings - END ----------------------

// ------------------ Form rules execution - START ----------------------
(function () {
    'use strict';

    var formContainerMap = new Map();
    var FormRulesExecutor = function (container) {
        this._init(container);
    };

    FormRulesExecutor.prototype = {

        _init: function (target) {
            this.fieldContainerSelector = '[data-sf-role$="field-container"]';
            this.separatorSelector = '[data-sf-role="separator"]';
            this.formContainerSelector = getFormContainerSelector();
            this.skipFieldsSelector = 'input[type="hidden"][data-sf-role="form-rules-skip-fields"]';
            this.hiddenFieldsSelector = 'input[type="hidden"][data-sf-role="form-rules-hidden-fields"]';
            this.formContainer = target.closest(this.formContainerSelector);

            var separator = target.closest(this.separatorSelector);
            this.fieldsContainer = separator || this.formContainer;
            this.pages = Array.from(this.formContainer.querySelectorAll(this.separatorSelector));

            this._initializeFormRules();

            this.iterationsMaxCount = 50;

            var that = this;
            this.formContainer.addEventListener("form-page-changed", function (e, nextIndex, previousIndex) {
                that._updateSkipPages(previousIndex, nextIndex);
            });
        },

        process: function () {
            if (!this._hasRules()) return;

            this.hiddenFields = this._getHiddenFields();
            this.skipFields = this._getSkipFields();

            var context = this._contextInitialization();
            var updatedContext = this._evaluateFormRules(context);

            this._applyActionsState(updatedContext);

            this.hiddenFields = updatedContext.hiddenFields;
            this._setHiddenFields(this.hiddenFields);
            this._setExecutedActions(updatedContext.activeActions);
        },

        _hasRules: function () {
            return this.formRules && this.formRules.length !== 0;
        },

        _evaluateFormRules: function (context) {
            var currentFieldsVisibility = context.fields.map(function (field) { return field.Visible; });
            var updatedContext = this._updateContext(context);
            var updatedFieldsVisibility = updatedContext.fields.map(function (field) { return field.Visible; });

            var noChanges = this._compareArrays(currentFieldsVisibility, updatedFieldsVisibility);
            if (context.iterationsCounter > this.iterationsMaxCount || noChanges) {
                return updatedContext;
            }

            context.iterationsCounter++;

            return this._evaluateFormRules(updatedContext);
        },

        _updateContext: function (context) {
            var actions = this._getRulesActionsState(context);

            for (var i = 0; i < actions.length; i++) {
                var action = actions[i];
                var activeActionIndex = context.helper.actionItemIndexOf(context.activeActions, action.data);
                if (action.data.applyRule) {
                    var updated = action.executor.updateState(context, action.data);
                    if (activeActionIndex === -1) {
                        if (updated) {
                            context.activeActions.push(action);
                        }
                    }
                }
                else {
                    if (activeActionIndex > -1) {
                        action.executor.undoUpdateState(context, action.data);
                        context.activeActions.splice(activeActionIndex, 1);
                    }
                }
            }

            return context;
        },

        _getRulesActionsState: function (context) {
            var actions = [];
            var that = this;

            var addOrUpdateAction = function (actions, currentAction, applyRule) {
                var actionIndex = that._actionItemIndexOf(actions, currentAction.data);
                if (actionIndex === -1) {
                    if (applyRule) {
                        // if status of current conditions is set to be executed, iterate through previously added actions and change their execution status
                        for (var actionIndex = 0; actionIndex < actions.length; actionIndex++) {
                            if (actions[actionIndex].data.applyRule === true) {
                                actions[actionIndex].data.applyRule = !actions[actionIndex].executor.isConflict(actions[actionIndex].data, currentAction.data);
                            }
                        }
                    }

                    currentAction.data.applyRule = applyRule;
                    actions.push(currentAction);
                }
                else if (actions[actionIndex].data.applyRule === false) {
                    // for duplicated actions, we replace with current execution status only if we find that action is inactive
                    actions[actionIndex].data.applyRule = applyRule;
                }
            };

            for (var i = 0; i < this.formRules.length; i++) {
                // execute rule conditions and get execution status
                var rule = this.formRules[i];
                var applyRule = this._evaluateConditions(context, rule);

                for (var j = 0; j < rule.actions.length; j++) {
                    var action = rule.actions[j];

                    addOrUpdateAction(actions, action, applyRule);
                }
            }

            return actions;
        },

        _applyActionsState: function (context) {
            for (var actionIndex = 0; actionIndex < context.activeActions.length; actionIndex++) {
                var action = context.activeActions[actionIndex];
                action.executor.applyState(context, action.data);
            }

            var deactivatedActions = context.executedActions.filter(function (a) { return context.activeActions.indexOf(a) === -1; });
            for (var deactivatedActionIndex = 0; deactivatedActionIndex < deactivatedActions.length; deactivatedActionIndex++) {
                var deactivatedAction = deactivatedActions[deactivatedActionIndex];
                deactivatedAction.executor.applyState(context, deactivatedAction.data);
            }
        },

        _evaluateConditions: function (context, rule) {
            var executeStatus = [];
            for (var conditionIndex = 0; conditionIndex < rule.conditions.length; conditionIndex++) {
                var condition = rule.conditions[conditionIndex];
                var field = this._getContextField(context, condition.data.id);
                var fieldType = this._getFieldType(condition.data.id);

                var applyRule = false;
                if (field.Visible === true && condition.evaluator) {
                    if (field.Values && field.Values.length > 0) {
                        for (var h = 0; h < field.Values.length; h++) {
                            applyRule = condition.evaluator.process(field.Values[h], condition.data.value, fieldType);
                            if (applyRule) break;
                        }
                    } else {
                        applyRule = condition.evaluator.process(null, condition.data.value, fieldType);
                    }
                }

                executeStatus.push(applyRule);
            }

            var execute;
            if (rule.operator === "And") {
                execute = this._every(executeStatus, function (e) { return e; });
            } else {
                // Operator "Or"
                execute = this._some(executeStatus, function (e) { return e; });
            }

            return execute;
        },

        _initializeFormRules: function () {
            this.formRules = [];
            var formRulesElement = this.formContainer.querySelector('input[data-sf-role="form-rules"]');
            var deserializedFormRules = formRulesElement && formRulesElement.value.length > 0 ? JSON.parse(formRulesElement.value) : null;
            if (deserializedFormRules) {
                for (var ruleIndex = 0; ruleIndex < deserializedFormRules.length; ruleIndex++) {
                    var rule = deserializedFormRules[ruleIndex];
                    var newRule = {
                        operator: rule.Operator,
                        conditions: [],
                        actions: []
                    };

                    var rulePageIndex = 0;
                    for (var conditionIndex = 0; conditionIndex < rule.Conditions.length; conditionIndex++) {
                        var condition = rule.Conditions[conditionIndex];
                        var conditionEvaluator = FormRulesSettings.getConditionEvaluator(condition.Operator);
                        newRule.conditions.push({
                            data: {
                                id: condition.Id,
                                value: condition.Value
                            },
                            evaluator: conditionEvaluator
                        });

                        var conditionTargetPageIndex = this._getFieldPageContainerIndex(condition.Id);
                        rulePageIndex = conditionTargetPageIndex && conditionTargetPageIndex > rulePageIndex ? conditionTargetPageIndex : rulePageIndex;
                    }

                    for (var actionIndex = 0; actionIndex < rule.Actions.length; actionIndex++) {
                        var action = rule.Actions[actionIndex];
                        var actionExecutor = FormRulesSettings.getActionExecutor(action.Action);
                        if (actionExecutor) {
                            newRule.actions.push({
                                data: {
                                    target: action.Target,
                                    name: action.Action,
                                    pageIndex: rulePageIndex
                                },
                                executor: actionExecutor
                            });
                        }
                    }

                    newRule.actions = this._filterConflictingRuleActions(newRule.actions);

                    this.formRules.push(newRule);
                }
            }
        },

        _filterConflictingRuleActions: function (ruleActions) {
            var filteredActions = [];

            // iterate backward and get the first rule action of the action list, skip others
            for (var i = ruleActions.length - 1; i >= 0; i--) {
                if (filteredActions.filter(function (a) { return ruleActions[i].executor.isConflict(ruleActions[i].data, a.data); }).length === 0) {
                    filteredActions.push(ruleActions[i]);
                }
            }

            return filteredActions.reverse();
        },

        _contextInitialization: function () {
            var executedActions = this._getExecutedActions();
            return {
                fields: this._fieldsInitialization(),
                executedActions: executedActions.slice(),
                activeActions: executedActions.slice(),
                formContainer: this.formContainer,
                formContainerSelector: this.formContainerSelector,
                iterationsCounter: 0,
                hiddenFields: this.hiddenFields,
                skipToPageCollection: [],
                helper: {
                    showField: this._showField.bind(this),
                    hideField: this._hideField.bind(this),
                    getFieldElement: this._getFieldElement.bind(this),
                    getFieldStartSelector: this._getFieldStartSelector.bind(this),
                    getFieldEndSelector: this._getFieldEndSelector.bind(this),
                    fieldIndexOf: this._fieldIndexOf.bind(this),
                    arrayIndexOf: this._arrayIndexOf.bind(this),
                    actionItemIndexOf: this._actionItemIndexOf.bind(this)
                }
            };
        },

        _fieldsInitialization: function () {
            var fields = [];
            var formRuleFields = this._getFormRulesFields();
            for (var i = 0; i < formRuleFields.length; i++) {
                fields.push({
                    FieldControlId: formRuleFields[i],
                    Values: this._getFieldValues(formRuleFields[i]),
                    Visible: this._arrayIndexOf(this.hiddenFields, formRuleFields[i]) === -1 && this._arrayIndexOf(this.skipFields, formRuleFields[i]) === -1
                });
            }

            return fields;
        },

        _getFormRulesFields: function () {
            var fields = [];

            for (var i = 0; i < this.formRules.length; i++) {
                for (var s = 0; s < this.formRules[i].conditions.length; s++) {
                    var conditionFieldName = this.formRules[i].conditions[s].data.id;
                    if (this._arrayIndexOf(fields, conditionFieldName) === -1) {
                        fields.push(conditionFieldName);
                    }
                }

                for (var j = 0; j < this.formRules[i].actions.length; j++) {
                    var actionFieldIds = this.formRules[i].actions[j].executor.getActionFieldIds(this.formRules[i].actions[j].data);
                    if (actionFieldIds && actionFieldIds.length > 0) {
                        for (var k = 0; k < actionFieldIds.length; k++) {
                            if (this._arrayIndexOf(fields, actionFieldIds[k]) === -1) {
                                fields.push(actionFieldIds[k]);
                            }
                        }
                    }
                }
            }

            return fields;
        },

        _updateSkipPages: function (previousIndex, nextIndex) {
            if (previousIndex < nextIndex) {
                // next page - disable fields in skipped pages
                var fieldContainerNames = FormRulesSettings.getFieldsContainerNames();
                for (var skipPage = previousIndex + 1; skipPage < nextIndex; skipPage++) {
                    for (var k = 0; k < fieldContainerNames.length; k++) {
                        var fieldsContainers = this.pages.eq(skipPage).find('[data-sf-role="' + fieldContainerNames[k] + '"]');
                        for (var j = 0; j < fieldsContainers.length; j++) {
                            var skippedField = FormRulesSettings.getFieldValueElements(fieldsContainers[j]);
                            if (skippedField.length) {
                                var fieldName = skippedField.first().setAttribute("name");
                                var fieldStartWrapper = this.formContainer.querySelectorAll("script[data-sf-role-field-name='" + fieldName + "']")[0];
                                if (fieldStartWrapper) {
                                    var dataSfRole = fieldStartWrapper.getAttribute("data-sf-role");
                                    if (dataSfRole) {
                                        var fieldControlId = dataSfRole.replace("start_field_", "");
                                        this._skipField(fieldControlId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else {
                // previous page - iterate through skipped fields and enable them again
                for (var fieldIndex = this.skipFields.length - 1; fieldIndex >= 0; fieldIndex--) {
                    var fieldPageIndex = this._getFieldPageContainerIndex(this.skipFields[fieldIndex]);
                    if (nextIndex < fieldPageIndex && fieldPageIndex < previousIndex) {
                        this._unskipField(this.skipFields[fieldIndex]);
                    }
                }
            }

            this._setSkipFields(this.skipFields);
        },

        _skipField: function (fieldControlId) {
            var index = this._arrayIndexOf(this.skipFields, fieldControlId);
            if (index === -1) {
                this.skipFields.push(fieldControlId);

                var fieldElement = this._getFieldElement(fieldControlId);
                fieldElement.setAttribute('disabled', 'disabled');
            }
        },

        _unskipField: function (fieldControlId) {
            var index = this._arrayIndexOf(this.skipFields, fieldControlId);
            if (index > -1) {
                this.skipFields.splice(index, 1);

                var fieldElement = this._getFieldElement(fieldControlId);
                fieldElement.removeAttribute('disabled');
            }
        },

        _actionItemIndexOf: function (actions, actionData) {
            for (var i = 0; i < actions.length; i++) {
                if (actions[i].data.target === actionData.target && actions[i].data.name === actionData.name && actions[i].data.pageIndex === actionData.pageIndex) {
                    return i;
                }
            }

            return -1;
        },

        _getContextField: function (context, fieldControlId) {
            for (var i = 0; i < context.fields.length; i++) {
                if (context.fields[i].FieldControlId === fieldControlId) return context.fields[i];
            }
            return null;
        },

        _getFieldElement: function (fieldControlId) {
            var scriptWrapper = this.formContainer.querySelector(this._getFieldStartSelector(fieldControlId));
            if (scriptWrapper) {
                var fieldAllElements = [];
                var sibling = scriptWrapper.nextElementSibling;
                while (!sibling.matches(this._getFieldEndSelector(fieldControlId))) {
                    fieldAllElements.push(sibling);
                    sibling = sibling.nextElementSibling;
                }

                // Get field element based on container selector registered in FormRulesSettings
                var fieldContainerNames = FormRulesSettings.getFieldsContainerNames();
                if (fieldContainerNames && fieldContainerNames.length > 0) {
                    for (var i = 0; i < fieldContainerNames.length; i++) {
                        var containerSelector = '[data-sf-role="' + fieldContainerNames[i] + '"]';
                        var fieldContainer = null;
                        for (let i = 0; i < fieldAllElements.length; i++) {
                            var element = fieldAllElements[0];
                            if (element.matches(containerSelector)) {
                                fieldContainer = element;
                                break;
                            }
                        }

                        // If not found in root elements, try searching in descendants
                        if (!fieldContainer) {
                            for (let i = 0; i < fieldAllElements.length; i++) {
                                var element = fieldAllElements[0];
                                if (element.querySelector(containerSelector)) {
                                    fieldContainer = element;
                                    break;
                                }
                            }
                        }

                        if (fieldContainer) {
                            return FormRulesSettings.getFieldValueElements(fieldContainer);
                        }
                    }
                }
            }

            return null;
        },

        _showField: function (context, fieldControlId) {
            var index = this._arrayIndexOf(context.hiddenFields, fieldControlId);
            if (index > -1) {
                context.hiddenFields.splice(index, 1);

                var scriptWrapper = context.formContainer.querySelector(this._getFieldStartSelector(fieldControlId));
                if (scriptWrapper) {
                    var fieldElement = this._getFieldElement(fieldControlId);
                    if (fieldElement) {
                        fieldElement.removeAttribute('disabled');
                    }

                    var sibling = scriptWrapper.nextElementSibling;
                    while (!sibling.matches(this._getFieldEndSelector(fieldControlId))) {
                        sibling.classList.remove("hide");
                        sibling.classList.add("visible");
                        sibling = sibling.nextElementSibling;
                    }
                }
            }
        },

        _hideField: function (context, fieldControlId) {
            var index = this._arrayIndexOf(context.hiddenFields, fieldControlId);
            if (index === -1) {
                context.hiddenFields.push(fieldControlId);

                var scriptWrapper = context.formContainer.querySelector(this._getFieldStartSelector(fieldControlId));
                if (scriptWrapper) {
                    var fieldElement = this._getFieldElement(fieldControlId);
                    if (fieldElement) {
                        fieldElement.setAttribute('disabled', 'disabled');
                    }

                    var sibling = scriptWrapper.nextElementSibling;
                    while (!sibling.matches(this._getFieldEndSelector(fieldControlId))) {
                        sibling.classList.add("hide");
                        sibling.classList.remove("visible");
                        sibling = sibling.nextElementSibling;
                    }
                }
            }
        },

        _getFieldType: function (fieldControlId) {
            var fieldElement = this._getFieldElement(fieldControlId);
            var fieldType = null;
            if (fieldElement) {
                fieldType = fieldElement.getAttribute("data-sf-input-type");
                if (!fieldType) {
                    fieldType = this._getFieldElement(fieldControlId).getAttribute("type");
                }
            }


            return fieldType;
        },

        _getFieldValues: function (fieldControlId) {
            var fieldContainer = this._getFieldContainer(fieldControlId);
            return FormRulesSettings.getFieldValues(fieldContainer);
        },

        _getFieldContainer: function (fieldControlId) {
            return this._getFieldElement(fieldControlId).closest(this.fieldContainerSelector);
        },

        _getFieldPageContainer: function (fieldControlId) {
            var element = this._getFieldElement(fieldControlId);
            var separator = element.closest(this.separatorSelector);
            return separator || element.closest(this.formContainerSelector);
        },

        _getFieldPageContainerIndex: function (fieldControlId) {
            var fieldPageContainer = this._getFieldPageContainer(fieldControlId);
            return this.pages.findIndex(function (x) { return x === fieldPageContainer });
        },

        _fieldIndexOf: function (fields, fieldControlId) {
            for (var i = 0; i < fields.length; i++) {
                if (fields[i].FieldControlId === fieldControlId) return i;
            }

            return -1;
        },

        _getExecutedActions: function () {
            return formContainerMap.get(this.formContainer) || [];
        },

        _setExecutedActions: function (actions) {
            formContainerMap.set(this.formContainer, actions);
        },

        _getHiddenFields: function () {
            var hiddenFields = this.formContainer.querySelector(this.hiddenFieldsSelector);
            if (hiddenFields) {
                return this._createArrayFromCsvValue(hiddenFields.value);
            }

            return [];
        },

        _setHiddenFields: function (fields) {
            var hiddenFields = this.formContainer.querySelector(this.hiddenFieldsSelector);
            if (hiddenFields)
                hiddenFields.value = fields.join(',');
        },

        _getSkipFields: function () {
            var skipFields = this.formContainer.querySelector(this.skipFieldsSelector);
            if (skipFields) {
                return this._createArrayFromCsvValue(skipFields.value);
            }

            return [];
        },

        _setSkipFields: function (fields) {
            this.formContainer.querySelector(this.skipFieldsSelector).value = fields.join(',');
        },

        _createArrayFromCsvValue: function (value) {
            var array = (value || '').split(","), newArray = [];
            for (var i = 0; i < array.length; i++) {
                if (array[i] && array[i] !== '') newArray.push(array[i]);
            }

            return newArray;
        },

        _arrayIndexOf: function (array, value) {
            for (var i = 0; i < array.length; i++) {
                if (array[i] === value) return i;
            }

            return -1;
        },

        _compareArrays: function (array1, array2) {
            var i = array1.length;
            if (i !== array2.length) return false;
            while (i--) {
                if (array1[i] !== array2[i]) return false;
            }
            return true;
        },

        _some: function (array, func) {
            for (var i = 0; i < array.length; i++) {
                if (func(array[i])) return true;
            }
            return false;
        },

        _every: function (array, func) {
            for (var i = 0; i < array.length; i++) {
                if (!func(array[i])) return false;
            }
            return true;
        },

        _getFieldStartSelector: function (fieldControlId) {
            return 'script[data-sf-role="start_field_' + fieldControlId + '"]';
        },

        _getFieldEndSelector: function (fieldControlId) {
            return 'script[data-sf-role="end_field_' + fieldControlId + '"]';
        }
    };

    var formRuleExecutorsCache = [];

    function processFormRules(formContainer, resetElementCache) {
        if (formContainer) {
            var formRulesExecutor = null;
            var cachedFormRulesExecutorObject = formRuleExecutorsCache.filter(function (e) { return e.formContainer === formContainer; })[0];
            if (resetElementCache !== true && cachedFormRulesExecutorObject) {
                formRulesExecutor = cachedFormRulesExecutorObject.formRulesExecutor;
            } else {
                formRulesExecutor = new FormRulesExecutor(formContainer);
                if (resetElementCache === true) {
                    formRuleExecutorsCache = formRuleExecutorsCache.filter(function (e) { return e.formContainer !== cachedFormRulesExecutorObject.formContainer; });
                }

                formRuleExecutorsCache.push({
                    formContainer: formContainer,
                    formRulesExecutor: formRulesExecutor
                });
            }

            formRulesExecutor.process();
        }
    }

    function getFormContainerSelector() {
        var selector = '[data-sf-role="form-container"]';
        const containers = document.querySelectorAll(selector);
        if (containers.length > 0)
            return selector;

        return null;
    }

    window.processFormRules = processFormRules;

    window.sfFormValueChanged = function (element) {
        var formContainer = element.closest(getFormContainerSelector());
        processFormRules(formContainer, false);
    };
}());

// ------------------Form rules execution - END ----------------------

// ------------------ Form hidden fields execution - Start ----------------------
function formHiddenFieldsInitialization(formContainer) {
    var hiddenFieldsInput = formContainer.querySelector('[data-sf-role="form-rules-hidden-fields"]');
    if (hiddenFieldsInput) {
        var hiddenFieldsInputValue = hiddenFieldsInput.value;
        if (hiddenFieldsInputValue) {
            var hiddenFields = hiddenFieldsInputValue.split(',');
            hiddenFields.forEach((hiddenField) => {
                var scriptWrapper = formContainer.querySelector('script[data-sf-role="start_field_' + hiddenField + '"]');
                if (scriptWrapper) {
                    var fieldName = scriptWrapper.getAttribute('data-sf-role-field-name');
                    var fieldElement = scriptWrapper.querySelector('[name="' + fieldName + '"]');
                    if (fieldElement) {
                        fieldElement.setAttribute('disabled', 'disabled');
                    }

                    var sibling = scriptWrapper.nextElementSibling;
                    while (!sibling.matches('script[data-sf-role="end_field_' + hiddenField + '"]')) {
                        sibling.classList.add("hide");
                        sibling.classList.remove("visible");
                        sibling = sibling.nextElementSibling;
                    }
                }
            });
        }
    }

    if (typeof processFormRules === 'function') {
        processFormRules(formContainer);
    }

    var wrapper = formContainer.closest('[data-sf-role="form-visibility-wrapper"]');
    if (wrapper) {
        Array.from(wrapper.children).forEach((child) => {
            wrapper.parentElement.insertBefore(child, wrapper);
        });
        wrapper.remove();
    }
}

// ------------------ Form hidden fields execution - End ----------------------

init(null);
