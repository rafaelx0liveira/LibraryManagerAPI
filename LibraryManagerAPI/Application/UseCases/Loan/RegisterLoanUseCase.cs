using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.Exceptions.LoanExceptions;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Loan;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan;

namespace LibraryManagerAPI.Application.UseCases.Loan
{
    public class RegisterLoanUseCase(
        ILoanRepository loanRepository,
        IUserRepository userRepository,
        IBookRepository bookRepository
    )
        : IRegisterLoanUseCase
        {
            private readonly ILoanRepository _loanRepository = loanRepository;
            private readonly IUserRepository _userRepository = userRepository;
            private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<LoanResultVO> RegisterLoan(LoanVO loanVO)
        {
            ValidatorService.Validate(loanVO);

            bool bookAvailable = await _bookRepository.IsBookAvailableAsync(loanVO.ISBN);

            if (!bookAvailable)
            {
                throw new BookNotAvailableException($"The book with ISBN '{loanVO.ISBN}' is not available.");
            }

            bool bookAlreadyLoaned = await _bookRepository.IsBookAlreadyLoanedByUserAsync(loanVO.Email, loanVO.ISBN);

            if (bookAlreadyLoaned)
            {
                throw new BookAlreadyLoanedByUserException($"User {loanVO.Email} has already borrowed the book with ISBN {loanVO.ISBN}");
            }

            bool userExists = await _userRepository.ExistsByEmailAsync(loanVO.Email);

            if (!userExists)
            {
                throw new UserNotFoundException($"User with email '{loanVO.Email}' was not found.");
            }

            BookResultVO bookVO = await _bookRepository.GetBookByISBNAsync(loanVO.ISBN);
            UserResultVO userVO = await _userRepository.GetUserByEmailAsync(loanVO.Email);

            return await _loanRepository.RegisterLoanAsync(bookVO, userVO);
        }

    }
}
