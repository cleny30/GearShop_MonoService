using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<ProductModel> GetProduct()
        {
            List<Product> products;
            try
            {
                var dbContext = new PrndatabaseContext();
                products = dbContext.Products.ToList();

                List<ProductModel> ProductModels = new List<ProductModel>();

                foreach (var product in products)
                {
                    ProductModel ProductModel = new ProductModel();
                    ProductModel.CopyProperties(product);
                    ProductModels.Add(ProductModel);
                }
                return ProductModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductData> GetProductData()
        {
            List<Product> products;
            try
            {
                var dbContext = new PrndatabaseContext();
                products = dbContext.Products.ToList();

                List<ProductData> pro = new List<ProductData>();

                foreach (var product in products)
                {
                    ProductData ProductData = new ProductData();
                    ProductData.CopyProperties(product);
                    pro.Add(ProductData);
                }
                return pro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
