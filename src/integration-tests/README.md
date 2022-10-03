# Integration tests

This project demonstrates how to test your .NET Renderer pages and widgets.

For more information on writing integration tests with .NET Core see this [article](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)

## Prerequisites
The way of integration testing requires a live CMS instance ready to accept any requests by the Renderer application. This is the most reliable way of ensuring that your code works with the latest changes to the Sitefinity CMS.

## How it works
Underneath the ASP.NET Core framework spins up its own instance of the web application and provides you with a ready to use HttpClient class to interact with it. This gives the ability to interact with the web application and to execute HTTP requests against it. Moreover since the web application is actings as a proxy(forwarding requests to Sitefinity CMS), we can create content items in the CMS.

## The CleanUpRestClient
This is a handy class because it cleans up any created resources by it **automatically**. You do not need to worry about deletion of the created content items when the test finishes. So, the only thing to do is just create a new instance, **wrap it in a using statement** and everything will be cleaned-up automatically. There is a helper method provided in the [IntegrationTestsWebApplicationFactory class](./integrationtests/Framework/IntegrationTestsWebApplicationFactory.cs).

**If some of the items fail to delete, it may be because the item is locked. Be sure to unlock it(or publish it) before the end of the test, so it gets deleted.**

## Authentication

In order to be able to create items in the CMS, the IRestClient must be authenticated. For this you need to setup the OAuth authentication as described [here](https://www.progress.com/documentation/sitefinity-cms/configure-oauth2) and enter the 'clientid' and 'clientsecret' in the appsettings.json file. Further you would need a user (preferably admin) present in the CMS. Enter his username(email) and password in the appsettings.json file as well.

The settings are placed in the appsettings.json file deliberately, so that they can be changed through CI/CD.

## Sample tests
The 'Tests' folder contains some sample tests that can help you get started.