namespace AllupProjectMVC.Exceptions.BlogExceptions
{
    public class BlogNotFoundException : Exception
    {
        public BlogNotFoundException()
        {
        }

        public BlogNotFoundException(string? message) : base(message)
        {
        }
    }
}
