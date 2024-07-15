using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System.Linq.Expressions;
using ISUZU_NEXT.Server.Core.Extentions;
using CloudinaryDotNet;
using System;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                var user = dbContext.Customers.SingleOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException("User not found.");
                }
                user.Password = GetMD5Hash(newPassword);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool FogotPassword(LoginAccountModel userLogin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Login(LoginAccountModel userLogin)
        {
            try
            {
                var dbContext = new PrndatabaseContext();

                var user = dbContext.Customers.SingleOrDefault(d => d.Username == userLogin.Username && d.Password == userLogin.Password);

                return user != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="userRegist"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Regist(AccountModel userRegist)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    Customer customer = new Customer();

                    customer.CopyProperties(userRegist);

                    dbContext.Customers.Add(customer);

                    dbContext.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public AccountModel GetAccount<T>(Expression<Func<Customer, bool>> filterExpression)
        {
            using (var dbContext = new PrndatabaseContext())
            {
                Customer? customer = dbContext.Customers.SingleOrDefault(filterExpression);
                if (customer != null)
                {
                    var account = new AccountModel();
                    account.CopyProperties(customer);
                    return account;
                }
            }
            return null;
        }

        public AccountModel GetAccountByUsername(string username)
        {

            using (var dbContext = new PrndatabaseContext())
            {
                Customer? customer = dbContext.Customers.Where(a => a.Username.Equals(username)).SingleOrDefault();
                if (customer != null)
                {
                    var account = new AccountModel();
                    account.CopyProperties(customer);
                    return account;
                }
            }
            return null;
        }


        /// <summary>
        /// Update Customer Addcount
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        public bool UpdateCustomerInfor(AccountModel accountModel)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    var account = dbContext.Customers.FirstOrDefault(p => p.Username == accountModel.Username);
                    if (account != null)
                    {
                        account.Fullname = accountModel.Fullname;
                        account.Email = accountModel.Email;
                        account.Phone = accountModel.Phone;
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getUsername(string username)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    var account = dbContext.Customers.FirstOrDefault(p => p.Username == username);
                    if(account != null)
                    {
                        return account.Username;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); // Convert the input string to bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes); // Compute the MD5 hash

                // Convert the byte array to a hexadecimal string representation of the hash
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" means lowercase hexadecimal
                }
                return sb.ToString();
            }
        }

        public string GetAccountByEmail(string email)
        {
            using (var dbContext = new PrndatabaseContext())
            {
                Customer? customer = dbContext.Customers.Where(a => a.Email.Equals(email)).SingleOrDefault();
                if (customer != null)
                {
                    var account = new AccountModel();
                    account.CopyProperties(account);
                    return email;
                }
            }
            return null;
        }

        public bool ForgetPassword(string password, string email)
        {
            using (var dbContext = new PrndatabaseContext())
            {
                var account = dbContext.Customers.FirstOrDefault(c => c.Email.Equals(email));
                if(account != null)
                {
                    account.Password = GetMD5Hash(password);
                    dbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }
    }
}

