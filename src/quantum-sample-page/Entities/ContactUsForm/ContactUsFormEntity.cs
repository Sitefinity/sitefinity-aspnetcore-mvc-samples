using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Renderer.Entities.ContactUsForm
{
    public class ContactUsFormEntity
    {
        /// <summary>
        /// Gets or sets the heading of the form.
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Gets or sets the message upon successfull form submition.
        /// </summary>
        public string SuccessMessage { get; set; }
    }
}
