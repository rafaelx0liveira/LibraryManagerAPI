using LibraryManagerAPI.Domain.ValueObjects;

namespace LibraryManagerAPI.Presentation.Interfaces.Repository.User
{
    public interface IUserRepository
    {
        Task<UserVO> RegisterUser(UserVO userVO);
    }
}
