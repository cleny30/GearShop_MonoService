using BusinessObject.Model.Page;

namespace DataAccess.Service
{
    public class HeaderService
    {
        private readonly BrandService brandService;
        private readonly CategoryService categoryService;
        private readonly ProductService productService;

        public HeaderService(BrandService brandService, CategoryService categoryService, ProductService productService)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public HeaderModel GetData() {

            // Retrieve product, brand, and category lists
            var productList = productService.GetProductList();
            var brandList = brandService.GetBrandList();
            var categoryList = categoryService.GetCategoryList();

            // Calculate quantities for each brand
            foreach (var brand in brandList)
            {
                brand.quantity = productList.Count(product => product.BrandId == brand.BrandId);
            }

            // Calculate quantities for each category
            foreach (var category in categoryList)
            {
                category.quantity = productList.Count(product => product.CateId == category.CateId);
            }

            // Create and populate the HeaderModel
            HeaderModel headerModel = new HeaderModel
            {
                brand = brandList,
                category = categoryList
            };

            return headerModel;
        }
    }
}
