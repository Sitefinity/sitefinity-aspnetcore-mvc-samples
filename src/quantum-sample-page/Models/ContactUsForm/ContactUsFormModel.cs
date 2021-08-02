using System.ComponentModel.DataAnnotations;
using Renderer.Entities.ContactUsForm;

namespace Renderer.Models.ContactUsForm
{
    public class ContactUsFormModel
    {
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [MaxLength(150)]
        [Display(Name = "YourMessage")]
        public string YourMessage { get; set; }

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