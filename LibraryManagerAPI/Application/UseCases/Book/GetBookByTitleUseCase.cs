using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;
using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class GetBookByTitleUseCase (IBookRepository libraryRepository) 
        : IGetBookByTitleUseCase
    {
        private readonly IBookRepository _libraryRepository = libraryRepository;

        public async Task<IEnumerable<BookVO>> GetBookByTitle(string title)
        {
            ValidatorService.Validate(title);

            var books = await _libraryRepository.GetBookByTitle(title);

            if (books == null || !books.Any())
            {
                throw new BookNotAvailableException($"The book '{title}' is not available.");
            } 

            return books;
        }


    }
}
