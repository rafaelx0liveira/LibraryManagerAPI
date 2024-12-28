using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetBookByISBNUseCase (IBookRepository libraryRepository) : IGetBookByISBNUseCase
    {
        IBookRepository _libraryRepository = libraryRepository;

        public async Task<BookVO> GetBookByISBN(string isbn)
        {
            ValidatorService.Validate(isbn);

            return await _libraryRepository.GetBookByISBN(isbn);
        }
    }
}
