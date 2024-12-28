using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.Book
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookVO>> RegisterBooks(IEnumerable<BookVO> bookVO);
        Task<IEnumerable<BookVO>> GetAllBooks();
        Task<IEnumerable<BookVO>> GetBookByTitle(string title);
        Task<IEnumerable<BookVO>> GetBookByAuthor(string author);
        Task<BookVO> GetBookByISBN(string isbn);
        Task<bool> MarkBookAsUnavailable(string isbn);
        Task<bool> UpdateBookQuantity(UpdateBookQuantityVO updateBookQuantityVO);
    }
}
