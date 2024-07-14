using BusinessObject.Model.Page;
using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Packaging.Signing;

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

        [HttpGet("/Order")]
        public IActionResult Index()
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            var productuserSession = _contx.HttpContext.Session.GetString("proId");
            
            if(!string.IsNullOrEmpty(userSession))
            {
                if(!string.IsNullOrEmpty(productuserSession))
                {
                    var productChecked = productuserSession.Split(",");

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
                    return RedirectToAction("Index","Cart");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("/Order/PostCheckout")]
        public IActionResult PostCheckout()
        {
            _contx.HttpContext.Session.Remove("proId");
            return View();
        }

        [HttpPost]
		public IActionResult StoreCheckedProduct(List<string> proIds)
		{
			_contx.HttpContext.Session.SetString("proId", string.Join(',', proIds));
			return Content("OK");
		}

        [HttpPost]
        public async Task<DataResult> CheckOut(OrderModel order)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult result = new DataResult();
            var productChecked = _contx.HttpContext.Session.GetString("proId");

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
                Username = userSession,
            };
            result.IsSuccess = await _orderService.Checkout(orderModel);
            return result;
        }
    }
}
