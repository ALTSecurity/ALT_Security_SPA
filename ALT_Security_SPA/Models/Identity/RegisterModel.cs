using System.ComponentModel.DataAnnotations;

namespace ALT_Security_SPA.Models.Identity
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(MessageResources), ErrorMessageResourceName = "Required")]
        [MaxLength(255, ErrorMessageResourceType = typeof(MessageResources), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResources), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(MessageResources), ErrorMessageResourceName = "IncorrectEmail")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResources),ErrorMessageResourceName = "Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
