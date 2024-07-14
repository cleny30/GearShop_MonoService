using BusinessObject.Model;
using BusinessObject.Model.Page;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;


namespace GearShopWeb.Controllers
{
    public class AccountController : Controller
    {


        private readonly AccountService accountService;
        private readonly OrderService orderService;
        private readonly OrderDetailService orderDetailService;
        private readonly AddressService addressService;
        private readonly IHttpContextAccessor _contx;

        public AccountController(AccountService accountService, IHttpContextAccessor contx, OrderService orderService, OrderDetailService orderDetailService, AddressService addressService)
        {
            this.accountService = accountService;
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
            this.addressService = addressService;
            _contx = contx;
        }

        [HttpGet("/Account/MyAccount")]
        public IActionResult MyAccount(string username)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                DataResult dataResult = new DataResult();
                AccountModel account = accountService.getAccount(userSession);
                dataResult.Result = account;
                return View(dataResult);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("/Account/MyOrder")]
        public IActionResult MyOrder(string username)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                DataResult dataResult = new DataResult();
                List<OrderDataModel> orderData = orderService.GetOrdersByCustomer(userSession);
                dataResult.Result = orderData;

                return View(dataResult);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("/Account/GetOrderData")]
        public IActionResult GetOrderData(string username)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                DataResult dataResult = new DataResult();
                List<OrderDataModel> orderData = orderService.GetOrdersByCustomer(userSession);
                dataResult.Result = orderData;

                return View(dataResult);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("/Account/GetOrderDataSignalR")]
        public DataResult GetOrderDataSignalR(string username)
        {
            DataResult dataResult = new DataResult();
            List<OrderDataModel> orderData = orderService.GetOrdersByCustomer(username);
            dataResult.Result = orderData;

            return dataResult;
        }

        [HttpGet("/Account/MyAddress")]
        public IActionResult MyAddress(string username)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                DataResult dataResult = new DataResult();
                dataResult.Result = addressService.GetAddressByUsername(userSession);
                return View(dataResult);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("/Account/ChangePassword")]
        public IActionResult ChangePassword()
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }



        [HttpPost]
        public IActionResult UpdateProfile(AccountModel accountModel, string username)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            accountService.UpdateCustomerInfor(accountModel, userSession);
            return RedirectToAction("MyAccount", "Account");
        }

        [HttpPost]
        public IActionResult OrderDetail(string id)
        {
            try
            {
                DataResult dataResult = new DataResult();
                var a = orderService.GetOrderByID(id);
                var orderThis = orderDetailService.GetOrderDetailsById(id);
                var rs = new
                {
                    orderDetails = orderThis,
                    orderDick = a
                };
                return Json(rs);
            }
            catch
            {
                return RedirectToAction("/StatusCodeError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(string orderId, int status)
        {
            try
            {

                // Get the order by orderId (this step depends on how you retrieve your orders)
                var order = orderService.GetOrderByID(orderId);

                if (order == null)
                {
                    return NotFound();
                }
                bool isStatusChanged = await orderService.ChangeOrderStatus(order, status);
                if (!isStatusChanged)
                {
                    return StatusCode(500, "Failed to cancel the order.");
                }

                return Json(new { redirectToUrl = Url.Action("MyOrder", "Account") });
            }
            catch (Exception ex)
            {

                return Json(new { redirectToUrl = Url.Action("MyAddress", "Account") });
            }
        }

        [HttpPost]
        public IActionResult AddAddress(DeliveryAddressModel addressModel)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            addressService.AddNewAddress(addressModel, userSession);
            return RedirectToAction("MyAddress", "Account");
        }
        [HttpPost]
        public IActionResult UpdateAddress(DeliveryAddressModel addressModel)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            addressModel.Username = userSession;
            addressService.UpdateAddress(addressModel);
            return RedirectToAction("MyAddress", "Account");
        }

        [HttpPost]
        public IActionResult AddAddressOrder(DeliveryAddressModel addressModel)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            addressService.AddNewAddress(addressModel, userSession);
            return RedirectToAction("Index", "Order");
        }
        [HttpPost]
        public IActionResult UpdateAddressOrder(DeliveryAddressModel addressModel)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            addressModel.Username = userSession;
            addressService.UpdateAddress(addressModel);
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult DeleteAddress(int id)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            addressService.DeleteAddress(userSession, id);
            return RedirectToAction("MyAddress", "Account");
        }

        [HttpPost]
        public IActionResult ChangePassword(LoginAccountModel userLogin)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            DataResult dataResult = new DataResult();
            userLogin.Username = userSession;
            accountService.ChangePassword(userLogin);
            return RedirectToAction("ChangePassword", "Account");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("username");
            _contx.HttpContext.Session.Remove("username");
            _contx.HttpContext.Session.Remove("proId");
            return Content("OK");
        }

    }
}
