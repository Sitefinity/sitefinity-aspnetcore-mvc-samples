# Captcha widget sample
 
This example demonstrates how to create a captcha widget using Google reCAPTCHA v2 or Google reCAPTCHA v3.
 
Note: You will need to update your privacy and GDPR statements according to Google reCAPTCHA policies.
 
## Sitefinity setup
 
To start using reCAPTCHA, you need to [sign up for an API key pair](http://www.google.com/recaptcha/admin) for your site.
The keys are created for either reCAPTCHA v2 or reCAPTCHA v3 and can be used with the corresponding widget from the sample.
 
In order to run this sample, you need to configure the Sitefinity Web Security module. Make sure that the module is 'Active' in Administration > Modules & Services.
Note: If the Web Security module is not visible in the list of modules, you can install the Progress.Sitefinity.WebSecurity NuGet package for your current Sitefinity version.
Note: If the WebSecurity module is still not available, you can istall it from Administration > Modules & Services > 'Install a module' button, with name: Web security and type: Telerik.Sitefinity.WebSecurity.WebSecurityModule, Telerik.Sitefinity.WebSecurity. You need to have the Progress.Sitefinity.WebSecurity NuGet package installed for this to work.

Go to Administration > Settings > Advanced > Web Security > Captcha > Parameters and add 3 new parameters:
1. Key 'VerificationUrl' and  value 'https://www.google.com/recaptcha/api/siteverify'.
2. Key 'ResponseKey' and  value 'g-recaptcha-response'.
3. Key 'SecretKey' and  value '<<your secret key>>'.
 
Once you have everything set, you should see two new widgets for forms - Captcha2 (for reCAPTCHA v2) and Captcha3 (for reCAPTCHA v3).

In order for the captca widgets to work correctly on forms you will need to configure the Content Security Policy (CSP) header to allow loading the captcha scripts from the google domain. You can do that as described in the documentation: https://www.progress.com/documentation/sitefinity-cms/configure-content-security-policy-header
## The captcha widgets
 
In order to setup the widgets the site key must be populated in the captcha scripts - captcha2.js or captcha3.js.