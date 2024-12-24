using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.ValueObjects;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;

namespace LibraryManagerAPI.Application.UseCases.User
{
    public class RegisterUserUseCase (IUserRepository userRepository) 
        : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserVO> RegisterUser(UserVO userVO)
        {
            ValidatorService.Validate(userVO);

            return await _userRepository.RegisterUser(userVO);
        }
    }
}
