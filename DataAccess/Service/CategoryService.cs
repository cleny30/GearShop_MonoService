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

        public bool IsKeywordExisted(string Keyword) => _repo.IsKeywordExisted(Keyword);

        public bool AddCategory(CategoryModel cate)
        {
            return _repo.AddCategory(cate);
        }
    }
}
