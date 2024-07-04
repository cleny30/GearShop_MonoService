using BusinessObject.Model.Page;
using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class OrderController : Controller
    {
		private readonly IHttpContextAccessor _contx;
		private readonly CartService _cartService;
		private readonly AddressService _addressService;

        public OrderController(IHttpContextAccessor contx, CartService cartService, AddressService addressService)
        {
            _contx = contx;
            _cartService = cartService;
            _addressService = addressService;
        }

        public IActionResult Index()
        {
			var productChecked = _contx.HttpContext.Session.GetString("proId").Split(",");
			var list=_cartService.GetCheckedProduct("cleny30", productChecked.ToList());

			var addresses = _addressService.GetAddressByUsername("cleny30");
			DataResult data = new DataResult();

			data.Result = new
			{
				list = list,
				addresses = addresses
			};

            return View(data);
        }

		[HttpPost]
		public IActionResult StoreCheckedProduct(List<string> proIds)
		{
			_contx.HttpContext.Session.SetString("proId", string.Join(',', proIds));
			return Content("OK");
		}
	}
}
