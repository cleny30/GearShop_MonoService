using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IManagerRepository
    {
        public bool CheckUsernameExisted(string username);
        public bool CheckManagerExisted(string username, string password);
    }
}
