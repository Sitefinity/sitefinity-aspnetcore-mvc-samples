# Error pages

## Integration
Shows integration between ASP.NET Core error pages middleware and status code middleware for integration with the 
Sitefinity .NET Core renderer. The implementation itself has no customizations and adheres to the official [MS docs]() on handling error pages.

The example uses RazorPages to show the custom error pages with their original status code.

## Logging
The sample uses Serilog for customized logging. Again there are no customizations made, just the standard implementation integrated with [this Serilog sample](https://github.com/datalust/dotnet6-serilog-example)