using BusinessLayer.Abstract;
using DTOLayer.DTOs.RequestDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TalepDestekCore.Areas.Student.Models;

namespace TalepDestekCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Request")]
	public class RequestController : Controller
	{
		private readonly IUnitService _unitService;
		private readonly ICategoryService _categoryService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IWebHostEnvironment _environment;
		IRequestService _requestService;
		IRequestActivityService _requestActivityService;

		public RequestController(IUnitService unitService, ICategoryService categoryService, IWebHostEnvironment environment, IRequestService requestService, UserManager<AppUser> userManager, IRequestActivityService requestActivityService)
		{
			_unitService = unitService;
			_categoryService = categoryService;
			_environment = environment;
			_requestService = requestService;
			_userManager = userManager;
			_requestActivityService = requestActivityService;
		}
		private List<SelectListItem> GetCategories()
		{
			List<SelectListItem> categories = (from x in _categoryService.TGetListofActiveCategories()
											   select new SelectListItem
											   {
												   Text = x.CategoryName,
												   Value = x.CategoryID.ToString()
											   }).ToList();

			return categories;
		}

		private List<SelectListItem> GetUnits()
		{
			List<SelectListItem> units = (from x in _unitService.TGetListofActiveUnits()
										  select new SelectListItem
										  {
											  Text = x.UnitName,
											  Value = x.UnitID.ToString()
										  }).ToList();


			return units;
		}


		[HttpGet]
		[Route("NewRequest")]
		public IActionResult NewRequest()
		{

			ViewBag.Units = GetUnits();


			ViewBag.Categories = GetCategories();
			return View();
		}


		[HttpPost]
		[Route("NewRequest")]
		public async Task<IActionResult> NewRequest(SendRequestDTO sendRequestDTO, IFormFile? requestFile)
		{
			if (ModelState.IsValid)
			{


				string fileName = null;
				string guidFileName = null;
				var userid = Convert.ToInt32(_userManager.GetUserId(User));


				if (requestFile != null && requestFile.Length > 0)
				{
					if (requestFile.Length > 10 * 1024 * 1024)
					{
						ModelState.AddModelError("File", "Dosya boyutu 10 MB'tan büyük olamaz.");
						return View(sendRequestDTO); // Veya hata mesajını göstereceğiniz uygun bir yere yönlendirin
					}
					// Dosya işlemi
					fileName = Path.GetFileName(requestFile.FileName);
					guidFileName = Guid.NewGuid().ToString() + Path.GetFileName(fileName); // Uzantıyı koruyarak
					var filePath = Path.Combine(_environment.WebRootPath, "RequestDocs", guidFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await requestFile.CopyToAsync(stream);
					}
				}

				// Yeni talebi ekliyoruz ve eklenen talebin ID'sini alıyoruz
				var newRequest = new Request()
				{
					RequestUserID = userid,
					RequestCategoryID = sendRequestDTO.RequestCategoryID,
					RequestLastActivityID = 1, // Pending
					RequestUnitID = sendRequestDTO.RequestUnitID,
					RequestTitle = sendRequestDTO.RequestTitle,
					RequestDescription = sendRequestDTO.RequestDescription,
					RequestDate = DateTime.Now,
					RequestFileUrl = guidFileName,
					RequestFileName = fileName
				};

				// Talebi veritabanına ekleme
				_requestService.TInsert(newRequest);


				var newRequestActivity = new RequestActivity()
				{
					RequestID = newRequest.RequestID,
					ActivityStatusID = 1,//pending
					AssignedUserID = null,//Not initialized
					RequestActivityUserID = null,
					ActivityDescription = $"Talebiniz oluşturuldu : {newRequest.RequestDate.ToString("d MMMM yyyy dddd HH.mm")} ",
					ActivityDate = newRequest.RequestDate,
					FileUrl = null,
					FileName = null,
					Status = true,
				};
				// Talebi veritabanına ekleme
				_requestActivityService.TInsert(newRequestActivity);

				// Detay sayfasına yönlendirme
				return RedirectToAction("Details", new { id = newRequest.RequestID });
			}

			// Eğer ModelState geçerli değilse kategorileri ve birimleri tekrar yüklüyoruz
			ViewBag.Units = GetUnits();
			ViewBag.Categories = GetCategories();
			return View(sendRequestDTO);
		}




		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(int id)
		{
			var requestInfo = _requestService.TGetRequestInfo(id);
			var requestActivities = _requestActivityService.TGetRequestActivityByRequestID(id);
			var requestDetails = new RequestDetailsViewModel()
			{
				RequestActivities = requestActivities,
				Request = requestInfo
			};
			return View(requestDetails);
		}




		[HttpGet]
		[Route("MyRequests")]
		public IActionResult MyRequests(bool showInactiveRequests = false)
		{
			var userid = Convert.ToInt32(_userManager.GetUserId(User));

			var userActiveRequests = _requestService.TGetActiveUserRequests(userid);
			var userInactiveRequests = _requestService.TGetInactiveUserRequests(userid);

			var userRequests = new StudentRequestsViewModel
			{
				ActiveRequests = userActiveRequests,
				InactiveRequests = userInactiveRequests,
				ShowInactiveRequests = showInactiveRequests
			};
			return View(userRequests);
		}


		[HttpGet]
		[Route("AllRequests")]
		public IActionResult AllRequests(bool showInactiveRequests = false)
		{
			var activeRequests = _requestService.TGetAllActiveUserRequests();
			var ınactiveRequests = _requestService.TGetAllInactiveUserRequests();

			var userRequests = new StudentRequestsViewModel
			{
				ActiveRequests = activeRequests,
				InactiveRequests = ınactiveRequests,
				ShowInactiveRequests = showInactiveRequests
			};
			return View(userRequests);
		}

	}

}
