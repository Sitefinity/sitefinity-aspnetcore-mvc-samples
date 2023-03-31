# Minimal package #

This is a basic package, which contains all default templates of the frontend widgets as plain HTML and minimal CSS. It can be used as a foundation for custom MVC packages, as well as packages that are based on frontend frameworks of your choice. In order to use it you need to add the package to the `ResourcePackages` folder of your project. If the `ResourcePackages` folder doesn't contain any packages, widget templates will be loaded from *Embedded resources*. The priority for loading templates is as follows (in descending order):
1. Templates from the resource package
2. Templates from the *Embedded resources* source

**Note**: Keep in mind that if Sitefinity doesn't find a template for a built-in widget in your package, it will use the default embedded one. This is the difference between the .NET Core packages and the MVC packages.

## Npm
### Install
Npm is the package manager for JavaScript that enables you to assemble packages of reusable code. For more information, visit [Node.js](https://nodejs.org/) and [npm](https://www.npmjs.com/) websites. Currently *Minimal* package supports Node.js LTS version 12.17.0.
```
> npm install
> npm start
```
`npm start` executes the default **npm** scritps and subsequently watches for any changes in the files after.

## Package structure ##

The Minimal package contains minimal front-end assets and widget template. Following is a hierarchical list of the major folders and files that come with the *Minimal* package.
 - **assets** - contains frontend files such as CSS, JS, images and SVG sprites
    - **dist** - contains the processed ready-to-use frontend assets
        - **css** - contains the processed CSS files
            - **main.css** - this is output of the processed `main.scss` from `assets/src/scss`. This file contains Sitefinity CMS and the project itself
    - **src** - contains the source front-end files which are processed via grunt to dist folder
        - **scss** - create subfolders in this folder to add your SCSS files
            - **main.scss** - import all your SCSS files here. This file will be processed to the `assets/dist/main.css` folder.

    **IMPORTANT:** We do **not** recommend renaming the subfolders since they are used in npm scripts

- **Views folder** 
    - **Components** - contains all widget templates (categorized by widget) and the Razor layout file
 	- **Shared/default.cshtml** - Razor layout file
	- **Scripts** - contains overrides for default widget scripts
- **appsettings.json** - contains a styling config per package
- **package.json** - stores metadata that the project requires and all the **npm** scripts that are used for the build

## Create and modify widget templates
By default, all widget templates are included in the Minimal package. To modify a widget template, simply open the respective template, say, *Horizontal* navigation template. To do so, navigate to `/ResourcePackages/Minimal/Views/Shared/Components/SitefinityNavigation`, open the `Horizontal.cshtml` file, and make your changes.

Creating a new template is just as simple:
1. Duplicate an existing template.
2. Rename the file: `XXXXXX.cshtml`.
**NOTE:** For Content List widget, the naming convention for the detail views should be `Detail.XXXXXX.cshtml`, respectively.

As a result, the new template is displayed in widget designer for this widget in the list with templates.

## Recommendations for managing widget templates

### Modifying widget templates

To make upgrades easier, we recommend not to change the default widget templates. If you need to make changes to a default template, we recommend that you create a new one by duplicating the existing template you want to modify. You then make the changes on the newly created template.

## Location of project frontend assets
All project-specific frontend assets like SCSS, JavaScript files, and so on need to be placed in the `assets/src` folder. When the `npm start` is run, all source files are processed and moved to the `assets/dist` folder, from which they are used in the project.

### Scss
Place all your SCSS files in the `assets/src/scss` folder. We recommend that you create subfolders to organize the project files and only then import the files in `main.scss`
**Example:**
```
File structure
|- scss
|-- settings
|--- _colors.scss
|--- _typography.scss
...
|-- base
|--- _link.scss
|--- _typography.scss
...

main.scss

//Import .scss files here
@import "setting/colors";
@import "setting/typography";
...
@import "base/link";
@import "base/typography";
...
```
When you run `npm start`, all SCSS files imported in `assets/src/scss/main.scss` are processed and output in `assets/dist/css/main.css`

### Javascript
In the `packages.json -> config.copyJs` section, list all your JavaScript files (separated by space) that you want to be moved to `assets/dist/js`.
