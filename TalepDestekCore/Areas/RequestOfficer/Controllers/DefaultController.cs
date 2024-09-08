using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.RequestOfficer.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
