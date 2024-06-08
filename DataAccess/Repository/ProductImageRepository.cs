using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;


namespace DataAccess.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        public List<ProductImageModel> GetProductImage()
        {
            List<ProductImage> _proImg;
            try
            {
                var dbContext = new PrndatabaseContext();
                _proImg = dbContext.ProductImages.ToList();

                List<ProductImageModel> ProImg = new List<ProductImageModel>();

                foreach (var item in _proImg)
                {
                    ProductImageModel ProductImageModel = new ProductImageModel();
                    ProductImageModel.CopyProperties(item);
                    ProImg.Add(ProductImageModel);
                }
                return ProImg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
