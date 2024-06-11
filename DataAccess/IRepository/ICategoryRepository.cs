using BusinessObject.Model.Page;


namespace DataAccess.IRepository
{
    public interface ICategoryRepository
    {
        public List<CategoryModel> GetCategory();

        public bool IsKeywordExisted(string Keyword);

        public bool AddCategory(CategoryModel cate);

        public bool UpdateCategory(CategoryModel cate);

        public bool ChangeCategoryStatus(int ID, bool availability);
    }
}
