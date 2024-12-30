using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.Book
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookResultVO>> RegisterBooksAsync(IEnumerable<BookVO> bookVO);
        Task<IEnumerable<BookResultVO>> GetAllBooksAsync();
        Task<IEnumerable<BookResultVO>> GetBookByTitleAsync(string title);
        Task<IEnumerable<BookResultVO>> GetBookByAuthorAsync(string author);
        Task<BookResultVO> GetBookByISBNAsync(string isbn);
        Task<bool> MarkBookAsUnavailableAsync(string isbn);
        Task<bool> UpdateBookQuantityAsync(UpdateBookQuantityVO updateBookQuantityVO);
        Task<bool> IsBookAvailableAsync(string isbn);
        Task<bool> IsBookAlreadyLoanedByUserAsync(string userEmail, string isbn);
        Task<bool> BookExistsAsync(string isbn);
    }
}
