using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetAllBooksUseCase (IBookRepository libraryRepository) : IGetAllBooksUseCase
    {
        IBookRepository _libraryRepository = libraryRepository;

        public async Task<IEnumerable<BookVO>> GetAllBooks()
        {         
            return await _libraryRepository.GetAllBooks();
        }
    }
}
