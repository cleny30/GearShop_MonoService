using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
