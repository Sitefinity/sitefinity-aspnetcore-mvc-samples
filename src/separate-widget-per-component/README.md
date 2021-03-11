# Separate widget per template sample
An example on how to create a separate entry for each widget view inside the Select widget dialog.


## View component explained: 

We use the [SitefinityWidget] attribute in order to load the widget automatically in the Sitefinity InsertWidget dialogs.
The SeparateWidgetPerTemplate property needs to be set to true in order to generate separate widget for each view template of this widget. The separate entries will be autogenated based on the view name inside the Select widget dialog.

You need to inherit the ViewComponent so we can detect and render your functionality inside your pages.

The InvokeAsync method is required. It will be called automatically every time the page is requested.

The InvokeAsync method needs argument of type IViewComponentContext<T>. We will populate directly the argument in this method when we render the page. The generic parameter refers to the "entity" of the widget. Beside your business logic, the "entity" contains all properties of the widget that are persisted in Sitefinity.

Note: The SfViewName property of the entity will be automatically populated with the name of the view that you selected though the InsertWidget dialog.