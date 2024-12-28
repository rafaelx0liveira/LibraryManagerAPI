using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetBookByTitleUseCase
    {
        public Task<IEnumerable<BookVO>> GetBookByTitle(string title);
    }
}
