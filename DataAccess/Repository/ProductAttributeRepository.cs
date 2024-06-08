using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        public List<ProductAttributeModel> GetProductAttribute()
        {
            List<ProductAttribute> _proAtt;
            try
            {
                var dbContext = new PrndatabaseContext();
                _proAtt = dbContext.ProductAttributes.ToList();

                List<ProductAttributeModel> ProAtt = new List<ProductAttributeModel>();

                foreach (var item in _proAtt)
                {
                    ProductAttributeModel ProductAttributeModel = new ProductAttributeModel();
                    ProductAttributeModel.CopyProperties(item);
                    ProAtt.Add(ProductAttributeModel);
                }
                return ProAtt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
