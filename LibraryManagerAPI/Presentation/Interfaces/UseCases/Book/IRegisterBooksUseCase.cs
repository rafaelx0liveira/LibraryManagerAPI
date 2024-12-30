using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IRegisterBooksUseCase
    {
        Task<IEnumerable<BookResultVO>> RegisterBooks(IEnumerable<BookVO> bookVO);
    }
}
