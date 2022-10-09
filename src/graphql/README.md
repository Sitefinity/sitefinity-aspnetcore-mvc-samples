# GraphQL

A widget demonstrating how to setup graphQL service calls with both Sitefinity and external applications.

## For Sitefinity GraphQL

Firstly, you must install the [Progress.Sitefinity.GraphQL](https://nuget.sitefinity.com/#/package/Progress.Sitefinity.GraphQL) nuget package. Follow the documentation for the setup [here](https://www.progress.com/documentation/sitefinity-cms/use-graphql-protocol)

## For Other GraphQL Services

Just enter the URL in the designer to the service and enter your query.

## How it works

The example works by using the library [GraphQL .NET](https://github.com/graphql-dotnet/graphql-client) and what it does is just to execute a query to the service given an endpoint and a query in the widget designer.