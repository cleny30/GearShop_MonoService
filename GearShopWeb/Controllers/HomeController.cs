using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GearShopWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Home()
        {
            HomeService service = new HomeService();

            DataResult data = new DataResult();

            data.Result = service.GetData();

            return View(data);
        }
    }
}
