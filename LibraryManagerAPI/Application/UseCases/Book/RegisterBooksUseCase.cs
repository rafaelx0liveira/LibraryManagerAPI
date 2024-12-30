using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class RegisterBooksUseCase(IBookRepository bookRepository) : IRegisterBooksUseCase
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<IEnumerable<BookResultVO>> RegisterBooks(IEnumerable<BookVO> bookVO)
        {
            ValidatorService.Validate(bookVO);

            return bookVO == null ? throw new ArgumentNullException("Book is required") : await _bookRepository.RegisterBooksAsync(bookVO);
        }
    }
}
