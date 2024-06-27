using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System.Linq.Expressions;
using ISUZU_NEXT.Server.Core.Extentions;

namespace DataAccess.Repository
{
    public class AccountRepository:IAccountRepository
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ChangePassword(LoginAccountModel userLogin)
        {
            throw new NotImplementedException();
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
            }catch (Exception ex)
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

    }
}

