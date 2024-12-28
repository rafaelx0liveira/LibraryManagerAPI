using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;

namespace LibraryManagerAPI.Application.UseCases.User
{
    public class DeleteUserUseCase(IUserRepository userRepository)
        : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> DeleteUser(string email)
        {
            ValidatorService.Validate(email);

            return await _userRepository.DeleteUser(email);
        }
    }
}
