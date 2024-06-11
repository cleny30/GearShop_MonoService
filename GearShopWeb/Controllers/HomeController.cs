using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GearShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly HomeService service;
        private readonly HeaderService headerService;

        public HomeController(IHttpContextAccessor contx, HomeService service, HeaderService headerService)
        {
            _contx = contx;
            this.service = service;
            this.headerService = headerService;
        }

        [HttpGet]
        public IActionResult Home()
        {
            DataResult data = new DataResult();
            _contx.HttpContext.Session.SetString("HeaderData", JsonConvert.SerializeObject(headerService.GetData()));

            data.Result = service.GetData();

            
            return View(data);
        }
    }
}
