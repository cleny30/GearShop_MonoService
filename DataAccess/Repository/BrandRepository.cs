using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
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
    }
}
