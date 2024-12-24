using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.ValueObjects;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class RegisterBooksUseCase(IBookRepository libraryRepository) : IRegisterBooksUseCase
    {
        private readonly IBookRepository _libraryRepository = libraryRepository;

        public async Task<IEnumerable<BookVO>> RegisterBooks(IEnumerable<BookVO> bookVO)
        {
            ValidatorService.Validate(bookVO);

            return bookVO == null ? throw new ArgumentNullException("Book is required") : await _libraryRepository.RegisterBooks(bookVO);
        }
    }
}
