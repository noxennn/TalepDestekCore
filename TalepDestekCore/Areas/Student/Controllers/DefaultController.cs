using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Student.Controllers
{
	[Area("Student")]
	public class DefaultController : Controller
    {
		private readonly IRequestService _requestService;
		private readonly UserManager<AppUser> _userManager;

		public DefaultController(IRequestService requestService, UserManager<AppUser> userManager)
		{
			_requestService = requestService;
			_userManager = userManager;
		}

		public IActionResult Index()
        {
			var studentId = Convert.ToInt32(_userManager.GetUserId(User));

			// View'a verileri aktarıyoruz
			ViewBag.PendingCount = _requestService.TGetCountPendingRequestForRequestOwner(studentId);
			ViewBag.ActiveCount = _requestService.TGetCountActiveRequestForRequestOwner(studentId);
			ViewBag.InactiveCount = _requestService.TGetCountInactiveRequestForRequestOwner(studentId);

			return View();
        }
    }
}
