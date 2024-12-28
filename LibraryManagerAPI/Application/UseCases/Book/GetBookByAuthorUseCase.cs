using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetBookByAuthorUseCase (IBookRepository libraryRepository)
        : IGetBookByAuthorUseCase
    {
        IBookRepository _libraryRepository = libraryRepository;

        public async Task<IEnumerable<BookVO>> GetBookByAuthor(string author)
        {
            ValidatorService.Validate(author);

            var books = await _libraryRepository.GetBookByAuthor(author);

            if (books == null || !books.Any())
            {
                throw new BookNotAvailableException($"No books available by the author '{author}'.");
            }

            return books;
        }
    }
}
