using BusinessObject.Model;
using BusinessObject.Model.Page;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult Index()
        {
            DataResult dataResult = new DataResult();
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession)) {
                List<UserCartData> list = cartService.GetCartsByUserName(userSession);

                dataResult.Result = list;
                return View(dataResult);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public DataResult AddProductToCart(string data, int amount)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
                DataResult dataResult = new DataResult();
            if (!string.IsNullOrEmpty(userSession)) {
                ProductData productData = System.Text.Json.JsonSerializer.Deserialize<ProductData>(data);
                dataResult.IsSuccess = cartService.AddOrUpdateCart(userSession, productData, amount);
                _contx.HttpContext.Session.SetString("cartQuantity", JsonConvert.SerializeObject(cartService.GetCartsByUserName(userSession).Count()));
            }
            else
            {
                dataResult.IsSuccess = false;
            }

            return dataResult;
        }

        [HttpPost]
        public DataResult UpdateCartData(string ProId, int amount)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult data = new DataResult();
            Tuple<bool,double> result = cartService.UpdateCart(userSession, ProId, amount);
            data.IsSuccess = result.Item1;
            data.Result = result.Item2;
            return data;
        }

        [HttpPost]
        public DataResult Delete(string ProId)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult data = new DataResult();
            data.IsSuccess = cartService.DeleteCartById(ProId,userSession);
            _contx.HttpContext.Session.SetString("cartQuantity", JsonConvert.SerializeObject(cartService.GetCartsByUserName(userSession).Count()));
            return data;
        }
    }
}
