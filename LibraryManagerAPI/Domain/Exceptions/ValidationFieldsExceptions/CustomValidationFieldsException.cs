namespace LibraryManagerAPI.Domain.Exceptions.ValidationFieldsExceptions
{
    public class CustomValidationFieldsException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public CustomValidationFieldsException(IEnumerable<string> errors)
            : base("One or more validation failures have occurred.")
        {
            Errors = errors;
        }

        public CustomValidationFieldsException(string message, IEnumerable<string> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
