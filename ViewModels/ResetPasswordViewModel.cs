using System.ComponentModel.DataAnnotations;

namespace CompanyG02.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "New Password Is Required")]

		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[Compare("NewPassword", ErrorMessage = "Confirm Password Does not Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
