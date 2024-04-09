namespace AllupProjectMVC.Exceptions.ProductExceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string? message) : base(message)
        {
        }
    }
}
