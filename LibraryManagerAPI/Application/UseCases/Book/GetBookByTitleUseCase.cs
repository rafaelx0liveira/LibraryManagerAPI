using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetBookByTitleUseCase (IBookRepository bookRepository) 
        : IGetBookByTitleUseCase
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<IEnumerable<BookResultVO>> GetBookByTitle(string title)
        {
            ValidatorService.Validate(title);

            var books = await _bookRepository.GetBookByTitleAsync(title);

            if (books == null || !books.Any())
            {
                throw new BookNotAvailableException($"The book '{title}' is not available.");
            } 

            return books;
        }


    }
}
