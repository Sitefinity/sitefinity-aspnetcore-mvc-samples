# All properties widget
An example on how to create a hello world widget.

This example demonstrates how the different types of types that are supported are displayed in the widget designers. Refer to the AllPropertiesEntity class for reference.

## Extensibility

The example demonstrates how to add a field that is dynamically populated with choice options from an external data source.

1. First declare a [custom Attribute](./Extensibility/ExternalDataChoiceAttribute.cs) and [decorate the properties of your Entity class](./Entities/AllProperties/AllPropertiesEntity.cs)

2. Afterwords you declare a [custom IPropertyConfigurator](./Extensibility/ExternalChoicePropertyConfigurator.cs) that fetches the values from the external data source and passes them along to the metadata of the property


3. Register your IPropertyConfigurator with the DI framework as shown in the Startup class.

``` c#

builder.Services.AddSingleton<IPropertyConfigurator, ExternalChoicePropertyConfigurator>();

```