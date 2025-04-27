using CompanyG02.DAL.Models;
using CompanyG02.PL.Helper;
using CompanyG02.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyG02.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		#region Register
		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        { 
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree

                };
                var Result = await userManager.CreateAsync(User,model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                foreach (var error in Result.Errors) 
                {
                    ModelState.AddModelError(string.Empty, error.Description); 
                }

            }
            return View(model);
        }
		#endregion
		#region Login
		public IActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				
				if (user is not null)
				{
                    var flag = await userManager.CheckPasswordAsync(user,model.Password);
                    if (flag) 
                    {
                        var Result=await signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index","Home");
                        }
					}
                        ModelState.AddModelError(string.Empty, "Password Is Invalid");
				}
				
					ModelState.AddModelError(string.Empty, "Email Is Invalid");
				

			}
			return View(model);
		}
        //Get
        public IActionResult ForgetPassword()
        {  return View(); }
        //post
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)//server side validation
            {
                var User=await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                { //Account/ResetPassword
                    var Token=await userManager.GeneratePasswordResetTokenAsync(User); //Token Valid for 1 Time
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = User.Email,token=Token}, Request.Scheme);
					//https:\\localhost:44302\Account\ResetPassword\mh0056592@gmail.com&tokesdcefvrgt52rfgdtfhgyj
					var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = User.Email,
                        Body= PasswordResetLink
					};//Helper Function => Static Function
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheCkYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is not Valid");
              
            }
            return View(model);
        }
        public IActionResult CheCkYourInbox()
        {
            return View();
        }
        //httpGet
		public IActionResult ResetPassword(string email,string token) 
		{
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model) 
		{
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var user=await userManager.FindByEmailAsync(email);
                var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                   return RedirectToAction(nameof (Login));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);    }
			}
          
            return View();
        }
		#endregion
		#region SiginOut
		public new async Task<IActionResult>SiginOut()
        { 
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


		#endregion
	}
}
