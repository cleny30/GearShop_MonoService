using BusinessObject.Model.Page;

namespace DataAccess.Service
{
    public class HeaderService
    {
        private readonly BrandService brandService;
        private readonly CategoryService categoryService;
        private readonly ProductService productService;
        private readonly CartService cartService;
        public HeaderService(BrandService brandService, CategoryService categoryService, ProductService productService, CartService cartService)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.cartService = cartService;
        }

        public HeaderModel GetData(string? username, out int count) {

            // Retrieve product, brand, and category lists
            var productList = productService.GetProductList();
            var brandList = brandService.GetBrandList().Where(b => b.IsAvailable).ToList();
            var categoryList = categoryService.GetCategoryList().Where(c => c.IsAvailable).ToList();

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
                category = categoryList,
            };
            count = username == null ? 0 : cartService.GetCartsByUserName(username).Count();

            return headerModel;
        }
    }
}
