# Extend Content Block Widget
An example on how to extend(replace) the model of the Content Block widget and how to extend(replace) the default entity type enabling to add additional properties to the designer.

This example demonstrates how to extend the model of the Content Block widget with custom logic and add an additional property in the designer.

## Implementation explained:

You need to inherit the ContentBlockModel in order to extend and slightly modify its logic, or implement the IContentBlockModel interface to apply brand new busness logic to the widget.
In the *InitialzieViewModel* method you can modify the generated view model that will be supplied to the view, or provide additional data to the view (see ExtendedContentBlockModel.cs for reference).

You can modify the view of the ContentBlock widget by placing a Default view in Views/Shared/Components/ContentBlock folder, and changing the default code of the view depending on your requirements.

Once the extended model is ready, you can regiser it in the Startup.cs file in the ConfigureServices method after the registration of the default ComponentView models:

``` C#
    public virtual void ConfigureServices(IServiceCollection services)
	{
		// add sitefinity related services
		services.AddSitefinity();
		services.AddViewComponentModels();

		services.AddSingleton<IContentBlockModel, ExtendedContentBlockModel>();
	}
```

To add an additional property in the designer there needs to be one more registration in the DI container. Here we register a diffrent class that will provide metadata for the designer - ExtendedContentBlockEntity. One important consideration here is that you always need to inherit from the default "Entity" class.

``` C#
    public virtual void ConfigureServices(IServiceCollection services)
	{
		// add sitefinity related services
		services.AddSitefinity();
		services.AddViewComponentModels();

		services.AddSingleton<IContentBlockModel, ExtendedContentBlockModel>();
		services.AddSingleton<IEntityExtender, EntityExtender<ContentBlockEntity, ExtendedContentBlockEntity>>();
	}

	// where custom contnet block entity is defined as so

	public class ExtendedContentBlockEntity : ContentBlockEntity
	{
		public string CustomProp { get; set; }
	}

```

## Upgrade considerations
If you plan to make changes to the views of the OOB widgets, please bare in mind that you would not get the changes that we introduced in the views with each release (rare occasion, but not unlikely).