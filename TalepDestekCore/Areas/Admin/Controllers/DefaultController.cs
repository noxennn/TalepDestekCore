using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("Admin/Default")]
    public class DefaultController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly IOfficerUnitService _officerUnitService;
		private readonly IRequestService _requestService;

		public DefaultController(UserManager<AppUser> userManager, IOfficerUnitService officerUnitService, IRequestService requestService)
		{
			_userManager = userManager;
			_officerUnitService = officerUnitService;
			_requestService = requestService;
		}

		[Route("Index")]
		public IActionResult Index()
		{

			var OfficerID = Convert.ToInt32(_userManager.GetUserId(User));


			var OfficerUnitIDs = _officerUnitService.TGetUnitIDsByOfficerID(OfficerID);

			//Talep yetkilisinin kendi gönderdiği talepler
			ViewBag.AllRequestsPendingCount = _requestService.TGetCountAllPendingRequests();
			ViewBag.AllRequestsActiveCounts = _requestService.TGetCountAllActiveRequests();
			ViewBag.AllRequestsInactiveCounts = _requestService.TGetCountAllInactiveRequests();

			//Talep yetkilisinin baktığı talepler
			ViewBag.MyRequestsPendingCounts = _requestService.TGetCountPendingRequestForRequestOwner(OfficerID);
			ViewBag.MyRequestsActiveCounts = _requestService.TGetCountActiveRequestForRequestOwner(OfficerID);
			ViewBag.MyRequestsInactiveCounts = _requestService.TGetCountInactiveRequestForRequestOwner(OfficerID);



			return View();
		}
	}
}
