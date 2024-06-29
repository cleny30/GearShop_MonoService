using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IManagerRepository
    {
        Task<bool> CheckUsernameExistedAsync(string username);
        Task<bool> CheckManagerExistedAsync(string username, string password);
    }
}
