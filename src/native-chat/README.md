# Native chat widget
An example integration with the Progress NativeChat platform.

This example demonstrates several practical use cases:
1. How to create a native chat widget
2. How to create a custom populated dropdown in the auto generated widget designers.

## The native chat widget

In order to setup the widget to communicate with the native chat service, you need to add the ApiKey in the appsettings.json file:

``` json
{
  "Sitefinity": {
      ...
  },
  "NativeChat": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

The widget itself is nothing special. It serves the purpose to configure the native chat script that initializes the chat bot. In the [NativeChatEntity](./Entities/NativeChat/NativeChatEntity.cs) class you can see all the properties that can be configured. The most important property there us the **BotId**, which comes automatically populated and must be selected in order for the widget to show the bot on the frontend. The other settings are for configuring the nickname of the bot, icons, google maps integration etc..

## Creating a custom populated dropdown

In the current example we populated a dropdown field with a list of possible values (the bot names).

In order to create a custom data source for the dropdown we utilize the **IPropertyConfigurator** interface that is automatically invoked for every property in our *Entity* class. One of the properties in the NativeChatEntity is decorated with a marker attribute - [ExternalDataChoiceAttribute](./Attributes/ExternalDataChoiceAttribute.cs). This attribute does not hold any logic but just serves as a marker for our [ExternalPropertyConfigurator](./Attributes/ExternalPropertyConfigurator.cs) class to find the property decorated with the attribute and provide custom metadata - in this case the bot names to choose from
