using System.ComponentModel.DataAnnotations;

namespace PL_PresentationLayerMVC.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required to reset password")]
        public string Email { get; set; }
    }
}
