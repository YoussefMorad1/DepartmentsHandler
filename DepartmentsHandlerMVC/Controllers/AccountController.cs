using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL_PresentationLayerMVC.ViewModels;
using System.Threading.Tasks;

namespace PL_PresentationLayerMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;

		public AccountController(UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
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
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
	}
}
