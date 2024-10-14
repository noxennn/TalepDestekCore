using DTOLayer.DTOs.ProfileDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using TalepDestekCore.Models;

namespace TalepDestekCore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.GenderList = new List<SelectListItem>
        {
            new SelectListItem { Text = "Erkek", Value = "Erkek" },
            new SelectListItem { Text = "Kadın", Value = "Kadın" }
        };

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.UserRole = roles.FirstOrDefault();
            ViewBag.UserTcKimlikNo = user.UserName;

            var userProfile = new EditProfileDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Gender = user.Gender,
                ImageURL = user.ImageURL,
                PhoneNumber = user.PhoneNumber
            };

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EditProfileDTO userProfile, IFormFile? profileImage)
        {
            if (!ModelState.IsValid)
            {
                // Model geçerli değilse, formu tekrar göster.
                ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Erkek", Value = "Erkek" },
                new SelectListItem { Text = "Kadın", Value = "Kadın" }
            };

                var user = await _userManager.GetUserAsync(User);
                var role = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = role.FirstOrDefault();
                ViewBag.UserTcKimlikNo = user.UserName;

                return View(userProfile);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fotoğraf yüklenmişse işlemi yap
            if (profileImage != null && profileImage.Length > 0)
            {
                // Eski fotoğrafı sil
                if (!string.IsNullOrEmpty(currentUser.ImageURL))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, "Userpfp", currentUser.ImageURL);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                // Yeni fotoğrafı ekle
                var fileName = Path.GetFileName(profileImage.FileName);
                fileName= Guid.NewGuid().ToString() + fileName;
                var filePath = Path.Combine(_environment.WebRootPath, "Userpfp", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                currentUser.ImageURL = fileName;  // Kullanıcı modelinde fotoğraf URL'ini güncelle
            }

            // Kullanıcı bilgilerini güncelle
            currentUser.Id = userProfile.Id;
            currentUser.Name = userProfile.Name;
            currentUser.Surname = userProfile.Surname;
            currentUser.Email = userProfile.Email;
            currentUser.Gender = userProfile.Gender;
            currentUser.PhoneNumber = userProfile.PhoneNumber;

            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(currentUser);
                TempData["SuccessMessage"] = "Profiliniz başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Eğer güncelleme başarısızsa, formu tekrar göster.
            ViewBag.GenderList = new List<SelectListItem>
        {
            new SelectListItem { Text = "Erkek", Value = "Erkek" },
            new SelectListItem { Text = "Kadın", Value = "Kadın" }
        };

            var roles = await _userManager.GetRolesAsync(currentUser);
            ViewBag.UserRole = roles.FirstOrDefault();
            ViewBag.UserTcKimlikNo = currentUser.UserName;

            return View(userProfile);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserName = user.UserName;
            ViewBag.Name = user.Name;
            ViewBag.Surname = user.Surname;
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
		{
			if (ModelState.IsValid)
			{
				// Şifre güncelleme işlemi (daha önceki adımlarda gösterildi)
				var user = await _userManager.GetUserAsync(User);
				var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
				if (result.Succeeded)
				{
					await _signInManager.SignOutAsync();
					return RedirectToAction("SignIn", "Account");
				}

				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
				return View(model);
			}

			
			return View(model);
		}
	}

}
