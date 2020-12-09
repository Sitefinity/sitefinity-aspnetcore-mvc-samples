using System.ComponentModel.DataAnnotations;

namespace Renderer.Models
{
    public class DemoRequestModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Compnay")]
        public string Company { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(150)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Required]
        public bool IsPrivacyPolicyAccepted { get; set; }
    }
}