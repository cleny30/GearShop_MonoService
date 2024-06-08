using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class HomeService
    {
        public HomeModel GetData()
        {
            ProductService productService = new ProductService();
            
            HomeModel homeModel = new HomeModel
            {
                products = productService.GetProducts(),
            };

            return homeModel;
        }
    }
}
