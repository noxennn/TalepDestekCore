﻿using Microsoft.AspNetCore.Mvc;

namespace TalepDestekCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
