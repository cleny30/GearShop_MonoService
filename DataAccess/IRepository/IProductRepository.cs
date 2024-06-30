using BusinessObject.Model.Page;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        public List<ProductModel> GetProduct();
        public List<ProductData> GetProductData();
        public string GetNewProductID(int ID);
        public void InsertProduct(ProductData product, List<string> imageLink, List<string> attribute, List<string> description);
        public ProductModel GetProduct(string pro_id);
        public void UpdateProduct(ProductData product, List<string> deleteList, List<string> imageLink, List<string> attribute, List<string> description);
        public Task<bool> AddQuantityToProductAsync(List<ReceiptProductModel> products);
        public Task<bool> ChangeProductStatus(ProductModel product, bool Status);


    }
}
