namespace AllupProjectMVC.Exceptions.FeatureExceptions
{
    public class FeatureNotFoundException : Exception
    {
        public FeatureNotFoundException()
        {
        }

        public FeatureNotFoundException(string? message) : base(message)
        {
        }
    }
}
