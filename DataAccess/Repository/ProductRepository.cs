﻿using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.Extensions.Options;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<ProductModel> GetProduct()
        {
            List<Product> products;
            try
            {
                var dbContext = new PrndatabaseContext();
                products = dbContext.Products.ToList();

                List<ProductModel> ProductModels = new List<ProductModel>();

                foreach (var product in products)
                {
                    ProductModel ProductModel = new ProductModel();
                    ProductModel.CopyProperties(product);
                    ProductModels.Add(ProductModel);
                }
                return ProductModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductData> GetProductData()
        {
            List<Product> products;
            try
            {
                var dbContext = new PrndatabaseContext();
                products = dbContext.Products.ToList();

                List<ProductData> pro = new List<ProductData>();

                foreach (var product in products)
                {
                    ProductData ProductData = new ProductData();
                    ProductData.CopyProperties(product);
                    pro.Add(ProductData);
                }
                return pro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetNewProductID(int CatID)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    var lastProductId = dbContext.Products
                        .Where(p => p.CateId == CatID)
                        .OrderByDescending(p => p.ProId)
                        .Select(p => p.ProId)
                        .FirstOrDefault();

                    if (lastProductId == null)
                    {
                        var categoryKeyword = dbContext.Categories
                            .Where(c => c.CateId == CatID)
                            .Select(c => c.Keyword)
                            .FirstOrDefault();

                        if (categoryKeyword != null)
                        {
                            string newProductId = $"{categoryKeyword}001";
                            return newProductId;
                        }
                    }
                    else
                    {
                        string numericPart = lastProductId.Substring(2);
                        int currentNumber = int.Parse(numericPart);
                        int newNumber = currentNumber + 1;
                        string newProductId = $"{lastProductId.Substring(0, 2)}{newNumber:D3}";
                        return newProductId;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You might want to rethrow the exception here for handling further up the call stack.
            }

            // Return null only if an exception occurs or no product ID is found
            return null;
        }

        public void InsertProduct(ProductData product, List<string> imageLink, List<string> attribute, List<string> description)
        {
            Product _product = new Product
            {
                ProId = product.ProId,
                ProName = product.ProName,
                BrandId = product.BrandId,
                CateId = product.CateId,
                Discount = product.Discount,
                ProDes = product.ProDes,
                ProPrice = product.ProPrice,
                IsAvailable = product.IsAvailable,
                ProQuan = 0
            };
            try
            {
                var dbContext = new PrndatabaseContext();
                dbContext.Products.Add(_product);
                dbContext.SaveChanges();

                foreach(var items in imageLink)
                {
                    dbContext.ProductImages.Add(new ProductImage { ProId = _product.ProId, ProImg = items});
                    dbContext.SaveChanges();
                }

                foreach (var (attr, desc) in attribute.Zip(description, (attr, desc) => (attr, desc)))
                {
                    dbContext.ProductAttributes.Add(new ProductAttribute
                    {
                        ProId = _product.ProId,
                        Description = desc,
                        Feature = attr
                    });
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
