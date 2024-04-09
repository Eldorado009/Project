namespace AllupProjectMVC.Exceptions.FeatureExceptions
{
    public class FeatureInvalidCredentialException : Exception
    {
        public string PropertyName { get; set; }
        public FeatureInvalidCredentialException()
        {
        }

        public FeatureInvalidCredentialException(string? message) : base(message)
        {
        }

        public FeatureInvalidCredentialException(string? propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
