# Sitefinity insight

This project demonstrates how to submit custom data to sitefinity insight.

The way this is done is by including a custom script inside the view where it will be used - see Views/Shared/Components/HelloInsight/Default.cshtml. The scripts themselves are located in the wwwroot folder - one for development and one minified for production. This is controlled with the environment tag helper inside the above mentioned view. The script is very simple - it attaches itself to the click event of the anchor tag and if the Sitefinity Insight script is loaded, then the script will submit a request and then navigate to the requested page.
