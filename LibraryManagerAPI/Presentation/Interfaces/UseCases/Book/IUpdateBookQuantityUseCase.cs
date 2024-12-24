using LibraryManagerAPI.Domain.ValueObjects;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Book
{
    public interface IUpdateBookQuantityUseCase
    {
        Task<bool> UpdateBookQuantity(UpdateBookQuantityVO updateBookQuantityVO);
    }
}
