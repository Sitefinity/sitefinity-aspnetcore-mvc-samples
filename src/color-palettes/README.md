# Color Palettes
This sample demonstrates how to configure the values of the color pickers in the widget designers.

## Default Color Palette

The name of the palette that is used for the widget selectors by default is called 'Default'. This palette is used for the OOB widgets such as the Section widget. Additionally it can be reused for other types of widgets as well by using the following attribute decoration:

``` c#

    [ColorPalette("Default")]
    public string Color { get; set; }

```

It can be overridden in the [configuration file](./appsettings.json) as well. Here is an example:

``` json

 "Widgets": {
    "Styling": {
      "ColorPalettes": {
        "Default": {
          "DefaultColor": "#AABBCC",
          "Colors": ["#AABBCC", "#BBAACC", "#CCBBAA"]
        },
      }
    }
  }

```

## Custom Color Palettes

Custom color palettes are supported as well. They need to be declared in the [appsettings.json file](./appsettings.json). Here is an example:

``` json

"Widgets": {
    "Styling": {
      "ColorPalettes": {
        "Custom": {
          "DefaultColor": "#DDEEFF",
          "Colors": ["#DDEEFF", "#EEDDFF", "#FFEEDD"]
        }
      }
    }
  }

```