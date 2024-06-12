using BusinessObject.Model;
using BusinessObject.Model.Page;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;
        private readonly IHttpContextAccessor _contx;

        public CartController(CartService cartService, IHttpContextAccessor contx)
        {
            this.cartService = cartService;
            _contx = contx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public DataResult AddProductToCard(ProductData data, int amount)
        {
            //string username = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();

            dataResult.IsSuccess = cartService.AddOrUpdateCart("cleny30", data, amount);

            return dataResult;
        }
    }
}
