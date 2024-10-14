using DTOLayer.DTOs.AppRoleDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<IActionResult> CreateRole(CreateRoleDTO createRoleDTO)
		{
			var appRole = new AppRole
			{
				Name = createRoleDTO.RoleName
			
			};

			var result = await _roleManager.CreateAsync(appRole);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Role", new { area = "Admin" });
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Rol eklenirken bir hata oluştu.");
				return View(createRoleDTO);
			}

		}

		[Route("DeleteRole/{id}")]
		public async Task<IActionResult> DeleteRole(int id)
		{

			var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
			await _roleManager.DeleteAsync(role);
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("EditRole/{id}")]
		public async Task<IActionResult> EditRole(int id)
		{
			var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
				EditRoleDTO editRoleDTO = new EditRoleDTO
				{
					RoleID = value.Id,
					RoleName = value.Name
				};
			return View(editRoleDTO);
		}

		[HttpPost]
		[Route("EditRole/{id}")]
		public async Task< IActionResult> EditRole(EditRoleDTO editRoleDTO)
		{
			var value = _roleManager.Roles.FirstOrDefault(x => x.Id == editRoleDTO.RoleID);
			value.Name=editRoleDTO.RoleName;
			await _roleManager.UpdateAsync(value);
			return RedirectToAction("Index");

		}


	}
}
