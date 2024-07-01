using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
