Custom Progress Agentic RAG widgets
======

This guide demonstrates how to create and configure a set of custom widgets powered by Progress Agentic RAG.

## Available Widgets
- AI Assistant Chat
- AI Ask Box
- AI Answer
- AI Search Results

## Installing the widgets

Follow these steps to install the custom Agentic RAG widgets in your Sitefinity application:

## Sitefinity setup
1. Clone the [mvc-samples](https://github.com/Sitefinity/mvc-samples) repository.
2. Check Sitefinity NuGet versions - Ensure that the Sitefinity NuGet package versions used in the sample match those used in your project.If they differ, update the NuGet references in the widget project to match your Sitefinity version.
3. Build the `ProgressAgenticRAGWidgets` project.
4. Reference the **PARAGCore** DLL - Add references to the **PARAGCore** DLL in your Sitefinity web application.
5. Create a `Global.asax.cs` file, if your Sitefinity web application doesn’t already include one.
6. Configure the `Global.asax.cs` file - Modify its contents to match the sample located at: `{{YOUR_PROJECT}}/SitefinityWebApp/Global.asax.cs`.
7. If you are using a Sitefinity version >= 15.4.8623 you should do one of the following:
	- If you want to use the sample widgets instead of the built-in ones, disable the `Progress Agentic RAG connector` module.
	- If you want to use a mix of the sample and the built-in widgets:
		- remove the `PARAGOperationProvider` registration from the `Global.asax.cs` file.
		- populate the _PARAG_ and the _AgenticRAG_ configurations with the same values.
8. Rebuild your Sitefinity web application - Once built, the new widgets will appear in your Sitefinity Page Toolbox.

## Renderer setup
1. Open `appsettings.json` file.
2. Under **Sitefinity » Url** enter your Sitefinity URL
3. Under **SitefinityAssistant » CdnHostName** enter `cdn.assistant.cloud.sitefinity.com`.
4. Open `Program.cs` file.
5. Update the `cspDirectives` value for `ScriptSrc`, `StyleSrc` and `ImgSrc` with the `CdnHostName` value.
6. Start the project.

## Configuring the Widgets

### Configure Sitefinity CMS

Before you can use Progress Agentic RAG, you must configure the respective setting in Sitefinity CMS:

  1. Log in to the [Progress Agentic RAG Dashboard](https://rag.progress.cloud/).
  1. From the _NucliaDB API endpoint_, copy **the host part** of the URL without the rest of the URL.
     Save the value somewhere, for example &ndash; in Notepad.<br>
     You will need this value later.
  1. Copy the _Knowledge Box_ and save it somewhere.
  1. In the Agentic RAG Dashboard, navigate to _Advanced » API Keys_
  1. Create a new API key, copy it, and save it somewhere, for example &ndash; in Notepad.
  1. In Sitefinity CMS backend, navigate to _Administration » Settings » Advanced_.
  1. In the tree on the left, expand the _PARAG » Knowledge Boxes_ node.
  1. Click _Create new_.
  1. In _Base URL_, paste the endpoint you from _Step 2_.<br>
     For example, <code>https://europe-1.rag.progress.cloud</code>.
  1. In _Knowledge box UID_, paste the UID from _Step 3_.
  1. In _Knowledge box API key_, paste the API key from _Step 5_.
  1. Save your changes.

### Configure Assistant Settings

To configure the Sitefinity AI Assistant settings, perform the following:

  1. In Sitefinity CMS backend, navigate to _Administration » Settings » Advanced_.
  1. In the tree on the left, expand the _PARAG » Assistant_ node.
  1. In _AdminApiBaseUrl_ enter `https://api.sitefinity.cloud/Version`.
  1. In _CdnHostName_ enter `cdn.assistant.cloud.sitefinity.com`.
