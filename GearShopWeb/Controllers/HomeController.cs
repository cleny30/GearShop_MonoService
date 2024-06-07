using GearShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GearShopWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
