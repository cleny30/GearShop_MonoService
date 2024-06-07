
namespace BusinessObject.Model
{
    public class Error
    {
        public string Message { get; set; } = string.Empty;
        public string? Property { get; set; }
        public Error() { }
        public Error(string message)
        {
            Message = message;
        }
        public Error(string property, string message)
        {
            Message = message;
            Property = property;
        }
    }
}
