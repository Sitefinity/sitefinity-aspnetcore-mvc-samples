# Ovierview

With the .NET Core Renderer version 14.0, we introduced a C# RestSdk that can work with Sitefinity content. This REST SDK can be used for both retrieveing and creating content.

## Setup procedure

In order to take advantage of the c# REST SDK, you need to reference the nugget package - "Progress.Sitefinity.RestSdk". This package is build on top of .NET Standard v2.0, so it can be used for both .NET Framework and .NET Core projects.

This assembly is automatically referenced for newly created projects using the [dotnet cli](https://www.progress.com/documentation/sitefinity-cms/setup-the-asp.net-core-renderer#quick-setup-procedure).

## The IRestClient interface

The RestSdk works with the IRestClient interface and can be used through DI. The IRestClient interface has the following primary methods for working with Data ->

* GetItem
* GetItems
* UpdateItem
* DeleteItem
* CreateItem

The API can be split down into two levels:

* Low level API that works with the argument types directly.
* High level API that works with C# classes and LINQ expressions that is entirely based on extension methods calling the low-level APIs. This makes the code reusable and more maintanable.

## The SdkItem class and ISdkItem interface
The SdkItem class is the base class for working with Sitefinity content. It holds the following signature:
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

This class can hold any kind of content(both static and dynamic) retrieved from the server and is usefull if the model is not known in advance, or the model can be changed in the future. The two static properties - Id and Provider are always there on any kind of Sitefinity content item and can be used for other calls to the REST API. The method ****GetValue<T>** is more intresting since it provides access to any kind of defined fields on the server. For examples on the diffrent kinds of fields that this method supports refer to this sample [project](../all-fields/Views/Shared/Fields).

## Explicitly typed content
Sometimes it is better to work with explicitly typed content since the model is known in advance. This is the case for the news item class, but is valid for any kind of dynamic content as well. Here is how the NewsDto represents the News content:

``` C#

/// <summary>
/// Class mapped to the news item class in Sitefinity.
/// </summary>
[MappedSitefinityTypeAttribute(RestClientContentTypes.News)]
public class NewsDto : ContentBaseDto
{
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

**NOTE - The REST SDK provides dtos for all of the static types - news, events, lists, taxons, taxonomies, media, pages and page templates, so you do not need to define them yourself unless you wish for your custom fields to be defined in a custom DTO class**

There are three things to note here:

1. The NewsDto inherits from ContentBaseDto(that holds a generic set of properties), which in turn inherits from SdkItem. This gives us the method GetValue<T>, and the properties Id and Provider

2. The class is marked with the attribute - **MappedSitefinityTypeAttribute** that basically holds the clr type of the mapped sitefinity type. In this case RestClientContentTypes.News = "Telerik.Sitefinity.News.Model.NewsItem"

3. All of the custom properties are defined.

## Taxa items

Working with taxa items is siilar to the way we work with content items. They all must inherit from the TaxonDto class, which in turn inherits from the SdkItem class.

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

The diffrence here is in the **RestClientContentTypes.Tags** constant. It holds the value of "Taxonomy_Tags", which if we generalize is "Taxonomy_{TaxonomyDeveloperName}". If you wish to have a static model defined for a custom taxonomy with the developer name "geographical-regions", then the way you would map it is:

``` c#
    /// <summary>
    /// The taxon class.
    /// </summary>
    [MappedSitefinityTypeAttribute("Taxonomy_geographical-regions")]
    public class TagDto : TaxonDto
    {
    }

```


We will first look at the high-level API and some useful examples for retrieving content.

### Retrieving a single item

Retrieving a single item can be achieved through the extensions methods:

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

### Refreshing an item

Sometimes it is usefull if we have made changes to an item - e.g. Published, Scheduled, Updated to refresh the item. This can be done with the following methods:

``` C#
public static Task<T> RefreshItem<T>(this IRestClient restClient, T item)
    where T : class, ISdkItem


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

The "Provider" property is automatically passed since NewsDto inherits from the SdkItem class and thus is not needed as an additonal argument.

### Get items

Retriving a collection of items can be done by using the methods:

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

These methods accespt an **expression** parameter that is automatically converted to a REST call. These expressions are limited to the bellow usages:

Example usages:

``` C#

// filtering with a collection of ids
var ids = new string[] { item.Id, item2.Id };
var result = await restService.GetItems<NewsDto>(x => ids.Contains(x.Id));

```

``` C#

// filtering by classifications(tags, categories, custom classifications)
var result = await restService.GetItems<NewsDto>(x => x.Tags.Contains(tag.Id));

```

``` C#

// filtering classifications by inverting the filter
var result = await restService.GetItems<NewsDto>(x => !x.Tags.Contains(tag.Id));

```

``` C#

// string filter with StartsWtih
var result = await restService.GetItems<NewsDto>(x => x.Title.StartsWith(subString));

```

``` C#

// string filter with EndsWith
var result = await restService.GetItems<NewsDto>(x => x.Title.EndsWith(subString));

```

``` C#

// string filter with Equals
var result = await restService.GetItems<NewsDto>(x => x.Title.Equals(otherString));

```

``` C#

// string filter with Contains
var result = await restService.GetItems<NewsDto>(x => x.Title.Contains(subString));

```

``` C#

// negated string filters
var result = await restService.GetItems<NewsDto>(x => !x.Title.StartsWith(subString));

```

``` C#

// filtering by basic properties with equals
var result = await restService.GetItems<NewsDto>(x => x.Id == item.Id);

```

``` C#

// filtering by basic properties with not equals
var result = await restService.GetItems<NewsDto>(x => x.Id != item.Id);

```

``` C#

// filtering by greater than operator
var referenceDate = DateTime.UtcNow;
var result = await restService.GetItems<NewsDto>(x => x.PublicationDate > referenceDate.AddHours(-24));

```


``` C#

// filtering by less than operator
var referenceDate = DateTime.UtcNow;
var result = await restService.GetItems<NewsDto>(x => x.PublicationDate < referenceDate.AddHours(-24));

```

``` C#

// filtering by more complex expressions
var result = await restService.GetItems<NewsDto>(x => ((x.Id == item.Id) || (x.Id == item2.Id)));

```

All of the above expressions can be combined in any binary operator form (&& ||).