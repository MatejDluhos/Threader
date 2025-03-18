using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThreaderApp.Models;
using ThreaderApp.ViewModels;

namespace ThreaderApp.Controllers
{
    public class AuthController : Controller
    {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ILogger<HomeController> _logger;

		public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
		}
		
        public IActionResult Login()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Register()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User {FullName = model.FullName, UserName = model.UserName, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					// Sign the user in after registration
					await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home"); // Redirect to the home page or dashboard
				}
				// If creation fails, add the errors to ModelState and show them in the view
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			// Return to the view if the model is invalid
			return View(model);
		}
	}
}
