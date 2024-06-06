using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class BrandService
    {

        private readonly IBrandRepository _repo;
        public BrandService()
        {
            _repo = new BrandRepository();
        }

        public List<BrandModel> GetBrandList()
        {
            return _repo.Getbrand();
        }
    }
}
