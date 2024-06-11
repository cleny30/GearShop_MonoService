using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace DataAccess.Service
{
    public class BrandService
    {

        private readonly IBrandRepository _repo;

        public BrandService(IBrandRepository repo)
        {
            _repo = repo;
        }

        public List<BrandModel> GetBrandList()
        {
            return _repo.Getbrand();
        }
    }
}
