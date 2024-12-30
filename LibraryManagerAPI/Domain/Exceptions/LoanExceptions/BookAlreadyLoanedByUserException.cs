namespace LibraryManagerAPI.Domain.Exceptions.LoanExceptions
{
    public class BookAlreadyLoanedByUserException : Exception
    {
        public BookAlreadyLoanedByUserException(string message) : base(message) { }
    }
}
