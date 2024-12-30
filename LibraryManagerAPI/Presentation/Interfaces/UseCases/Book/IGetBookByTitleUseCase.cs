using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IGetBookByTitleUseCase
    {
        public Task<IEnumerable<BookResultVO>> GetBookByTitle(string title);
    }
}
