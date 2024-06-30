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
            DataResult dataResult = new DataResult();
            username = "cleny30";
            AccountModel account = accountService.getAccount(username);
            dataResult.Result = account;
            return View(dataResult);
        }
        
        [HttpGet("/Account/MyOrder")]
        public IActionResult MyOrder(string username)
        {
            DataResult dataResult = new DataResult();
            username = "cleny30";
            List<OrderDataModel> orderData = orderService.GetOrdersByCustomer(username);
            dataResult.Result = orderData;

            return View(dataResult);
        }
      

        [HttpGet("/Account/MyAddress")]
        public IActionResult MyAddress(string username)
        {
            DataResult dataResult = new DataResult();
            username = "cleny30";
            dataResult.Result = addressService.GetAddressByUsername(username);
            return View(dataResult);
        }

        [HttpGet("/Account/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }



        [HttpPost]
        public IActionResult UpdateProfile(AccountModel accountModel,string username)
        {
            DataResult dataResult = new DataResult();
            username = "cleny30";
            accountService.UpdateCustomerInfor(accountModel, username);
            return RedirectToAction("MyAccount", "Account");
        }

        [HttpPost]
        public IActionResult OrderDetail(string id)
        {
            try
            {
                DataResult dataResult = new DataResult();
                string username = "cleny30";


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
        public IActionResult AddAddress(DeliveryAddressModel addressModel, string username)
        {
            DataResult dataResult = new DataResult();
            username = "cleny30";
            addressService.AddNewAddress(addressModel,username);
            return RedirectToAction("MyAddress", "Account");
        }
        [HttpPost]
        public IActionResult UpdateAddress(DeliveryAddressModel addressModel)
        {
            DataResult dataResult = new DataResult();
             string username = "cleny30";
            addressModel.Username = username;
            addressService.UpdateAddress(addressModel);
            return RedirectToAction("MyAddress", "Account");
        }

        [HttpPost]
        public IActionResult DeleteAddress(int id)
        {
            DataResult dataResult = new DataResult();
            string username = "cleny30";
            addressService.DeleteAddress(username, id);
            return RedirectToAction("MyAddress", "Account");
        }

        [HttpPost]
        public IActionResult ChangePassword(LoginAccountModel userLogin)
        {
            DataResult dataResult = new DataResult();
            string username = "cleny30";
            userLogin.Username = username;
            accountService.ChangePassword(userLogin);
            return RedirectToAction("ChangePassword", "Account");
        }


    }
}
