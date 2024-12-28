namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.User
{
    public interface IDeleteUserUseCase
    {
        Task<bool> DeleteUser(string email);
    }
}
