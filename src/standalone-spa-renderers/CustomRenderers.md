# Building custom renderers for the WYSIWYG editor

Sitefinity enables developers to build their own custom renderers that can work with the Layout service and the Page Builder editor to take advantage of a user friendly and simplified way of building web pages and interactive frontends.

There are two main parts to building custom renderers:

1. Consuming the layout and content services on the front end.
2. Integrating the renderer with the WYSIWYG editor in the administration UI.

For the bellow explanations, we will be using the [Angular Renderer](https://github.com/Sitefinity/angular-standalone-renderer) as an example reference.

Additionally the same implementation was built for React and can be found [here](https://github.com/Sitefinity/react-standalone-renderer).

## Consuming the layout and content services on the front end

Renderers can be built with any client-side JavaScript framework of the user's choice (event with vanilla JS) or with any server-side framework as well. The flow to render a page is described below:

1. When a request for a page is made, the Renderer must first call the layout service for the page that was requested ([example](https://github.com/Sitefinity/angular-standalone-renderer/tree/master/src/app/app.component.ts#L31)). The call to the layout service is just [a call to the page with some additional headers attached to it](https://github.com/Sitefinity/angular-standalone-renderer/tree/master/src/app/sdk/services/layout.service.ts#L15).

The response from the service is in JSON and has [the following set of properties](https://github.com/Sitefinity/angular-standalone-renderer/tree/master/src/app/models/service-response.ts#L4):

1. The ready-to-use ordered hierarchical tree of the widgets (ComponentContext property). This tree corresponds to the preorded layout of widgets, that each page consists of. Having it preordered leaves the renderer to only care about the rendering of the widgets and not their order.
2. The SiteId and Culture context parameters, which are populated with respect to the current page.
3. The Scripts property contains scripts from the CMS that include personalization, tracking etc..

1. After receiving the layout each renderer must iterate the hierarchical tree of widgets and render those in the provided order ([example](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/directives/component-wrapper.directive.ts#L11)).
2. Rendering each widget requires a corresponding widget implementation (corresponding to the widget name specified in the layout service response) to be executed. This implementation is up to the user. The logic behind the implementation is controlled by the set of properties provided in the layout service response for this widget.

## Integrating the renderer in the AdminApp UI

The backend administrative interface provides an interactive page builder that is used for building the layout of the page. This page builder renders the content of the page inside an \<iframe\> element to enable isolation and a better WYSIWYG experience.

There are several requirements needed by any custom renderer to be implemented for it to be integrated into the page builder interface. You need to first implement a working renderer that can render widgets and pages following the steps above.

### Opening the renderer in the page editor

Whenever the page editor is opened it automatically loads the frontend URL of the page in an iframe element to enable isolation. Additionally, the URL which the \<iframe\> element is opened with contains the current domain name the AdminApp UI is browsed with and the URL if the page. For example, if you have browsed your sitefinity website with **https://www.mycustomdomain.com** and the URL of your page is / **home** , then the URL of the iframe will be: **https://www.mycustomdomain.com/home?sfaction=edit**.

Notice the **sfaction** query parameter. It is used as a marker to indicate to the current renderer that the page is being opened for editing ([example](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/services/render-context.ts#L9)). This triggers the renderer to enter 'edit' mode and apply additional modifications to the HTML of the page, so that the rendered page is interactive with the Page Editor.

Next the Page Builder will attempt to load the iframe HTML content and it will look for two things:

1. [Container tags](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/app.component.ts#L79)
2. [A JavaScript object for communication](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/app.component.ts#L90)
3. [Firing an event when renderer is ready](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/app.component.ts#L91)

All the above-mentioned changes to the rendered page need to be made only during editing of the page (the sfaction parameter must be used for this conditional logic). Otherwise, the HTML and JavaScript changes will pollute the front-end render.

**Container tags**

Container tags are used as a starting point to place widgets in. Examples of container tags are the 'body' element or child 'div' elements of the 'body' element. One necessary requirement for these container tags is that they are decorated with the attribute ' **data-sfcontainer**'. If you need a user-friendly name for your placeholders during page editing, you can assign a value to the above attribute like so: '\<div data-sfcontainer=Root\>\</div\>', where 'Root' is the label that will appear in the editor.

**Javascript Object for communication**

The page editor exposes several integration points to plug in your custom logic for inserting widgets. This is controlled by a custom defined JavaScript object that needs to be placed on the window object. This JavaScript object holds several methods that communicate the information from the renderer to the page builder and vice-versa. The interface for this object has several methods:

1. [getWidgets
](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/editor/renderer-contract.ts#L54)This method is used to retrieve the available widgets from the renderer. Each renderer may have different sets of widgets available. These widgets are then displayed in the Widget Selector dialog, so that users can pick from them.
2. [getWidgetMetadata
](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/editor/renderer-contract.ts#L24)This method is used to retrieve the metadata that the widget has. This metadata describes what kinds of properties the widget has, so that the page builder can dynamically build an interactive widget editing UI that is used for setting the properties of the widget itself.
3. [getCategories
](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/editor/renderer-contract.ts#L50)This method is used to retrieve a set of Categories that appear in the Widget Selector dialog and groups the Widgets. Note that the 'Layout' category must be always present in the return result.
4. [renderWidget
](https://github.com/Sitefinity/angular-standalone-renderer/blob/master/src/app/editor/renderer-contract.ts#L38)This method is called whenever a widget is inserted into the page during design time. The expected return result of the method is either plain html decorated with a set of attributes, or an already built element with the attributes already placed on it. The element (or html) is then processed and appended to the DOM.

**Firing an event when renderer is ready**

When the renderer is ready (has rendered all the decorated widgets and the JavaScript object is present on the window object), then the Renderer can fire an event with the name 'contractReady'. This event signals the Page Editor that the Renderer is ready with the output of the HTML and the communication between the two can start.