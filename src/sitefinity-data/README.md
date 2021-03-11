# Sitefinity Data widget

An example on how to create a widget that consumes that from Sitefinity.

## Prerequsities
* A dynamic type with an oData path of "pressreleases" (other content types can be used as well - e.g. "newsitems")

In many cases custom widgets need to get and display some items from the Sitefinity backend. Using the current example, you can work with different kind of content persisted in Sitefinity- news, content blocks, blogs, blog posts, media, dynamic items, etc. 

To achieve this, check the SitefinityDataViewComponent.

This sample works with the IRestClient to access data within Sitefinity. The configuration from the app.settings.json file for the WebServicePath
is used to control which service the client will work with.

## Securing the Renderer -> Sitefinity communication.
If you wish to restrict the web service in Sitefinity to disallow access to other clients except the renderer, you should enter an API Key for the referenced service in the appsettings.json file by adding the property "WebServiceApiKey" like so:

``` json

"Sitefinity": {
    "Url": "https://yoursitefinitywebsiteurl",
    "WebServicePath": "api/default",
    "WebServiceApiKey: "mysecret",
},

```

Using this configuration, when API calls are made to the OData service under the route api/default, a special header will be passed carrying this api key and allowing the call through. This does not authenticate the call however, the user is still left anonymous. For this to fully work the Web Service in Sitefinity(under Administration/WebServices) should have the same Api Key value set.