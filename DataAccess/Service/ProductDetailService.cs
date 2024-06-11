using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ProductDetailService
    {
        private readonly ProductService _productService;

        public ProductDetailService(ProductService productService)
        {
            _productService = productService;
        }

        public ProductDetailModel GetData(string id) 
        {
            var product = _productService.GetProducts().FirstOrDefault(p => p.ProId.Equals(id));
            ProductDetailModel model = new ProductDetailModel()
            {
                productData = product,
                products = _productService.GetProducts().Where(p => p.CateId == product.CateId).ToList()
            };
            return model;
        }
    }
}
