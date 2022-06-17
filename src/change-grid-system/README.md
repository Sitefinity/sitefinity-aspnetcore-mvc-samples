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

## appsettings.json Styling documentation
- used to configure preferred css classes for different styling options

###Root properties

**CssGridSystemColumnCount** - the number of grid columns in the grid system

**VideoBackgroundClass** - sets the background video class of the section widget

**ImageBackgroundClass** - sets the background image class of the section widget

**DefaultPadding** - sets the default padding size for all widget designers, can be None, S, M, L

**DefaultMargin** - sets the default margin size for all widget designers, can be None, S, M, L

### AlignmentClasses
- sets the classes used to align the buttons in the CTA widget

**Left** - aligns buttons to the left

**Center** - centers the buttons

**Right** - aligns buttons to the right

**Justify** - positions the buttons in the left and the right side respectively 

### ButtonClasses
- a list of all button variations, which can be applied to the primary in the secondary buttons in the CTA widget. Each variation is represented by an object with the following properties:

 **Key** - The unique identifier of the button variation, used by the system

 **Title** - The title of the button variation, displayed in the

 **Value** - css classes applied to the primary buttons

### OffsetClasses
- all offset (padding, margin) classes used by the widgets

 **PaddingTopNONE** - class that sets no padding top

 **PaddingTopS** - class that sets S size padding top 

 **PaddingTopM** - class that sets M size padding top

 **PaddingTopL** - class that sets L size padding top

 **PaddingLeftNONE** - class that sets no padding left

 **PaddingLeftS** - class that sets S size padding left

 **PaddingLeftM** - class that sets M size padding left

 **PaddingLeftL** - class that sets L size padding left

 **PaddingBottomNONE** - class that sets no padding bottom

 **PaddingBottomS** - class that sets S size padding bottom

 **PaddingBottomM** - class that sets M size padding bottom

 **PaddingBottomL** - class that sets L size padding bottom

 **PaddingRightNONE** - class that sets no padding right

 **PaddingRightS** - class that sets S size padding right

 **PaddingRightM** - class that sets M size padding right

 **PaddingRightL** - class that sets L size padding right

 **MarginTopNONE** - class that sets no margin top

 **MarginTopS** - class that sets S size margin top

 **MarginTopM** - class that sets M size margin top

 **MarginTopL** - class that sets L size margin top

 **MarginLeftNONE** - class that sets no margin left

 **MarginLeftS** - class that sets S size margin left

 **MarginLeftM** - class that sets M size margin left

 **MarginLeftL** - class that sets L size margin left

 **MarginBottomNONE** - class that sets no margin bottom

 **MarginBottomS** - class that sets S size margin bottom

 **MarginBottomM** - class that sets M size margin bottom

 **MarginBottomL** - class that sets L size margin bottom

 **MarginRightNONE** - class that sets no margin right

 **MarginRightS** - class that sets S size margin right

 **MarginRightM** - class that sets M size margin right

 **MarginRightL** - class that sets L size margin right

### FieldSizeClasses
- set of predefined form field widths (set in the property editor of each form widget)

 **WidthNONE** - class that sets no width

 **WidthS** - class that sets S size width

 **WidthM** - class that sets M size width

 **WidthL** - class that sets L size width
