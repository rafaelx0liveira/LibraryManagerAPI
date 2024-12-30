using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.User
{
    public interface IGetUserByEmailUseCase
    {
        Task<UserResultVO> GetUserByEmail(string email);
    }
}
