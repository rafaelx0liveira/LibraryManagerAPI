using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;

namespace LibraryManagerAPI.Application.UseCases.User
{
    public class ExistsByEmailUseCase (IUserRepository userRepository)
        : IExistsByEmailUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> ExistsByEmail(string email)
        {
            ValidatorService.Validate(email);

            bool userExists = await _userRepository.ExistsByEmailAsync(email);

            if (!userExists)
            {
                throw new UserNotFoundException($"User with email '{email}' was not found.");
            }

            return true;
        }
    }
}
