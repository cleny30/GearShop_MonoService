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

		public OrderController(IHttpContextAccessor contx, CartService cartService)
		{
			_contx = contx;
			_cartService = cartService;
		}

		public IActionResult Index()
        {
			var productChecked = _contx.HttpContext.Session.GetString("proId").Split(",");
			var list=_cartService.GetCheckedProduct("cleny30", productChecked.ToList());
			DataResult result = new DataResult();
			result.Result = list;
            return View(result);
        }

		[HttpPost]
		public IActionResult StoreCheckedProduct(List<string> proIds)
		{
			_contx.HttpContext.Session.SetString("proId", string.Join(',', proIds));
			return Content("OK");
		}
	}
}
