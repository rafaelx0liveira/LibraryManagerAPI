using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.User
{
    public interface IUserRepository
    {
        Task<UserResultVO> RegisterUserAsync(UserVO userVO);
        Task<UserResultVO> GetUserByEmailAsync(string email);
        Task<bool> DeleteUserAsync(string email);

        Task<bool> ExistsByEmailAsync(string email);
    }
}
