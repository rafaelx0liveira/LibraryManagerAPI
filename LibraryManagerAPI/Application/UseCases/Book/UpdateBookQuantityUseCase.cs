using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class UpdateBookQuantityUseCase (IBookRepository libraryRepository) 
        : IUpdateBookQuantityUseCase
    {
        private readonly IBookRepository _libraryRepository = libraryRepository;

        public async Task<bool> UpdateBookQuantity(UpdateBookQuantityVO updateBookQuantityVO)
        {
            ValidatorService.Validate(updateBookQuantityVO);

            return await _libraryRepository.UpdateBookQuantity(updateBookQuantityVO);
        }

    }
}
