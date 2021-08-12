# Extend Content List Widget
An example on how to extend the views of the content list widget by providing an implementation for a [custom list view](./Views/Shared/Components/ContentList/CardsListCustom.cshtml) and a [custom details view](./Views/Shared/Components/ContentList/Details.Custom.cshtml)

## List Views

To define your own set of detail views in the code, you must do the following:

1. Create a .cshtml file under Views/Shared/Componenents/ContentList/ directory in the root web app (the directory structure must be manually created if not already present).

2. The file name must not start or end with “Details”

3. The model for the view must be of type:
@model Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList.ContentListViewModel

The ContentListViewModel class has a method T GetValue<T>(string name) which works with the Field mappings described above. The name argument of the function accepts one of the mappings defined for this view and automatically maps the name to the field name selected in the user interface for the current content type.

Additionally, the ContentListViewModel has a property called “Items” which contains all the content items that need to be rendered. These items are of type Progress.Sitefinity.RestSdk.Dto.SdkItem, which is the base class for working with the Rest services in Sitefinity.

## ViewsMetadata.json

The ViewsMetadata.json file must reside in the directory "Views/Shared/Componenents/ContentList/ViewsMetadata.json". It contains all of the mappings between views and their coresspnding metadata.

### Mappings
The metadata for mappings serves the purpose to create reusable views that can be used across diffrent content types. This is achieved by providing a list of fields and their types as a json entry in the ViewsMetadata.json file. Thus when selecting different content types, each of the provided fields by the view can be mapped to a coresponding field of the selected content type. An example can be found [here](./Views/Shared/Componenents/ContentList/ViewsMetadata.json)

The supported field types are the following:
* ShortText - for string fields
* LongText - for html fields
* Text - for all kinds of string fields (including LongText)
* YesNo - for boolean fields
* DateTime - for dates
* Number - for all kinds of numbers
* Classification - for classifications like Tags, Categories etc..
* Address - for address fields
* RelatedData - for related data like News, Blogs, Dynamic
* RelatedImages - for related images
* RelatedVideos - for related videos
* RelatedDocuments - for related documents

All of these fields are almost a 1:1 mapping with the custom fields dialog in the administrative interface. Additonally they can be used for any kind of .Net CLR type that matches the description. The entries for the mappings are case sensitive, so be sure to provide an exact match of the field.

## Detail Views

To define your own set of detail views in the code, you must do the following:

1. Create a .cshtml file under Views/Shared/Componenents/ContentList/ directory in the root web app (the directory structure must be manually created if not already present).

2. The file name must be in the format “Details.{customname}.cshtml”

3. The model for the view must be of type:
@model Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList.ContentDetailViewModel

The ContentDetailViewModel contains a property called “Item” which refers to the currently rendered item. The item property is of type Progress.Sitefinity.RestSdk.Dto.SdkItem. This class contains a method called T GetValue<T>(string fieldName) by which the field values of the content item can be accessed.

Example for accessing a ShortText(or any string) field value:

Model.Item.GetValue<string>("MyShortTextFieldName")).

## Fields

To render the diffrent kinds of fields in both details and list mode refer to [this sample project](../all-fields/Views/Shared/Fields)