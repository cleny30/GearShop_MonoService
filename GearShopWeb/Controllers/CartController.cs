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

        [HttpGet("/Cart")]
        public IActionResult Cart()
        {
            DataResult dataResult = new DataResult();

            List<UserCartData> list = cartService.GetCartsByUserName("cleny30");

            dataResult.Result = list;
            return View(dataResult);
        }

        [HttpPost]
        public DataResult AddProductToCart(string data, int amount)
        {
            //string username = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            ProductData productData = System.Text.Json.JsonSerializer.Deserialize<ProductData>(data);
            dataResult.IsSuccess = cartService.AddOrUpdateCart("cleny30", productData, amount);

            return dataResult;
        }

        [HttpPost]
        public DataResult UpdateCartData(string ProId, int amount)
        {
            DataResult data = new DataResult();
            Tuple<bool,double> result = cartService.UpdateCart("cleny30", ProId, amount);
            data.IsSuccess = result.Item1;
            data.Result = result.Item2;
            return data;
        }
    }
}
