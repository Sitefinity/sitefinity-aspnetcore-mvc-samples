# Hello World widget
An example on how to create a hello world widget.

This example demonstrates how to create a ViewComponent that will be used to display a configurable message.

## View component explained: 

We use the [SitefinityWidget] attribute in order to load the widget automatically in the Sitefinity InsertWidget dialogs.
It has a number of properties that can be used to tweak the appearance and functionality of the widget:

* Title -> the friendly name of the widget
* Category -> which category the widget is going to appear in- e.g. Content, Layout
* EmptyIcon -> what icon to show when the widget has an empty state. The available icon names are font-awesome icons.
* EmptyIconText -> the text to be shown when the widget has an empty state

You need to inherit the ViewComponent so we can detect and render your functionality inside your pages.

The InvokeAsync method is required. It will be called automatically every time the page is requested.

The InvokeAsync method needs argument of type IViewComponentContext<T>. We will populate directly the argument in this method when we render the page. The generic parameter refers to the "entity" of the widget. Beside your business logic, the "entity" contains all properties of the widget that are persisted in Sitefinity.

Just like in the MVC and WebForms widgets, the .Net core widgets will persist your public properties and will provide user friendly interface to edit them though the designer during the edit of your pages.  

.Net core widgets supports the new property editor designers that we introduced for MVC custom widgets. Therefore, based on attributes we will autogenerate user friendly editing interface for them. 

Note: Only the public properties from the model will be persisted and not the public properties that you may have in the ViewComponent.cs file. This way we provide better separation of concerns between your business logic and rendering. 

NOTE: 

if the user changes the model and deletes or renames a property. He will still 

be able to access it in the properties collection but it will not be automatically binded 