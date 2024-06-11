using BusinessObject.Model;
using BusinessObject.Model.Page;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ProductDetailService _productDetailService;

        public ProductDetailController(ProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet("/ProductDetail")]
        public IActionResult ProductDetail(string ProId)
        {
            DataResult data = new DataResult();
            ProductDetailModel model = _productDetailService.GetData(ProId);
            data.Result = model;
            return View(data);
        }
    }
}
