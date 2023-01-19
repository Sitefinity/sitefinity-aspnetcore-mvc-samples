# Foundation Package #

This is a package, which contains all default templates of the frontend widgets with all classes replaced with Foundation CSS classes. In order to use it you need to add the package to the `ResourcePackages` folder of your project. If the `ResourcePackages` folder doesn't contain any packages, widget templates will be loaded from *Embedded resources*. The priority for loading templates is as follows (in descending order):
1. Templates from the resource package
2. Templates from the *Embedded resources* source

**Note**: Keep in mind that if Sitefinity doesn't find a template for a built-in widget in your package, it will use the default embedded one. This is the difference between the .NET Core packages and the MVC packages.

## Npm
### Install
Npm is the package manager for JavaScript that enables you to assemble packages of reusable code. For more information, visit [Node.js](https://nodejs.org/) and [npm](https://www.npmjs.com/) websites. Currently *Foundation* package supports Node.js LTS version 18.13.0.
```
> npm install
> npm start
```
`npm start` executes the default **npm** scritps and subsequently watches for any changes in the files after.

## Package structure ##

The Foundation package contains Foundation assets and widget templates. Following is a hierarchical list of the major folders and files that come with the *Foundation* package.
 - **assets** - contains frontend files such as CSS, JS, images and SVG sprites
    - **dist** - contains the processed ready-to-use frontend assets
        - **images** - contains compressed images from the project's `src` folder. These images are usually used as background images in the CSS.
        - **js** - contains a js file which are listed in `package.json` file in `config.copyJs` section. To use this file add a reference to it in the package Razor layout file `MVC/Views/Layouts/default.cshtml`
        - **css** - contains the processed CSS files
            - **main.css** - this is output of the processed `main.scss` from `assets/src/project/sass`. This file contains Sitefinity CMS and the project itself
            - **main.min.css** - this is the minified version of the `main.css` file. The `main.min.css` file is the distributed CSS file that is linked in the `MVC/Views/Layouts/default.cshtml` Razor layout file of the package.
            - **sitefinity.css** - this is output of the processed `sitefinity.scss` from `assets/src/sitefinity/sass`. This files contain CSS added by Sitefinity CMS
            - **sitefinity.min.css** - this is the minified version of `sitefinity.css`
    - **src** - contains the source front-end files which are processed via npm scripts
            - **images** - add images in this folder. These images will be compressed and output to the `assets/dist/images` folder.
            - **js** - add JavaScript files in this folder that need to be moved to the `assets/dist/js` folder. You will need to extend the file paths listed in `package.json` file in `config.copyJs` section.
            - **scss** - create subfolders in this folder to add your SCSS files
                - **main.scss** - import all your SCSS files here. This file will be processed to the `assets/dist/css/main.min.css` folder.
				- **sitefinity** - contains the SCSS files for Sitefinity CMS styling


    Following is an example of how to structure files hierarchically:
    ```     
    | ResourcePackages
    |-- Foundation
    |---- assests
    |------ dist
    |------ src
    |-------- scss
    |---------- sitefinity
	|---------- main.scss
    |-------- images
    |-------- js

    ```
    **IMPORTANT:** We do **not** recommend renaming the subfolders since they are used in npm scripts

- **Views folder** 
    - **Components** - contains all widget templates (categorized by widget) and the Razor layout file
 	- **Shared/default.cshtml** - Razor layout file
	- **Scripts** - contains overrides for default widget scripts
- **appsettings.json** - contains a styling config per package
- **package.json** - stores metadata that the project requires and all the **npm** scripts that are used for the build

## Create and modify widget templates
By default, all widget templates are included in the Foundation package. To modify a widget template, simply open the respective template, say, *Horizontal* navigation template. To do so, navigate to `/ResourcePackages/Foundation/Views/Shared/Components/SitefinityNavigation`, open the `Horizontal.cshtml` file, and make your changes.

Creating a new template is just as simple:
1. Duplicate an existing template.
2. Rename the file: `XXXXXX.cshtml`.
**NOTE:** For Content List widget, the naming convention for the detail views should be `Detail.XXXXXX.cshtml`, respectively.

As a result, the new template is displayed in widget designer for this widget in the list with templates.

## Recommendations for managing widget templates

### Modifying widget templates

To make upgrades easier, we recommend not to change the default widget templates. If you need to make changes to a default template, we recommend that you create a new one by duplicating the existing template you want to modify. You then make the changes on the newly created template.

## Location of project frontend assets
All project-specific frontend assets like SCSS, images, JavaScript files, and so on need to be placed in the `assets/src` folder. When the `npm start` is run, all source files are processed and moved to the `assets/dist` folder, from which they are used in the project.

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
// Sitefinity CSS
@import "sitefinity";

//Import .scss files here
@import "setting/colors";
@import "setting/typography";
...
@import "base/link";
@import "base/typography";
...
```
When you run `npm start`, all SCSS files imported in `assets/src/scss/main.scss` are processed and output in `assets/dist/css/main.css`

If you do not want to include Sitefinity CMS remove the import rule `@import "sitefinity";` in `assets/src/scss/main.scss`.

### Images
Place all images in `assets/src/images`. After `npm start` is run, all images from this folder will be compressed and moved to `assets/dist/images`.

### Javascript
In the `packages.json -> config.copyJs` section, list all your JavaScript files (separated by space) that you want to be moved to `assets/dist/js`.
