# Change grid system
An example on how to change the css grid system.

This example demonstrates how to:

1. Create a custom layout file and load your own css framework.
2. Override form default layout file and load your own css framework.
3. Change the template of the section widget
3. Change the template of the form section widget
4. Create a tag helper for building custom grid system classes for the section widget columns.

## Custom layout:

Create a new layout cshtml file in the Views/Shared directory. In the example it is called _Layout.cshtml ((the name of the layout file should contain "Layout" or "Template", so that Sitefinity recognizes it).

Add a stylesheet link to your prefered css framework (in this example we're using https://tailwindcss.com/)

In Sitefinity base your page on this new _Layout template.

## Override form layout:

In order to override the form default layout file create a new layout cshtml file in the Views/Shared directory. The file should be called FormsDefault.cshtml, so that Sitefinity recognizes it.

Add a stylesheet link to your prefered css framework (in this example we're using https://tailwindcss.com/)

Note that if your Web security module is turned on you need to add the tailwindcss.com domain as a Trusted source. 

Now all forms will use the provided layout file instead of the default one.

## Custom template for section widget:

Add a Default.cshtml file under Views/Shared/Components/Section. This replaces the default template of the widget.

In it we build the tailwind classes using a custom Tag helper. 

## Custom template for form section widget:

The process here is the same as with a page widget.

Add a Default.cshtml file under Views/Shared/Components/FormSection. This replaces the default template of the widget.

In it we build the tailwind classes using a custom Tag helper. 

## ColumnClassTagHelper: 

The tag helper has 3 properties:
* GridSize (the number of grid columns in the system - 12 for example)
* AddtionalClass (all additional classes, set to the column, for example from the property editor's advanced view)
* CssColsPerRowCol  (number of grid coluimns per row column) which we'll pass through the section template. 

We pass those as attributes of the div element that represend the column wrapper.

One thing to keep in mind is that property names in tag helpers follow pascall case naming convention in the class, but kebap case when used in the html. 

General information about tag helpers can be found here: https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro?view=aspnetcore-3.1 