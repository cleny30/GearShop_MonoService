using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<CategoryModel> GetCategory()
        {
            List<Category> category;
            try
            {
                var dbContext = new PrndatabaseContext();
                category = dbContext.Categories.ToList();
                List<CategoryModel> categoryModels = new List<CategoryModel>();

                foreach(var items in category)
                {
                    CategoryModel categoryModel = new CategoryModel();
                    categoryModel.CopyProperties(items);
                    categoryModels.Add(categoryModel);
                }
                return categoryModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
