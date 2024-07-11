using BusinessObject.Model;
using BusinessObject.Model.Page;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;

namespace GearShopWeb.Controllers
{
    public class RegisterController : Controller
    {
        public readonly AccountService accountService;
        public RegisterController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpGet("/Signup")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OnPostRegister(string username, string fullname, string phone, string email, string password, string rePassword)
        {
            DataResult data = new DataResult();
            ValidateResult validateResult = accountService.ValidateRegister(username, fullname, phone, email, password, rePassword);
            if (!validateResult.IsValid)
            {
                data.SetValidateResult(validateResult);
                return Content(data.IsSuccess.ToString());
            }
            if (accountService.Regist(username, fullname, phone, email, password, rePassword))
            {
                data.Message = "Regist success";
            }
            else
            {
                data.Message = "Regist fail";
                data.IsSuccess = false;
            }
            return Content(data.IsSuccess.ToString());
        }
    }
}
