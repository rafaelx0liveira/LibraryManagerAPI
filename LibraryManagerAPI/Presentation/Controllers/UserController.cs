using LibraryManagerAPI.Domain.Exceptions;
using LibraryManagerAPI.Domain.ValueObjects;
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
            IRegisterUserUseCase _registerUserUseCase
        ) 
        : ControllerBase
    {

        private readonly IRegisterUserUseCase _registerUserUseCase = _registerUserUseCase;

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="userVO">User to be registered</param>
        /// <response code="200">Returns the user registered</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost]
        [Route("registerUser")]
        public async Task<IActionResult> RegisterBooks([FromBody] UserVO userVO)
        {
            try
            {
                var result = await _registerUserUseCase.RegisterUser(userVO);
                return Ok(result);
            }
            catch (CustomValidationException ex)
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
