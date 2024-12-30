using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.Loan
{
    public interface ILoanRepository
    {
        Task<LoanResultVO> RegisterLoanAsync(BookResultVO bookResultVO, UserResultVO userResultVO);
        Task<IEnumerable<LoanFromAUserResultVO>> GetLoanFromAUserAsync(string email);
        Task<LoanFromAUserResultVO> ReturnBookAndCloseLoanAsync(UserResultVO userResultVO, string isbn);
    }
}
