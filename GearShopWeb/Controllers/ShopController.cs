using BusinessObject.Model;
using BusinessObject.Model.Page;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("/Shop")]
        public IActionResult Shop()
        {
            string sortFilter = Request.Query["sort"].ToString();
            string orderFilter = Request.Query["order"].ToString();
            string category = Request.Query["category"].ToString();
            string brand = Request.Query["brand"].ToString();
            int currentPage = Request.Query["page"].ToString() != "" ? Convert.ToInt32(Request.Query["page"]) : 1; ; // Default value

            ShopModel model = _shopService.GetData(sortFilter, orderFilter, category, brand, currentPage);

            DataResult data = new DataResult();

            data.Result = model;

            return View(data);
        }
    }
}
