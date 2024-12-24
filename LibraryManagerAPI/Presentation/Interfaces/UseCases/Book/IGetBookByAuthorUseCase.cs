using LibraryManagerAPI.Domain.ValueObjects;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetBookByAuthorUseCase
    {
        Task<IEnumerable<BookVO>> GetBookByAuthor(string author);
    }
}
