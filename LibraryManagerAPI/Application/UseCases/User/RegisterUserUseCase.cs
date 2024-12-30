using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;

namespace LibraryManagerAPI.Application.UseCases.User
{
    public class RegisterUserUseCase (IUserRepository userRepository) 
        : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResultVO> RegisterUser(UserVO userVO)
        {
            ValidatorService.Validate(userVO);

            bool userAlreadyExists = await _userRepository.ExistsByEmailAsync(userVO.Email);

            if (userAlreadyExists)
            {
                throw new UserAlreadyExistsException($"User with email {userVO.Email} already exists.");
            }

            return await _userRepository.RegisterUserAsync(userVO);
        }
    }
}
