namespace AllupProjectMVC.Exceptions.ProductExceptions
{
    public class TitleAlreadyExistException : Exception
    {
        public string PropertyName { get; set; }
        public TitleAlreadyExistException() { }

        public TitleAlreadyExistException(string? message) : base(message) { }

        public TitleAlreadyExistException(string? propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
