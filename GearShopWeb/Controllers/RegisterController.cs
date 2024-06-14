using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet("/Signup")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
