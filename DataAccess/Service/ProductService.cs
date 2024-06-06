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
            BrandService brandService = new BrandService();
            List<ProductModel> productModels = _repo.GetProduct();
            List<BrandModel> brandModels = brandService.GetBrandList();

            foreach (ProductModel items in productModels)
            {
                items.BrandName = brandModels.FirstOrDefault(b => b.BrandId == items.BrandId).BrandName;
            }

            return productModels;
        }
    }
}
