using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Loan;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan;

namespace LibraryManagerAPI.Application.UseCases.Loan
{
    public class ReturnBookAndCloseLoanUseCase (
            ILoanRepository loanRepository,
            IUserRepository userRepository,
            IBookRepository bookRepository
        )
        : IReturnBookAndCloseLoanUseCase
    {
        private readonly ILoanRepository _loanRepository = loanRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<LoanFromAUserResultVO> ReturnBookAndCloseLoanAsync(LoanVO loanVO)
        {
            ValidatorService.Validate(loanVO);

            bool userExists = await _userRepository.ExistsByEmailAsync(loanVO.Email);

            if (!userExists)
            {
                throw new UserNotFoundException($"User with email '{loanVO.Email}' was not found.");
            }

            UserResultVO userVO = await _userRepository.GetUserByEmailAsync(loanVO.Email);

            bool bookExists = await _bookRepository.BookExistsAsync(loanVO.ISBN);

            if (!bookExists)
            {
                throw new BookNotAvailableException($"The book with ISBN {loanVO.ISBN} was not found.");
            }

            return await _loanRepository.ReturnBookAndCloseLoanAsync(userVO, loanVO.ISBN);
        }
    }
}
