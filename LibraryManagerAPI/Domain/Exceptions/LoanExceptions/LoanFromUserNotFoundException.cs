namespace LibraryManagerAPI.Domain.Exceptions.LoanExceptions
{
    public class LoanFromUserNotFoundException : Exception
    {
        public LoanFromUserNotFoundException(string message) : base(message) { }
    }
}
