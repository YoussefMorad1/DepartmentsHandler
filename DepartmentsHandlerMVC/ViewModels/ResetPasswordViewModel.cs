using System.ComponentModel.DataAnnotations;

namespace PL_PresentationLayerMVC.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage="New Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage="Confirm Password is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage="Password and Confirm Password do not match")]
		public string ConfirmPassword { get; set; }
	}
}
