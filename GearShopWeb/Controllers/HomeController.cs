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
        private readonly ProductService productService;

        public HomeController(IHttpContextAccessor contx, HomeService service, HeaderService headerService, ProductService productService)
        {
            _contx = contx;
            this.service = service;
            this.headerService = headerService;
            this.productService = productService;
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
            else
            {
                var usernameCookie = _contx.HttpContext.Request.Cookies["username"];
                if (!string.IsNullOrEmpty(usernameCookie))
                {
                    username = usernameCookie;
                    _contx.HttpContext.Session.SetString("username", usernameCookie);
                }
            }
            _contx.HttpContext.Session.SetString("HeaderData", JsonConvert.SerializeObject(headerService.GetData(username, out count)));
            _contx.HttpContext.Session.SetString("cartQuantity", JsonConvert.SerializeObject(count));

            data.Result = service.GetData();
            
            return View(data);
        }

        [HttpGet]
        public IActionResult Search(string pattern)
        {
            DataResult data = new DataResult();
            data.Result= productService.SearchProduct(pattern);
            return Json(data);
        }
    }
}
