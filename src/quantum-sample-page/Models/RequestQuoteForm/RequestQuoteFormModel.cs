using System.ComponentModel.DataAnnotations;

namespace Renderer.Models
{
    public class RequestQuoteFormModel
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
        public int PhoneNumber { get; set; }        

        [MaxLength(150)]
        [Display(Name = "YourMessage")]
        public string YourMessage { get; set; }
    }
}