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
using System.Text.RegularExpressions;

namespace DataAccess.Service
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
            else if (userLogin.Username.Length > 50)
            {
                validateResult.AddError(nameof(LoginAccountModel.Username), "Maxlength of username is 50 characters");
            }

            if (string.IsNullOrEmpty(userLogin.Password))
            {
                validateResult.AddError(nameof(LoginAccountModel.Password), "Password can't be empty");
            }
            else if (userLogin.Password.Length > 32)
            {
                validateResult.AddError(nameof(LoginAccountModel.Password), "Maxlength of password is 32 characters");
            }
            return validateResult;
        }

        public ValidateResult ValidateRegister(string username, string fullname, string phone, string email, string password, string rePassword)
        {
            ValidateResult validateResult = new ValidateResult();
            AccountModel registAccount = new AccountModel
            {
                Username = username,
                Fullname = fullname,
                Phone = phone,
                Email = email,
                Password = password,
            };
            // Null registAccount
            if (registAccount == null)
            {
                validateResult.AddError("", "Regist error");
                return validateResult;
            }
            //Username error
            if (string.IsNullOrEmpty(registAccount.Username))
            {
                validateResult.AddError(nameof(AccountModel.Username), "Username can't be empty");
            }
            else if (registAccount.Username.Length > 50)
            {
                validateResult.AddError(nameof(AccountModel.Username), "Maxlength of username is 50 characters");
            }
            //Fullname error
            if (string.IsNullOrEmpty(registAccount.Fullname))
            {
                validateResult.AddError(nameof(AccountModel.Fullname), "Fullname can't be empty");
            }
            else if (registAccount.Fullname.Length > 100)
            {
                validateResult.AddError(nameof(AccountModel.Fullname), "Maxlength of fullname is 100 characters");
            }
            //Email error
            var emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Match m = Regex.Match(registAccount.Email, emailRegex);
            if (string.IsNullOrEmpty(registAccount.Email))
            {
                validateResult.AddError(nameof(AccountModel.Email), "Email can't be empty");
            }
            else if (registAccount.Email.Length > 255)
            {
                validateResult.AddError(nameof(AccountModel.Email), "Maxlength of email is 255 characters");
            }
            else if (!m.Success)  //Add check email regex
            {
                validateResult.AddError(nameof(AccountModel.Email), "Email is not in the correct format");
            }
            //Phone error
            var phoneRegex = @"([0]{1})([0-9]{9})";
            m = Regex.Match(registAccount.Phone, phoneRegex);
            if (string.IsNullOrEmpty(registAccount.Phone))
            {
                validateResult.AddError(nameof(AccountModel.Phone), "Phone can't be empty");
            }
            else if (registAccount.Phone.Length > 11)
            {
                validateResult.AddError(nameof(AccountModel.Phone), "Maxlength of phone is 11");
            }
            else if (!m.Success) //Add check phone regex
            {
                validateResult.AddError(nameof(AccountModel.Phone), "Phone is not in the correct format");
            }
            //Password error
            if (string.IsNullOrEmpty(registAccount.Password))
            {
                validateResult.AddError(nameof(AccountModel.Password), "Password can't be empty");
            } else if (registAccount.Password.Length > 32)
            {
                validateResult.AddError(nameof(AccountModel.Password), "Maxlength of password is 32");
            } else if (!registAccount.Password.Equals(rePassword))
            {
                validateResult.AddError(nameof(AccountModel.Password), "Your reentered password is different from your password");
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
        /// TODO
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public bool ChangePassword(LoginAccountModel userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Regist(string username, string fullname, string phone, string email, string password, string rePassword)
        {
            try
            {
                AccountModel registAccount = new AccountModel
                {
                    Username = username,
                    Fullname = fullname,
                    Phone = phone,
                    Email = email,
                    Password = password,
                };
                registAccount.Password = CalculateMD5Hash(registAccount.Password);
                _accountRepository.Regist(registAccount);
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
    }
}
