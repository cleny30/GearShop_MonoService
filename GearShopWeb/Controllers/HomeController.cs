using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GearShopWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }
    }
}
