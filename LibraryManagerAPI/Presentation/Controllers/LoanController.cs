using LibraryManagerAPI.Domain.Exceptions.ValidationFieldsExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerAPI.Presentation.Controllers
{
    /// <summary>
    /// Loan Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoanController (
        IRegisterLoanUseCase _registerLoanUseCase
        ) : ControllerBase
    {
        private readonly IRegisterLoanUseCase _registerLoanUseCase = _registerLoanUseCase;

        /// <summary>
        /// Register a Loan
        /// </summary>
        /// <param name="loanVO">Loan to be made</param>
        /// <response code="200">Returns the loan made</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterLoan([FromBody] LoanVO loanVO)
        {
            try
            {
                var result = await _registerLoanUseCase.RegisterLoan(loanVO);
                return Ok(result);
            }
            catch (CustomValidationFieldsException ex)
            {
                return BadRequest(new
                {
                    Message = "Validation failed",
                    ex.Errors
                });
            }
        }

    }
}
