using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class BookExistsUseCase (IBookRepository bookRepository) : IBookExistsUseCase
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<bool> BookExists(string isbn)
        {
            ValidatorService.Validate(isbn);

            bool bookExists = await _bookRepository.BookExistsAsync(isbn);

            if (!bookExists)
            {
                throw new BookNotAvailableException($"The book with ISBN {isbn} was not found.");
            }

            return true;
        }
    }
}
