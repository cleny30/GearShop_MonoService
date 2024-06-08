using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace DataAccess.Service
{
    public class ProductImageService
    {
        private readonly IProductImageRepository _repo;
        public ProductImageService()
        {
            _repo = new ProductImageRepository();
        }

        public List<ProductImageModel> GetProductImageList()
        {
            return _repo.GetProductImage();
        }
    }
}
