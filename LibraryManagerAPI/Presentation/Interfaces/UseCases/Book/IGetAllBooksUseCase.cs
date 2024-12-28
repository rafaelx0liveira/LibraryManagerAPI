using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetAllBooksUseCase
    {
        public Task<IEnumerable<BookVO>> GetAllBooks();
    }
}
