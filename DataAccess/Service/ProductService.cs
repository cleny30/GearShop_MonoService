using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        private readonly BrandService brandService;
        private readonly CategoryService categoryService;
        private readonly ProductImageService _productImageService;
        private readonly ProductAttributeService _productAttributeService;

        public ProductService(IProductRepository repo, BrandService brandService, CategoryService categoryService, ProductImageService productImageService, ProductAttributeService productAttributeService)
        {
            _repo = repo;
            this.brandService = brandService;
            this.categoryService = categoryService;
            _productImageService = productImageService;
            _productAttributeService = productAttributeService;
        }

        public List<ProductModel> GetProductList()
        {
            List<ProductModel> productModels = _repo.GetProduct();
            List<BrandModel> brandModels = brandService.GetBrandList();
            List<CategoryModel> categoryModels = categoryService.GetCategoryList();

            foreach (ProductModel items in productModels)
            {
                items.BrandName = brandModels.FirstOrDefault(b => b.BrandId == items.BrandId).BrandName;
                items.CateName = categoryModels.FirstOrDefault(b => b.CateId == items.CateId).CateName;
            }

            return productModels;
        }

        public List<ProductData> GetProducts()
        {
           
            List<ProductData> products = _repo.GetProductData();
            List<ProductImageModel> imgs = _productImageService.GetProductImageList();
            List<ProductAttributeModel> att = _productAttributeService.GetProductAttributeList();

            foreach (ProductData product in products)
            {
                product.ProImg = imgs
                       .Where(img => img.ProId == product.ProId)
                       .Select(img => img.ProImg)
                       .ToList();
                var attributes = att
                        .Where(attribute => attribute.ProId == product.ProId)
           .GroupBy(attribute => attribute.Feature)
           .ToDictionary(group => group.Key, group => group.First().Description); // Lấy Description đầu tiên

                product.ProAttribute = attributes;
            }
            return products;
        }

        public string GetNewProductID(int ID)
        {
            return _repo.GetNewProductID(ID);
        }

        public void InsertProduct(ProductData product, List<string> imageLink, List<string> attribute, List<string> description)
        {
            _repo.InsertProduct(product, imageLink, attribute, description);
        }

        public ProductModel GetProduct(string pro_id)
        {
            return _repo.GetProduct(pro_id);
        }

        public void UpdateProduct(ProductData product, List<string> deleteList, List<string> imageLink, List<string> attribute, List<string> description)
        {
           _repo.UpdateProduct(product, deleteList, imageLink, attribute, description);
        }
    }
}
