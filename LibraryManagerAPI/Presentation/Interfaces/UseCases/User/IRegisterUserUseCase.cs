using LibraryManagerAPI.Domain.ValueObjects;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.User
{
    public interface IRegisterUserUseCase
    {
        Task<UserVO> RegisterUser(UserVO userVO);
    }
}
