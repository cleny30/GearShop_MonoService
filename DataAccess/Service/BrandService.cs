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

        public bool ChangeBrandStatus(int ID, bool availability)
        {
            return _repo.ChangeBrandStatus(ID, availability);
        }

        public bool AddBrand(BrandModel brand)
        {
            return _repo.AddBrand(brand);
        }

        public bool UpdateBrand(BrandModel _brand)
        {
            return _repo.UpdateBrand(_brand);
        }
    }
}
