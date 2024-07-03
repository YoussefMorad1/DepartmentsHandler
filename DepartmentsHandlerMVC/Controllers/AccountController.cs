using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL_PresentationLayerMVC.Helpers;
using PL_PresentationLayerMVC.ViewModels;
using System.Threading.Tasks;

namespace PL_PresentationLayerMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		#region Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUser
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					FName = model.FName,
					LName = model.LName,
					IsAgree = model.IsAgree
				};
				var result = await userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					return RedirectToAction("Login");
				foreach (var err in result.Errors)
					ModelState.AddModelError(string.Empty, err.Description);
			}
			return View(model);
		}
		#endregion

		#region Login
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user != null)
			{
				var passCheck = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
				if (passCheck.Succeeded)
					return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError(string.Empty, "The Email and Password are not matched");
			return View(model);
		}
		#endregion
		// Logout
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var token = await userManager.GeneratePasswordResetTokenAsync(user);
					var resetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);
					var email = new Email
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = resetLink
					};
					EmailHelper.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "The Email is not registered");
			}
			return View(nameof(ForgetPassword), model);
		}

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
				return RedirectToAction(nameof(Login));
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;
				var user = await userManager.FindByEmailAsync(email);
				if (user != null)
				{
					var result = await userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
						return RedirectToAction(nameof(Login));
					foreach (var err in result.Errors)
						ModelState.AddModelError(string.Empty, err.Description);
				}
				else
					ModelState.AddModelError(string.Empty, "The Email is not registered");
			}
			return View(model);
		}
	}

}
