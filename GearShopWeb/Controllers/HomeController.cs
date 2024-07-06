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
            string username=null;
            int count = 0;
            if (_contx.HttpContext.Session.GetString("username")!=null)
            {
                 username = _contx.HttpContext.Session.GetString("username");
            }
            _contx.HttpContext.Session.SetString("HeaderData", JsonConvert.SerializeObject(headerService.GetData("cleny30", out count)));
            _contx.HttpContext.Session.SetString("cartQuantity", JsonConvert.SerializeObject(count));

            data.Result = service.GetData();
            
            return View(data);
        }
    }
}
