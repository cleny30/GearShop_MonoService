using BusinessObject.Model.Page;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        public List<ProductModel> GetProduct();

    }
}
