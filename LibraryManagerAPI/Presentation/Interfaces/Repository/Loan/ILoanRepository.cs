using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.Loan
{
    public interface ILoanRepository
    {
        Task<LoanResultVO> RegisterLoan(LoanVO loan);
    }
}
