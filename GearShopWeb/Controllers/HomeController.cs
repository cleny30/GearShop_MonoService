using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GearShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        public HomeController(IHttpContextAccessor contx)
        {
            _contx = contx;
        }

        [HttpGet]
        public IActionResult Home()
        {
            HomeService service = new HomeService();

            HeaderService headerService = new HeaderService();

            DataResult data = new DataResult();
            _contx.HttpContext.Session.SetString("HeaderData", JsonConvert.SerializeObject(headerService.GetData()));

            data.Result = service.GetData();

            
            return View(data);
        }
    }
}
