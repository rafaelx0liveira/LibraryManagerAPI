using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetAllBooksUseCase
    {
        public Task<IEnumerable<BookResultVO>> GetAllBooks();
    }
}
