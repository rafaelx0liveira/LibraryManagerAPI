namespace LibraryManagerAPI.Domain.Exceptions
{
    public class CustomValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public CustomValidationException(IEnumerable<string> errors) 
            : base("One or more validation failures have occurred.")
        {
            Errors = errors;
        }

        public CustomValidationException(string message, IEnumerable<string> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
