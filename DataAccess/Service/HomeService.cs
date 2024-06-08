using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class HomeService
    {
        ProductService productService = new ProductService();
        BrandService brandService = new BrandService();
        CategoryService categoryService = new CategoryService();

    }
}
