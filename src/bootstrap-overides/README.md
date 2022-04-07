# Customize Bootstrap variables

## Using npm scripts to generate CSS bundle
1. Navigate to `client-src` and open terminal
2. Run `npm i` - this will install all the needed dependencies
3. Run `npm start` - this will generate CSS bundle that contains the **Bootstrap** and **Sitefinity** (`_sitefinity.scss`) stylesheets. The bundle is called `project[.min].css` and it is located inside `wwwroot` direcotry. Also this script will watch the `.scss` files inside `client-src/scss` and will generate new CSS bundle if changed.

## Below are shown 2 options to overide the !default Bootstrap variables
### Using Dart sass modules

In `project.scss` do the following
```
@use "../node_modules/bootstrap/scss/bootstrap.scss" with (
    $body-bg: #789,
    $grid-breakpoints: (
        xs: 0,
        sm: 371px,
        md: 768px,
        lg: 992px,
        xl: 1200px,
        xxl: 1400px
    )
);
```

### Using @import - this approach is not recommended as @import statement might be deprecated sometime in the future: Read more [here](https://sass-lang.com/documentation/at-rules/import)

In `project.scss` do the following
```
$body-bg: #789;
$grid-breakpoints: (
    xs: 0,
    sm: 371px,
    md: 768px,
    lg: 992px,
    xl: 1200px,
    xxl: 1400px
);

@import "./node_modules/bootstrap/scss/bootstrap.scss";
```
**Note:** This approach lets you import only what you need as mentioned [here](https://getbootstrap.com/docs/5.0/customize/sass/) in the Bootstrap documentation
```
// Required
@import "../node_modules/bootstrap/scss/functions";

// Default variable overrides
$body-bg: #000;
$body-color: #111;

// Required
@import "../node_modules/bootstrap/scss/variables";
@import "../node_modules/bootstrap/scss/mixins";

// Optional Bootstrap components here
@import "../node_modules/bootstrap/scss/root";
@import "../node_modules/bootstrap/scss/reboot";
@import "../node_modules/bootstrap/scss/type";
// etc
```
