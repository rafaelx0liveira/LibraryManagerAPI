using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.User
{
    public interface IUserRepository
    {
        Task<UserVO> RegisterUser(UserVO userVO);
        Task<bool> DeleteUser(string email);
    }
}
