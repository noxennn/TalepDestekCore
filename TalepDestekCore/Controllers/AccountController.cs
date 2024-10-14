using BusinessLayer.ValidationRules.AppUserValidationRules;
using DTOLayer.DTOs.AppUserDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(AppUserSignInDTO p)
		{


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(p.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

					if (roles.Contains("Admin"))
					{
						return RedirectToAction("Index", "Default", new { area = "Admin" });
					}
					else if (roles.Contains("RequestOfficer"))
					{
						return RedirectToAction("Index", "Default", new { area = "RequestOfficer" });
					}
					else if (roles.Contains("Student"))
					{
						return RedirectToAction("Index", "Default", new { area = "Student" });
					}
					else
                    {
                        ModelState.AddModelError(string.Empty, "Rolünüz bulunamadı, lütfen sistem yöneticinize başvurun.");
                        return View(p);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "TC Kimlik No veya şifreniz yanlış, lütfen tekrar deneyin!");
                    return View(p);
                }
            }
            return View(p);
        }




		//Kayıt olma



		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(AppUserSignUpDTO p)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(p.UserName);
				if (user != null)
				{
					ModelState.AddModelError("UserName", "Bu TC Kimlik Numarasını kullanan bir hesap zaten var");
					return View(p);
				}

				var emailExists = await _userManager.FindByEmailAsync(p.Email);
				if (emailExists != null)
				{
					ModelState.AddModelError("Email", "Bu e-posta adresi alınmış, lütfen başka bir e-posta deneyin.");
					return View(p);
				}

				AppUser appUser = new AppUser()
				{
					Name = p.Name,
					Surname = p.Surname,
					UserName = p.UserName,
					Email = p.Email,
					PhoneNumber = p.PhoneNumber,
					//Şifre eklenmiyor çünkü arka tarafta hashlanerek gönderilecek
				};
				//şifre eşitse
				if (p.Password == p.ConfirmPassword)
				{
					var result = await _userManager.CreateAsync(appUser, p.Password);
					await _userManager.AddToRoleAsync(appUser, "Student");
					if (result.Succeeded)
					{
						return RedirectToAction("SignIn", "Account");
					}
					else
					{
						foreach (var item in result.Errors)
						{
							ModelState.AddModelError("", item.Description);
						}
					}
				}
			}
			
			return View(p);
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn", "Account", new { area = "" });
		}
	}
}
