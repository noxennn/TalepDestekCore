using BusinessLayer.Abstract;
using DTOLayer.DTOs.CategoryDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		[Route("Index")]
		public IActionResult Index()
        {
			var activeCategories = _categoryService.TGetListofActiveCategories();
			ViewBag.ActiveCategories = activeCategories;

			var inactiveCategories = _categoryService.TGetListofInactiveCategories();
			ViewBag.InactiveCategories = inactiveCategories;

			return View();
        }

		[HttpGet]
		[Route("CreateCategory")]
		public IActionResult CreateCategory()
		{
			return View();
		}


		[HttpPost]
		[Route("CreateCategory")]
		public IActionResult CreateCategory(CreateCategoryDTO createCategoryDTO)
		{
			if (ModelState.IsValid)
			{
				_categoryService.TInsert(new Category()
				{
					CategoryName = createCategoryDTO.CategoryName,

				});
				return RedirectToAction("Index");
			}
			return View();
		}



		[Route("DeactivateCategory/{id}")]
		public IActionResult DeactivateCategory(int id)
		{
			var category = _categoryService.TGetByID(id);
			category.Status = false; // Birimi pasife al
			_categoryService.TUpdate(category);

			// Pasif birimler tablosunu seçili olarak göndermek için
			return RedirectToAction("Index");
		}



		[Route("ActivateCategory/{id}")]
		public IActionResult ActivateCategory(int id)
		{
			var category = _categoryService.TGetByID(id);
			category.Status = true; // Birimi aktif et
			_categoryService.TUpdate(category);

			// Aktif birimler tablosunu seçili olarak göndermek için
			return RedirectToAction("Index");
		}


		[HttpGet]
		[Route("EditCategory/{id}")]
		public IActionResult EditCategory(int id)
		{
			var category = _categoryService.TGetByID(id);
			var viewModel = new EditCategoryDTO
			{
				CategoryID = category.CategoryID,
				CategoryName = category.CategoryName,
			};
			return View(viewModel);
		}

		[HttpPost]
		[Route("EditCategory/{id}")]
		public IActionResult EditCategory(EditCategoryDTO editCategoryDTO)
		{

			var category = _categoryService.TGetByID(editCategoryDTO.CategoryID);

			category.CategoryName = editCategoryDTO.CategoryName;
			_categoryService.TUpdate(category);

			return RedirectToAction("Index");
		}

	}
}
