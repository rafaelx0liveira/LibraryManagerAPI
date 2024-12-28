using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetBookByISBNUseCase
    {
        Task<BookVO> GetBookByISBN(string isbn);
    }
}
