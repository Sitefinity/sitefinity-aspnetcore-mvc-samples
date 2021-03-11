# Sitefinity Data widget

An example on how to create a widget that consumes that from Sitefinity. This example demonstrates how to filter and perform CRUD operations with the IRestClient interface.

## Prerequsities
* A dynamic type with an oData path of "pressreleases" (other content types can be used as well - e.g. "newsitems")

In many cases custom widgets need to get and display some items from the Sitefinity backend. Using the current example, you can work with different kind of content persisted in Sitefinity- news, content blocks, blogs, blog posts, media, dynamic items, etc. 

To achieve this, check the SitefinityDataViewComponent.

This sample works with the IRestClient to access data within Sitefinity. The configuration from the app.settings.json file for the WebServicePath
is used to control which service the client will work with.

