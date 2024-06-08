using BusinessObject.Model.Page;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        public List<ProductModel> GetProduct();
        public List<ProductData> GetProductData();
        public string GetNewProductID(int ID);

        public void InsertProduct(ProductData product, List<string> imageLink, List<string> attribute, List<string> description);
    }
}
