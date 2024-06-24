using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ManagerService
    {
        private readonly IManagerRepository _repository;

        public ManagerService(IManagerRepository repo)
        {
            _repository = repo;
        }

        public bool CheckUsernameExisted(string username)
        {
            return _repository.CheckUsernameExisted(username);
        }

        public bool CheckManagerExisted(string username, string password)
        {
            return !_repository.CheckManagerExisted(username, password);
        }
    }
}
