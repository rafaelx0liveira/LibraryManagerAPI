using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.Exceptions.LoanExceptions;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
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
        IRegisterLoanUseCase _registerLoanUseCase,
        IGetLoanFromAUserUseCase _getLoanFromAUserUseCase,
        IReturnBookAndCloseLoanUseCase _returnBookAndCloseLoanUseCase
        ) : ControllerBase
    {
        private readonly IRegisterLoanUseCase _registerLoanUseCase = _registerLoanUseCase;
        private readonly IGetLoanFromAUserUseCase _getLoanFromAUserUseCase = _getLoanFromAUserUseCase;
        private readonly IReturnBookAndCloseLoanUseCase _returnBookAndCloseLoanUseCase = _returnBookAndCloseLoanUseCase;

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
            catch (UserNotFoundException ex)
            {
                return NotFound(new
                {
                    ex.Message
                });
            }
            catch (BookAlreadyLoanedByUserException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            catch (BookNotAvailableException ex)
            {
                // Return resource not found (404 Not Found)
                return NotFound(new
                {
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Get a Loan from a user
        /// </summary>
        /// <param name="email">Email for loan inquiry</param>
        /// <response code="200">Returns the loans from a user</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("loans/by-user")]
        public async Task<IActionResult> GetLoanFromAUser([FromQuery] string email)
        {
            try
            {
                var result = await _getLoanFromAUserUseCase.GetLoanFromAUser(email);
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
            catch (LoanFromUserNotFoundException ex)
            {
                return NotFound(new
                {
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Return book and close loan
        /// </summary>
        /// <param name="loanVO">Email for loan inquiry</param>
        /// <response code="200">Returns the loans from a user</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost]
        [Route("return")]
        public async Task<IActionResult> ReturnBookAndCloseLoan([FromBody] LoanVO loanVO)
        {
            try
            {
                var result = await _returnBookAndCloseLoanUseCase.ReturnBookAndCloseLoanAsync(loanVO);
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
            catch (LoanFromUserNotFoundException ex)
            {
                return NotFound(new
                {
                    ex.Message
                });
            }
            catch (UserNotFoundException ex)
            {
                // Return resource not found (404 Not Found)
                return NotFound(new
                {
                    ex.Message
                });
            }
        }

    }
}
