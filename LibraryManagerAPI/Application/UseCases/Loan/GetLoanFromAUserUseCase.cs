using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.LoanExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Loan;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan;

namespace LibraryManagerAPI.Application.UseCases.Loan
{
    public class GetLoanFromAUserUseCase (ILoanRepository loanRepository)
        : IGetLoanFromAUserUseCase
    {
        private readonly ILoanRepository _loanRepository = loanRepository;

        public async Task<IEnumerable<LoanFromAUserResultVO>> GetLoanFromAUser(string email)
        {
            ValidatorService.Validate(email);

            IEnumerable<LoanFromAUserResultVO> result = await _loanRepository.GetLoanFromAUser(email);

            if (!result.Any())
            {
                throw new LoanFromUserNotFoundException($"No loans found for the user {email}.");
            }

            return result;
        }
    }
}
