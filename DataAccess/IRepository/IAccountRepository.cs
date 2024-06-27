using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using System.Linq.Expressions;

namespace DataAccess.IRepository
{
    public interface IAccountRepository
    {
        public bool Login(LoginAccountModel userLogin);
        public bool FogotPassword(LoginAccountModel userLogin);
        public bool ChangePassword(LoginAccountModel userLogin);
        public bool Regist(AccountModel userRegist);
        public AccountModel GetAccount<T>(Expression<Func<Customer, bool>> filterExpression);
        public AccountModel GetAccountByUsername(string username);
    }
}
