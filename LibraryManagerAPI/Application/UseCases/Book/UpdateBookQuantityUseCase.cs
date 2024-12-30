using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class UpdateBookQuantityUseCase (IBookRepository bookRepository) 
        : IUpdateBookQuantityUseCase
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<bool> UpdateBookQuantity(UpdateBookQuantityVO updateBookQuantityVO)
        {
            ValidatorService.Validate(updateBookQuantityVO);

            bool isBookAvailable = await _bookRepository.IsBookAvailableAsync(updateBookQuantityVO.ISBN);

            if (!isBookAvailable)
            {
                throw new BookNotAvailableException($"The book with ISBN {updateBookQuantityVO.ISBN} is not available.");
            }

            return await _bookRepository.UpdateBookQuantityAsync(updateBookQuantityVO);
        }

    }
}
