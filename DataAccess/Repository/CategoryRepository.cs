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
        public bool AddCategory(CategoryModel cate)
        {
            Category _cate = new Category
            {
                CateName = cate.CateName,
                Keyword = cate.Keyword,
                IsAvailable = true
            };
            try
            {
                var dbContext = new PrndatabaseContext();
                var entityEntry = dbContext.Categories.Add(_cate);
                int result = dbContext.SaveChanges();
                if(result > 0)
                {
                    return true;
                } else
                {
                    return false;
                }

            } catch (Exception ex)
            {
                return false;
            }
            
        }

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

        public bool IsKeywordExisted(string Keyword)
        {
            Category _cate;
            try
            {
                var dbContext = new PrndatabaseContext(); 
                _cate = dbContext.Categories.FirstOrDefault(c => c.Keyword.Equals(Keyword));
                if(_cate == null)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }
    }
}
