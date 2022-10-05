namespace html_sanitizer
{
    public class CustomHtmlSanitizer: Progress.Sitefinity.AspNetCore.Web.Security.HtmlSanitizer
    {
        public CustomHtmlSanitizer()
            : base()
        {
            // add the tel scheme
            this.AllowedSchemes.Add("tel");
        }
    }    
}
