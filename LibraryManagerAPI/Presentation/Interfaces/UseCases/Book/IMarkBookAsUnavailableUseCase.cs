namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IMarkBookAsUnavailableUseCase
    {
        Task<bool> MarkBookAsUnavailable(string isbn);
    }
}
