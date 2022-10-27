# Script widget

Script widget sample used for dynamically inserting scripts inside the page. The available options are:

* Inline
* BodyTop
* BodyBottom

## Inline mode

In inline mode the script widget just renders the content of the textarea in the widget designer on the place where the widget is dropped in the WYSIWYG editor

## BodyTop and BodyBottom

In these two modes the script widget registers the content of the textarea with the [IScriptRepository](./TagHelpers/IScriptRepository.cs) class and later on that
script is read from the [ScriptTagHelperTag](./TagHelpers/ScriptTagHelper.cs) class and the content is placed at the begining or the end of the body tag.

## Prerequisites

* The registration of the [IScriptRepository](./TagHelpers/IScriptRepository.cs) class in the Program.cs file is necessary for this sample to work
* The tag helper registration in the [_Layout.cshtml](./Views/Shared/_Layout.cshtml) file is necesary. That means this custom base layout is required for every page where the ScriptWidget is placed


