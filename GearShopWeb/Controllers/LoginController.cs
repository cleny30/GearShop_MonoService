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

        public LoginController(AccountService accountService)
        {
            this.accountService = accountService;
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
                    var options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                        IsEssential = true,
                        HttpOnly = true,
                        Secure = true
                    };

                    Response.Cookies.Append("username", username, options);
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
