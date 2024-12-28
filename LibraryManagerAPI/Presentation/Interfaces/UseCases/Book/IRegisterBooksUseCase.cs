using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IRegisterBooksUseCase
    {
        Task<IEnumerable<BookVO>> RegisterBooks(IEnumerable<BookVO> bookVO);
    }
}
