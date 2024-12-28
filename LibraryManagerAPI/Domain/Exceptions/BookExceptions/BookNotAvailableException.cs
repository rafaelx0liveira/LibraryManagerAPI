namespace LibraryManagerAPI.Domain.Exceptions.BookExceptions
{
    public class BookNotAvailableException : Exception
    {
        public BookNotAvailableException(string message)
            : base(message)
        {
        }
    }
}
