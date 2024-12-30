namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.User
{
    public interface IExistsByEmailUseCase
    {
        Task<bool> ExistsByEmail(string email);
    }
}
