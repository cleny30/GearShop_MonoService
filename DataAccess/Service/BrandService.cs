using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;

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
