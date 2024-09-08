using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
