using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Loan;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan;

namespace LibraryManagerAPI.Application.UseCases.Loan
{
    public class RegisterLoanUseCase (ILoanRepository loanRepository)
        : IRegisterLoanUseCase
    {
        private readonly ILoanRepository _loanRepository = loanRepository;

        public async Task<LoanResultVO> RegisterLoan(LoanVO loan)
        {
            ValidatorService.Validate(loan);

            return await _loanRepository.RegisterLoan(loan);
        }
    }
}
