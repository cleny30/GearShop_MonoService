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
        public IActionResult OnPostLogin(string username, string password)
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
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(model));
                data.Message = "Login success";
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
