using System.ComponentModel.DataAnnotations;

namespace CompanyG02.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="FirstName Is Required")]
		public string FName { get; set; }
		[Required(ErrorMessage = "LastName Is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password Is Required")]

		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[Compare("Password",ErrorMessage = "Confirm Password Does not Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		public bool IsAgree {  get; set; }



	}
}
