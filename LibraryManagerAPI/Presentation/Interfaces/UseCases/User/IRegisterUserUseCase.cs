using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.User
{
    public interface IRegisterUserUseCase
    {
        Task<UserVO> RegisterUser(UserVO userVO);
    }
}
