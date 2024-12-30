using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;

namespace LibraryManagerAPI.Application.UseCases.User
{
    public class GetUserByEmailUseCase (IUserRepository userRepository)
        : IGetUserByEmailUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResultVO> GetUserByEmail(string email)
        {
            ValidatorService.Validate(email);

            return await _userRepository.GetUserByEmailAsync(email) ?? throw new UserNotFoundException($"User with email '{email}' was not found.");
        }

    }
}
