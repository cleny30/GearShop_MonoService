using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public bool AddCategory(CategoryModel cate)
        {
            Category _cate = new Category
            {
                CateId = GetNewestCateID() + 1,
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

        public bool UpdateCategory(CategoryModel cate)
        {
            Category _cate = new Category
            {
                CateId = cate.CateId,
                CateName = cate.CateName,
                Keyword = cate.Keyword
            };

            try
            {
                var dbContext = new PrndatabaseContext();
                dbContext.Entry<Category>(_cate).State = EntityState.Modified;
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

        public int GetNewestCateID()
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                int newestCateId = dbContext.Categories
                                         .OrderByDescending(c => c.CateId)
                                         .Select(c => c.CateId)
                                         .FirstOrDefault();
                return newestCateId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool ChangeCategoryStatus(int ID, bool availability)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    // Retrieve the category by its ID
                    var category = dbContext.Categories.SingleOrDefault(c => c.CateId == ID);

                    if (category != null)
                    {
                        // Update the IsAvailable property
                        category.IsAvailable = availability;

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
    }
}
