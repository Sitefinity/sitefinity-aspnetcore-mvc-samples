# Shared data between widgets

This project serves the purpose of providing an example where two widgets can communicate with each other in an early stage of the pipeline.


## The IRequestPreparation interface
The example takes advantage of the **IRequestPreparation** class, that is executed prior to the page execution. This is handy in a number of cases:

* To share data between widgets.
* To perform some initialization based on the current page and widgets.
* To prefetch data using the **IRestClient** interface that is passed as an argument.

The **IRequestPreparation** implementation must be registered in the DI container during startup (see the Startup.cs file).

The implementation in this sample monitors the unresolved segments in the url e.g.
* localhost/home/cat1 -> [ cat1 ] will be the parameter in the UrlParameters collection
* localhost/home/cat1/cat2 -> [ cat1, cat2 ] will be the parameters in the UrlParameters collection

If there is a match with the existing segments, then the method MarkUrlParametersResolved() is called to notify the underlying framework that the
additional parameters are resolved and this is a valid URL. If an invalid url is requested (/cat4 or /cat1/cat2), then the Renderer App will return a 404 response. Upon a successful match, the resolved parameter is added to the state of the **CategorySelector** and **DataBasedOnSelectedCategory** so they can display their views according to the current selection.

## The widgets

There are two widgets in this sample - **CategorySelector** and **DataBasedOnSelectedCategory**.

* The **CategorySelector** widget is used to show the different types of categories available.
* The **DataBasedOnSelectedCategory** is used to show the data filtered data based on the selected category.


## How to setup the sample

* Run the sample and add both the **CategorySelector** and **DataBasedOnSelectedCategory** widgets on the same page.
* Open the page on the frontend
* Click through all of the category links. The data will be filtered based on the selected category in the url.