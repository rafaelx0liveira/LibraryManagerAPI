using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.Exceptions.ValidationFieldsExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerAPI.Presentation.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController (
            IRegisterUserUseCase _registerUserUseCase,
            IGetUserByEmailUseCase _getUserByEmailUseCase,
            IDeleteUserUseCase _deleteUserUseCase,
            IExistsByEmailUseCase _existsByEmailUseCase
        ) 
        : ControllerBase
    {

        private readonly IRegisterUserUseCase _registerUserUseCase = _registerUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase = _deleteUserUseCase;
        private readonly IGetUserByEmailUseCase _getUserByEmailUseCase = _getUserByEmailUseCase;
        private readonly IExistsByEmailUseCase _existsByEmailUseCase = _existsByEmailUseCase;

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="userVO">User to be registered</param>
        /// <response code="200">Returns the user registered</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserVO userVO)
        {
            try
            {
                var result = await _registerUserUseCase.RegisterUser(userVO);
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
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <response code="200">Returns true</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var result = await _getUserByEmailUseCase.GetUserByEmail(email);
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
                return NotFound(
                    new
                    {
                        ex.Message,
                    }
                );
            }
        }

        /// <summary>
        /// Check if user exists by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <response code="200">Returns true</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("exists")]
        public async Task<IActionResult> ExistsByEmail([FromQuery] string email)
        {
            try
            {
                var result = await _existsByEmailUseCase.ExistsByEmail(email);
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
                return NotFound(
                    new
                    {
                        ex.Message,
                    }
                );
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="email">User email</param>
        /// <response code="200">Returns true</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser([FromQuery] string email)
        {
            try
            {
                var result = await _deleteUserUseCase.DeleteUser(email);
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
            catch (UserNotFoundException ex) {
                return NotFound(
                    new
                    {
                        ex.Message,
                    }
                );
            }
        }
    }
}
