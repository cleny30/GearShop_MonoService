using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public List<BrandModel> Getbrand()
        {           
            List<Brand> brand;
            try
            {
                var dbContext = new PrndatabaseContext();
                brand = dbContext.Brands.ToList();

                List<BrandModel> BrandModels = new List<BrandModel>();

                foreach (var item in brand)
                {
                    BrandModel BrandModel = new BrandModel();
                    BrandModel.CopyProperties(item);
                    BrandModels.Add(BrandModel);
                }
                return BrandModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ChangeBrandStatus(int ID, bool availability)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    var brand = dbContext.Brands.SingleOrDefault(b => b.BrandId.Equals(ID));

                    if (brand != null)
                    {
                        // Update the IsAvailable property
                        brand.IsAvailable = availability;

                        // Save changes to the database
                        int result = dbContext.SaveChanges();
                        return result > 0;
                    }
                    else
                    {
                        // Category not found
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public bool AddBrand(BrandModel _brand)
        {
            try
            {
                Brand brand = new Brand
                {
                    BrandId = GetNewestBrandID() + 1,
                    BrandName = _brand.BrandName,
                    BrandLogo = _brand.BrandLogo,   
                    IsAvailable = true
                };

                var dbContext = new PrndatabaseContext();
                dbContext.Brands.Add(brand); 
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public int GetNewestBrandID()
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                int newestBrandId = dbContext.Brands
                                         .OrderByDescending(c => c.BrandId)
                                         .Select(c => c.BrandId)
                                         .FirstOrDefault();
                return newestBrandId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool UpdateBrand(BrandModel _brand)
        {
            Brand brand = new Brand
            {
                BrandId= _brand.BrandId,
                BrandLogo= _brand.BrandLogo,
                BrandName= _brand.BrandName,
                IsAvailable= true
            };
            try
            {
                var dbContext = new PrndatabaseContext();
                dbContext.Entry<Brand>(brand).State = EntityState.Modified;
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
