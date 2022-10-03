# Ovierview

With the .NET Core Renderer version 14.0, we introduced a C# RestSdk that can work with Sitefinity content. This REST SDK can be used for both retrieving and creating content.

## Setup procedure

In order to take advantage of the c# REST SDK, you need to reference the nugget package - "Progress.Sitefinity.RestSdk". This package is build on top of .NET Standard v2.0, so it can be used for both .NET Framework and .NET Core projects.

This assembly is automatically referenced for newly created projects using the [dotnet cli](https://www.progress.com/documentation/sitefinity-cms/setup-the-asp.net-core-renderer#quick-setup-procedure).

## The IRestClient interface

The RestSdk works with the **IRestClient** interface and can be used through DI. The **IRestClient** interface has the following primary methods for working with Data ->

* GetItem
* GetItems
* UpdateItem
* DeleteItem
* CreateItem

### Usage in custom controllers

``` C#

public class TestController : Controller
{
    private IRestClient restClient;

    public TestController(IRestClient restClient)
    {
        this.restClient = restClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var taxonType = RestClientContentTypes.GetTaxonType("Tags");

        // necessary to initialize this. automatically done for page requests
        // adds the current cookies if the user is logged-in, so that the request is authenticated
        var args = new RequestArgs();
        var requestCookie = this.HttpContext.Request.Headers[HeaderNames.Cookie];
        if (!string.IsNullOrEmpty(requestCookie))
            args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);

        await this.restClient.Init(args);
        var result = await this.restClient.GetItems<TaxonDto>(new GetAllArgs()
        {
            Type = taxonType,
            Fields = new[] { "Id", "Title" }
        });

        return this.Json(result);
    }
}

```

### Registration for custom implementations **in external** .NET Core(non .NET Renderer related) applications

``` C#

services.AddHttpClient("sfservice", (servicesProvider, client) =>
{
    client.BaseAddress = new Uri("http://your.sitefinity.site/api/default");

    // optional. this key is used for restriction of the Web Service. it is not used for managing content.
    client.DefaultRequestHeaders.Add("X-SF-APIKEY", "your api key");

}).ConfigurePrimaryHttpMessageHandler(configure =>
{
    return new HttpClientHandler
    {
        AllowAutoRedirect = false,
        UseCookies = false,
    };
})

services.AddScoped<IRestClient>((x) =>
{
    var factory = x.GetRequiredService<IHttpClientFactory>();
    var httpClient = factory.CreateClient("sfservice");

    var restClient = new RestClient(httpClient);
    return restClient;
});

```

### Initialization

Before usage the IRestClient must be initialized by calling the function Init:

**This is automatically done for widgets in the Sitefinity .NET Core Renderer projects. For any custom controller implementations the bellow code must be executed.**

``` C#

// the RequestArgs class contains headers and query parameters that will be passed to subsequent service calls
await restClient.Init(new RequestArgs());

```


## The SdkItem class and ISdkItem interface
The **SdkItem** class is the base class for working with the **IRestClient** interface and Sitefinity content. It holds the following signature:
``` C#

/// <summary>
/// Interface used to work with Sitefinity dto objects.
/// </summary>
public class SdkItem : ISdkItem
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the provider name.
    /// </summary>
    string Provider { get; }

    /// <summary>
    /// Gets a value from the item with the provided field name.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <param name="fieldName">The name of the field.</param>
    /// <returns>The value of the field.</returns>
    public T GetValue<T>(string fieldName)
}

```

This class can be used for any kind of content (both static and dynamic) and is useful if the model is not known in advance, or the model will be changed in the future. The two static properties - Id and Provider are always there on any kind of Sitefinity content item and can be used for other calls to the REST API. The method **GetValue<T>** is more interesting since it provides access to any kind of defined fields on the server. For examples on the different kinds of fields that this method supports refer to this sample [project](../all-fields/Views/Shared/Fields).

## Explicitly typed content
Sometimes it is better to work with explicitly typed content since the model is known in advance. This is the case for the news item class, but is valid for any kind of dynamic content as well. Here is how the NewsDto represents the News content:

``` C#

/// <summary>
/// Class mapped to the news item class in Sitefinity.
/// </summary>
[MappedSitefinityTypeAttribute(RestClientContentTypes.News)]
public class NewsDto : SdkItem
{
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the publication date.
    /// </summary>
    public DateTime PublicationDate { get; set; }

    /// <summary>
    /// Gets or sets the date of creation.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the url name.
    /// </summary>
    public string UrlName { get; set; }

    /// <summary>
    /// Gets or sets the item default url.
    /// </summary>
    public string ItemDefaultUrl { get; set; }

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    public string[] Tags { get; set; }
}

```

**NOTE - The REST SDK provides dtos for all of the static types - news, events, lists, taxons, taxonomies, media, pages and page templates, so you do not need to define them yourself unless you wish for your custom fields to be defined in a custom DTO class. In this case it is best to inherit from the already defined types and add your custom fields there.**

There are three things to note here:

1. The NewsDto inherits from from SdkItem. This gives us the method GetValue<T>, and the properties Id and Provider.

2. The class is marked with the attribute - **MappedSitefinityTypeAttribute** that basically holds the clr type of the mapped sitefinity type. In this case RestClientContentTypes.News = "Telerik.Sitefinity.News.Model.NewsItem"

3. All of the custom properties are defined.

## Taxa items

Working with taxa items is similar to the way we work with content items. They all must inherit from the TaxonDto class, which in turn inherits from the SdkItem class.

``` C#

    /// <summary>
    /// The taxon class.
    /// </summary>
    public class TaxonDto : SdkItem
    {
        /// <summary>
        /// Gets or sets the taxonomy id.
        /// </summary>
        public string TaxonomyId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }

```

The TagDto class is defined like so:

``` c#
    /// <summary>
    /// The taxon class.
    /// </summary>
    [MappedSitefinityTypeAttribute(RestClientContentTypes.Tags)]
    public class TagDto : TaxonDto
    {
    }

```

The difference here is in the **RestClientContentTypes.Tags** constant. It holds the value of "Taxonomy_Tags", which if we generalize is "Taxonomy_{TaxonomyDeveloperName}". If you wish to have a static model defined for a custom taxonomy with the developer name "geographical-regions", then the way you would map it is:

``` c#
    /// <summary>
    /// The taxon class.
    /// </summary>
    [MappedSitefinityTypeAttribute("Taxonomy_geographical-regions")]
    public class GeographicalRegion : TaxonDto
    {
    }

```

## Retrieving a single item

Retrieving a single item can be achieved through these methods:

``` C#
    /// <summary>
    /// Gets a single item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="restClient">The rest client.</param>
    /// <param name="id">The id of the item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static Task<T> GetItem<T>(this IRestClient restClient, string id)
        where T : class, ISdkItem

    /// <summary>
    /// Gets a single item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="restClient">The rest client.</param>
    /// <param name="id">The id of the item.</param>
    /// <param name="provider">The provider of the item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static Task<T> GetItem<T>(this IRestClient restClient, string id, string provider)
        where T : class, ISdkItem

    /// <summary>
    /// Gets a single item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="restClient">The rest client.</param>
    /// <param name="id">The id of the item.</param>
    /// <param name="provider">The provider of the item.</param>
    /// <param name="culture">The culture, in which to get the item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static Task<T> GetItem<T>(this IRestClient restClient, string id, string provider, string culture)
        where T : class, ISdkItem
```

**Note that these methods retrieve all of the fields of the item both plain and related.**

An example usage for retrieving a single news item by passing an id only would be:
``` C#

restClient.GetItem<NewsDto>(Guid.NewGuid().ToString());

```

An alternative method that allows more granular control over the retrieving of a single item can be found on the IRestClient interface.
``` C#

    /// <summary>
    /// Gets a single item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="args">The arguments to be passed to the service.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<T> GetItem<T>(GetItemArgs args)
        where T : class;

```

The **GetItemArgs** argument holds properties that enable more granular control over the fetching of an item. Here is an example:

``` C#

    restClient.GetItem<NewsDto>(new GetItemArgs()
    {
        Type = RestClientContentTypes.News,
        Provider = "random provider",
        Id = item.Id,
        Fields = new [] { "Title" },
        Culture = "en",
    });

```
**Type property** is note required if the generic argument (in this case \<NewsDto\>) is decorated with the MapppedSitefinityContentTypeAttribute.

### Refreshing an item

Sometimes it is useful if we have made changes to an item - e.g. Published, Scheduled, Updated to refresh the item. This can be done with the following methods:

``` C#

    /// <summary>
    /// Fetches the item again with the latest data.
    /// </summary>
    /// <typeparam name="T">The type of the item to create.</typeparam>
    /// <param name="restClient">The rest client.</param>
    /// <param name="item">The item dto.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static Task<T> RefreshItem<T>(this IRestClient restClient, T item)
        where T : class, ISdkItem

    /// <summary>
    /// Fetches the item again with the latest data.
    /// </summary>
    /// <typeparam name="T">The type of the item to create.</typeparam>
    /// <param name="restClient">The rest client.</param>
    /// <param name="item">The item dto.</param>
    /// <param name="cultureName">The culture name.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static Task<T> RefreshItem<T>(this IRestClient restClient, T item, string cultureName)
        where T : class, ISdkItem

```

Usage:

``` C#

var news = restClient.GetItem<NewsDto>(Guid.NewGuid().ToString());
news.Title = "updated";
restClient.UpdateItem(news);
news = restClient.RefreshItem(news);

```

The "Provider" property is automatically passed since NewsDto inherits from the SdkItem class and thus is not needed as an additional argument.

## Filtering items

Retrieving a collection of items can be done by using the methods:

``` C#
    /// <summary>
    /// Gets the first 50 items that matches the filter
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="restClient">The rest client.</param>
    /// <param name="expression">The LINQ expression to pass as a filter.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static Task<CollectionResponse<T>> GetItems<T>(this IRestClient restClient, Expression<Func<T, bool>> expression)
        where T : class, ISdkItem

```

These methods accept an **expression** parameter that is automatically converted to a REST call. These expressions are limited to the bellow usages:

Example usages:

``` C#

// filtering with a collection of ids
var ids = new string[] { item.Id, item2.Id };
var result = await restClient.GetItems<NewsDto>(x => ids.Contains(x.Id));

```

``` C#

// filtering by classifications(tags, categories, custom classifications)
var result = await restClient.GetItems<NewsDto>(x => x.Tags.Contains(tag.Id));

```

``` C#

// filtering by multiple classifications(tags, categories, custom classifications)
var result = await restClient.GetItems<NewsDto>(x => x.Tags.Contains(tag.Id) || x.Tags.Contains(tag2.Id));

```

``` C#

// filtering classifications by inverting the filter
var result = await restClient.GetItems<NewsDto>(x => !x.Tags.Contains(tag.Id));

```

``` C#

// string filter with StartsWtih
var result = await restClient.GetItems<NewsDto>(x => x.Title.StartsWith(subString));

```

``` C#

// string filter with EndsWith
var result = await restClient.GetItems<NewsDto>(x => x.Title.EndsWith(subString));

```

``` C#

// string filter with Equals
var result = await restClient.GetItems<NewsDto>(x => x.Title.Equals(otherString));

```

``` C#

// string filter with Contains
var result = await restClient.GetItems<NewsDto>(x => x.Title.Contains(subString));

```

``` C#

// negated string filters
var result = await restClient.GetItems<NewsDto>(x => !x.Title.StartsWith(subString));

```

``` C#

// filtering by basic properties with equals
var result = await restClient.GetItems<NewsDto>(x => x.Id == item.Id);

```

``` C#

// filtering by basic properties with not equals
var result = await restClient.GetItems<NewsDto>(x => x.Id != item.Id);

```

``` C#

// filtering by greater than operator
var referenceDate = DateTime.UtcNow;
var result = await restClient.GetItems<NewsDto>(x => x.PublicationDate > referenceDate.AddHours(-24));

```


``` C#

// filtering by less than operator
var referenceDate = DateTime.UtcNow;
var result = await restClient.GetItems<NewsDto>(x => x.PublicationDate < referenceDate.AddHours(-24));

```

``` C#
// filtering by dynamic fields
var result = await restClient.GetItems<NewsDto>(x => x.GetValue<string>("Title") == "searchTitle");

```

``` C#

// filtering by more complex expressions
var result = await restClient.GetItems<NewsDto>(x => ((x.Id == item.Id) || (x.Id == item2.Id)));

```

``` C#

// filtering by related items
var result = await restClient.GetItems<NewsDtoInQuantum>(x => x.Thumbnail.Any(y => y.Title.StartsWith("Sample title", StringComparison.Ordinal) && y.Title.EndsWith(endsWithFilter, StringComparison.Ordinal)));

```

All of the above expressions can be combined in any binary operator form (&& ||).

For more complex filters, an alternative method is provided on the IRestClient interface

``` C#

    /// <summary>
    /// Gets a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="args">The arguments to be passed to the service.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<CollectionResponse<T>> GetItems<T>(GetAllArgs args)
        where T : class;

```

The **GetAllArgs** argument allows the user to provide additional parameters for projection, filtering, sorting, pagination and total count. Here is the signature:

``` C#

    /// <summary>
    /// The args class used for getting a collection of items.
    /// </summary>
    public class GetAllArgs : GetCommonArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether to retrieve the total count of the items.
        /// </summary>
        public bool Count { get; set; }

        /// <summary>
        /// Gets or sets the order by clauses.
        /// </summary>
        public IList<OrderBy> OrderBy { get; set; } = new List<OrderBy>();

        /// <summary>
        /// Gets or sets the skip count.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take count.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the filter <see cref="Progress.Sitefinity.RestSdk.Filters.FilterClause"/> or <see cref="Progress.Sitefinity.RestSdk.Filters.CombinedFilter"/>.
        /// </summary>
        public object Filter { get; set; }
    }

```

An example usage for it:

``` C#

    var combinedFilter = new CombinedFilter()
    {
        Operator = CombinedFilter.LogicalOperators.Or,
        ChildFilters = new[]
        {
            new FilterClause()
            {
                FieldName = nameof(NewsDto.Title),
                Operator = FilterClause.StringOperators.Contains,
                FieldValue = uniqueFilter,
            },

            new FilterClause()
            {
                FieldName = nameof(NewsDto.Title),
                Operator = FilterClause.StringOperators.Contains,
                FieldValue = secondItemUniqueFilter,
            },
        },
    };

    var items = await restClient.GetItems<NewsDto>(new GetAllArgs()
    {
        Filter = combinedFilter,
        Count = true,
        Fields = new[] { "Id", "Title" },
        OrderBy = new[] { new OrderBy() { Name = "Title", Type = OrderType.Descending } }
    });

```

This filter expression is a hierarchical object structure of the types ***FilterClause* and **CombinedFilter**. Examples for such filters are:

The operators available for this basic filter(**FilterClause**) are listed below:

#### Logical operators
* Equals= "eq"
* Does not equal = "ne"
* Greater than (for numbers) = "gt"
* Less than (for numbers) = "lt";
* Greater than or equal (for numbers) = "ge"
* Less than or equal (for numbers) = "le";
* Contains any of the collection = "any+or";
* Contains all of the collection = "any+and";
* Does not contain any of the collection = "not+(any+or)";

#### Specific string operators
* Starts with = "startswith";
* Ends with = "endswith";
* Contains = "contains";

``` C#

    new FilterClause()
    {
        FieldName = nameof(NewsDto.Title),
        Operator = FilterClause.StringOperators.Contains,
        FieldValue = secondItemUniqueFilter,
    },

```

The properties here are self-explanatory. FieldName maps to the name of the custom field for the selected content type. Operator maps to the logical operator and FieldValue maps to the value with which to execute the logical operation.

The combined filter is recursive and can contain child combined filters as well. The combined filter has an additional property called Operator which can have the value of “And” or “Or” which correspond to Logical AND and logical OR between the child filters.

``` c#

var combinedFilter = new CombinedFilter()
{
    Operator = CombinedFilter.LogicalOperators.Or,
    ChildFilters = new[]
    {
        new FilterClause()
        {
            FieldName = nameof(NewsDto.Title),
            Operator = FilterClause.StringOperators.Contains,
            FieldValue = uniqueFilter,
        },

        new FilterClause()
        {
            FieldName = nameof(NewsDto.Title),
            Operator = FilterClause.StringOperators.Contains,
            FieldValue = secondItemUniqueFilter,
        },
    },
};

```

The two filtering strategies can be mixed with the bellow method. It simplifies the usage of the filters and provides a parameter to control the pagination, ordering, total count and projection.

``` C#

/// <summary>
/// Gets the first 50 items that matches the filter
/// </summary>
/// <typeparam name="T">The type of the item.</typeparam>
/// <param name="restClient">The rest client.</param>
/// <param name="expression">The LINQ expression to pass as a filter.</param>
/// <param name="args">The LINQ expression to pass as a filter.</param>
/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
public static Task<CollectionResponse<T>> GetItems<T>(this IRestClient restClient, Expression<Func<T, bool>> expression, GetAllArgs args)
    where T : class, ISdkItem

```

## Useful filtering examples

``` C#
    // filter translated tags in the "en" culture
    var getAllArgs = new GetAllArgs()
    {
        Culture = "en",
    };

    var items = await restClient.GetItems<TagDto>(x => x.Title != null && ids.Contains(x.Id), getAllArgs);
    
    // for custom taxonomies
    var taxonType = RestClientContentTypes.GetTaxonType("Regions");
    var result = await this.restClient.GetItems<TaxonDto>(x => x.Title != null && ids.Contains(x.Id), new GetAllArgs()
    {
        Type = taxonType,
        Fields = new[] { "Id", "Title" }
    });

```
    
``` C#
    // filter by parent items with sdk object
    var children = await this.restClient.GetItems<SdkItem>(x => x.GetValue<string>("ParentId") == "myparentguid");
    
    // filter by parent items with mapped object
    [MappedSitefinityType("Telerik.Sitefinity.DynamicTypes.Model.Pressreleases.Child")]
    public class ChildDto : SdkItem
    {
        public string Title { get; set; }

        public string ParentId { get; set; }
    }
    
    var children = await this.restClient.GetItems<ChildDto>(x => x.ParentId =="myparentguid");
```

## Projection of items

To control the returned fields (**related items, related media** and non-related fields) from the service, the **Fields** property is used for both getting a single item and a collection of items. The fields are passed as a comma separated list of values. E.g.

``` C#

await restClient.GetItems<NewsDto>(new GetAllArgs()
{
    Fields = new[] { "Id", "Title" },
});

```

If you wish to retrieve all of the fields (related and non-related) a "*" value can be passed.

``` C#

await restClient.GetItems<NewsDto>(new GetAllArgs()
{
    Fields = new[] { "*" },
});

```

If you wish to expand more than one level of relations, you can use the following syntax.

``` C#

await restClient.GetItems<NewsDto>(new GetAllArgs()
{
    Fields = new[] { "Id", "Title", "Thumbnail(Title, ThumbnailUrl, Parent(Title, Description), Tags)" },
});

```

In the above example Thumbnail is a Related image and Parent is the Library of that image.
**Note that the expansion of levels is limited to 2 by default. If you wish to expand more than two levels, the configuration for the web service must be changed in Sitefinity (under Administration/Advanced/WebServices/)**

## Create, Update, Delete

### Prerequisities

Creating items with the SDK is possible, but there are some prerequisities for this. The main prerequiesite is that the request must be authenticated. The whole authentication is automatically managed if the current user is logged-in (provided he has already been logged-in using the already provided [LoginWidget](https://www.progress.com/documentation/sitefinity-cms/login-form-widget-net-core) and has the necessary permissions. There is no action needed here as the needed data will be sent automatically with the request to authenticate the user with the CMS.

A namespace import is required as well. All of the methods needed are provided in the namespaces 

* **Progress.Sitefinity.RestSdk**
* **Progress.Sitefinity.RestSdk.Management**

### Creating items
Items can be created by using the CreateItem method.

```C#
await restClient.CreateItem(new NewsDto() 
{ 
    Title = "Sample title" 
});

```

Custom fields for predefined types can be set via the following code

``` c#
var newsItem = new NewsDto() 
{ 
    Title = "Sample title" 
};

newsItem.SetValue<string>("MyCustomFieldName", "My custom field value");

await restClient.CreateItem(newsItem);

```

Also with the dynamic API

``` C#
var item = await restClient.CreateItem<SdkItem>(new CreateArgs()
{
    Type = RestClientContentTypes.News,
    Data = new 
    { 
        Title = "Sample title",
        MyCustomFieldName = "My custom field value"
    },
}, /* optional providerName parameter */);

```

### Updateing items
Items can be updated by using the UpdateItem method.

```C#
await restClient.UpdateItem(new NewsDto() 
{
    Id = "ID_OF_THE_ITEM",
    // Privider = "PROVIDER_OF_THE_ITEM" //optional
    Title = "Sample title" 
});

```

Custom fields for predefined types can be set via the following code

``` c#
var newsItem = new NewsDto() 
{ 
    Id = "ID_OF_THE_ITEM",
    // Privider = "PROVIDER_OF_THE_ITEM" //optional
    Title = "Sample title" 
};

newsItem.SetValue<string>("MyCustomFieldName", "My custom field value");

await restClient.UpdateItem(newsItem);

```

Also with the dynamic API

``` C#
await restClient.UpdateItem(new UpdateArgs()
{
    Type = RestClientContentTypes.News,
    Data = new 
    {
        Id = "ID_OF_THE_ITEM",
        // Privider = "PROVIDER_OF_THE_ITEM" //optional
        Title = "Sample title",
        MyCustomFieldName = "My custom field value"
    },
});

```

### Deleting items

Items can be deleted by using the DeleteItem method.

```C#
await restClient.DeleteItem(new NewsDto() 
{
    Id = "ID_OF_THE_ITEM",
    // Privider = "PROVIDER_OF_THE_ITEM" //optional
});

```

Also with the dynamic API

``` C#

await restClient.DeleteItem(new DeleteArgs()
{
    Type = RestClientContentTypes.News,
    Id = item.Id,
    // Privider = "PROVIDER_OF_THE_ITEM" //optional
});

```