using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetBookByAuthorUseCase (IBookRepository bookRepository)
        : IGetBookByAuthorUseCase
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<IEnumerable<BookResultVO>> GetBookByAuthor(string author)
        {
            ValidatorService.Validate(author);

            IEnumerable<BookResultVO> books = await _bookRepository.GetBookByAuthorAsync(author);

            if (books == null || !books.Any())
            {
                throw new BookNotAvailableException($"No books available by the author '{author}'.");
            }

            return books;
        }
    }
}
