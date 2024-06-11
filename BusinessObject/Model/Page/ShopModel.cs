using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ShopModel
    {
        public List<ProductData>? products { get; set; }
        public List<BrandModel> brandModels { get; set; }
        public List<CategoryModel> categoryModels { get; set; }
        public int saleAmount { get; set; }
        public List<int> selectedCategory { get; set; }
        public List<int> selectedBrand { get; set; }
        public string sortFilter { get; set; }
        public string orderFilter { get; set; }
        public PageNavigationModel navigationModel { get; set; }
    }

    public class PageNavigationModel
    {
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public bool isFirstPage { get; set; }
        public bool isLastPage { get; set;}
    }
}
