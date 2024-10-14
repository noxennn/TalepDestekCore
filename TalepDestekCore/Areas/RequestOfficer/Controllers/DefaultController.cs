using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.RequestOfficer.Controllers
{
    [Area("RequestOfficer")]
    [Route("RequestOfficer/Default")]
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


            var OfficerUnitIDs =_officerUnitService.TGetUnitIDsByOfficerID(OfficerID);

            //Talep yetkilisinin kendi gönderdiği talepler
            ViewBag.MyRequestsPendingCount = _requestService.TGetCountPendingRequestForRequestOwner(OfficerID);
            ViewBag.MyRequestsActiveCounts = _requestService.TGetCountActiveRequestForRequestOwner(OfficerID);
            ViewBag.MyRequestsInactiveCounts = _requestService.TGetCountInactiveRequestForRequestOwner(OfficerID);

            //Talep yetkilisinin baktığı talepler
            ViewBag.RequestsPendingCounts = _requestService.TGetCountPendingRequestForRequestOfficer(OfficerID,OfficerUnitIDs);
            ViewBag.RequestsActiveCounts = _requestService.TGetCountActiveRequestForRequestOfficer(OfficerID,OfficerUnitIDs);
            ViewBag.RequestsInactiveCounts = _requestService.TGetCountInactiveRequestForRequestOfficer(OfficerID,OfficerUnitIDs);



            return View();
        }
    }
}
