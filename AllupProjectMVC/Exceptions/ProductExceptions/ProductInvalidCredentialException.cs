namespace AllupProjectMVC.Exceptions.ProductExceptions
{
    public class ProductInvalidCredentialException : Exception
    {
        public string PropertyName { get; set; }
        public ProductInvalidCredentialException()
        {
        }

        public ProductInvalidCredentialException(string? message) : base(message)
        {
        }

        public ProductInvalidCredentialException(string? propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
