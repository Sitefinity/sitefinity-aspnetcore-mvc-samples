# Extend Content Block Widget
An example on how to extend(replace) the model of the Content Block widget.

This example demonstrates how to extend the model of the Content Block widget with custom logic.

## Implementation explained: 

You need to inherit the ContentBlockModel in order to extend and slightly modify its logic, or implement the IContentBlockModel interface to apply brand new busness logic to the widget.
In the *InitialzieViewModel* method you can modify the generated view model that will be supplied to the view, or provide additional data to the view (see ExtendedContentBlockModel.cs for reference).

You can modify the view of the ContentBlock widget by placing a Default view in Views/Shared/Components/ContentBlock folder, and changing the default code of the view depending on your requirements.

Once the extended model is ready, you can regiser it in the Startup.cs file in the ConfigureServices method after the registration of the default ComponentView models:
    public virtual void ConfigureServices(IServiceCollection services)
	{
		// add sitefinity related services
		services.AddSitefinity();
		services.AddViewComponentModels();

		services.AddSingleton<IContentBlockModel, ExtendedContentBlockModel>();
	}