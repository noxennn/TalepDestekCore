using BusinessLayer.Abstract;
using DTOLayer.DTOs.OfficerDTOs;
using DTOLayer.DTOs.RequestDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using TalepDestekCore.Areas.RequestOfficer.Models;
using TalepDestekCore.Areas.Student.Models;
using RequestDetailsViewModel = TalepDestekCore.Areas.RequestOfficer.Models.RequestDetailsViewModel;


namespace TalepDestekCore.Areas.RequestOfficer.Controllers
{
	[Area("RequestOfficer")]
	[Route("RequestOfficer/Request")]


	public class RequestController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IAppUserService _appUserService;
		private readonly IRequestService _requestService;
		private readonly IRequestActivityService _requestActivityService;
		private readonly IOfficerUnitService _officerUnitService;
		private readonly IActivityService _activityService;
		private readonly IWebHostEnvironment _environment;
		private readonly ICategoryService _categoryService;
		private readonly IUnitService _unitService;

		public RequestController(IRequestService requestService, UserManager<AppUser> userManager, IOfficerUnitService officerUnitService, IRequestActivityService requestActivityService, IActivityService activityService, IWebHostEnvironment environment, ICategoryService categoryService, IUnitService unitService, IAppUserService appUserService)
		{
			_requestService = requestService;
			_userManager = userManager;
			_officerUnitService = officerUnitService;
			_requestActivityService = requestActivityService;
			_activityService = activityService;
			_environment = environment;
			_categoryService = categoryService;
			_unitService = unitService;
			_appUserService = appUserService;
		}
		private List<SelectListItem> GetActivities(int? selectedActivityID = null)
		{
			var activities = (from x in _activityService.TGetList()
							  select new SelectListItem
							  {
								  Text = x.ActivityName,
								  Value = x.ActivityID.ToString(),
								  Selected = selectedActivityID.HasValue && x.ActivityID == selectedActivityID.Value
							  }).ToList();

			return activities;
		}
		private List<SelectListItem> GetPriorities(string? selectedPriority = null)
		{
			// Öncelik seçeneklerini tanımlıyoruz
			var priorities = new List<SelectListItem>
			{
				new SelectListItem { Text = "Düşük", Value = "Düşük", Selected = selectedPriority == "Düşük" },
				new SelectListItem { Text = "Normal", Value = "Normal", Selected = selectedPriority == "Normal" },
				new SelectListItem { Text = "Yüksek", Value = "Yüksek", Selected = selectedPriority == "Yüksek" }
			};

			return priorities;
		}




		[HttpGet]
		[Route("Requests")]
		public IActionResult Requests(bool showInactiveRequests = false)
		{
			var userID = Convert.ToInt32(_userManager.GetUserId(User));

			var officerUnitIDs = _officerUnitService.TGetUnitIDsByOfficerID(userID);

			var activeRequestsForOfficer = _requestService.TGetActiveUserRequestsForOfficer(userID, officerUnitIDs);
			var inactiveRequestsForOfficer = _requestService.TGetInactiveUserRequestsForOfficer(userID, officerUnitIDs);

			var requestsForOfficer = new RequestsForOfficerViewModel
			{
				ActiveRequests = activeRequestsForOfficer,
				InactiveRequests = inactiveRequestsForOfficer,
				ShowInactiveRequests = showInactiveRequests
			};

			return View(requestsForOfficer);
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
		[Route("Details/{id}")]
		public IActionResult Details(int id)
		{
			var requestInfo = _requestService.TGetRequestInfo(id);
			var requestActivities = _requestActivityService.TGetRequestActivityByRequestID(id);
			var currentActivityID = requestInfo.RequestLastActivityID;
			var officerUnits = _officerUnitService.TGetUnitIDsByOfficerID(Convert.ToInt32(_userManager.GetUserId(User)));//Kullanıcının Unitlerini al
			ViewBag.OfficerUnits = officerUnits;
			ViewBag.AssignedUserID = requestInfo.AssignedUserID;
			ViewBag.CurrentUserID = Convert.ToInt32(_userManager.GetUserId(User));
			ViewBag.Activities = GetActivities(currentActivityID);

			var requestDetails = new RequestDetailsViewModel
			{
				RequestActivities = requestActivities,
				Request = requestInfo
			};
			return View(requestDetails);
		}
		[HttpPost]
		[Route("Details/{id}")]
		public IActionResult Details(RequestDetailsViewModel model, IFormFile? requestActivityFile)
		{


			// Talebin durmunu güncelle
			model.Request.RequestLastActivityID = model.OfficerActivity.ActivityStatusID;
			model.Request.AssignedUserID = Convert.ToInt32(_userManager.GetUserId(User));
			if (model.OfficerActivity.ActivityStatusID > 2)
			{
				model.Request.Status = false; //talep sonuçlandıysa status false çek kapat
			}
			else
			{
				model.Request.Status = true;
			}
			// Aktif durumu güncelle

			// Talebe atanmış kullanıcıyı güncelle
			var activity = new RequestActivity
			{
				RequestID = model.Request.RequestID,
				ActivityStatusID = model.OfficerActivity.ActivityStatusID,
				AssignedUserID = Convert.ToInt32(_userManager.GetUserId(User)), // Talebe atanmış mevcut kullanıcı
				RequestActivityUserID = Convert.ToInt32(_userManager.GetUserId(User)),// İşlem yapan kullanıcı ID'sini al
				ActivityDescription = model.OfficerActivity.ActivityDescription,
				ActivityDate = DateTime.Now,
				FileUrl = SaveFile(requestActivityFile), // Dosyayı kaydet ve URL'i al
				FileName = requestActivityFile?.FileName,
				Status = true
			};

			// Veritabanına ekle
			_requestActivityService.TInsert(activity);

			// Talebin durumu ve atanmış kullanıcıyı güncelle
			_requestService.TUpdate(model.Request);


			// Başarı mesajı ekleyebilir ve/veya yönlendirme yapabilirsiniz
			TempData["SuccessMessage"] = "Talep başarıyla güncellendi!";
			return RedirectToAction("Details", new { id = model.Request.RequestID });
		}

		//Service'e taşı
		private string SaveFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return null;

			var fileName = Path.GetFileName(file.FileName);
			var guidFileName = Guid.NewGuid().ToString() + Path.GetFileName(fileName); // Uzantıyı koruyarak
			var filePath = Path.Combine(_environment.WebRootPath, "RequestActivityDocs", guidFileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return guidFileName;
		}

		[HttpPost]
		public IActionResult UndoActivity(int id)
		{
			// Geri alınacak aktiviteyi bul
			var activity = _requestActivityService.TGetByID(id);

			// Aktiviteye ait dosyayı kontrol et ve varsa sil
			if (!string.IsNullOrEmpty(activity.FileUrl))
			{
				var filePath = Path.Combine(_environment.WebRootPath, "RequestActivityDocs", activity.FileUrl);
				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
				activity.FileUrl = "İşlem Geri alındığı için silindi";
			}

			// Aktiviteyi geri al (durumunu false yap)
			activity.Status = false;
			_requestActivityService.TUpdate(activity);

			// Talebin önceki aktivitesini bul
			var previousActivity = _requestActivityService.TGetRequestActivityByRequestID(activity.RequestID).FirstOrDefault();


			// Talebin son aktivite durumunu güncelle
			var request = _requestService.TGetByID(activity.RequestID);

			request.RequestLastActivityID = previousActivity.ActivityStatusID;
			//Havale edilmiş olabileceğinden talebe atanmış yetkiliyi güncelle
			request.AssignedUserID = activity.RequestActivityUserID;


			if (previousActivity.ActivityStatusID > 2)
			{
				request.Status = false; //önceki aktiviteye göre talebin durumunu güncelle kapalıysa kapat
			}
			else
			{
				request.Status=true; //açıksa aç
			}

				_requestService.TUpdate(request);

			// Başarı mesajı döndür
			TempData["SuccessMessage"] = "Aktivite başarıyla geri alındı!";
			return RedirectToAction("Details", new { id = request.RequestID });
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
		[Route("AssignOfficer/{requestID}")]
		public IActionResult AssignOfficer(int requestID)
		{
			ViewBag.RequestID = requestID;
			ViewBag.CurrentOfficerID = Convert.ToInt32( _userManager.GetUserId(User));

			var unitID = _requestService.TGetUnitIDByRequestID(requestID);

			var officerIDs = _officerUnitService.TGetOfficerIDsByUnitID(unitID);

			var officers = _appUserService.TGetAssignableOfficerList(officerIDs);

			return View(officers);
		}

		[HttpPost]
		[Route("AssignOfficer/{requestID}")]
		public IActionResult AssignOfficer(int requestID, int selectedOfficerID)
		{
			var request = _requestService.TGetRequestInfo(requestID);
			request.AssignedUserID = selectedOfficerID;
			_requestService.TUpdate(request);

			var requestActivityAssignOfficer = new RequestActivity()
			{
				RequestID = requestID,
				ActivityStatusID = request.RequestLastActivityID,
				AssignedUserID = selectedOfficerID,
				RequestActivityUserID = Convert.ToInt32(_userManager.GetUserId(User)),
				ActivityDescription = "Talep "+_appUserService.TGetUserNameByUserID(selectedOfficerID)+" Adlı Yetkiliye Havale Edildi.",
				ActivityDate = DateTime.Now,
				FileName = null,
				FileUrl = null,
				Status = true,

			};
			_requestActivityService.TInsert(requestActivityAssignOfficer);

			//son durumu al
			//talebe yeni harekette durumu aynen koru ve açıklama : Havale edildi .. kişisine
			//İşlem yapanın id sini harekete ekle
			//Talepe atanmış kullanıcıyı güncelle
			//
			return RedirectToAction("Details", new { id = requestID });
		}

	}
}
