using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Student.Controllers
{
    public class DefaultController : Controller
    {
        [Area("Student")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
