using BusinessObject.Model.Page;
using BusinessObject.Model;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GearShopWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountService accountService;
        private readonly IHttpContextAccessor _contx;

        public LoginController(AccountService accountService, IHttpContextAccessor contx)
        {
            this.accountService = accountService;
            _contx = contx;
        }

        [HttpGet("/Login")]
        public ActionResult Index() {
            string username = null;
            if (_contx.HttpContext.Session.GetString("username") != null)
            {
                username = _contx.HttpContext.Session.GetString("username");
            }
            else
            {
                var usernameCookie = _contx.HttpContext.Request.Cookies["username"];
                if (!string.IsNullOrEmpty(usernameCookie))
                {
                    username = usernameCookie;
                    _contx.HttpContext.Session.SetString("username", usernameCookie);
                }
            }

            if (!string.IsNullOrEmpty(username)) {
                return Redirect("/Home");

            }
            return View();
        }

        [HttpPost]
        public IActionResult OnPostLogin(string username, string password, bool isRemember)
        {
            DataResult data = new DataResult();

            LoginAccountModel model = new LoginAccountModel
            {
                Username = username,
                Password = password
            };

            ValidateResult validateResult = accountService.Validate(model);
            if (!validateResult.IsValid)
            {
                data.SetValidateResult(validateResult);
                return Content(data.IsSuccess.ToString());
            }
            if (accountService.Login(model))
            {
                HttpContext.Session.SetString("username", username);
                data.Message = "Login success";
                data.Result = model;
                if(isRemember)
                {
                    HttpContext.Response.Cookies.Append("username", username, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                    });
                }
            }
            else
            {
                data.Message = "Login fail";
                data.IsSuccess = false;
            }
            return Content(data.IsSuccess.ToString());
        }
    }
}
