# Sitefinity Data widget
An example on how to create a widget that consumes that from Sitefinity.

In many cases custom widgets need to get and display some items from the Sitefinity backend. Using the current example, you can work with different kind of content persisted in Sitefinity- news, content blocks, blogs, blog posts, media, dynamic items, etc. 

This is possible with the NET Core widget if they make oData calls to interact with Sitefiity.  

Currently this could be achieved using HttpClientFactory that we provide for working with oData services. 

To achieve this, check the SitefinityDataViewComponent.

Important parts here are:
Create an instance of the HttpClient using the HttpClientFactory and the name Constants.HttpClients.ODataHttpClientName.
Requests to this client are constructed using the settings provided in the appsettings.json file. So all of the urls that are referenced must be relative to the Sitefinity oData service.

Every request to Sitefinity must contain the current site and lanuage so the proper context data can be retrieved. This can be achieved by reading them from the IRenderContext interface.
