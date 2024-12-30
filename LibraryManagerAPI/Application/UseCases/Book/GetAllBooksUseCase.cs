using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetAllBooksUseCase (IBookRepository bookRepository) : IGetAllBooksUseCase
    {
        IBookRepository _bookRepository = bookRepository;

        public async Task<IEnumerable<BookResultVO>> GetAllBooks()
        {         
            return await _bookRepository.GetAllBooksAsync();
        }
    }
}
