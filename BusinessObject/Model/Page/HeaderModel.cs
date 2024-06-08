
namespace BusinessObject.Model.Page
{
    public class HeaderModel
    {
        public List<BrandModel>? brand { get; set; }
        public List<CategoryModel>? category { get; set; }
        public int cardQuantity { get; set; } = 0;
    }
}
