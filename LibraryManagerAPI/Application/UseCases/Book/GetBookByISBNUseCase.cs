using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetBookByISBNUseCase (IBookRepository bookRepository) : IGetBookByISBNUseCase
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<BookResultVO> GetBookByISBN(string isbn)
        {
            ValidatorService.Validate(isbn);

            bool isBookAvailable = await _bookRepository.IsBookAvailableAsync(isbn);

            if (!isBookAvailable)
            {
                throw new BookNotAvailableException($"The book with ISBN {isbn} is not available.");
            }

            return await _bookRepository.GetBookByISBNAsync(isbn);
        }
    }
}
