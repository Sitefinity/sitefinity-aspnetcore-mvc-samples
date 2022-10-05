# Html sanitizer

The sample project allows customization of the Default html sanitizer object in the Renderer project. The customization adds the 'tel' attribute to be excluded from sanitization. The change is in the [CustomHtmlSanitizer.cs file](./CustomHtmlSanitizer.cs).

The DI registration happens in [the Program.cs file](./Program.cs)