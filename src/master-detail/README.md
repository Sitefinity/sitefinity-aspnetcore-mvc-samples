# Shared data between widgets

This project serves the purpose of providing an example for a custom implementation of a master/detail widget where the data comes from an external data source.

## The IRequestPreparation interface
The example takes advantage of the **IRequestPreparation** class, that is executed prior to the page execution. This is handy in a number of cases:

* To share data between widgets.
* To perform some initialization based on the current page and widgets.
* To prefetch data using the **IRestClient** interface that is passed as an argument.

The **IRequestPreparation** implementation must be registered in the DI container during startup (see the Program.cs file).

The implementation in this sample monitors the unresolved segments in the url e.g.
* localhost/home/event-{eventId}

If there is a match with the existing segments, then the method MarkUrlParametersResolved() is called to notify the underlying framework that the additional parameters are resolved and this is a valid URL. If an invalid url is requested (invalid id), then the Renderer App will return a 404 response. Upon a successful match, the resolved parameter is added to the state of the **ContentViewComponent** so it can display the detail view of the event.

## How to setup the sample

* Run the sample and add the **ContentViewComponent** widget on the page.
* Open the page on the frontend
* Click through all of the event links. The events will be shown in detail mode.