using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DTOLayer.DTOs.AppRoleDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TalepDestekCore.Areas.Admin.Models;

namespace TalepDestekCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/User")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IOfficerUnitService _officerUnitService;
        private readonly IUnitService  _unitService;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOfficerUnitService officerUnitService, IUnitService unitService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _officerUnitService = officerUnitService;
            _unitService = unitService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var users = _userManager.Users.Select(u => new UserViewModel
            {
                UserID = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                Surname = u.Surname,
                Gender = u.Gender,
                Email = u.Email,
                UserRole = _userManager.GetRolesAsync(u).Result.FirstOrDefault() // her kullanıcıda bir rol
            }).ToList();


            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u =>
                            u.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            u.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            u.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            u.UserRole.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }

            return View(users);
        }




        [HttpGet]
        [Route("AssignRole/{userId}")]
        public async Task<IActionResult> AssignRole(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);

            // Rolleri getiriyoruz
            var roles = _roleManager.Roles.ToList();

            // SelectListItem oluşturuyoruz ve kullanıcıya ait rolü seçili yapıyoruz
            ViewBag.Roles = roles.Select(role => new SelectListItem
            {
                Value = role.Id.ToString(),
                Text = role.Name,
                Selected = userRoles.Contains(role.Name) // Mevcut rolün seçili olmasını sağlıyoruz
            }).ToList();

            // Kullanıcı bilgisi de ViewBag ile gönderiliyor
            ViewBag.UserName = user.UserName;
            ViewBag.Surname = user.Surname;
            ViewBag.Name= user.Name;
            ViewBag.UserId = user.Id;

            return View();
        }


        [HttpPost]
        [Route("AssignRole/{userId}")]
        public async Task<IActionResult> AssignRole(int userId, int SelectedRoleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Mevcut rolleri kaldırıyoruz
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            // Yeni rolü ekliyoruz
            var selectedRole = await _roleManager.FindByIdAsync(SelectedRoleId.ToString());
            if (selectedRole != null)
            {
                await _userManager.AddToRoleAsync(user, selectedRole.Name);
            }

            return RedirectToAction("Index");
        }




        [HttpGet]
        [Route("AssignUnit/{userId}")]
        public  IActionResult AssignUnit(int userId)
        {
            //
            var assignedUnitIds = _officerUnitService.TGetUnitIDsByOfficerID(userId);
            // tüm ünitleri getir
            var allUnits = _unitService.TGetList();

            //  Unit atama içiv viewmodel oluştur
            var model = allUnits.Select(unit => new AssignUnitViewModel
            {
                UnitID = unit.UnitID,
                UnitName = unit.UnitName,
                IsAssigned = assignedUnitIds.Contains(unit.UnitID) // Kullanıcıya atanmış mı kontrol et
            }).ToList();

            // Viewe model gönder
            ViewBag.OfficerId = userId;
            return View(model);
        }


        [HttpPost]
        [Route("AssignUnit/{userId}")]
        public IActionResult AssignUnit(int userId, List<AssignUnitViewModel> model)
        {
            // Kullanıcının mevcut birimlerini al
            var existingUnitIDs = _officerUnitService.TGetUnitIDsByOfficerID(userId).ToList();

            // Silinecek ve eklenecek birimleri belirle
            var unitsToRemove = new List<int>();
            var unitsToAdd = new List<int>();

            foreach (var unit in model)
            {
                if (unit.IsAssigned)
                {
                    // Eğer birim atanmışsa ve mevcut birimlerin arasında yoksa ekle
                    if (!existingUnitIDs.Contains(unit.UnitID))
                    {
                        unitsToAdd.Add(unit.UnitID);
                    }
                }
                else
                {
                    // Eğer birim atanmış değilse ve mevcut birimlerin arasında varsa sil
                    if (existingUnitIDs.Contains(unit.UnitID))
                    {
                        unitsToRemove.Add(unit.UnitID);
                    }
                }
            }

            // Silinecek birimleri kaldır
            foreach (var unitId in unitsToRemove)
            {
                _officerUnitService.TRemoveOfficerUnit(unitId, userId);
            }

            // Yeni atanacak birimleri ekle
            foreach (var unitId in unitsToAdd)
            {
                _officerUnitService.TAddOfficerUnit(unitId, userId);
            }

            return RedirectToAction("Index"); // İşlem tamamlandıktan sonra listeye geri dön
        }







    }
}
