using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan
{
    public interface IReturnBookAndCloseLoanUseCase
    {
        Task<LoanFromAUserResultVO> ReturnBookAndCloseLoanAsync(LoanVO loanVO);
    }
}
