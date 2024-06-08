using BusinessObject.Model.Page;


namespace DataAccess.IRepository
{
    public interface ICategoryRepository
    {
        public List<CategoryModel> GetCategory();
    }
}
