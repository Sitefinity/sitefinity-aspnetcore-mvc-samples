# Captcha widget sample

This example demonstrates how to create a captcha widget using Google reCAPTCHA v2 or Google reCAPTCHA v3.

Note: You will need to update your privacy and GDPR statements according to Google reCAPTCHA policies.

## Sitefinity setup

To start using reCAPTCHA, you need to [sign up for an API key pair](http://www.google.com/recaptcha/admin) for your site.
The keys are created for either reCAPTCHA v2 or reCAPTCHA v3 and can be used with the corresponding widget from the sample.

In order to run this sample, you must populate the captcha configuration in your Sitefinity WebSecurity config with the generated secret key. The verification URL for Google reCAPTCHA is https://www.google.com/recaptcha/api/siteverify and the response key is g-recaptcha-response.
These 3 parameters should be added in the config in thw following way:
1. Key 'VerificationUrl' and  value 'https://www.google.com/recaptcha/api/siteverify'.
2. Key 'ResponseKey' and  value 'g-recaptcha-response'.
3. Key 'SecretKey' and  value '<<your secret key>>'.

Once you have everything set-upped, you should see two new widgets for forms - Captcha2 (for reCAPTCHA v2) and Captcha3 (for reCAPTCHA v3).

## The captcha widgets

In order to setup the widgets the site key must be populated in the captcha scripts - captcha2.js or captcha3.js.
