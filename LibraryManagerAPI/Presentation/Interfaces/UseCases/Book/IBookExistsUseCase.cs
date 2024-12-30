namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IBookExistsUseCase
    {
        Task<bool> BookExists(string isbn);
    }
}
