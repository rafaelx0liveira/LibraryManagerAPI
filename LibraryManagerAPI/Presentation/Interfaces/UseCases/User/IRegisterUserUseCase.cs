using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.User
{
    public interface IRegisterUserUseCase
    {
        Task<UserResultVO> RegisterUser(UserVO userVO);
    }
}
