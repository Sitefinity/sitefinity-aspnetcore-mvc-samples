# All properties widget
An example on how to create a hello world widget.

This example demonstrates how the different types of types that are supported are displayed in the widget designers. Refer to the AllPropertiesEntity class for reference.

## Extensibility

The example demonstrates how to add a field that is dynamically populated with choice options from an external data source.
1. First declare a custom Attribute and decorating the property of your Entity class (as in AllPropertiesEntity -> ExternalChoice property).
2. Afterwords you declare a custom IPropertyConfigurator that fetches the values from the external data source and passes them along to the metadata of the property (See ExternalChoicePropertyConfigurator class)
3. Register your configurator with the DI framework as shown in the Startup class.

For reference see the files under The Extensibility Folder.