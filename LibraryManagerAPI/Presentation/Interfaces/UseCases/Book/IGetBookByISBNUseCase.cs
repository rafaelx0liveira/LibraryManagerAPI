using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetBookByISBNUseCase
    {
        Task<BookResultVO> GetBookByISBN(string isbn);
    }
}
