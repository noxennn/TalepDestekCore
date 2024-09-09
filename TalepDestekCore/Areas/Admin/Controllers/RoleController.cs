using DTOLayer.DTOs.AppRoleDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Role")]
	public class RoleController : Controller
	{
		private readonly RoleManager<AppRole> _roleManager;

		public RoleController(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[Route("Index")]
		public IActionResult Index()
		{
			var values = _roleManager.Roles.ToList();
			return View(values);
		}


		[HttpGet]
		[Route("CreateRole")]
		public IActionResult CreateRole()
		{
			return View();
		}


		[HttpPost]
		[Route("CreateRole")]
		public async Task<IActionResult> CreateRole(CreateRoleDTO p)
		{
			var appRole = new AppRole
			{
				Name = p.RoleName
			};

			var result = await _roleManager.CreateAsync(appRole);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Role", new { area = "Admin" });
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Rol eklenirken bir hata oluştu.");
				return View(p);
			}

		}
	}
}
