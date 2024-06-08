using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace DataAccess.Service
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService()
        {
            _repo = new CategoryRepository();
        }

        public List<CategoryModel> GetCategoryList()
        {
            return _repo.GetCategory();
        }
    }
}
