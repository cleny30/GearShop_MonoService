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

        public async Task<bool> CheckUsernameExistedAsync(string username)
        {
            return await _repository.CheckUsernameExistedAsync(username);
        }

        public async Task<bool> CheckManagerExistedAsync(string username, string password)
        {
            return await _repository.CheckManagerExistedAsync(username, password);
        }
    }
}
