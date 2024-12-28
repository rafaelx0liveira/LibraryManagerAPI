using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan
{
    public interface IGetLoanFromAUserUseCase
    {
        Task<IEnumerable<LoanFromAUserResultVO>> GetLoanFromAUser(string email);
    }
}
