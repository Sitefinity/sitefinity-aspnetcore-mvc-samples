document.addEventListener("DOMContentLoaded", function () {
    var submitFormSelector = "submitForm";
    var submitSuccessSelector = "submitSuccess";
    var hiddenClass = "d-none";
    var submitForm = document.getElementById(submitFormSelector);
    var submitSuccess = document.getElementById(submitSuccessSelector);

    submitForm.onsubmit = function (event) {
        event.preventDefault();

        if (submitForm.checkValidity()) {
            var formData = new FormData(submitForm);
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState === 4 && (this.status === 200 || this.status === 204)) {
                    submitForm.classList.add(hiddenClass);
                    submitSuccess.classList.remove(hiddenClass);

                    submitSuccess.scrollIntoView({
                        behavior: "smooth"
                    });
                }
            };
            xhr.open("post", "/FormValues/ContactUs", true);
            xhr.send(new URLSearchParams(formData));
            return false;
        }
    };
});