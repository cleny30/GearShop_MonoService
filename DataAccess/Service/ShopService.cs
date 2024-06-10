using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ShopService
    {
        public ShopModel GetData()
        {
            ProductService productService = new ProductService();

            var productList = productService.GetProducts();

            ShopModel model = new ShopModel
            {
                product = productList.ToList(),
                saleAmount = productList.Where(p => p.Discount > 0).Count(),
            };

            return model;
        }
    }
}
