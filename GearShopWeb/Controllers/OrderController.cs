using BusinessObject.Model.Page;
using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Packaging.Signing;
using Microsoft.IdentityModel.Tokens;

namespace GearShopWeb.Controllers
{
    public class OrderController : Controller
    {
		private readonly IHttpContextAccessor _contx;
		private readonly CartService _cartService;
		private readonly AddressService _addressService;
		private readonly OrderService _orderService;

        public OrderController(IHttpContextAccessor contx, CartService cartService, AddressService addressService, OrderService orderService)
        {
            _contx = contx;
            _cartService = cartService;
            _addressService = addressService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            var productSession = _contx.HttpContext.Session.GetString("proId");
            if (!string.IsNullOrEmpty(userSession))
            {
                if (!string.IsNullOrEmpty(productSession))
                {
                    var productChecked = productSession.Split(",");
                    var list = _cartService.GetCheckedProduct(userSession, productChecked.ToList());

                    var addresses = _addressService.GetAddressByUsername(userSession);
                    DataResult data = new DataResult();

                    data.Result = new
                    {
                        list = list,
                        addresses = addresses
                    };

                    return View(data);
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }
            
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("/Order/PostCheckout")]
        public IActionResult PostCheckout()
        {
            return View();
        }

        [HttpPost]
		public IActionResult StoreCheckedProduct(List<string> proIds)
		{
			_contx.HttpContext.Session.SetString("proId", string.Join(',', proIds));
			return Content("OK");
		}

        [HttpPost]
        public DataResult CheckOut(OrderModel order)
        {
            DataResult result = new DataResult();
            var productChecked = _contx.HttpContext.Session.GetString("proId");
            var username = "cleny30";

            OrderModel orderModel = new OrderModel
            {
                Address = order.Address,
                OrderDes = order.OrderDes ==null?order.OrderDes:"None",
                Fullname = order.Fullname,
                Phone = order.Phone,
                proId = productChecked,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Status = 1,
                TotalPrice = order.TotalPrice,
                Username = username,
            };
            result.IsSuccess = _orderService.Checkout(orderModel);
            return result;
        }
    }
}
