using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using BusinessObject.Model;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Service
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountService()
        {
        }

        public bool Login(LoginAccountModel userLogin)
        {
            userLogin.Password = CalculateMD5Hash(userLogin.Password);
            return _accountRepository.Login(userLogin);
        }

        public ValidateResult Validate(LoginAccountModel userLogin)
        {
            ValidateResult validateResult = new ValidateResult();
            if (userLogin == null)
            {
                validateResult.AddError("", "Login error");
                return validateResult;
            }
            if (string.IsNullOrEmpty(userLogin.Username))
            {
                validateResult.AddError(nameof(LoginAccountModel.Username), "Username can't be empty");
            }
            else if (userLogin.Username.Length > 250)
            {
                validateResult.AddError(nameof(LoginAccountModel.Username), "Maxlenght of user name is 250 character");
            }

            if (string.IsNullOrEmpty(userLogin.Password))
            {
                validateResult.AddError(nameof(LoginAccountModel.Password), "Password can't be empty");
            }
            return validateResult;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public bool FogotPassword(LoginAccountModel userLogin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public bool ChangePassword(LoginAccountModel userLogin)
        {
            var account = getAccount(userLogin.Username);
            if (userLogin.Password != userLogin.RePassword)
            {
                return false;
            }
             userLogin.OldPassword= CalculateMD5Hash(userLogin.OldPassword);
 
            if (account.Password != userLogin.OldPassword)
            {
                return false;
            }
            if (userLogin == null)
            {
                throw new ArgumentNullException(nameof(userLogin));
            }
             userLogin.Password = CalculateMD5Hash(userLogin.Password);
            _accountRepository.ChangePassword(userLogin.Username, userLogin.Password);
            return true;
        }

        public bool Regist(AccountModel userRegist)
        {
            try
            {
                userRegist.Password = CalculateMD5Hash(userRegist.Password);
                _accountRepository.Regist(userRegist);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during registration: {ex.Message}");
                return false;
            }

        }

        public List<string> VerifyAccount(string userName, string email)
        {
            List<string> list = new List<string>();

            CheckIfExists<AccountModel>(a => a.Username == userName, "Username is already taken", ref list);
            CheckIfExists<AccountModel>(a => a.Email == email, "Email is already taken", ref list);

            return list;
        }

        private void CheckIfExists<T>(Expression<Func<Customer, bool>> filterExpression, string message, ref List<string> list)
        {
            AccountModel acc = _accountRepository.GetAccount<T>(filterExpression);

            if (acc == null)
            {
                list.Add(null);
            }
            else
            {
                list.Add(message);
            }
        }

        public AccountModel getAccount(string userName)
        {
            return _accountRepository.GetAccount<Customer>(a => a.Username == userName);
        }

        public string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        public bool UpdateCustomerInfor(AccountModel customer, string username)
        {
            var account = _accountRepository.GetAccount<Customer>(a => a.Username == username);
            customer.Username = username;
            customer.Password = account.Password;
            return _accountRepository.UpdateCustomerInfor(customer);
        }

    }
}
