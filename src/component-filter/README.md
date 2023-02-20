# Components filter

The sample project demonstrates the ability to filter the components visible in the widget selectors for Pages, Templates and Forms based on a certain criteria. For the purpose the Renderer project provides an interface **IViewComponentFilter** that has to be implemented. The interface declares a method **FilterViewComponents** accepting 3 parameters:
- **componentName** - the name of the component (declared by the **ViewComponentAttribute**)
- **toolbox** - the toolbox of the component (declared by the **SitefinityWidgetAttribute**). The default value for Forms widgets is **'Forms'**, **null** otherwise.
- **category** - the category of the component (declared by the **SitefinityWidgetAttribute**)

The method should return **true** for components that should be visible, false otherwise.
This specific customization hides the **Section** widget for users with the "Editors" role. The implementation is in the [CustomViewComponentFilter.cs file](./CustomViewComponentFilter.cs).

The DI registration happens in [the Program.cs file](./Program.cs)