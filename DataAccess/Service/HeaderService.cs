using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class HeaderService
    {
        public HeaderModel GetData() {
            BrandService brandService = new BrandService();
            CategoryService categoryService = new CategoryService();

            HeaderModel headerModel = new HeaderModel
            {
                brand = brandService.GetBrandList(),
                category = categoryService.GetCategoryList(),
            };

            return headerModel;
        }
    }
}
