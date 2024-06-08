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

        public ProductService()
        {
            _repo = new ProductRepository();
        }

        public List<ProductModel> GetProductList()
        {
            //initialize service
            BrandService brandService = new BrandService();
            CategoryService categoryService = new CategoryService();

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
            ProductImageService _productImageService = new ProductImageService();
            ProductAttributeService _productAttributeService = new ProductAttributeService();
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
    }
}
